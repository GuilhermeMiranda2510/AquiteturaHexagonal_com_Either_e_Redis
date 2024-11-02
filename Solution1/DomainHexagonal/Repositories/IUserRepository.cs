﻿using DomainHexagonal.Entities;
using DomainHexagonal.Utilities;

namespace DomainHexagonal.Repositories
{
    public interface IUserRepository
    {
        Task<Either<string, User>> GetByCPFAsync(string cpf);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(string cpf);
    }
}