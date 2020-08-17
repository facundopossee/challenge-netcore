using Microsoft.VisualStudio.TestTools.UnitTesting;
using challenge_netcore.Models;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System;
using System.Net;
using Microsoft.AspNetCore.Routing;

namespace challenge_netcore.Controllers.Tests
{
    [TestClass()]
    public class VehiclesControllerTests
    {
        public VehiclesController Setup()
        {
            var list = new List<Vehicle>
                {
                    new Vehicle { ID = 1, LicensePlate = "123", Brand = "Jeep", Model = "Renegade", Doors = 5, Owner = "Pedrito" },
                    new Vehicle { ID = 2, LicensePlate = "3245", Brand = "Jeep", Model = "Compass", Doors = 5, Owner = "Pedrito2" },
                    new Vehicle { ID = 3, LicensePlate = "4567", Brand = "Jeep", Model = "Wrangler", Doors = 3, Owner = "Pedrito3" },
            }.AsQueryable();
            var set = new Mock<DbSet<Vehicle>>();
            set.As<IQueryable<Vehicle>>().Setup(m => m.Provider).Returns(list.Provider);
            set.As<IQueryable<Vehicle>>().Setup(m => m.Expression).Returns(list.Expression);
            set.As<IQueryable<Vehicle>>().Setup(m => m.ElementType).Returns(list.ElementType);
            set.As<IQueryable<Vehicle>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());

            var context = new Mock<ApplicationDbContext>();
            context.Setup(c => c.Vehicles).Returns(set.Object);
            var controller = new VehiclesController(context.Object);

            return controller;
        }

        [TestMethod()]
        public void IndexTest_ReturnViewnWithModelAnd3Vehicles()
        {
            string expectedView = "Index";
            var controller = Setup();

            var result = controller.Index() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(result.ViewName, expectedView);
            Assert.AreEqual(3, (result.Model as List<Vehicle>).Count());
        }

        [TestMethod()]
        public void DetailTest_ReturnDetailViewWithModel()
        {
            string expectedView = "Details";
            var controller = Setup();

            var result = (controller.Details(1)) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
            Assert.AreEqual(result.ViewName, expectedView);
            Assert.IsInstanceOfType(result.Model, typeof(Vehicle));
            Assert.AreEqual(1, (result.Model as Vehicle).ID);
        }

        [TestMethod]
        public void DetailsTest_ReturnNotFoundResultWithNullID()
        {
            var controller = Setup();

            var result = controller.Details(null) as NotFoundResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public void DetailsTest_ReturnNotFoundResultWithFakeID()
        {
            var controller = Setup();

            var result = controller.Details(6) as NotFoundResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }


        [TestMethod()]
        public async Task CreateTest_ReturnToCreateView()
        {
            var expectedView = "Create";
            var controller = Setup();

            var actionResult = await controller.Create() as ViewResult;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(ViewResult));
            Assert.AreEqual(actionResult.ViewName, expectedView);
        }


        [TestMethod()]
        public void CreateTest_ReturnNotFoundWithDuplicatedID()
        {
            var controller = Setup();
            var vehicle = new Vehicle { LicensePlate = "4545", Brand = null, Model = "TT", Doors = 3, Owner = "Juan" };

            var result = controller.Create(vehicle);

            Assert.IsNotNull(result);
        }

        [TestMethod()]
        public void DeleteTest_ReturnNotFoundVehicle()
        {
            var controller = Setup();

            var result = controller.Delete(8);
            StatusCodeResult statusCodeResult = result as StatusCodeResult;

            Assert.AreEqual<HttpStatusCode>(HttpStatusCode.NotFound, (HttpStatusCode)statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public void DeleteTest_ReturnNotFoundWithNullVehicle()
        {
            var controller = Setup();

            var result = controller.Delete(null);
            StatusCodeResult statusCodeResult = result as StatusCodeResult;

            Assert.AreEqual<HttpStatusCode>(HttpStatusCode.NotFound, (HttpStatusCode)statusCodeResult.StatusCode);
        }

        [TestMethod()]
        public void DeleteTest_ReturnRedirect()
        {
            var controller = Setup();

            var result = controller.Delete(1) as ViewResult;

            Assert.AreEqual(result.ViewName, "Delete");
        }

        [TestMethod()]
        public void DeleteConfirmTest_ReturnRedirect()
        {
            var expectedView = "Index";
            var controller = Setup();

            var result = controller.DeleteConfirmed(1) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));
            Assert.AreEqual(result.ActionName, expectedView);
        }

        [TestMethod()]
        public void DeleteConfirmTest_ReturnNotFound()
        {
            var controller = Setup();

            var actionResult = controller.DeleteConfirmed(10) as NotFoundResult;
            // Assert
            Assert.IsNotNull(actionResult);
            Assert.IsInstanceOfType(actionResult, typeof(NotFoundResult));
        }

        [TestMethod()]
        public void GetOwnersTest_Return12Names() //APIQuantityNames3Pages
        {
            var context = new Mock<ApplicationDbContext>();
            var obj = new VehiclesController(context.Object);

            var result = obj.GetOwners();

            Assert.AreEqual(result.Result.Count(), 12);
        }
    }
}