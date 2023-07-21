    public class StripeTransferService : IStripeTransferService
    {
        IDataProvider _data = null;
        private StripeKeys _stripeKeys = null;
        private HostUrl _hostUrl = null;
        private TransferService _transferService = null;
        public StripeTransferService(IDataProvider data, IOptions<StripeKeys> stripekeys, IOptions<HostUrl> hostUrl)
        {
            _data = data;
            _stripeKeys = stripekeys.Value;
            _hostUrl = hostUrl.Value;
            _transferService = new TransferService();

            StripeConfiguration.ApiKey = _stripeKeys.SecretKey;
        }

        public int CreateTransfer(TransferAddRequest model, int userId)
        {
            TransferCreateOptions options = new TransferCreateOptions
            {
                Amount = model.Amount,
                Currency = model.Currency,
                Destination = model.Destination
            };

            Transfer transferResponse = _transferService.Create(options);

            TransferRecordAddRequest requestModel = new TransferRecordAddRequest
            {
                Amount = (int)transferResponse.Amount,
                Currency = transferResponse.Currency,
                Destination = transferResponse.DestinationId,
                TransactionId = transferResponse.Id,
                SourceId = transferResponse.SourceTransactionId,
                Description = transferResponse.Description,
                Type = transferResponse.Object
            };

            int id = 0;

            string procName = "[dbo].[StripeTransaction_Insert]";

            string description = "Transfering " + transferResponse.Amount + " to " + transferResponse.DestinationId + " from UserId " + userId;

            _data.ExecuteNonQuery(
                procName,
                inputParamMapper: delegate (SqlParameterCollection collection)
                {
                    collection.AddWithValue("@TransactionId", transferResponse.Id);
                    collection.AddWithValue("@SourceId", userId);
                    collection.AddWithValue("@DestinationId", transferResponse.DestinationId);
                    collection.AddWithValue("@Type", transferResponse.Object);
                    collection.AddWithValue("@Amount", transferResponse.Amount);
                    collection.AddWithValue("@Description", description);

                    SqlParameter idOut = new SqlParameter("@Id", SqlDbType.Int);
                    idOut.Direction = ParameterDirection.Output;

                    collection.Add(idOut);
                },
                returnParameters: delegate(SqlParameterCollection returnCollection)
                {
                    object oId = returnCollection["@Id"].Value;
                    int.TryParse(oId.ToString(), out id);
                }
                );
            return id;
        }
    }
