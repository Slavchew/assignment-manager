using System;
using System.Linq;

using AssignmentManager.Data;
using AssignmentManager.Services.Implementations;
using AssignmentManager.Services.Models.Class;

using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace AssignmentManager.Services.Tests.Classes
{
    [TestFixture]
    public class ClassesTests
    {
        private AssignmentManagerDbContext context;
        private ClassesService service;
        public static DbContextOptions<AssignmentManagerDbContext> dbContextOptions { get; }
        public static string connectionString = 
            "Server=.;Database=AssignmentManagerTests;Integrated Security=true;";

        static ClassesTests()
        {
            dbContextOptions = new DbContextOptionsBuilder<AssignmentManagerDbContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        [OneTimeSetUp]
        public void Init()
        {
            context = new AssignmentManagerDbContext(dbContextOptions);
            TestDBInitializer db = new TestDBInitializer();
            db.Seed(context);

            service = new ClassesService(context);
        }

        [TestCase(1, "Math", "#FF0000", 1)]
        [TestCase(2, "Art", "#0000FF", 1)]
        [TestCase(3, "Music", "#FFFF00", 2)]
        [TestCase(4, "English", "#00FF00", 2)]
        [TestCase(5, "Algebra", "#FF0000", 1)]
        public void GetByIdShouldWorkCorrectly(int id, string name, string color, int assignmentsCount)
        {
            var serviceModel = service.GetById(id);

            Assert.AreEqual(id, serviceModel.Id);
            Assert.AreEqual(name, serviceModel.Name);
            Assert.AreEqual(color, serviceModel.Color);
            Assert.AreEqual(assignmentsCount, serviceModel.Assignments.Count);
        }

        [TestCase(8)]
        [TestCase(-1)]
        [TestCase(1000000)]
        public void GetByIdShouldThrowExceptionForInvalidId(int id)
        {
            Assert.Throws<InvalidOperationException>(() => service.GetById(id));
        }

        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public void ExistsShouldReturnTrueIfClassExists(int id)
        {
            var expected = service.Exists(id);

            Assert.True(expected);
        }

        [TestCase(-1)]
        [TestCase(10000)]
        [TestCase(-55555)]
        public void ExistsShouldReturnFalseIfClassNotExists(int id)
        {
            var expected = service.Exists(id);

            Assert.False(expected);
        }

        [Test]
        public void GetAllShouldWorkCorrectly()
        {
            var classes = service.GetAll().ToList();

            Assert.AreEqual(5, classes.Count);
            Assert.AreEqual("Art", classes[1].Name);
            Assert.AreEqual("#00FF00", classes[3].Color);
            Assert.AreEqual(1, classes[4].Assignments.Count);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(5)]
        public void GetAllAssignmentsByClassIdShouldWorkCorrectly(int id)
        {
            var assignments = service.GetAllAssignmentsByClassId(id).ToList();

            Assert.AreEqual(id, assignments[0].ClassId);
        }

        [Test]
        public void CreateShouldWorkCorrectly()
        {
            var model = new CreateClassServiceModel()
            {
                Name = "Physics",
                ColorId = 3
            };

            var classesCountBefore = service.GetAll().ToList().Count;
            service.Create(model);
            var classesCountAfter = service.GetAll().ToList().Count;


            Assert.AreEqual(classesCountAfter, classesCountBefore + 1);

            // To Use Same Data -> Can use [SetUp] on Init method, but time to start tests increase.
            service.Remove(classesCountAfter);
        }

        [TestCase("Physics", -1)]
        [TestCase("Physics", 888888)]
        [TestCase("Physics", 0)]
        public void CreateShouldThrowExceptionForInvalidModelColor(string name, int colorId)
        {
            var model = new CreateClassServiceModel()
            {
                Name = name,
                ColorId = colorId
            };

            Assert.Throws<InvalidOperationException>(() => { service.Create(model); });
        }

        [TestCase(1, "Mathematics", "#00FF00")]
        [TestCase(3, "Geography", "#0000FF")]
        [TestCase(5, "Philosophy", "#FFFF00")]
        public void EditShouldWorkCorrectly(int id, string name, string color)
        {
            var model = new EditClassServiceModel()
            {
                Id = id,
                Name = name,
                Color = color
            };

            var oldClassObj = service.GetById(id);
            service.Edit(model);
            var newClassObj = service.GetById(id);

            Assert.AreEqual(name, newClassObj.Name);
            Assert.AreEqual(color, newClassObj.Color);

            // To Use Same Data -> Can use [SetUp] on Init method, but time to start tests increase.
            var oldModel = new EditClassServiceModel()
            {
                Id = oldClassObj.Id,
                Name = oldClassObj.Name,
                Color = oldClassObj.Color
            };

            service.Edit(oldModel);
        }

        [TestCase(1, "Mathematics", "#FFFF0F")]
        [TestCase(3, "Geography", "#F0FF0F")]
        [TestCase(5, "Philosophy", "#000F00")]
        public void EditShouldThrowExceptionForInvalidModelColor(int id, string name, string color)
        {
            var model = new EditClassServiceModel()
            {
                Id = id,
                Name = name,
                Color = color
            };

            Assert.Throws<InvalidOperationException>(() => { service.Edit(model); });
        }



        // Can use it only with [SetUp] on Init method, but time to start tests increase.
        /*
        [TestCase(1)]
        [TestCase(3)]
        [TestCase(5)]
        public void RemoveShouldWorkCorrectly(int id)
        {
            var classObj = this.service.GetById(id);

            var success = service.Remove(id);

            Assert.True(success);

            // To Use Same Data
            // Not working because of ordering in database

            var color = context.Colors.FirstOrDefault(x => x.Name == classObj.Color);

            var model = new CreateClassServiceModel()
            {
                Name = classObj.Name,
                ColorId = color.Id    
            };

            service.Create(model);
        }
        */

        [TestCase(-1)]
        [TestCase(100000000)]
        [TestCase(0)]
        public void RemoveShouldReturnFalseIfClassNotExists(int id)
        {
            var success = service.Remove(id);

            Assert.False(success);
        }


        [TestCase(1, ExpectedResult = "#FF0000")]
        [TestCase(4, ExpectedResult = "#00FF00")]
        [TestCase(2, ExpectedResult = "#0000FF")]
        [TestCase(3, ExpectedResult = "#FFFF00")]
        public string GetColorHexShouldWorkCorrectly(int classId)
        {
            return service.GetColorHex(classId);
        }

        [TestCase(1, ExpectedResult = "Math")]
        [TestCase(4, ExpectedResult = "English")]
        [TestCase(2, ExpectedResult = "Art")]
        public string GetClassNameShouldWorkCorrectly(int classId)
        {
            return service.GetClassName(classId);
        }
    }
}
