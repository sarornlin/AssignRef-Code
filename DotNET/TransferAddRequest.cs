using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sabio.Models.Requests.Stripe
{
    public class TransferAddRequest
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int Amount { get; set; }

        [Required]
        [StringLength(3,MinimumLength =3)]
        public string Currency { get; set; }
        [Required]
        public string Destination { get;set; }

    }
}
