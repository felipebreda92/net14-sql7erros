using MeuTrabalho.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeuTrabalho.Repository
{
    public interface IAccountRepository
    {
        string Login(LoginViewModel model);
    }
}
