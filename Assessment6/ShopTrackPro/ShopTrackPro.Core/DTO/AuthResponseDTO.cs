using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTrackPro.Core.DTO
{
    public class AuthResponseDTO
    {
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }

    }
}
