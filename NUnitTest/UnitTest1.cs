using challenge_netcore.Controllers;
using challenge_netcore.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NUnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        //[Test]
        //public void TestIndexCount()
        //{
        //    var context = new Mock<ApplicationDbContext>();
        //    var obj = new VehiclesController(context.Object);

        //    var resultTask = obj.Index();
        //    resultTask.Wait();
        //    var result = resultTask.Result;
        //    var model = (List<Vehicle>)((ViewResult)result).Model;

        //    //Assert
        //    Assert.IsNotNull(result);
        //    Assert.AreEqual(2, model.Count());

        //    }

            //[Test]
            //public void TestCreate()
            //{

            //    var set = new Mock<DbSet<Vehicle>>();
            //    var context = new Mock<ApplicationDbContext>();
            //    context.Setup(x => x.Vehicles).Returns(set.Object);

            //    var controller = new VehiclesController(context.Object);

            //    var result = controller.Index();

            //    mockSet.Verify(m => m.Add(It.IsAny<Blog>()), Times.Once());
            //    mockContext.Verify(m => m.SaveChanges(), Times.Once());

            //    Assert.IsNotNull(result.Result);

            //}

            //[Test]
            //public void TestOwnersQuantity()
            //{
            //    var context = new Mock<ApplicationDbContext>();
            //    var obj = new VehiclesController(context.Object);

            //    var result = obj.GetOwners();

            //    Assert.AreEqual(result.Result.Count(), 12);

            //}
        }
}