using System;

namespace LeaveManagementSystem.Models
{
    public class CasualLeave : LeaveRequest, IApprovable
    {
        public string Reason { get; set; }

        public CasualLeave(int id, string employeeName, int daysRequested, string reason)
            : base(id, employeeName, daysRequested)
        {
            Reason = reason;
        }

        public override void DisplayRequestDetails()
        {
            Console.WriteLine($"[Casual Leave] ID: {Id}, Name: {EmployeeName}, Days: {DaysRequested}, Reason: {Reason}");
        }

        public override void ShowApprovalStatus()
        {
            Console.WriteLine($"[Casual Leave] ID: {Id}, Name: {EmployeeName}, Status: {Status}");
        }
    }
}
