using System.Collections.Generic;
using LeaveManagementSystem.Models;

namespace LeaveManagementSystem.Services
{
    public interface ILeaveService
    {
        void DisplayALL(List<LeaveRequest> leaveRequests);
        void ShowApprovals(List<IApprovable> approvables);
    }
}
