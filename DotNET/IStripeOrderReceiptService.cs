    public interface IStripeOrderReceiptService
    {
        int Create(OrderReceiptAddRequest model, int userId);
    }
