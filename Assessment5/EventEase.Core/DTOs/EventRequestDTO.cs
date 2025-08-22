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

        public required string Title { get; set; }


        public required string Description { get; set; }


        public DateTime Date { get; set; }

        public required string Location { get; set; }
    }
}
