using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Shared.Models.Users
{
    public class TokenRequestModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
