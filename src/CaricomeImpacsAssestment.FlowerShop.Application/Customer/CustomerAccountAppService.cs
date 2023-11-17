using CaricomeImpacsAssestment.FlowerShop.Customer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CaricomeImpacsAssestment.FlowerShop.Customer
{
    public class CustomerAccountAppService : CrudAppService<
        CustomerAccount,
        CustomerAccountDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateCustomerAccountDto>,
        ICustomerAccountAppService
    {
        private readonly IRepository<CustomerAccount, Guid> _customerRepository;
        private readonly IRepository<Contact, Guid> _contactRepository;
        private readonly IRepository<Address, Guid> _addressRepository;
       
        public CustomerAccountAppService(
            IRepository<CustomerAccount, Guid> customerRepository,
            IRepository<Contact, Guid> contactRepository,
            IRepository<Address, Guid> addressRepository
           
            ) : base(customerRepository)
        {
            _addressRepository = addressRepository;            
            _customerRepository = customerRepository;
            _contactRepository = contactRepository;
        }

        public async Task<List<CustomerAllDto>> GetAllCustomersWithRefrence()
        {
            //Am aware that this section of my code could have db perfromance issue if am dealing with large data.
            var _address = await _addressRepository.GetListAsync();
            var _contact = await _contactRepository.GetListAsync();
            var _account = await _customerRepository.GetListAsync();

            var mapAddress = ObjectMapper.Map<List<Address>, List<AddressDto>>(_address);
            var mapContact = ObjectMapper.Map<List<Contact>, List<ContactDto>>(_contact);
            var mapAccount = ObjectMapper.Map<List<CustomerAccount>, List<CustomerAccountDto>>(_account);

            var queryResult = (from account in mapAccount
                               join address in mapAddress on account.AddressId equals address.Id
                               join contact in mapContact on account.ContactId equals contact.Id
                               where
                               account.IsDeleted == false
                               select new CustomerAllDto
                               {
                                   account = account,
                                   contact = contact,
                                   address = address

                               }).ToList();

            return queryResult;


        }
        public async Task<CustomerAllDto> GetCustomeWithRefrence()
        {
            //Am aware that this section of my code could have db perfromance issue if am dealing with large data.
            var _address = await _addressRepository.GetListAsync();
            var _contact = await _contactRepository.GetListAsync();
            var _account = await _customerRepository.GetListAsync();

            var mapAddress = ObjectMapper.Map<List<Address>, List<AddressDto>>(_address);
            var mapContact = ObjectMapper.Map<List<Contact>, List<ContactDto>>(_contact);
            var mapAccount = ObjectMapper.Map<List<CustomerAccount>, List<CustomerAccountDto>>(_account);

            var queryResult = (from account in mapAccount
                               join address in mapAddress on account.AddressId equals address.Id
                               join contact in mapContact on account.ContactId equals contact.Id
                               where
                               account.IsDeleted == false
                               && account.UserId == CurrentUser.Id
                               select new CustomerAllDto
                               {
                                   account = account,
                                   contact = contact,
                                   address = address

                               }).SingleOrDefault();

            return queryResult;



        }
    }
}
