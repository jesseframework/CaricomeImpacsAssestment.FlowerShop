using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace CaricomeImpacsAssestment.FlowerShop.Settings
{
    public class SerialNumberGeneratorService : DomainService
    {
        private readonly IRepository<SerialNumber, Guid> _serialRepository;

        public SerialNumberGeneratorService(IRepository<SerialNumber, Guid> serialRepository)
        {
            _serialRepository = serialRepository;
        }

        public async Task<string> GetSerialNoAsync(string type, string accountNo, string partnerNo)
        {

            string _configPrefix = "";
            string _juliDateCode = "";
            string _accountNo = "";
            string _partnerCode = "";
            String _newEndingString = "";
            string _actualSerialNo = "";
            string _autoNumber = "";

            var serialNoConfig = await _serialRepository.GetListAsync();
            var quertSerialNo = serialNoConfig
                .FirstOrDefault(s => s.Type.Equals(type));

            RandonNumberGenerator rn = new();

            if (quertSerialNo != null)
            {
                if (quertSerialNo.IncludePrefix == true)
                {
                    _configPrefix = quertSerialNo.ConfigPrefix;
                }

                if (quertSerialNo.IncludeJulianDate == true)
                {
                    _juliDateCode = JulianDate.ConvertToJulian(DateTime.Now).ToString();
                }

                if (quertSerialNo.UserAccountNo == true && string.IsNullOrEmpty(accountNo))
                {
                    _accountNo = accountNo;
                }

                if (quertSerialNo.UsePartnerCode == true)
                {
                    _partnerCode = partnerNo;

                }

                if (quertSerialNo.UserAutoNo == true)
                {
                    _autoNumber = rn.RandonCriptoCaraptherNumber(quertSerialNo.EndingNumber);
                }
                else
                {
                    _autoNumber = rn.RandonCryptoCaraptherAlfa(quertSerialNo.EndingNumber);
                }



                if (quertSerialNo.AddLedingZero == true)
                {
                    _newEndingString = _autoNumber.PadLeft(quertSerialNo.LeadingZero, '0');
                }
                else
                {
                    _newEndingString = _autoNumber;
                }



                _actualSerialNo = _configPrefix + _juliDateCode + _accountNo + _partnerCode + _newEndingString;
            }
            else
            {
                _actualSerialNo = rn.RandonCriptoCaraptherNumber(10);
            }



            return _actualSerialNo;




        }
    }
}
