namespace EcommerceDomainDrivenDesign.Domain.Customers.Orders
{
    public enum OrderStatus
    {
        Placed = 1,
        WaitingForPayment = 2,
        Sent = 3,
        Delivered = 4,
        Canceled = 0,
    }
}
