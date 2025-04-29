using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.OrderModels;
using Services.Abstractions;
using Services.Specifications;
using Shared;
using Shared.OrdersModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class OrderService(IMapper mapper, IBasketRepsository basketRepository, IUnitOfWork unitOfWork) : IOrderService
    {
        public async Task<OrderResultDto> CreateOrderAsync(OrderRequestDto orderRequest, string userEmail)
        {
            //1- Address
            var address= mapper.Map<Address>(orderRequest.ShipToAddress);

            //2- order item => basket
            var basket = await basketRepository.GetBasketAsync(orderRequest.BasketId);
            if (basketRepository is null) throw new BasketNotFoundException(orderRequest.BasketId);
             
            var orderItems= new List<OrderItem>();
            foreach (var item in basket.Items) {

                var product = await unitOfWork.GetRepository<Products, int>().GetAsync(item.Id);
                if (product is null) { throw new ProductNotFoundExceptions(item.Id); }
                var orderItem = new OrderItem(new ProductInOrderItem(product.Id, product.Name, product.PictureUrl),
                    item.Quantity, product.Price);
                orderItems.Add(orderItem);

            }
            var deliveryMethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(orderRequest.DeliverMethodId);
            if (deliveryMethod is null) throw new DeliveryMethodNotFoundException(orderRequest.DeliverMethodId);

            var subtotal= orderItems.Sum(i=> i.Price *i.Quantity);

            var order = new Order(userEmail, address, orderItems,deliveryMethod, subtotal, "");

            await unitOfWork.GetRepository<Order,Guid>().AddAsync(order);

            var count = await unitOfWork.SaveChangesAsync();
            if(count == 0) throw new OrderCreateBadRequestException();  
            var result= mapper.Map<OrderResultDto>(order);

            return result ;
        }

     
        public async Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods()
        {
           var deliveryMethod= await unitOfWork.GetRepository<DeliveryMethod, int>().GetAllAsync();
            var result = mapper.Map<IEnumerable<DeliveryMethodDto>>(deliveryMethod);
            return result;
        }

        public async Task<OrderResultDto> GetOrderByIdAsync(Guid id)
        {
            var spec = new OrderSpecification(id);
          var order= await  unitOfWork.GetRepository<Order,Guid>().GetAsync(spec);
            if (order is null) throw new OrderNotFoundException(id);
            var result =mapper.Map<OrderResultDto>(order);
            return result;
        }

        public async Task<IEnumerable<OrderResultDto>> GetOrderByUserEmailAsync(string userEmail)
        {
            var spec = new OrderSpecification(userEmail);
            var order = await unitOfWork.GetRepository<Order, Guid>().GetAllAsync(spec);
  
            var result = mapper.Map<IEnumerable<OrderResultDto>>(order);
            return result;
        }
    }
}
