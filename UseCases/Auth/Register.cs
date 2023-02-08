using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using CoolGuys.UseCases._contracts;

namespace CoolGuys.UseCases.Auth
{
    public class Register
    {
        private readonly IAuthService authService;

        public Register(IAuthService authService)
        {
            this.authService = authService;
        }

        public Task Exec(RegisterDto data)
        {
            return authService.Register(data);
        }
    }
}
