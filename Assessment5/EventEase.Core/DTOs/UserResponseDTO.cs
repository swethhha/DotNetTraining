using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventEase.Core.DTOs
{
    public class UserResponseDTO
    {
        public int Id { get; set; }

        public required string Name { get; set; }

        public required string Email { get; set; }
    }
}
