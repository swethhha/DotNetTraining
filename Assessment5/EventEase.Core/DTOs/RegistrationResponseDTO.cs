using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventEase.Core.DTOs
{
    public class RegistrationResponseDTO
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int EventId { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
