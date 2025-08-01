﻿using System;
using System.Collections.Generic;

namespace EFCoreDbFirstDemoPhase2.Models;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
}
