import axios from "axios";
import {
  onGlobalError,
  onGlobalSuccess,
  API_HOST_PREFIX,
} from "./serviceHelpers";

const endpoint = `${API_HOST_PREFIX}/api/stripe/orderreceipts`;

const createOrderReceipt = (payload) => {
  const config = {
    method: "POST",
    url: `${endpoint}`,
    data: payload,
    crossdomain: true,
    withcredentials: true,
    headers: { "Content-Type": "application/json" },
  };
  return axios(config).then(onGlobalSuccess).catch(onGlobalError);
};

const stripeOrderReceiptsService = { createOrderReceipt };

export default stripeOrderReceiptsService;
