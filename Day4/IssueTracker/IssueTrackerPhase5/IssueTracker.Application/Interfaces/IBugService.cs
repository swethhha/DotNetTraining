// File path: IssueTracker.Application/Interfaces/IBugService.cs
using System.Collections.Generic;
using IssueTracker.Core.Entities;

namespace IssueTracker.Application.Interfaces
{
    public interface IBugService
    {
        void Add(Bug bug);
        List<Bug> GetAll();
        Bug GetById(int id);
        void Update(Bug bug);
        void Delete(int id);
    }
}
