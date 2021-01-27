using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EpiSandbox.Models
{
    public class AuthenticationModel
    {
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
    }
}