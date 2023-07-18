import axios from "axios";
import {
  onGlobalError,
  onGlobalSuccess,
  API_HOST_PREFIX,
} from "./serviceHelpers";
const endpoint = `${API_HOST_PREFIX}/api/stripe/balance`;

const GetBalance = () => {
  const config = {
    method: "GET",
    url: `${endpoint}`,
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const stripeBalanceService = { GetBalance };

export default stripeBalanceService;
