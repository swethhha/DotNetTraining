using BugTracker.Core.DTOs;
using System.Collections.Generic;

namespace BugTracker.Core.Interfaces
{
    public interface IBugService
    {
        void AddBug(BugRequestDTO bugRequest);
        void UpdateBug(BugRequestDTO bugRequest);
        void DeleteBug(int id);
        List<BugResponseDTO> GetAllBugs();
        BugResponseDTO GetBugById(int id);
    }
}
