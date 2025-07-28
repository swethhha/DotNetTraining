using LeaveManagementSystem.Core.Models;

namespace LeaveManagementSystem.Application.Interfaces;

public interface ILeaveService
{
    void ApplyLeave(LeaveRequest request);
    List<LeaveRequest> GetAllLeaves();
    LeaveRequest? GetLeaveById(int id);
    void DeleteLeave(int id);
}
