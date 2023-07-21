    public class StripeOrderReceiptService : IStripeOrderReceiptService
    {
        IDataProvider _data = null;
        public StripeOrderReceiptService(IDataProvider data)
        {
            _data = data;
        }

        public int Create(OrderReceiptAddRequest model, int userId)
        {
            int id = 0;
            string procName = "[dbo].[StripeOrderReceipts_Insert]";

            _data.ExecuteNonQuery(
                procName,
                inputParamMapper: delegate (SqlParameterCollection collection)
                {
                    collection.AddWithValue("@UserId", userId);
                    collection.AddWithValue("@CheckoutSessionId", model.CheckoutSessionId);
                    collection.AddWithValue("@LineItemId", model.LineItemId);
                    collection.AddWithValue("@ProductId", model.ProductId);
                    collection.AddWithValue("@AmountDiscount", model.AmountDiscount);
                    collection.AddWithValue("@AmountSubtotal", model.AmountSubtotal);
                    collection.AddWithValue("@AmountTax", model.AmountTax);
                    collection.AddWithValue("@AmountTotal", model.AmountTotal);
                    collection.AddWithValue("@Currency", model.Currency);
                    collection.AddWithValue("@Description", model.Description);
                    collection.AddWithValue("@Quantity", model.Quantity);

                    SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                    idOut.Direction = ParameterDirection.Output;
                    collection.Add(idOut);
                },
                returnParameters: delegate (SqlParameterCollection returnCollection)
                {
                    object oId = returnCollection["@Id"].Value;
                    int.TryParse(oId.ToString(), out id);
                }
                );
            return id;
        }
    }
