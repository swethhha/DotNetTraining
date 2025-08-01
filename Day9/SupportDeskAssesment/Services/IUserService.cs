using SupportDeskAssesment.Models;
using System.Collections.Generic;

public interface IUserService
{
    void AddUser(string name);
    List<User> GetAllUsers();
    User? GetUserById(int id);
}
