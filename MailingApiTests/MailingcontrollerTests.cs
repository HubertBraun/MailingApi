using MailingApi.Controllers;
using MailingApi.Layers;
using MailingApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace MailingApiTests
{
    [TestClass]
    public class MailingcontrollerTests
    {
        private static MailingController controller;
        private static BuisnessLayer buisness;

        [ClassInitialize]
        public static void Initialize(TestContext testcontext)
        {
            var options = new DbContextOptionsBuilder<MailingApiContext>().UseInMemoryDatabase(databaseName: "MailDatabase")
                .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)).Options;
            var context = new MailingApiContext(options);
            buisness = new BuisnessLayer(context);
            controller = new MailingController(buisness);
            var user = new BuissnesModelUser()
            {
                Username = "TestUser",
                Password = "Password"
            };
            var userId = buisness.RegisterUser(user);
            var model = new BuissnessModelGroup()
            {
                GroupName = "testName",
                GroupOwnerId = userId,
                OwnerName = "Owner",
                Emails = new List<BuissnessModelEmails>() { new BuissnessModelEmails { Email = "a@a.com" } }
            };
            buisness.SaveBuissnesModelGroup(model);
            var model2 = new BuissnessModelGroup()
            {
                GroupName = "testName2",
                GroupOwnerId = userId,
                OwnerName = "Owner2",
                Emails = new List<BuissnessModelEmails>() { new BuissnessModelEmails { Email = "a@a.com" } }
            };
            buisness.SaveBuissnesModelGroup(model2);
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
            var expectedGroup = buisness.GetBuissnesModel(groupId);
            var actualGroup = (controller.Get(groupId) as OkObjectResult).Value as BuissnessModelGroup;
            var actualEmails = actualGroup.Emails as List<BuissnessModelEmails>;
            var expectedEmails = expectedGroup.Emails as List<BuissnessModelEmails>;
            Assert.AreEqual(actualGroup.Id, expectedGroup.Id);
            Assert.AreEqual(actualGroup.GroupName, expectedGroup.GroupName);
            Assert.AreEqual(actualGroup.OwnerName, expectedGroup.OwnerName);
            Assert.AreEqual(actualGroup.GroupOwnerId, expectedGroup.GroupOwnerId);
            Assert.AreEqual(actualEmails.Count, expectedEmails.Count);
            Assert.AreEqual(actualEmails[0].Id, expectedEmails[0].Id);
            Assert.AreEqual(actualEmails[0].Email, expectedEmails[0].Email);
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
