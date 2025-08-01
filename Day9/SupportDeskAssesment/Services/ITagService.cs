using SupportDeskAssesment.Models;
using System.Collections.Generic;

public interface ITagService
{
    void AddTag(string tagName);
    List<Tag> GetAllTags();
}
