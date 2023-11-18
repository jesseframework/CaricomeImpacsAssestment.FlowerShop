using CaricomeImpacsAssestment.FlowerShop.Order;
using CaricomeImpacsAssestment.FlowerShop.Payment.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CaricomeImpacsAssestment.FlowerShop.Payment
{
    public class AddPaymantAppService : CrudAppService<
        OrderPayment,
        OrderPaymentDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateOrderPaymentDto>,
        IOrderPaymentAppService
    {
        private readonly IRepository<OrderPayment, Guid> _orderPaymentRepository;
        public AddPaymantAppService(
            IRepository<OrderPayment, Guid> orderPaymentRepository
            ) : base(orderPaymentRepository) 
        {
            _orderPaymentRepository = orderPaymentRepository;
        }
    }
}
