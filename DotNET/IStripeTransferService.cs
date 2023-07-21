    public interface IStripeTransferService
    {
        int CreateTransfer(TransferAddRequest model, int userId);
    }
