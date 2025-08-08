using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EventEase.Core.DTOs
{
   public class UserRequestDTO
     {

      public required string Name { get; set; }
      public required string Email { get; set; }
     }
}

