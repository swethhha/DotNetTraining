using System;
using System.Linq;
using StudentCourseTracker.Models;

namespace StudentCourseTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            using var context = new StudentCourseDbContext();

            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("\n===== Student Course Tracker =====");
                Console.WriteLine("1. Add Course");
                Console.WriteLine("2. Add Student");
                Console.WriteLine("3. View All Students");
                Console.WriteLine("4. View All Courses");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");
                var input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddCourse(context);
                        break;
                    case "2":
                        AddStudent(context);
                        break;
                    case "3":
                        ViewAllStudents(context);
                        break;
                    case "4":
                        ViewAllCourses(context);
                        break;
                    case "5":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }
            }
        }

        static void AddCourse(StudentCourseDbContext context)
        {
            Console.Write("Enter Course Name: ");
            var name = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(name))
            {
                var course = new Course { CourseName = name };
                context.Courses.Add(course);
                context.SaveChanges();
                Console.WriteLine("Course added successfully.");
            }
            else
            {
                Console.WriteLine("Course name cannot be empty.");
            }
        }

        static void AddStudent(StudentCourseDbContext context)
        {
            Console.Write("Enter Student Name: ");
            var name = Console.ReadLine();

            Console.Write("Enter Age: ");
            bool validAge = int.TryParse(Console.ReadLine(), out int age);

            Console.WriteLine("\nAvailable Courses:");
            var courses = context.Courses.ToList();
            foreach (var c in courses)
            {
                Console.WriteLine($"{c.CourseId}. {c.CourseName}");
            }

            Console.Write("Enter Course ID: ");
            bool validCourse = int.TryParse(Console.ReadLine(), out int courseId);

            if (!string.IsNullOrWhiteSpace(name) && validAge && validCourse && courses.Any(c => c.CourseId == courseId))
            {
                var student = new Student
                {
                    StudentName = name,
                    Age = age,
                    CourseId = courseId
                };
                context.Students.Add(student);
                context.SaveChanges();
                Console.WriteLine("Student added successfully.");
            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
            }
        }

        static void ViewAllStudents(StudentCourseDbContext context)
        {
            var students = context.Students
                                  .Select(s => new
                                  {
                                      s.StudentId,
                                      s.StudentName,
                                      s.Age,
                                      Course = s.Course != null ? s.Course.CourseName : "N/A"
                                  })
                                  .ToList();

            Console.WriteLine("\n-- All Students --");
            foreach (var s in students)
            {
                Console.WriteLine($"ID: {s.StudentId}, Name: {s.StudentName}, Age: {s.Age}, Course: {s.Course}");
            }
        }

        static void ViewAllCourses(StudentCourseDbContext context)
        {
            var courses = context.Courses
                                 .Select(c => new
                                 {
                                     c.CourseId,
                                     c.CourseName,
                                     StudentCount = c.Students.Count
                                 })
                                 .ToList();

            Console.WriteLine("\n-- All Courses --");
            foreach (var c in courses)
            {
                Console.WriteLine($"ID: {c.CourseId}, Name: {c.CourseName}, Students: {c.StudentCount}");
            }
        }
    }
}
