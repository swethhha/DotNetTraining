using System;
using System.Linq;
using EFCoreDbFirstDemoPhase2.Models;

namespace EFCoreDbFirstDemoPhase2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CompanyContext())
            {

                var itDept = context.Departments.FirstOrDefault(d => d.DepartmentName == "IT");
                var hrDept = context.Departments.FirstOrDefault(d => d.DepartmentName == "HR");
                var salesDept = context.Departments.FirstOrDefault(d => d.DepartmentName == "Sales");

                if (itDept == null)
                {
                    itDept = new Department { DepartmentName = "IT" };
                    context.Departments.Add(itDept);
                }

                if (hrDept == null)
                {
                    hrDept = new Department { DepartmentName = "HR" };
                    context.Departments.Add(hrDept);
                }

                if (salesDept == null)
                {
                    salesDept = new Department { DepartmentName = "Sales" };
                    context.Departments.Add(salesDept);
                }

                context.SaveChanges();

                if (!context.Employees.Any(e => e.EmployeeName == "John"))
                {
                    context.Employees.Add(new Employee { EmployeeName = "John", Age = 28, DepartmentId = itDept.DepartmentId });
                }

                if (!context.Employees.Any(e => e.EmployeeName == "Priya"))
                {
                    context.Employees.Add(new Employee { EmployeeName = "Priya", Age = 31, DepartmentId = hrDept.DepartmentId });
                }

                if (!context.Employees.Any(e => e.EmployeeName == "Ali"))
                {
                    context.Employees.Add(new Employee { EmployeeName = "Ali", Age = 25, DepartmentId = salesDept.DepartmentId });
                }

                context.SaveChanges();

                Console.WriteLine("\n------ All Employees with Department Info ------");
                var employees = context.Employees.Select(e => new
                {
                    e.EmployeeId,
                    e.EmployeeName,
                    e.Age,
                    DepartmentName = e.Department != null ? e.Department.DepartmentName : "N/A"
                }).ToList();

                Console.WriteLine($"Total Employees Found: {employees.Count}");
                foreach (var emp in employees)
                {
                    Console.WriteLine($"ID: {emp.EmployeeId}, Name: {emp.EmployeeName}, Age: {emp.Age}, Department: {emp.DepartmentName}");
                }


                var employeeToUpdate = context.Employees.FirstOrDefault(e => e.EmployeeName == "John");
                if (employeeToUpdate != null)
                {
                    employeeToUpdate.Age = 29;
                    context.SaveChanges();
                    Console.WriteLine($"\nUpdated John's age to {employeeToUpdate.Age}");
                }


                var employeeToDelete = context.Employees.FirstOrDefault(e => e.EmployeeName == "Ali");
                if (employeeToDelete != null)
                {
                    context.Employees.Remove(employeeToDelete);
                    context.SaveChanges();
                    Console.WriteLine("Deleted employee Ali.");
                }


                Console.WriteLine("\n------ Final Employee List ------");
                var finalEmployees = context.Employees.Select(e => new
                {
                    e.EmployeeId,
                    e.EmployeeName,
                    e.Age,
                    DepartmentName = e.Department != null ? e.Department.DepartmentName : "N/A"
                }).ToList();

                foreach (var emp in finalEmployees)
                {
                    Console.WriteLine($"ID: {emp.EmployeeId}, Name: {emp.EmployeeName}, Age: {emp.Age}, Department: {emp.DepartmentName}");
                }
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
