using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Users;

namespace CaricomeImpacsAssestment.FlowerShop.Order.Manager
{
    public class UserCookieManager : DomainService
    {
        private readonly IRepository<CookieTracker, Guid> _cookietrackerRepository;
        public UserCookieManager(
            IRepository<CookieTracker, Guid> cookietrackerRepository) 
        {
            _cookietrackerRepository = cookietrackerRepository;


        }
        public async Task<Guid> SaveCookieAsync(             
            string cookieValue, 
            string domain,
            string path)
        {
            bool changeMade = false;
            Guid cookieId = Guid.NewGuid();
            var userSessionCookie = new CookieTracker()
            {
                Name = "Browser_Cart_Session",
                Value = cookieValue,
                Domain = domain,
                Path = path,
                Expiry = DateTime.Now.AddMinutes(4320),
                IsSecure = true,
                IsHttpOnly = false                
                
            };

            var userCookieNoInDb = await _cookietrackerRepository
                .GetListAsync(p=>p.Value.Equals(cookieValue) || p.Expiry < DateTime.Now);
            if (!userCookieNoInDb.Any())
            {
                await _cookietrackerRepository.InsertAsync(userSessionCookie);
                cookieId = userSessionCookie.Id;
            }
            else
            {
                var userCookieInDb = await _cookietrackerRepository.GetAsync(p => p.Value.Equals(cookieValue));
                if (userCookieInDb.Expiry > DateTime.Now)
                {
                    userCookieInDb.Value = cookieValue;
                    userCookieInDb.UserId = cookieId; 
                    userCookieInDb.OrderId = cookieId; 

                    changeMade = true;
                    cookieId = userCookieInDb.Id;
                }
                if(changeMade == true)
                {
                    await _cookietrackerRepository.UpdateAsync(userCookieInDb);
                }
                
            }
            return cookieId;
        }

    }
}
