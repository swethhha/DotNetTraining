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
        [Required(ErrorMessage = "UserId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "UserId must be greater than 0")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "EventId is required")]
        [Range(1, int.MaxValue, ErrorMessage = "EventId must be greater than 0")]
        public int EventId { get; set; }
    }
}
