using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Stripe
{
    public class OrderReceiptAddRequest
    {
        [Required] 
        public string CheckoutSessionId { get; set; }
        [Required]
        public string LineItemId { get; set; }
        [Required]
        public string ProductId { get; set; }
        [Required]
        public int AmountDiscount { get;set; }
        [Required]
        public int AmountSubtotal { get; set; }
        [Required]
        public int AmountTax { get; set; }
        [Required]
        public int AmountTotal { get; set; }
        [Required]
        public string Currency { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int Quantity { get; set; }


    }
}
