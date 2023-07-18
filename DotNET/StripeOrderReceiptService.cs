using CsvHelper.Configuration.Attributes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Sabio.Data.Providers;
using Sabio.Models.Domain.Stripe;
using Sabio.Models.Requests.Stripe;
using Sabio.Services.Interfaces;
using Stripe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Services
{
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
}
