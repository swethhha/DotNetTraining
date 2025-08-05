using BugTrackerGenericRepo.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BugTrackerGenericRepo.Core.Interfaces.IRepository;

namespace BugTrackerGenericRepo.Core.Interfaces
{
    public interface IBugRepository : IRepository<Bug>
    {

    }
}
