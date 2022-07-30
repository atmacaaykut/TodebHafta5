using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bussines.Configuration.Auth;
using Bussines.Configuration.Response;

namespace Bussines.Abstract
{
    public interface IAuthService
    {
        CommandResponse VerifyPassword(string email, string password);
        AccessToken Login(string email, string password);
    }
}
