using System;
using System.Collections.Generic;
using Day2proj1phase1.Models;

class Program
{
    static void Main()
    {
        var items = new List<IReportable>();

        var bug = new Bug(
            id: 1,
            title: "Login not working",
            assignedTo: "Swetha",
            severity: "High"
        );
        items.Add(bug);

        Day2proj1phase1.Models.Task task = new Day2proj1phase1.Models.Task(
            id: 2,
            title: "Implement Logout",
            assignedTo: "Mirdula",
            estimatedHours: 4
        );
        items.Add(task);

        // Output line by line
        Console.WriteLine($"[BUG] #{bug.Id}: {bug.Title}");
        Console.WriteLine($"Severity : {bug.Severity}");
        Console.WriteLine($"Assigned to : {bug.AssignedTo}");

        Console.WriteLine($"[TASK] #{task.Id} : {task.Title}");
        Console.WriteLine($"ETA : {task.EstimatedHours} hrs");
        Console.WriteLine($"Assigned to : {task.AssignedTo}");
        Console.WriteLine();

        bug.Display();
        bug.ReportStatus();

        task.Display();
        task.ReportStatus();
    }
}
