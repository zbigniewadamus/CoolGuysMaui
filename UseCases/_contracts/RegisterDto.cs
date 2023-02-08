using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolGuys.UseCases._contracts
{
    public class RegisterDto: LoginDto
    {
        public string ConfirmPassword { get; set; }
    }
}
