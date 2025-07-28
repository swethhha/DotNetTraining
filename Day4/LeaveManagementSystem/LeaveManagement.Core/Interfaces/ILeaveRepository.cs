using LeaveManagementSystem.Core.Models;

namespace LeaveManagementSystem.Core.Interfaces;

public interface ILeaveRepository
{
    void Add(LeaveRequest request);
    List<LeaveRequest> GetAll();
    LeaveRequest? GetById(int id);
    void Delete(int id);
}
