using MailingApi.Context;
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
        private static MailingApiContext context;

        [ClassInitialize]
        public static void Initialize(TestContext testcontext)
        {
            var options = new DbContextOptionsBuilder<MailingApiContext>().UseInMemoryDatabase(databaseName: "MailDatabase").Options;
            context = new MailingApiContext(options);
            controller = new MailingController(context);

            var consumer = new MailConsumer
            {
                ConsumerAddress = "testAddress",
            };
            var owner = new MailUser
            {
                Username = "testName1"
            };
            var owner2 = new MailUser
            {
                Username = "testName2"
            };
            var group = new MailingGroup
            {
                Name = "testName1",
                GroupOwnerId = 1,
            };
            var group2 = new MailingGroup
            {
                Name = "testName2",
                GroupOwnerId = 2,
            };
            context.Add(consumer);
            context.Add(group);
            context.Add(group2);
            context.Add(owner);
            context.Add(owner2);
            context.SaveChanges();
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
            var expectedGroup = context.GetBuissnesModelGroup(groupId);
            var actualGroup = (controller.Get(groupId) as OkObjectResult).Value as BuissnessModelGroup;
            Assert.AreEqual(actualGroup.GroupId, expectedGroup.GroupId);
            Assert.AreEqual(actualGroup.GroupName, expectedGroup.GroupName);
            Assert.AreEqual(actualGroup.OwnerName, expectedGroup.OwnerName);
            Assert.AreEqual(actualGroup.OwnerId, expectedGroup.OwnerId);
            Assert.AreEqual(actualGroup.Emails.Count, expectedGroup.Emails.Count);
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
