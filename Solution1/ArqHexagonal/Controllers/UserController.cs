using ApplicationHexagonal.Interfaces;
using ApplicationHexagonal.Services;
using DomainHexagonal.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace ArqHexagonal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly ICacheServices _cache;

        //O controller recebe uma requisição HTTP e chama o serviço de aplicação (UserService) para recuperar um usuário.

        public UserController(ICacheServices cache, UserService userService)
        {
            _userService = userService;
            _cache = cache; 
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            IEnumerable<User> users;
            var userCache = await _cache.GetAsync("userList"); // VE SE JA EXISTE A LISTA DE USUÁRIOS NO CACHE. 1HORA DE EXPIRAÇÃO

            if (userCache != null) {

                return Ok(userCache);
            }

            users = await _userService.GetAllUsersAsync();
            await _cache.SetAsync("userList", users); // SE NÃO TIVER, PEGA NO BANCO DE DADOS E GUARDA NO CACHE. 
            return Ok(users);
        }
          
        [HttpGet("{cpf}")]
        public async Task<ActionResult<User>> Get(string cpf)
        {
            var result = await _userService.GetUserByCPFAsync(cpf);
            if (result.IsLeft) 
                return NotFound(result.Left);

            return Ok(result.Right);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User user)
        {
            await _userService.AddUserAsync(user);
            return CreatedAtAction(nameof(Get), new { cpf = user.CPF }, user);
        }

        [HttpPut("{cpf}")]
        public async Task<ActionResult> Put(string cpf, [FromBody] User user)
        {
            if (cpf != user.CPF)
                return BadRequest();

            await _userService.UpdateUserAsync(user);
            return NoContent();
        }

        [HttpDelete("{cpf}")]
        public async Task<ActionResult> Delete(string cpf)
        {
            await _userService.DeleteUserAsync(cpf);
            return NoContent();
        }
    }
}
