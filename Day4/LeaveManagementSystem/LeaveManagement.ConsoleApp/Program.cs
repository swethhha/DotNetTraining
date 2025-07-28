using LeaveManagementSystem.Core.Models;
using LeaveManagementSystem.Application.Services;
using LeaveManagementSystem.Application.Interfaces;
using LeaveManagementSystem.Infrastructure.Repositories;

namespace LeaveManagementSystem.ConsoleApp;

class Program
{
    static void Main()
    {
        ILeaveService service = new LeaveService(new LeaveRepository());
        while (true)
        {
            Console.WriteLine("\n1. Apply Leave\n2. View All\n3. Approve/Reject\n4. Delete\n5. Exit");
            Console.Write("Choose option: ");
            string? opt = Console.ReadLine();

            switch (opt)
            {
                case "1":
                    LeaveRequest req = new();

                    Console.Write("Enter Name: ");
                    req.Name = Console.ReadLine()?.Trim() ?? "";
                    if (string.IsNullOrWhiteSpace(req.Name))
                    {
                        Console.WriteLine("Name is required.");
                        break;
                    }

                    Console.Write("Leave Type (Sick/Casual): ");
                    req.LeaveType = Console.ReadLine()?.Trim() ?? "";
                    if (req.LeaveType.ToLower() != "sick" && req.LeaveType.ToLower() != "casual")
                    {
                        Console.WriteLine("Only Sick or Casual allowed.");
                        break;
                    }

                    Console.Write("Start Date (yyyy-mm-dd): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out DateTime start))
                    {
                        Console.WriteLine("Invalid start date.");
                        break;
                    }
                    req.StartDate = start;

                    Console.Write("End Date (yyyy-mm-dd): ");
                    if (!DateTime.TryParse(Console.ReadLine(), out DateTime end))
                    {
                        Console.WriteLine("Invalid end date.");
                        break;
                    }
                    if (end < start)
                    {
                        Console.WriteLine("End date can't be before start.");
                        break;
                    }
                    req.EndDate = end;

                    Console.Write("Reason: ");
                    req.Reason = Console.ReadLine() ?? "";

                    service.ApplyLeave(req);
                    Console.WriteLine("Leave Applied.\n");
                    break;

                case "2":
                    var leaves = service.GetAllLeaves();
                    if (!leaves.Any())
                    {
                        Console.WriteLine("No requests.");
                        break;
                    }

                    foreach (var l in leaves)
                    {
                        Console.WriteLine($"ID:{l.Id}, Name:{l.Name}, Type:{l.LeaveType}, From:{l.StartDate:dd-MM-yyyy}, To:{l.EndDate:dd-MM-yyyy}, Reason:{l.Reason}, Status:{l.Status}");
                    }
                    break;

                case "3":
                    Console.Write("Enter ID: ");
                    if (!int.TryParse(Console.ReadLine(), out int id))
                    {
                        Console.WriteLine("Invalid ID.");
                        break;
                    }

                    var leave = service.GetLeaveById(id);
                    if (leave == null)
                    {
                        Console.WriteLine("Not found.");
                        break;
                    }

                    Console.Write("Approve or Reject (A/R): ");
                    string? choice = Console.ReadLine()?.ToUpper();
                    if (choice == "A")
                        leave.Status = "Approved";
                    else if (choice == "R")
                        leave.Status = "Rejected";
                    else
                    {
                        Console.WriteLine("Invalid choice.");
                        break;
                    }

                    Console.WriteLine("Status Updated.");
                    break;

                case "4":
                    Console.Write("Enter ID to delete: ");
                    if (!int.TryParse(Console.ReadLine(), out int delId))
                    {
                        Console.WriteLine("Invalid ID.");
                        break;
                    }

                    if (service.GetLeaveById(delId) == null)
                    {
                        Console.WriteLine("Not found.");
                        break;
                    }

                    service.DeleteLeave(delId);
                    Console.WriteLine("Deleted.");
                    break;

                case "5":
                    return;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }
        }
    }
}
