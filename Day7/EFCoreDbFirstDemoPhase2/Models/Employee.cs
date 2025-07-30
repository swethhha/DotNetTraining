using System;
using System.Collections.Generic;

namespace EFCoreDbFirstDemoPhase2.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? EmployeeName { get; set; }

    public int? Age { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }
}
