using Demo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Core.ServicesInterFaces
{
    public interface IPaymentService
    {
       Task<CustomerBasket> CreateOrUpdatePaymentIntentId(string BasketId);
    }
}
