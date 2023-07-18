import React, { useEffect, useState } from "react";
import { Card, Row, Col } from "react-bootstrap";
import { Formik, Form, Field, ErrorMessage } from "formik";
import * as Yup from "yup";
import debug from "sabio-debug";
import { onGlobalError } from "services/serviceHelpers";
import TitleHeader from "components/general/TitleHeader";
import { InputGroup } from "react-bootstrap";
import { FormControl } from "react-bootstrap";
import swal from "sweetalert";

import stripeAccountService from "../../services/stripeAccountsService";
import stripeBalanceService from "services/stripeBalanceService";
import stripeTransferService from "services/stripeTransferService";
import "./stripeMain.css";

function StripeTransfers() {
  const _logger = debug.extend("StripeTransfers");

  _logger("Stripe Transfer Loaded");

  const [formData] = useState({
    targetAccount: "",
    transferAmount: "",
  });

  const [balance, setBalance] = useState({ amount: "" });

  const [officials, setOfficials] = useState([]);

  const schema = Yup.object().shape({
    targetAccount: Yup.string().required(
      "You must select a recipient for the transfer."
    ),
    transferAmount: Yup.number()
      .positive("Amount must be greater than 0")
      .integer("Amount must be a whole number (e.g. 1000, 250, 99)")
      .max(balance.amount, "You have an insufficient balance for this amount.")
      .required("Is Required"),
  });

  useEffect(() => {
    stripeAccountService
      .GetAccounts()
      .then(onGetAccountsSuccess)
      .catch(onGlobalError);

    updateBalance();
  }, []);

  const updateBalance = () => {
    stripeBalanceService
      .GetBalance()
      .then(onGetBalanceSuccess)
      .catch(onGlobalError);
  };

  const onGetAccountsSuccess = (response) => {
    _logger("Officials", response.items);
    setOfficials(() => {
      return response.items;
    });
  };

  const onGetBalanceSuccess = (response) => {
    const balance = response.item.available[0].amount / 100;
    _logger("Balance:", balance);
    setBalance({ amount: balance });
  };

  const mapOfficials = (element) => {
    return (
      <option value={element.accountId} key={element.accountId}>
        {element.lastName}, {element.firstName} {element.email}
      </option>
    );
  };

  const officialsOptions = officials.map(mapOfficials);

  const updatePayload = (data) => {
    const obj = {
      amount: data.transferAmount * 100,
      currency: "USD",
      destination: data.targetAccount,
    };
    return obj;
  };

  const CustomInputComponent = (props) => (
    <InputGroup>
      <InputGroup.Text className="stripe StripeElement stripe-transfer-input">
        $
      </InputGroup.Text>
      <FormControl
        type="number"
        aria-label="Amount (to the nearest dollar)"
        className="stripe StripeElement stripe-transfer-input"
        pattern="^\d+$"
        placeholder="0"
        {...props}
      />
      <InputGroup.Text className="stripe StripeElement stripe-transfer-input">
        .00
      </InputGroup.Text>
    </InputGroup>
  );

  const submitHandler = (value) => {
    const payload = updatePayload(value);
    _logger("Payload:", payload);
    stripeTransferService
      .Transfer(payload)
      .then(onTransferSuccess)
      .catch(onTransferError);
  };

  const onTransferSuccess = (response) => {
    _logger("Success:", response.item);
    updateBalance();
    swal({ title: "Transaction Successful", icon: "success" });
  };

  const onTransferError = (response) => {
    _logger("Transfer Error:", response);
    swal("Something went wrong", "Transfer was not completed.", {
      icon: "error",
    });
  };

  return (
    <React.Fragment>
      <div className="container">
        <TitleHeader title="Make a Transfer" />
        <Card>
          <Card.Body>
            <Formik
              enableReinitialize={true}
              initialValues={formData}
              onSubmit={submitHandler}
              validationSchema={schema}
            >
              <Form>
                <Row className="form-group">
                  <Col>
                    <label htmlFor="targetAccount" className="stripe-label">
                      Destination:
                    </label>
                    <Field
                      component="select"
                      name="targetAccount"
                      className="form-control stripe StripeElement stripe-transfer-input"
                    >
                      <option value="">Select an Official...</option>
                      {officialsOptions}
                    </Field>
                    <ErrorMessage
                      name="targetAccount"
                      component="div"
                      className="small text-danger"
                    />
                  </Col>
                </Row>
                <Row className="mb-4">
                  <Col>
                    <label htmlFor="transferAmount" className="stripe-label">
                      Enter Amount to Transfer:
                    </label>
                    <Field name="transferAmount" as={CustomInputComponent} />
                    <ErrorMessage
                      name="transferAmount"
                      component="div"
                      className="small text-danger"
                    />
                    <div>Available Balance: {balance.amount}</div>
                  </Col>
                </Row>
                <Row className="d-flex justify-content-center ">
                  <button className="col-4 btn btn-primary " type="submit">
                    Transfer
                  </button>
                </Row>
              </Form>
            </Formik>
          </Card.Body>
        </Card>
      </div>
    </React.Fragment>
  );
}

export default StripeTransfers;
