using MailingApi.Controllers;
using MailingApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MailingApiTests
{
    [TestClass]
    public class MailingcontrollerTests
    {
        private static MailingController controller;
        private static MailGroupContext context;

        [ClassInitialize]
        public static void Initialize(TestContext testcontext)
        {
            var options = new DbContextOptionsBuilder<MailGroupContext>().UseInMemoryDatabase(databaseName: "MailDatabase").Options;
            context = new MailGroupContext(options);
            controller = new MailingController(context);
        }
        [DataRow(-1)]
        [DataRow(10)]
        [TestMethod]
        public void GetShoulReturnNotFound(int groupId)
        {
            var expected = new NotFoundResult();
            var actualGroup = controller.Get(groupId) as NotFoundResult;
            Assert.AreEqual(expected.GetType(), actualGroup.GetType());
            Assert.AreEqual(expected.StatusCode, actualGroup.StatusCode);
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public void GetShoulReturnOK(int groupId)
        {
            var expectedGroup = context.Groups.Find(groupId);
            var actualGroup = controller.Get(groupId);
            Assert.AreEqual(actualGroup, expectedGroup);
        }

        [TestMethod]
        public void PostShoulReturnConfilct()
        {
        }

        [TestMethod]
        public void PostShoulReturnCreated()
        {
        }

        [TestMethod]
        public void PutShoulReturnCreated()
        {
        }

        [TestMethod]
        public void PutShoulReturnNotFound()
        {
        }

        [TestMethod]
        public void PutShoulReturnOK()
        {
        }

        [TestMethod]
        public void DeleteShoulReturnNotFound()
        {
        }

        [TestMethod]
        public void DeleteShoulReturnOK()
        {
        }
    }
}
