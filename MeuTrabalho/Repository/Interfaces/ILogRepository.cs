using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeuTrabalho.Repository
{
    public interface ILogRepository
    {
        void Log(string log, string tipo, DateTime horaLog);
    }
}
