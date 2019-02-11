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

        public void Log(string log, string tipo, DateTime horaLogin)
        {
            _connection.ConnectionString = _configuration["ConnectionStrings:DefaultConnection"];
            _connection.Open();
            
            using (_connection)
            {
                _command.Connection = _connection;
                _command.CommandType = CommandType.Text;
                _command.CommandText = "INSERT tbLog (Log, Tipo, HoraLogin) VALUES (@log, @tipo, @HoraLogin)";
                _command.Parameters.Add(new SqlParameter("@log", log));
                _command.Parameters.Add(new SqlParameter("@tipo", tipo));
                _command.Parameters.Add(new SqlParameter("@HoraLogin", horaLogin));
                _command.ExecuteNonQuery();
            }
        }
    }
}
