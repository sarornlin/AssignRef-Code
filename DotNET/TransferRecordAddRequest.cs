using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Stripe
{
    public class TransferRecordAddRequest : TransferAddRequest
    {
        [Required]
        public string TransactionId { get; set; }
        [Required]
        public string SourceId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
