import { lazy } from "react";

const StripeTransfers = lazy(() =>
  import("../components/stripe/StripeTransfers")
);
const ProfileNav = lazy(() => import("../components/profilenav/ProfileNav"));
const stripeRoutes = [
  {
    path: "/stripe/transfer",
    name: "StripeTransfers",
    exact: true,
    element: StripeTransfers,
    roles: ["Admin"],
    isAnonymous: false,
  },
];

const userProfile = [
  {
    path: "/profile",
    name: "profile",
    element: ProfileNav,
    roles: [
      "User",
      "Admin",
      "Grader",
      "Supervisor",
      "Team Personnel",
      "Team Admin",
      "Official",
      "Game Day Personnel",
      "Team Admin",
    ],
    exact: true,
    isAnonymous: false,
  },
];

const allRoutes = [...stripeRoutes, ...userProfile];

export default allRoutes;
