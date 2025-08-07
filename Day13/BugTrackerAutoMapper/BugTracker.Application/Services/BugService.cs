using AutoMapper;
using BugTracker.Core.DTOs;
using BugTracker.Core.Entities;
using BugTracker.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Application.Services
{
    public class BugService : IBugService
    {
        private readonly IBugRepository _bugRepository;
        private readonly IMapper _mapper;
        public BugService(IBugRepository bugRepository, IMapper mapper)
        {
            _bugRepository = bugRepository;
            _mapper = mapper;
        }

      

        public void AddBug(BugRequestDTO bugRequest)
        {
            var bug = _mapper.Map<Bug>(bugRequest);
            _bugRepository.Add(bug);
        }
        public void UpdateBug(BugRequestDTO bugRequest)
        {
            var bug = _mapper.Map<Bug>(bugRequest);
            _bugRepository.Update(bug);
        }
        public void DeleteBug(int id)
        {
            _bugRepository.Delete(id);
        }
        public List<BugResponseDTO> GetAllBugs()
        {
            var bugs = _bugRepository.GetAll();
            return _mapper.Map<List<BugResponseDTO>>(bugs);
        }
        public BugResponseDTO GetBugById(int id)
        {
            var bug = _bugRepository.GetById(id);
            return _mapper.Map<BugResponseDTO>(bug);
        }
    }
}
