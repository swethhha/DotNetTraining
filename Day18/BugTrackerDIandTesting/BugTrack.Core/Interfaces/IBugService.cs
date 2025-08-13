using BugTrack.Core.DTOs;
using System.Collections.Generic;

namespace BugTrack.Core.Interfaces
{
    public interface IBugService
    {
        void AddBug(BugResquestDTO bug);
        void UpdateBug(int id, BugResquestDTO bug);
        void DeleteBug(int id);

        List<BugResponseDTO> GetAllBugs();
        BugResponseDTO? GetBugById(int id);

        Task AddBugAsync(BugResquestDTO bug);
        Task UpdateBugAsync(int id, BugResquestDTO bug);
        Task DeleteBugAsync(int id);
        Task<List<BugResponseDTO>> GetAllBugsAsync();
        Task<BugResponseDTO?> GetBugByIdAsync(int id);
       

    }
}
