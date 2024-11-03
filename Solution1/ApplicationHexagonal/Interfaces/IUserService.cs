using DomainHexagonal.Entities;
using DomainHexagonal.Entities.Response;
using DomainHexagonal.Utilities;

namespace ApplicationHexagonal.Interfaces
{
    public interface IUserService
    {
        Task<Either<ErrorResponse, User>> GetUserByCPFAsync(string cpf);

        Task<IEnumerable<User>> GetAllUsersAsync();

        Task AddUserAsync(User user);

        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(string cpf);

    }
}
