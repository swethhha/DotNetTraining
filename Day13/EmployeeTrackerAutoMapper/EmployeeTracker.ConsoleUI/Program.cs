using AutoMapper;
using EmployeeTracker.Application.Mapping;
using EmployeeTracker.Application.Services;
using EmployeeTracker.Core.DTOs;
using EmployeeTracker.Core.Entities;
using EmployeeTracker.Core.Interfaces;
using EmployeeTracker.Infrastructure.Repositories;

var config = new AutoMapper.MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MappingProfile());
});

var mapper = config.CreateMapper();

// Repositories
IEmployeeRepository employeeRepository = new EmployeeRepository();
IDepartmentRepository departmentRepository = new DepartmentRepository();

// Services
IEmployeeService employeeService = new EmployeeService(mapper, employeeRepository);
IDepartmentService departmentService = new DepartmentService(mapper, departmentRepository);

while (true)
{
    Console.Clear();
    Console.WriteLine("========= Employee Tracker =========");
    Console.WriteLine("1. Add Employee");
    Console.WriteLine("2. View All Employees");
    Console.WriteLine("3. Update Employee");
    Console.WriteLine("4. Delete Employee");
    Console.WriteLine("5. Add Department");
    Console.WriteLine("6. View All Departments");
    Console.WriteLine("7. Update Department");
    Console.WriteLine("8. Delete Department");
    Console.WriteLine("9. Exit");
    Console.Write("Select an option: ");

    var input = Console.ReadLine();

    switch (input)
    {
        case "1": AddEmployee(); break;
        case "2": ViewAllEmployees(); break;
        case "3": UpdateEmployee(); break;
        case "4": DeleteEmployee(); break;
        case "5": AddDepartment(); break;
        case "6": ViewAllDepartments(); break;
        case "7": UpdateDepartment(); break;
        case "8": DeleteDepartment(); break;
        case "9": return;
        default: Console.WriteLine("Invalid input!"); break;
    }

    Console.WriteLine("\nPress Enter to continue...");
    Console.ReadLine();
}

void AddEmployee()
{
    Console.Write("Enter Name: ");
    var name = Console.ReadLine();
    Console.Write("Enter Email: ");
    var email = Console.ReadLine();
    Console.Write("Enter Designation: ");
    var designation = Console.ReadLine();
    Console.Write("Enter Department: ");
    var department = Console.ReadLine();

    var dto = new EmployeeRequestDTO
    {
        EmployeeName = name ?? "Unknown",
        Email = email ?? "unknown@example.com",
        Designation = designation ?? "Unknown",
        Department = department ?? "Unknown"
    };

    employeeService.AddEmployee(dto);
    Console.WriteLine("✅ Employee added successfully.");
}

void ViewAllEmployees()
{
    var employees = employeeService.GetAllEmployees();
    if (employees.Count == 0)
    {
        Console.WriteLine("⚠️ No employees found.");
        return;
    }

    Console.WriteLine("====== All Employees ======");
    foreach (var e in employees)
    {
        Console.WriteLine($" Name: {e.EmployeeName}, Email: {e.Email}, Designation: {e.Designation}, Department: {e.Department}");
    }
}

void UpdateEmployee()
{
    Console.Write("Enter Employee ID to Update: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("❌ Invalid ID.");
        return;
    }

    var existing = employeeService.GetEmployeeById(id);
    if (existing == null)
    {
        Console.WriteLine("❌ Employee not found.");
        return;
    }

    Console.Write("Enter Updated Name: ");
    existing.EmployeeName = Console.ReadLine() ?? existing.EmployeeName;
    Console.Write("Enter Updated Email: ");
    existing.Email = Console.ReadLine() ?? existing.Email;
    Console.Write("Enter Updated Designation: ");
    existing.Designation = Console.ReadLine() ?? existing.Designation;
    Console.Write("Enter Updated Department: ");
    existing.Department = Console.ReadLine() ?? existing.Department;

    employeeService.UpdateEmployee(existing);
    Console.WriteLine("✅ Employee updated successfully.");
}

void DeleteEmployee()
{
    Console.Write("Enter Employee ID to Delete: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("❌ Invalid ID.");
        return;
    }

    var existing = employeeService.GetEmployeeById(id);
    if (existing == null)
    {
        Console.WriteLine("❌ Employee not found.");
        return;
    }

    employeeService.DeleteEmployee(id);
    Console.WriteLine("✅ Employee deleted successfully.");
}

void AddDepartment()
{
    Console.Write("Enter Department Name: ");
    var name = Console.ReadLine();

    var dto = new DepartmentRequestDTO
    {
        DepartmentName = name ?? "Unknown"
    };

    departmentService.AddDepartment(dto);
    Console.WriteLine("✅ Department added successfully.");
}

void ViewAllDepartments()
{
    var departments = departmentService.GetAllDepartments();
    if (departments.Count == 0)
    {
        Console.WriteLine("⚠️ No departments found.");
        return;
    }

    Console.WriteLine("====== All Departments ======");
    foreach (var d in departments)
    {
        Console.WriteLine($"Name: {d.DepartmentName}");
    }
}

void UpdateDepartment()
{
    Console.Write("Enter Department ID to Update: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("❌ Invalid ID.");
        return;
    }

    var existing = departmentService.GetDepartmentById(id);
    if (existing == null)
    {
        Console.WriteLine("❌ Department not found.");
        return;
    }

    Console.Write("Enter Updated Name: ");
    existing.DepartmentName = Console.ReadLine() ?? existing.DepartmentName;

    departmentService.UpdateDepartment(existing);
    Console.WriteLine("✅ Department updated successfully.");
}

void DeleteDepartment()
{
    Console.Write("Enter Department ID to Delete: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("❌ Invalid ID.");
        return;
    }

    var existing = departmentService.GetDepartmentById(id);
    if (existing == null)
    {
        Console.WriteLine("❌ Department not found.");
        return;
    }

    departmentService.DeleteDepartment(id);
    Console.WriteLine("✅ Department deleted successfully.");
}
