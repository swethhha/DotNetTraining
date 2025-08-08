using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventEase.Core.DTOs
{
    public class EventRequestDTO
    {
        [[Required(ErrorMessage = "Title is required")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public required string Description { get; set; }


        public DateTime Date { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public required string Location { get; set; }
    }
}
