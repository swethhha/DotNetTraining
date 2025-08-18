using System.Collections.Generic;
using System.Linq;
using SupportDesk.Core.Entities;
using SupportDesk.Infrastructure.Data;

namespace SupportDesk.Application.Services
{
    public class TagService : ITagService
    {
        private readonly SupportDeskDbContext _context;

        public TagService(SupportDeskDbContext context)
        {
            _context = context;
        }

        public List<Tag> GetAllTags() => _context.Tags.ToList();
    }
}