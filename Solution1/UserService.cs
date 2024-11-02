using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<User> GetUserByIdAsync(int id) => _userRepository.GetByIdAsync(id);
    public Task<IEnumerable<User>> GetAllUsersAsync() => _userRepository.GetAllAsync();
    public Task AddUserAsync(User user) => _userRepository.AddAsync(user);
    public Task UpdateUserAsync(User user) => _userRepository.UpdateAsync(user);
    public Task DeleteUserAsync(int id) => _userRepository.DeleteAsync(id);
}
