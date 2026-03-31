using System.Collections.Generic;

namespace EcommerceDomainDrivenDesign.Application.Orders.PlaceOrder
{
    public class PlaceOrderRequest
    {
        public List<ProductDto> Products { get; set; }
        public string Currency { get; set; }
    }
}
