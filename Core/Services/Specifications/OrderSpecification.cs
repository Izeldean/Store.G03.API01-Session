using Domain.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OrderSpecification: BaseSpecifications<Order,Guid>
    {
        public OrderSpecification(Guid id): base(O => O.Id==id)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o=> o.OrderItem);
        }

        public OrderSpecification(string userEmail) : base(O => O.UserEmail == userEmail)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItem);
            AddOrderBy(o=> o.OrderDate);
        }
    }
}
