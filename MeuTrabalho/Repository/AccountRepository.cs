using MeuTrabalho.Models;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MeuTrabalho.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private IConfiguration _configuration;
        private IDbConnection _connection;
        private IDbCommand _command;

        public AccountRepository(IConfiguration configuration, IDbConnection connection, IDbCommand command)
        {
            _configuration = configuration;
            _connection = connection;
            _command = command;
        }

        public string Login(LoginViewModel model)
        {
            _connection.ConnectionString = _configuration["ConnectionStrings:DefaultConnection"];
            _connection.Open();

            _command.Connection = _connection;
            _command.CommandType = CommandType.Text;
            _command.CommandText = "SELECT username FROM tbLogin WHERE email = @email AND pwd = @password";
            _command.Parameters.Add(new SqlParameter("@email", model.Email));
            _command.Parameters.Add(new SqlParameter("@password", model.Password));

            var username = (string)_command.ExecuteScalar();

            _connection.Close();
            return username;
        }
    }
}
