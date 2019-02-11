using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MeuTrabalho.Repository
{
    public class LogRepository : ILogRepository
    {
        private IConfiguration _configuration;
        private IDbConnection _connection;
        private IDbCommand _command;

        public LogRepository(IConfiguration configuration, IDbConnection connection, IDbCommand command)
        {
            _configuration = configuration;
            _connection = connection;
            _command = command;
        }

        public void Insert(string log)
        {
            _connection.ConnectionString = _configuration["ConnectionStrings:DefaultConnection"];
            _connection.Open();

            _command.Connection = _connection;
            _command.CommandType = CommandType.Text;
            _command.CommandText = "INSERT tbLog VALUES (@llog)";
            _command.Parameters.Add(new SqlParameter("@log", log));
            _command.ExecuteNonQuery();

            _connection.Close();
        }
    }
}
