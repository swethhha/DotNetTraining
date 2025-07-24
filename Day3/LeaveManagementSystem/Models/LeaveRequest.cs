using System;

namespace LeaveManagementSystem.Models
{
    public abstract class LeaveRequest : IApprovable
    {
        public int Id { get; set; }
        public string EmployeeName { get; set; }
        public int DaysRequested { get; set; }
        public string Status { get; set; }

        public LeaveRequest(int id, string employeeName, int daysRequested)
        {
            Id = id;
            EmployeeName = employeeName;
            DaysRequested = daysRequested;
            Status = "Pending";
        }

        public void Approve() => Status = "Approved";
        public void Reject() => Status = "Rejected";

        public abstract void DisplayRequestDetails();
        public abstract void ShowApprovalStatus();  // From IApprovable
    }
}
