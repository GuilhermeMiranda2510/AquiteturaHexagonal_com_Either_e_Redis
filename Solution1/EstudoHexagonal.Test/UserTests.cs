using ApplicationHexagonal.Interfaces;
using ArqHexagonal.Controllers;
using DomainHexagonal.Entities;
using DomainHexagonal.Utilities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using FluentAssertions;
using DomainHexagonal.Entities.Response;

namespace EstudoHexagonal.Test
{
    [TestFixture]
    public class UserTests
    {
        private Mock<IUserService> _mockUserService;
        private Mock<ICacheServices> _mockCacheService;
        private UserController _userController;

        [SetUp]
        public void Setup()
        {
            _mockUserService = new Mock<IUserService>();
            _mockCacheService = new Mock<ICacheServices>();
            _userController = new UserController(_mockCacheService.Object, _mockUserService.Object);
        }

        [Test]
        public async Task GetUserByCPF_UserExists_ReturnsOkWithUser()
        {
            // Arrange
            string cpf = "43066164870";
            var user = new User
            {
                Name = "GUILHERME SILVA MIRANDA",
                CPF = cpf,
                RG = "489212475",
                Celular = "11945130776",
                Email = "guilherme.smiranda@hotmail.com"
            };

            
            var rightResult = Either<ErrorResponse, User>.FromRight(user);

            _mockUserService
                .Setup(service => service.GetUserByCPFAsync(cpf))
                .ReturnsAsync(rightResult);

            // Act
            var result = await _userController.Get(cpf);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.StatusCode.Should().Be(200);
            okResult.Value.Should().BeEquivalentTo(user);
        }

        [Test]
        public async Task GetUserByCPF_UserDoesNotExist_ReturnsNotFound()
        {
            // Arrange
            string cpf = "9999999999";
            var errorResponse = new ErrorResponse(
                404,
                "Usuário não encontrado", 
                $"Não existe usário com o {cpf} cadastrado no banco de dados.", 
                "USER_NOT_FOUND");

            var notFoundResult = Either<ErrorResponse, User>.FromLeft(errorResponse);

            _mockUserService
                .Setup(service => service.GetUserByCPFAsync(cpf))
                .ReturnsAsync(notFoundResult);

            // Act
            var result = await _userController.Get(cpf);

            // Assert
            var notFoundObjectResult = result.Should().BeOfType<ObjectResult>().Subject; // Obtenha o ObjectResult
            notFoundObjectResult.StatusCode.Should().Be(404); // Verifica o status code

            // Verifique se o valor contido é do tipo ErrorResponse
            var actualErrorResponse = notFoundObjectResult.Value.Should().BeOfType<ErrorResponse>().Subject; // Verifique se o valor é do tipo ErrorResponse
            actualErrorResponse.StatusCode.Should().Be(errorResponse.StatusCode);
            actualErrorResponse.Message.Should().Be(errorResponse.Message);
            actualErrorResponse.Details.Should().Be(errorResponse.Details);
            actualErrorResponse.ErrorCode.Should().Be(errorResponse.ErrorCode);
        }
    }

    
}
