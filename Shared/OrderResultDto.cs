using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.OrdersModels;

namespace Shared
{
    public class OrderResultDto
    {
        public OrderResultDto()
        {

        }
 

        // Id
        public Guid Id { get; set; }    
        //User Email

        public string UserEmail { get; set; }

        // Shipping Address
        public AddressDto ShippingAddress { get; set; }
        public ICollection<OrderItemDto> OrderItem { get; set; } = new List<OrderItemDto>();// Navigational Property

        //DeliveryMethod
        public string DeliveryMethod { get; set; }


        public int DeliveryMethodId { get; set; }

        //Payment Status
        public string PaymentStatus { get; set; }

        //Sub Total
        public decimal SubTotal { get; set; }

        //Order Date
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        //Payment
        public string PaymentIntentId { get; set; } = string.Empty;

        public decimal Total {  get; set; }
        public string BasketId { get; set; }
    }
}
