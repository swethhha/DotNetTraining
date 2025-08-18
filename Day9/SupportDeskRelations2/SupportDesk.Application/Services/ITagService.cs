using System.Collections.Generic;
using SupportDesk.Core.Entities;

namespace SupportDesk.Application.Services
{
    public interface ITagService
    {
        List<Tag> GetAllTags();
    }
}