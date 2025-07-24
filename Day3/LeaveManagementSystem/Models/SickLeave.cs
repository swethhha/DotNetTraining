using System;

namespace LeaveManagementSystem.Models
{
    public class SickLeave : LeaveRequest, IApprovable
    {
        public string MedicalCertificate { get; set; }

        public SickLeave(int id, string employeeName, int daysRequested, string medicalCertificate)
            : base(id, employeeName, daysRequested)
        {
            MedicalCertificate = medicalCertificate;
        }

        public override void DisplayRequestDetails()
        {
            Console.WriteLine($"[Sick Leave] ID: {Id}, Name: {EmployeeName}, Days: {DaysRequested}, Certificate: {MedicalCertificate}");
        }

        public override void ShowApprovalStatus()
        {
            Console.WriteLine($"[Sick Leave] ID: {Id}, Name: {EmployeeName}, Status: {Status}");
        }
    }
}
