using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventEase.Core.DTOs
{
    public class RegistrationRequestDTO
    {
        public int UserId { get; set; }

        public int EventId { get; set; }
    }
}
