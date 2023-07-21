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
