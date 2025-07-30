using System;
using System.Linq;
using EFCoreDbFirstDemo.Models;
using Microsoft.EntityFrameworkCore;
namespace EFCoreDbFirstDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new CompanyContext())
            {
                Console.WriteLine("------- All Departments ------");
                foreach (var dept in context.Departments)
                {
                    Console.WriteLine($"  {dept.DepartmentId} - {dept.DepartmentName}");
                }

                Console.WriteLine("\n------- All Employees with Department Info ------");


                var employees = context.Employees
                                       .Select(e => new
                                       {
                                           e.EmployeeId,
                                           e.EmployeeName,
                                           e.Age,
                                           DepartmentName = e.Department != null ? e.Department.DepartmentName : "N/A"
                                       }).ToList();
                Console.WriteLine($"Total Employees Found :{employees.Count}");
                foreach (var emp in employees)
                {
                    Console.WriteLine($"  {emp.EmployeeId} - {emp.EmployeeName}, Age: {emp.Age}, Department: {emp.DepartmentName}");
                }
            }

            Console.WriteLine("\nDone. Press any key to exit.");
            Console.ReadKey();
        }
    }
}
