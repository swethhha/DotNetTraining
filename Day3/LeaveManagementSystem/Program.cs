using System;
using System.Collections.Generic;
using LeaveManagementSystem.Models;
using LeaveManagementSystem.Services;

namespace LeaveManagementSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            CasualLeave leaveRequest1 = new CasualLeave(1, "Mirdu", 3, "Personal reason");
            SickLeave leaveRequest2 = new SickLeave(2, "Swetha", 5, "Medical condition");

            leaveRequest1.Reject();
            leaveRequest2.Approve();

            var leaveRequests = new List<LeaveRequest> { leaveRequest1, leaveRequest2 };
            var approvables = new List<IApprovable> { leaveRequest1, leaveRequest2 };

            ILeaveService leaveService = new LeaveService();
            leaveService.DisplayALL(leaveRequests);
            leaveService.ShowApprovals(approvables);
        }
    }
}
