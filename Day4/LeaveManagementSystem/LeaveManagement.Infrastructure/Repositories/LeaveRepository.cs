using LeaveManagementSystem.Core.Models;
using LeaveManagementSystem.Core.Interfaces;

namespace LeaveManagementSystem.Infrastructure.Repositories;

public class LeaveRepository : ILeaveRepository
{
    private readonly List<LeaveRequest> _leaves = new();

    public void Add(LeaveRequest request)
    {
        request.Id = _leaves.Count + 1;
        _leaves.Add(request);
    }

    public List<LeaveRequest> GetAll()
    {
        return _leaves;
    }

    public LeaveRequest? GetById(int id)
    {
        return _leaves.FirstOrDefault(x => x.Id == id);
    }

    public void Delete(int id)
    {
        var req = GetById(id);
        if (req != null)
            _leaves.Remove(req);
    }
}
