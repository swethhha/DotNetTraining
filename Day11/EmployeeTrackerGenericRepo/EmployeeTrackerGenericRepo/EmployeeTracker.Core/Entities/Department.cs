﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeTracker.Core.Entities
{
    public class Department
    {
        public int DeptId { get; set; }
        public required string DeptName { get; set; }
    }
}

