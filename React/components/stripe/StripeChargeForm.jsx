import React from "react";
import { Elements } from "@stripe/react-stripe-js";
import { loadStripe } from "@stripe/stripe-js";
import PaymentForm from "./PaymentForm";

import "./stripeMain.css";

const StripeChargeForm = () => {
  const stripePromise = loadStripe(process.env.REACT_APP_TEMP_STRIPE_PK_TEST);

  return (
    <React.Fragment>
      <Elements stripe={stripePromise} classname="stripe">
        <PaymentForm />
      </Elements>
    </React.Fragment>
  );
};

export default StripeChargeForm;
