using System;
using System.Data.Entity;
using StudentSystem.Context;
using StudentSystem.Models;
using StudentSystem.Context.Migrations;

namespace StudentSystem.Client
{
    public class StudentSystemClient
    {
        static void Main()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion
                                     <StudentSystemContext, Configuration>());

            var context = new StudentSystemContext();

            //var student = new Student
            //{
            //    Name = "Pesho",
            //    FacultyNumber = "12345"
            //};

            //var student1 = new Student
            //{
            //    Name = "ivan",
            //    FacultyNumber = "15"
            //};

            var course1 = new Course
            {
                Name = "CSS",
                Description = "hard",
                Materials = "nakov book"
            };

            var course2 = new Course
            {
                Name = "HTML",
                Description = "not so hard",
                Materials = "nakov book"
            };

            var hw = new Homework
            {
                Content = "HTMLHW",
                TimeSent = DateTime.Now,
                StudentId = 1,
                CourseId = 1
            };

            var hw1 = new Homework
            {
                Content = "CSSHW",
                TimeSent = DateTime.Now,
                StudentId = 1,
                CourseId = 2
            };

            //context.Students.Add(student);
            //context.Students.Add(student1);

            context.Courses.Add(course1);
            context.Courses.Add(course2);

            context.Homeworks.Add(hw);
            context.Homeworks.Add(hw1);
            context.SaveChanges();
        }
    }
}
