using BugTrack.Core.DTOs;
using BugTrack.Core.Entities;
using BugTrack.Core.Interfaces;


namespace BugTracker.Core.Services
{
    public class BugService : IBugService
    {
        private readonly IBugRepository _bugRepository;

        public BugService(IBugRepository bugRepository)
        {
            _bugRepository = bugRepository;
        }

        // ----------------- SYNC -----------------
        public int CreateBug(BugResquestDTO request)
        {
            var bug = new Bug
            {
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                ProjectId = request.ProjectId
            };

            _bugRepository.Add(bug);
            return bug.Id;
        }

        public void UpdateBug(int id, BugResquestDTO request)
        {
            var bug = _bugRepository.GetById(id);
            if (bug == null)
                throw new Exception("Bug not found.");

            bug.Title = request.Title;
            bug.Description = request.Description;
            bug.Status = request.Status;
            bug.ProjectId = request.ProjectId;

            _bugRepository.Update(bug);
        }

        public void DeleteBug(int id)
        {
            _bugRepository.Delete(id);
        }

        public BugResponseDTO? GetBugById(int id)
        {
            var bug = _bugRepository.GetById(id);
            return bug == null ? null : MapToResponseDTO(bug);
        }

        public IEnumerable<BugResponseDTO> GetAllBugs()
        {
            return _bugRepository.GetAll().Select(MapToResponseDTO);
        }

        // ----------------- ASYNC -----------------
        public async Task<int> CreateBugAsync(BugResquestDTO request)
        {
            var bug = new Bug
            {
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                ProjectId = request.ProjectId
            };

            await _bugRepository.AddAsync(bug);
            return bug.Id;
        }

        public async Task UpdateBugAsync(int id, BugResquestDTO request)
        {
            var bug = await _bugRepository.GetByIdAsync(id);
            if (bug == null)
                throw new Exception("Bug not found.");

            bug.Title = request.Title;
            bug.Description = request.Description;
            bug.Status = request.Status;
            bug.ProjectId = request.ProjectId;

            await _bugRepository.UpdateAsync(bug);
        }

        public async Task DeleteBugAsync(int id)
        {
            await _bugRepository.DeleteAsync(id);
        }

        public async Task<BugResponseDTO?> GetBugByIdAsync(int id)
        {
            var bug = await _bugRepository.GetByIdAsync(id);
            return bug == null ? null : MapToResponseDTO(bug);
        }

        public async Task<IEnumerable<BugResponseDTO>> GetAllBugsAsync()
        {
            var bugs = await _bugRepository.GetAllAsync();
            return bugs.Select(MapToResponseDTO);
        }

        private BugResponseDTO MapToResponseDTO(Bug bug)
        {
            return new BugResponseDTO
            {
                Id = bug.Id,
                Title = bug.Title,
                Description = bug.Description,
                Status = bug.Status,
                CreatedOn = bug.CreatedOn,
                ProjectId = bug.ProjectId
            };
        }
    }
}