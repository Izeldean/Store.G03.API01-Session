using Shared;
using Shared.OrdersModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
   public interface IOrderService
    {
        //GetOrderByIdAsync
       Task<Shared.OrderResultDto> GetOrderByIdAsync(Guid id);
        // Get Order
        Task<IEnumerable<Shared.OrderResultDto>> GetOrderByUserEmailAsync(string userEmail);
        //Create Order

        Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRequest, string userEmail);

        //Get All Delivery Methods
        Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods();

    }
}
