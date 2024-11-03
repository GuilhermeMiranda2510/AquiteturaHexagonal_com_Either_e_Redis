using DomainHexagonal.Entities;
using DomainHexagonal.Repositories;
using DomainHexagonal.Utilities;
using System.Diagnostics.Metrics;
using System;
using DomainHexagonal.Entities.Response;
using ApplicationHexagonal.Interfaces;

namespace ApplicationHexagonal.Services
{
    public class UserService : IUserService
    {
        //O serviço encapsula a lógica de aplicação e chama o repositório para obter os dados.

        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<Either<ErrorResponse, User>> GetUserByCPFAsync(string cpf) => _userRepository.GetByCPFAsync(cpf);
        public Task<IEnumerable<User>> GetAllUsersAsync() => _userRepository.GetAllAsync();
        public Task AddUserAsync(User user) => _userRepository.AddAsync(user);
        public Task UpdateUserAsync(User user) => _userRepository.UpdateAsync(user);
        public Task DeleteUserAsync(string cpf) => _userRepository.DeleteAsync(cpf);
    }
}
