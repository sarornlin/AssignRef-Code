import React from "react";
import Icon from "@mdi/react";
import { Row, Col, Nav, Tab } from "react-bootstrap";
import { useNavigate } from "react-router-dom";
import PropTypes from "prop-types";
import debug from "sabio-debug";
import {
  mdiHomeVariantOutline,
  mdiCreditCardOutline,
  mdiAccountOutline,
  mdiStarOutline,
  mdiCircleSlice2,
  mdiCogOutline,
  mdiHanger,
  mdiCalendarBlank,
  mdiPower,
} from "@mdi/js";
import userService from "services/userService";
import Profile from "components/profiles/Profile";
import ApparelForm from "components/userapperal/ApparelForm";
import ConnectedAccount from "components/stripe/ConnectedAccount";
import BlockDates from "components/blockdates/BlockDates";
import MySubscriptionPage from "components/stripe/MySubscriptionPage";
import "./profilenav.css";

function ProfileNav({ currentUser }) {
  const _logger = debug.extend("ProfileNav");
  const navigate = useNavigate();
  _logger("Current user:", currentUser);

  const isOfficial = currentUser.roles.some(
    (role) =>
      role === "Official" ||
      role === "Admin" ||
      role === "Assigner" ||
      role === "Grader"
  );

  const subscriptionsMenu = [
    {
      key: "mysubscriptions",
      title: "My Subscriptions",
      component: <MySubscriptionPage currentUser={currentUser} />,
      icon: mdiHomeVariantOutline,
    },
    {
      key: "stripeaccount",
      title: "Stripe Account",
      component: <ConnectedAccount currentUser={currentUser} />,
      icon: mdiAccountOutline,
    },
    {
      key: "billinginfo",
      title: "Billing Info",
      component: "Billing Info Component",
      icon: mdiCreditCardOutline,
    },
    {
      key: "payment",
      title: "Payment",
      component: "Payment Component",
      icon: mdiStarOutline,
    },
    {
      key: "invoice",
      title: "Invoice",
      component: "Invoice Component",
      icon: mdiCircleSlice2,
    },
  ];
  const accountSettingsMenu = [
    {
      key: "editprofile",
      title: "Edit Profile",
      component: <Profile currentUser={currentUser} />,
      icon: mdiCogOutline,
    },
    {
      key: "calendar",
      title: "Calendar",
      component: <BlockDates currentUser={currentUser} />,
      icon: mdiCalendarBlank,
      roles: [
        "Admin",
        "Official",
        "Game Day Personnel",
        "Team Personnel",
        "Supervisor",
      ],
    },
    {
      key: "editapparelsizes",
      title: "Edit Apparel Sizes",
      component: <ApparelForm currentUser={currentUser} />,
      icon: mdiHanger,
      roles: [
        "Admin",
        "Official",
        "Game Day Personnel",
        "Team Personnel",
        "Supervisor",
      ],
    },
  ];

  const mapLink = (item, index) => {
    return (
      <Nav.Item as="li" className="mt-1" key={index}>
        <Nav.Link
          className="nav-link d-flex gap-2 profile-nav-links"
          eventKey={item.key}
        >
          <Icon
            path={item.icon}
            size={1}
            className="profile-nav-icons d-lg-block d-md-none"
          />
          {item.title}
        </Nav.Link>
      </Nav.Item>
    );
  };

  const mapContent = (item, index) => {
    return (
      <Tab.Pane eventKey={item.key} key={index}>
        {item.component}
      </Tab.Pane>
    );
  };

  const subscriptionTabs = subscriptionsMenu.map(mapLink);
  const accountSettingsTabs = accountSettingsMenu.map(mapLink);
  const subscriptionContent = subscriptionsMenu.map(mapContent);
  const accountSettingsContent = accountSettingsMenu.map(mapContent);

  const onSignOutHandler = () => {
    userService.logoutUser().then(onLogoutSuccess);
  };

  const onLogoutSuccess = () => {
    navigate("/", { state: { login: false } });
  };

  return (
    <React.Fragment>
      <Tab.Container
        id="navigation"
        defaultActiveKey="editprofile"
        mountOnEnter={true}
      >
        <Row className="d-flex align-items-stretch">
          <Col sm={12} md={3} className="mb-2">
            <div className="card">
              <Nav variant="pills" className="flex-column p-4" as="ul">
                {!isOfficial && (
                  <Nav.Item className="profile-nav-paragraph" as="li">
                    SUBSCRIPTIONS
                  </Nav.Item>
                )}
                {!isOfficial && subscriptionTabs}
                <Nav.Item className="pt-4 profile-nav-paragraph" as="li">
                  ACCOUNT SETTINGS
                </Nav.Item>
                {accountSettingsTabs}
                <Nav.Item as="li" className="mt-1">
                  <Nav.Link
                    className="nav-link d-flex gap-2 profile-nav-links"
                    onClick={onSignOutHandler}
                  >
                    <Icon
                      path={mdiPower}
                      size={1}
                      className="profile-nav-icons d-lg-block d-md-none"
                    />
                    Sign Out
                  </Nav.Link>
                </Nav.Item>
              </Nav>
            </div>
          </Col>
          <Col sm={12} md={9}>
            <Tab.Content>
              {subscriptionContent}
              {accountSettingsContent}
            </Tab.Content>
          </Col>
        </Row>
      </Tab.Container>
    </React.Fragment>
  );
}

export default ProfileNav;

ProfileNav.propTypes = {
  currentUser: PropTypes.shape({
    id: PropTypes.number.isRequired,
    name: PropTypes.string.isRequired,
    avatarUrl: PropTypes.string.isRequired,
    roles: PropTypes.arrayOf(PropTypes.string.isRequired),
  }).isRequired,
};
