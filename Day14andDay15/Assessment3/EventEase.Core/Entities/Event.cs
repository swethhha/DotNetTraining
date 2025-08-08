using System.ComponentModel.DataAnnotations;

namespace EventEase.Core.Entities
{
    public class Event
    {
        public int Id { get; set; }


        public required string Title { get; set; }

        public required string Description { get; set; }

        public DateTime Date { get; set; }

        public required string Location { get; set; }

        public ICollection<Registration> Registrations { get; set; }
    }
}