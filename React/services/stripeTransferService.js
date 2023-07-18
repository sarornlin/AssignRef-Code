import axios from "axios";
import {
  onGlobalError,
  onGlobalSuccess,
  API_HOST_PREFIX,
} from "./serviceHelpers";
const endpoint = `${API_HOST_PREFIX}/api/stripe/transfer`;

const Transfer = (payload) => {
  const config = {
    method: "POST",
    url: `${endpoint}`,
    data: payload,
    withCredentials: true,
    crossdomain: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const stripeTransferService = { Transfer };

export default stripeTransferService;
