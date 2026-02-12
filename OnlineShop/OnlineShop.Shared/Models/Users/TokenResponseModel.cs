using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Shared.Models.Users
{
    public class TokenResponseModel
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
        public int UserId { get; set; }
        public DateTime TokenExpire { get; set; }
        public DateTime RefreshTokenExpire { get; set; }
    }
}
