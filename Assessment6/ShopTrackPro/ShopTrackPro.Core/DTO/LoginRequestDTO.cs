using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTrackPro.Core.DTO
{
    public class LoginRequestDTO
    {
        public string Email { get; set; } = string.Empty;  // use Email as username
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = string.Empty ;
    }
}
