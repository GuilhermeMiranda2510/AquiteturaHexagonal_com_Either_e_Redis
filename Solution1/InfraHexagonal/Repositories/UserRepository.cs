using Dapper;
using DomainHexagonal.Entities;
using DomainHexagonal.Entities.Response;
using DomainHexagonal.Repositories;
using DomainHexagonal.Utilities;
using System.Data;

namespace InfraHexagonal.Repositories
{
    public class UserRepository : IUserRepository
    {
        //Essa Classe (Repositório) faz a consulta no banco de dados utilizando Dapper.
        //Se o usuário não é encontrado, ele retorna um Either com um erro à esquerda; caso contrário, retorna um Either com o usuário à direita.

        //Either: O Either permite ao serviço e ao controller tratar o resultado da operação de forma funcional, sem lançamentos de exceção.
        //Ele contém o valor de sucesso ou de erro de forma explícita, facilitando o controle de fluxo.

        //       ************** Vantagens do Uso de Either ***********
        //Controle de fluxo mais explícito: O Either torna o fluxo de operações de sucesso e erro mais claro, permitindo uma melhor compreensão da aplicação.
        //Redução de exceções: Em vez de lançar exceções para erros esperados, o Either encapsula erros, simplificando o tratamento de falhas.
        //Separação de responsabilidades: O serviço e o controller sabem tratar os resultados usando o Either, sem dependência de detalhes de implementação do repositório.

        private readonly IDbConnection _dbConnection;

        public UserRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Either<string, User>> GetByCPFAsync(string cpf)
        {
            var sql = $@"SELECT * FROM Users WHERE CPF = {cpf}";
            var user = await _dbConnection.QueryFirstOrDefaultAsync<User>(sql, new { CPF = cpf });

            if (user == null)
            {
                return Either<string, User>.FromLeft("Usuário não econtrado!");
                //return Either<ErrorResponse, User>.FromLeft(
                //    new ErrorResponse(

                //        statusCode: 404,
                //        message: "Usuário não encontrado",
                //        details: $"No user found with CPF {cpf}",
                //        errorCode: "USER_NOT_FOUND"
                //        ));
            }

            return Either<string, User>.FromRight(user);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var sql = "SELECT * FROM Users";
            return await _dbConnection.QueryAsync<User>(sql);
        }

        public async Task AddAsync(User user)
        {
            var sql = $@"INSERT INTO Users (Name, CPF, RG, Celular, Email) VALUES ('{user.Name}', '{user.CPF}', '{user.RG}', '{user.Celular}', '{user.Email}')";
            await _dbConnection.ExecuteAsync(sql, user);
        }

        public async Task UpdateAsync(User user)
        {
            var sql = $@"UPDATE Users SET Name = '{user.Name}', CPF = '{user.CPF}', RG = '{user.RG}', Celular = '{user.Celular}', Email = '{user.Email}' WHERE CPF = {user.CPF}";
            await _dbConnection.ExecuteAsync(sql, user);
        }

        public async Task DeleteAsync(string cpf)
        {
            var sql = $@"DELETE FROM Users WHERE CPF = {cpf}";
            await _dbConnection.ExecuteAsync(sql, new { cpf = cpf });
        }
    }
}
