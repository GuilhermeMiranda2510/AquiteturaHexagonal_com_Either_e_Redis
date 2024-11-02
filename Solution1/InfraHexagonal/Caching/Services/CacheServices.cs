using ApplicationHexagonal.Interfaces;
using DomainHexagonal.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InfraHexagonal.Caching.Services
{
    public class CacheServices : ICacheServices
    {
        private readonly IDistributedCache _cache;
        private readonly DistributedCacheEntryOptions _options;

        //DOCUMENTAÇÃO PARA INSTALAR REDIS NO WINDOWS. 
        //https://redis.io/docs/latest/operate/oss_and_stack/install/install-redis/install-redis-on-windows/
        // REDIS NÃO É NATIVO DO WINDOWS. 

        public CacheServices(IDistributedCache cache)
        {
            _cache = cache;
            _options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(3600), //1 hora para expiração absoluta.
                SlidingExpiration = TimeSpan.FromSeconds(1200) //se passar um tempo especifico sem acessar, expira.
            };
        }
        public async Task<IEnumerable<User>> GetAsync(string key)
        {
            var serializedUsers = await _cache.GetStringAsync(key);
            if(serializedUsers == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<IEnumerable<User>>(serializedUsers);
        }

        public async Task SetAsync(string key, IEnumerable<User> users)
        {
            string serializedUsers = JsonConvert.SerializeObject(users);
            await _cache.SetStringAsync(key, serializedUsers, _options);
        }
    }
}
