using System;
using System.Collections.Generic;
using LeaveManagementSystem.Models;

namespace LeaveManagementSystem.Services
{
    public class LeaveService : ILeaveService
    {
        public void DisplayALL(List<LeaveRequest> leaveRequests)
        {
            Console.WriteLine("=== All Leave Requests ===");
            foreach (var request in leaveRequests)
            {
                request.DisplayRequestDetails();
            }
        }

        public void ShowApprovals(List<IApprovable> approvables)
        {
            Console.WriteLine("\n=== Approval Statuses ===");
            foreach (var approvable in approvables)
            {
                approvable.ShowApprovalStatus();
            }
        }
    }
}
