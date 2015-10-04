using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TwitterWebApplication.Controllers;
using System.Web.Mvc;

namespace TwitterWebApplication.Tests.Controllers
{
    /// <summary>
    /// Summary description for HomeControllerTests
    /// </summary>
    [TestClass]
    public class HomeControllerTests
    {
        public HomeControllerTests()
        {
            
        }

        [TestMethod]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void TwitterBackupHomePage()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.TwitterBackupHomePage() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
