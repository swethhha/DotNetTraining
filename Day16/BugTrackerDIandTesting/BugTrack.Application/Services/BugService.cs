using BugTrack.Core.DTOs;
using BugTrack.Core.Entities;
using BugTrack.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BugTrack.Application.Services
{
    public class BugService : IBugService
    {
        private readonly IBugRepository _bugRepository;

        public BugService(IBugRepository bugRepository)
        {
            _bugRepository = bugRepository;
        }

        public void AddBug(BugResquestDTO resquest)
        {
            var newBug = new Bug
            {
                Title = resquest.Title,
                Description = resquest.Description,
                Status = resquest.Status,
                ProjectId = resquest.ProjectId
            };
            _bugRepository.Add(newBug);
            _bugRepository.SaveChanges();
        }

        public void UpdateBug(int id, BugResquestDTO resquest)
        {
            var existingBug = _bugRepository.GetById(id);
            if (existingBug == null) return;

            existingBug.Title = resquest.Title;
            existingBug.Description = resquest.Description;
            existingBug.Status = resquest.Status;
            existingBug.ProjectId = resquest.ProjectId;

            _bugRepository.Update(existingBug);
            _bugRepository.SaveChanges();
        }

        public void DeleteBug(int id)
        {
            _bugRepository.Delete(id);
            _bugRepository.SaveChanges();
        }

        public List<BugResponseDTO> GetAllBugs()
        {
            var bugs = _bugRepository.GetAll();
            return bugs.Select(b => new BugResponseDTO
            {
                Id = b.Id,
                Title = b.Title,
                Description = b.Description,
                Status = b.Status,
                ProjectId = b.ProjectId,
                CreatedOn = b.CreatedOn,
                ProjectName = b.Project?.ProjectName ?? string.Empty
            }).ToList();
        }

        public BugResponseDTO? GetBugById(int id)
        {
            var bug = _bugRepository.GetById(id);
            if (bug == null) return null;

            return new BugResponseDTO
            {
                Id = bug.Id,
                Title = bug.Title,
                Description = bug.Description,
                Status = bug.Status,
                ProjectId = bug.ProjectId,
                CreatedOn = bug.CreatedOn,
                ProjectName = bug.Project?.ProjectName ?? string.Empty
            };
        }
    }
}
