using SupportDeskAssesment.Data;
using SupportDeskAssesment.Models;
using System.Collections.Generic;
using System.Linq;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public void AddUser(string name)
    {
        var user = new User { UserName = name };
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public List<User> GetAllUsers() => _context.Users.ToList();

    public User? GetUserById(int id) => _context.Users.FirstOrDefault(u => u.UserId == id);
}