using MailingApi.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MailingApiTests.Helper
{
    public static class BuissnesTestHelper
    {

        public static BuissnessModelGroup CreateBuissnessModelGroup(int userId, string groupName, IEnumerable<string> emails)
        {
            return new BuissnessModelGroup()
            {
                GroupName = groupName,
                GroupOwnerId = userId,
                Emails = emails.Select(x => new BuissnessModelEmails { Email = x }).ToList(),
            };
        }

        public static BuissnesModelUser CreateBuissnesModelUser(string username, string password)
        {
            return new BuissnesModelUser()
            {
                Username = username,
                Password = password,
            };
        }

        public static void CompareBuissnessModelGroup(BuissnessModelGroup expected, BuissnessModelGroup actual)
        {
            var actualEmails = expected.Emails as List<BuissnessModelEmails>;
            var expectedEmails = actual.Emails as List<BuissnessModelEmails>;
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.GroupName, actual.GroupName);
            Assert.AreEqual(expected.GroupOwnerId, actual.GroupOwnerId);
            Assert.AreEqual(expectedEmails.Count, actualEmails.Count);
            Assert.AreEqual(expectedEmails[0].Email, actualEmails[0].Email);
        }
    }
}
