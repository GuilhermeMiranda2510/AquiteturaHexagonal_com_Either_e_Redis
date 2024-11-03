using DomainHexagonal.Entities;
using DomainHexagonal.Entities.Response;
using DomainHexagonal.Utilities;

namespace DomainHexagonal.Repositories
{
    public interface IUserRepository
    {
        Task<Either<ErrorResponse, User>> GetByCPFAsync(string cpf);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(string cpf);
    }
}
