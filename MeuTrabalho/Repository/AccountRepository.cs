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
            var username = "";
            _connection.ConnectionString = _configuration["ConnectionStrings:DefaultConnection"];
            _connection.Open();

            using (_connection)
            {
                _command.Connection = _connection;
                _command.CommandType = CommandType.Text;
                _command.CommandText = "SELECT username FROM tbLogin WHERE email = @email AND pwd = @password";
                _command.Parameters.Add(new SqlParameter("@email", model.Email));
                _command.Parameters.Add(new SqlParameter("@password", model.Password));

                var reader = _command.ExecuteReader();

                while (reader.Read())
                {
                    username = (string)reader[0];
                }
            }

            return username;
        }
    }
}
