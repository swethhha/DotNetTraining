using SupportDeskAssesment.Data;
using SupportDeskAssesment.Models;
using System.Collections.Generic;
using System.Linq;

public class TagService : ITagService
{
    private readonly AppDbContext _context;

    public TagService(AppDbContext context)
    {
        _context = context;
    }

    public void AddTag(string tagName)
    {
        var tag = new Tag { TagName = tagName };
        _context.Tags.Add(tag);
        _context.SaveChanges();
    }

    public List<Tag> GetAllTags() => _context.Tags.ToList();
}
