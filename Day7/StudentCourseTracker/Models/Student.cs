using System;
using System.Collections.Generic;

namespace StudentCourseTracker.Models;

public partial class Student
{
    public int StudentId { get; set; }

    public string StudentName { get; set; } = null!;

    public int Age { get; set; }

    public int? CourseId { get; set; }

    public virtual Course? Course { get; set; }
}
