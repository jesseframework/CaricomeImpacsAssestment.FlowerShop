using CaricomeImpacsAssestment.FlowerShop.Customer;
using CaricomeImpacsAssestment.FlowerShop.Order;
using CaricomeImpacsAssestment.FlowerShop.Product;
using CaricomeImpacsAssestment.FlowerShop.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace CaricomeImpacsAssestment.FlowerShop.Report
{
    public class SalesReportingService : ApplicationService
    {
        IRepository<OrderDetail> _orderDetailRepository;
        IRepository<OrderHeader> _orderHeaderRepository;
        IRepository<Category> _categoryRepository;
        IRepository<Item> _itemRepository;
        IRepository<CustomerAccount> _customerAccountRepository;

        public SalesReportingService(
            IRepository<OrderHeader> orderHeaderRepository,
            IRepository<OrderDetail> orderDetailRepository,
            IRepository<Category> categoryRepository,
            IRepository<Item> itemRepository,
            IRepository<CustomerAccount> customerAccount
            )
        {

            _orderDetailRepository = orderDetailRepository;
            _orderHeaderRepository = orderHeaderRepository;
            _categoryRepository = categoryRepository;
            _itemRepository = itemRepository;
            _customerAccountRepository = customerAccount;
        }

        public async Task<List<CategoryCountDto>> GetCategoryCounts(DateTime StartDateTime, DateTime EndDateTime)
        {
            var orderDetails = await _orderDetailRepository.GetListAsync(x => x.OrderDate >= StartDateTime && x.OrderDate <= EndDateTime);
            var items = await _itemRepository.GetListAsync();
            var categories = await _categoryRepository.GetListAsync();

            var categoryCounts = categories
                .GroupJoin(
                    items,
                    category => category.Id,
                    item => item.CategoryId,
                    (category, items) => new { CategoryId = category.Id, CategoryName = category.Name, Items = items }
                )
                .GroupJoin(
                    orderDetails,
                    categoryItem => categoryItem.CategoryId,
                    orderDetail => orderDetail.CategoryId,
                    (categoryItem, orderDetails) => new { categoryItem.CategoryId, categoryItem.CategoryName, Items = categoryItem.Items, OrderDetails = orderDetails }
                )
                .SelectMany(
                    x => x.OrderDetails.DefaultIfEmpty(),
                    (x, orderDetail) => new { x.CategoryId, x.CategoryName, ItemCount = x.Items.Count(), OrderDetail = orderDetail }
                )
                .GroupBy(
                    x => new { x.CategoryId, x.CategoryName },
                    (key, group) => new { CategoryName = key.CategoryName, Count = group.Count() }
                )
                .Select(x => new CategoryCountDto{ CategoryName= x.CategoryName, Count = x.Count })
                .ToList();

            return categoryCounts;
        }

        public async Task<DashboardStatsDto> GetDashboardStats(DateTime StartDateTime, DateTime EndDateTime)
        {
            var orders = await _orderDetailRepository.GetListAsync(x => x.OrderDate >= StartDateTime && x.OrderDate <= EndDateTime);
            var totalOrders = 0;
            if (orders.Any())
            {
                totalOrders = Convert.ToInt32(orders.Sum(o => o.Quantity));
            }

            var orderHeader = await _orderHeaderRepository.GetListAsync(o => o.OrderDate >= StartDateTime && o.OrderDate <= EndDateTime);
            var totalOrderAmount = 0.00D;
            if(orderHeader.Any()) 
            {
                totalOrderAmount = orderHeader.Sum(o => o.TotalDue);
            }

            var totalCustomers = await _customerAccountRepository.GetCountAsync();

            return new DashboardStatsDto
            {
                TotalOrders = Convert.ToInt32(totalOrders),
                TotalOrderAmount = totalOrderAmount,
                TotalCustomers = totalCustomers
            };
        }
    }
}
