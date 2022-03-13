using System;

using AssignmentManager.Data;
using AssignmentManager.Data.Models;

namespace AssignmentManager.Services.Tests
{
    public class TestDBInitializer
    {
        public TestDBInitializer()
        {

        }

        public void Seed(AssignmentManagerDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Colors.AddRange(
                new Color() { Name = "#FF0000" },
                new Color() { Name = "#00FF00" },
                new Color() { Name = "#0000FF" },
                new Color() { Name = "#FFFF00" }
            );

            context.SaveChanges();

            context.Classes.Add(new Class() { Name = "English", ColorId = 2 });
            context.Classes.Add(new Class() { Name = "Music", ColorId = 4 });
            context.Classes.Add(new Class() { Name = "Art", ColorId = 3 });
            context.Classes.Add(new Class() { Name = "Math", ColorId = 1 });
            context.Classes.Add(new Class() { Name = "Algebra", ColorId = 1 });

            context.SaveChanges();

            context.Assignments.AddRange(
                new Assignment()
                {
                    Name = "Exam",
                    ClassId = 1,
                    DueDate = new DateTime(2002, 12, 05),
                    Description = "empty",
                    IsCompleted = false
                },
                new Assignment()
                {
                    Name = "Test",
                    ClassId = 4,
                    DueDate = new DateTime(2000, 05, 18),
                    Description = null,
                    IsCompleted = true
                },
                new Assignment()
                {
                    Name = "Homework",
                    ClassId = 3,
                    DueDate = new DateTime(2018, 03, 22),
                    Description = string.Empty,
                    IsCompleted = false
                },
                new Assignment()
                {
                    Name = "Teamwork",
                    ClassId = 2,
                    DueDate = new DateTime(1985, 03, 01),
                    IsCompleted = true
                },
                new Assignment()
                {
                    Name = "Algebra Final Exam",
                    ClassId = 5,
                    DueDate = new DateTime(2021, 06, 15),
                    Description = "Grade 12 Final Exam",
                    IsCompleted = false
                },
                new Assignment()
                {
                    Name = "Audition Exam",
                    ClassId = 3,
                    DueDate = new DateTime(2020, 09, 25),
                    Description = "My first audition",
                    IsCompleted = true
                },
                new Assignment()
                {
                    Name = "Grammar Exam",
                    ClassId = 4,
                    DueDate = new DateTime(2022, 03, 08),
                    Description = "Passive and Infinitive forms",
                    IsCompleted = true
                }
            );


            context.SaveChanges();
        }
    }
}
