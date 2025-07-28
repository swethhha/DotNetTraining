using LeaveManagementSystem.Core.Models;
using LeaveManagementSystem.Core.Interfaces;
using LeaveManagementSystem.Application.Interfaces;

namespace LeaveManagementSystem.Application.Services;

public class LeaveService : ILeaveService
{
    private readonly ILeaveRepository _repo;

    public LeaveService(ILeaveRepository repo)
    {
        _repo = repo;
    }

    public void ApplyLeave(LeaveRequest request)
    {
        _repo.Add(request);
    }

    public List<LeaveRequest> GetAllLeaves()
    {
        return _repo.GetAll();
    }

    public LeaveRequest? GetLeaveById(int id)
    {
        return _repo.GetById(id);
    }

    public void DeleteLeave(int id)
    {
        _repo.Delete(id);
    }
}
