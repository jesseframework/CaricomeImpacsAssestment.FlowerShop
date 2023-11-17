using CaricomeImpacsAssestment.FlowerShop.Customer.Dto;
using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CaricomeImpacsAssestment.FlowerShop.Customer
{
    public class ContactAppService : CrudAppService<
        Contact,
        ContactDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateContactDto>,
        IContactAppService
    {
        private readonly IRepository<Contact, Guid> _contactRepository;
        public ContactAppService(
            IRepository<Contact, Guid> contactRepository) : base(contactRepository)
        {
            _contactRepository = contactRepository;
        }
    }


}
