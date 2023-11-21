using CaricomeImpacsAssestment.FlowerShop.Setting.Dto;
using CaricomeImpacsAssestment.FlowerShop.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace CaricomeImpacsAssestment.FlowerShop.Setting
{
    public class SerialNumberAppService : CrudAppService<
        SerialNumber,
        SerialNumberDto,
        Guid,
        PagedAndSortedResultRequestDto,
        CreateUpdateSerialNumberDto>,
        ISerialNumber
    {
        public SerialNumberAppService(IRepository<SerialNumber, Guid> repository)
      : base(repository)
        {
            
        }
    }
}
