using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Shared.Models.Users
{
    public class RefreshTokenRequestModel
    {
        public string Token { get; set; }
        public int UserId { get; set; }
    }
}
