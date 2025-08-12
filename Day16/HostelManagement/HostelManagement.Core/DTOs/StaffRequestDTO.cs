using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace HostelManagement.Core.DTOs
{

    namespace HostelManagement.Core.DTOs
    {
        public class StaffRequestDTO
        {
            [Required]
            public string Name { get; set; } = string.Empty;
        }
    }

}
