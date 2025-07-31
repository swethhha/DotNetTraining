using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
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
                Console.WriteLine("\nMenu:");
                Console.WriteLine("1. Add Course");
                Console.WriteLine("2. View Courses");
                Console.WriteLine("3. Update Course");
                Console.WriteLine("4. Delete Course");
                Console.WriteLine("5. Add Student");
                Console.WriteLine("6. View Students");
                Console.WriteLine("7. Update Student");
                Console.WriteLine("8. Delete Student");
                Console.WriteLine("9. Exit");
                Console.Write("Enter your choice: ");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1": AddCourse(context); break;
                    case "2": ViewCourses(context); break;
                    case "3": UpdateCourse(context); break;
                    case "4": DeleteCourse(context); break;
                    case "5": AddStudent(context); break;
                    case "6": ViewStudents(context); break;
                    case "7": UpdateStudent(context); break;
                    case "8": DeleteStudent(context); break;
                    case "9": exit = true; break;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }

        static void AddCourse(StudentCourseDbContext context)
        {
            Console.Write("Enter Course Name: ");
            string name = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Course name cannot be empty.");
                return;
            }

            if (context.Courses.Any(c => c.CourseName.ToLower() == name.ToLower()))
            {
                Console.WriteLine("Course already exists.");
                return;
            }

            var course = new Course { CourseName = name };
            context.Courses.Add(course);
            context.SaveChanges();
            Console.WriteLine("Course added.");
        }

        static void ViewCourses(StudentCourseDbContext context)
        {
            var courses = context.Courses.ToList();
            if (!courses.Any())
            {
                Console.WriteLine("No courses found.");
                return;
            }

            Console.WriteLine("Courses:");
            foreach (var c in courses)
                Console.WriteLine($"ID: {c.CourseId}, Name: {c.CourseName}");
        }

        static void UpdateCourse(StudentCourseDbContext context)
        {
            Console.Write("Enter Course ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var course = context.Courses.Find(id);
            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            Console.Write("Enter new Course Name: ");
            string newName = Console.ReadLine()?.Trim();

            if (string.IsNullOrWhiteSpace(newName))
            {
                Console.WriteLine("Course name cannot be empty.");
                return;
            }

            if (context.Courses.Any(c => c.CourseName.ToLower() == newName.ToLower() && c.CourseId != id))
            {
                Console.WriteLine("Another course with same name exists.");
                return;
            }

            course.CourseName = newName;
            context.SaveChanges();
            Console.WriteLine("Course updated.");
        }

        static void DeleteCourse(StudentCourseDbContext context)
        {
            Console.Write("Enter Course ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var course = context.Courses.Include(c => c.Students).FirstOrDefault(c => c.CourseId == id);
            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            context.Students.RemoveRange(course.Students); 
            context.Courses.Remove(course);
            context.SaveChanges();
            Console.WriteLine("Course deleted.");

            if (!context.Courses.Any())
            {
                context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Courses', RESEED, 0);");
                Console.WriteLine("Course ID reset to start from 1.");
            }
        }

        static void AddStudent(StudentCourseDbContext context)
        {
            Console.Write("Enter Student Name: ");
            string name = Console.ReadLine()?.Trim();
            Console.Write("Enter Course ID: ");
            if (!int.TryParse(Console.ReadLine(), out int courseId))
            {
                Console.WriteLine("Invalid Course ID.");
                return;
            }

            var course = context.Courses.Find(courseId);
            if (course == null)
            {
                Console.WriteLine("Course not found.");
                return;
            }

            var student = new Student { StudentName = name, CourseId = courseId };
            context.Students.Add(student);
            context.SaveChanges();
            Console.WriteLine("Student added.");
        }

        static void ViewStudents(StudentCourseDbContext context)
        {
            var students = context.Students.Include(s => s.Course).ToList();
            if (!students.Any())
            {
                Console.WriteLine("No students found.");
                return;
            }

            Console.WriteLine("Students:");
            foreach (var s in students)
                Console.WriteLine($"ID: {s.StudentId}, Name: {s.StudentName}, Course: {s.Course?.CourseName}");
        }

        static void UpdateStudent(StudentCourseDbContext context)
        {
            Console.Write("Enter Student ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var student = context.Students.Find(id);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            Console.Write("Enter new Student Name: ");
            string newName = Console.ReadLine()?.Trim();

            Console.Write("Enter new Course ID: ");
            if (!int.TryParse(Console.ReadLine(), out int newCourseId))
            {
                Console.WriteLine("Invalid Course ID.");
                return;
            }

            if (!context.Courses.Any(c => c.CourseId == newCourseId))
            {
                Console.WriteLine("Course not found.");
                return;
            }

            student.StudentName = newName;
            student.CourseId = newCourseId;
            context.SaveChanges();
            Console.WriteLine("Student updated.");
        }

        static void DeleteStudent(StudentCourseDbContext context)
        {
            Console.Write("Enter Student ID to delete: ");
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("Invalid ID.");
                return;
            }

            var student = context.Students.Find(id);
            if (student == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }

            context.Students.Remove(student);
            context.SaveChanges();
            Console.WriteLine("Student deleted.");

            if (!context.Students.Any())
            {
                context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Students', RESEED, 0);");
                Console.WriteLine("Student ID reset to start from 1.");
            }
        }
    }
}
