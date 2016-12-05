using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserStories.Web;
using UserStories.Web.Controllers;
using UserStories.Business.Managers;

namespace UserStories.Web.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {       
        [TestMethod]
        public void Stories()
        {
            // Arrange
            StoryController controller = new StoryController();

            // Act
            ViewResult result = controller.Stories() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Contact()
        {
            // Arrange
            GroupController controller = new GroupController();

            // Act
            ViewResult result = controller.Groups() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Delete()
        {

        }
    }
}
