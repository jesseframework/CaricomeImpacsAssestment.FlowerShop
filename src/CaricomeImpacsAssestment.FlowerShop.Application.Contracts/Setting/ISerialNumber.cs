using CaricomeImpacsAssestment.FlowerShop.Setting.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace CaricomeImpacsAssestment.FlowerShop.Setting
{
    public interface ISerialNumber : ICrudAppService<
                                        SerialNumberDto,
                                        Guid,
                                        PagedAndSortedResultRequestDto,
                                        CreateUpdateSerialNumberDto>
    {
    }
}
