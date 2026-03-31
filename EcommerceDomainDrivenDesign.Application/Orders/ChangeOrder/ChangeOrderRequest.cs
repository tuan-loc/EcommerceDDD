using System.Collections.Generic;

namespace EcommerceDomainDrivenDesign.Application.Orders.ChangeOrder
{
    public class ChangeOrderRequest
    {
        public List<ProductDto> Products { get; set; }
        public string Currency { get; set; }
    }
}
