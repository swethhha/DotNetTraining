using System;
using EmployeeTracker.Core.Entities;
using EmployeeTracker.Application.Services;
using EmployeeTracker.Infrastructure.Repositories;

class Program
{
    static DepartmentService departmentService = new DepartmentService(new DepartmentRepository());
    static EmployeeService employeeService = new EmployeeService(new EmployeeRepository());
    static int departmentIdCounter = 1;
    static int employeeIdCounter = 1;

    static void Main()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n=== Employee Tracker System ===");
            Console.WriteLine("1. Add Department");
            Console.WriteLine("2. View All Departments");
            Console.WriteLine("3. Update Department");
            Console.WriteLine("4. Delete Department");
            Console.WriteLine("5. Add Employee");
            Console.WriteLine("6. View All Employees");
            Console.WriteLine("7. Update Employee");
            Console.WriteLine("8. Delete Employee");
            Console.WriteLine("9. Exit");
            Console.Write("Enter your choice: ");

            string? input = Console.ReadLine();
            Console.WriteLine();
            switch (input)
            {
                case "1":
                    AddDepartment();
                    break;
                case "2":
                    ViewAllDepartments();
                    break;
                case "3":
                    UpdateDepartment();
                    break;
                case "4":
                    DeleteDepartment();
                    break;
                case "5":
                    AddEmployee();
                    break;
                case "6":
                    ViewAllEmployees();
                    break;
                case "7":
                    UpdateEmployee();
                    break;
                case "8":
                    DeleteEmployee();
                    break;
                case "9":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Try again.");
                    break;
            }
        }
    }

    static void AddDepartment()
    {
        Console.Write("Enter Department Name: ");
        string? name = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(name))
        {
            Console.WriteLine("Department name cannot be empty.");
            return;
        }

        var dept = new Department { DeptId = departmentIdCounter++, DeptName = name };
        departmentService.Add(dept);
        Console.WriteLine("Department added successfully.");
    }

    static void ViewAllDepartments()
    {
        var departments = departmentService.GetAll();
        if (departments.Count == 0)
        {
            Console.WriteLine("No departments found.");
            return;
        }

        Console.WriteLine("Departments:");
        foreach (var d in departments)
            Console.WriteLine($"ID: {d.DeptId}, Name: {d.DeptName}");
    }

    static void UpdateDepartment()
    {
        Console.Write("Enter Department ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var dept = departmentService.GetById(id);
        if (dept == null)
        {
            Console.WriteLine("Department not found.");
            return;
        }

        Console.Write("Enter new Department Name: ");
        string? newName = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(newName))
        {
            Console.WriteLine("Department name cannot be empty.");
            return;
        }

        dept.DeptName = newName;
        departmentService.Update(dept);
        Console.WriteLine("Department updated successfully.");
    }

    static void DeleteDepartment()
    {
        Console.Write("Enter Department ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var dept = departmentService.GetById(id);
        if (dept == null)
        {
            Console.WriteLine("Department not found.");
            return;
        }

        departmentService.Delete(id);
        Console.WriteLine("Department deleted successfully.");
    }

    static void AddEmployee()
    {
        Console.Write("Enter Employee Name: ");
        string? name = Console.ReadLine();
        Console.Write("Enter Department ID: ");
        string? deptInput = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(deptInput))
        {
            Console.WriteLine("All fields are required.");
            return;
        }

        if (!int.TryParse(deptInput, out int deptId))
        {
            Console.WriteLine("Invalid Department ID.");
            return;
        }

        var dept = departmentService.GetById(deptId);
        if (dept == null)
        {
            Console.WriteLine("Department not found.");
            return;
        }

        var emp = new Employee
        {
            Id = employeeIdCounter++,
            Name = name,
            DepartmentId = deptId
        };

        employeeService.Add(emp);
        Console.WriteLine("Employee added successfully.");
    }

    static void ViewAllEmployees()
    {
        var employees = employeeService.GetAll();
        if (employees.Count == 0)
        {
            Console.WriteLine("No employees found.");
            return;
        }

        Console.WriteLine("Employees:");
        foreach (var e in employees)
        {
            var dept = departmentService.GetById(e.DepartmentId);
            string deptName = dept != null ? dept.DeptName : "Unknown";
            Console.WriteLine($"ID: {e.Id}, Name: {e.Name}, Department: {deptName}");
        }
    }

    static void UpdateEmployee()
    {
        Console.Write("Enter Employee ID to update: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var emp = employeeService.GetById(id);
        if (emp == null)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        Console.Write("Enter new Name: ");
        string? name = Console.ReadLine();
        Console.Write("Enter new Department ID: ");
        string? deptInput = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(deptInput))
        {
            Console.WriteLine("All fields are required.");
            return;
        }

        if (!int.TryParse(deptInput, out int deptId))
        {
            Console.WriteLine("Invalid Department ID.");
            return;
        }

        var dept = departmentService.GetById(deptId);
        if (dept == null)
        {
            Console.WriteLine("Department not found.");
            return;
        }

        emp.Name = name;
        emp.DepartmentId = deptId;
        employeeService.Update(emp);
        Console.WriteLine("Employee updated successfully.");
    }

    static void DeleteEmployee()
    {
        Console.Write("Enter Employee ID to delete: ");
        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid ID.");
            return;
        }

        var emp = employeeService.GetById(id);
        if (emp == null)
        {
            Console.WriteLine("Employee not found.");
            return;
        }

        employeeService.Delete(id);
        Console.WriteLine("Employee deleted successfully.");
    }
}
