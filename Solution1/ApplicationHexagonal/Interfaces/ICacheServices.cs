using DomainHexagonal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationHexagonal.Interfaces
{
    public interface ICacheServices
    {
        Task SetAsync(string key, IEnumerable<User> value);

        Task<IEnumerable<User>> GetAsync(string key);
    }
}
