using System;
using System.Configuration;
using NUnit.Framework;
using SocialNetworkConsole.DataAccess;
using SocialNetworkConsole.Services;

namespace SocialNetworkConsole.UnitTests
{
    [TestFixture]
    public class Tests
    {
        [Test, Order(1)]
        public void ConnectsToDb()
        {
            // Arrange.
            string dbConnectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
            int timeoutSeconds = int.Parse(ConfigurationManager.AppSettings["TimeoutSeconds"]);
            DbConnection dbConnection = new DbConnection(dbConnectionString, timeoutSeconds);
            SocialNetworkService socialNetworkService = new SocialNetworkService(dbConnection);

            // Act and assert.
            Assert.DoesNotThrow(() => { socialNetworkService.CheckService(); });
        }

        [Test, Order(2)]
        public void GetsUserId()
        {
            // Arrange.
            string dbConnectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
            int timeoutSeconds = int.Parse(ConfigurationManager.AppSettings["TimeoutSeconds"]);
            DbConnection dbConnection = new DbConnection(dbConnectionString, timeoutSeconds);
            SocialNetworkService socialNetworkService = new SocialNetworkService(dbConnection);

            // Act and assert.
            Assert.DoesNotThrow(() =>
            {
                var id = socialNetworkService.GetUserId("Bodrul");
                if (id != 1) throw new ArgumentException();
            });
        }

        [Test, Order(3)]
        public void GetsUsersTimeline()
        {
            // Arrange.
            string dbConnectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
            int timeoutSeconds = int.Parse(ConfigurationManager.AppSettings["TimeoutSeconds"]);
            DbConnection dbConnection = new DbConnection(dbConnectionString, timeoutSeconds);
            SocialNetworkService socialNetworkService = new SocialNetworkService(dbConnection);

            // Act and assert.
            Assert.DoesNotThrow(() =>
            {
                var timeline = socialNetworkService.GetUserTimeline("Bodrul");
                if (timeline.Count < 1) throw new ArgumentException();
            });
        }

        [Test, Order(4)]
        public void GetUsersSubscriptions()
        {
            // Arrange.
            string dbConnectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
            int timeoutSeconds = int.Parse(ConfigurationManager.AppSettings["TimeoutSeconds"]);
            DbConnection dbConnection = new DbConnection(dbConnectionString, timeoutSeconds);
            SocialNetworkService socialNetworkService = new SocialNetworkService(dbConnection);

            // Act and assert.
            Assert.DoesNotThrow(() =>
            {
                var subscriptionUserNames = socialNetworkService.GetSubscriptionUserNames("Bodrul");
                if (subscriptionUserNames.Count < 1) throw new ArgumentException();
            });
        }

        [Test, Order(5)]
        public void PostTextWithUser()
        {
            // Arrange.
            string dbConnectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
            int timeoutSeconds = int.Parse(ConfigurationManager.AppSettings["TimeoutSeconds"]);
            DbConnection dbConnection = new DbConnection(dbConnectionString, timeoutSeconds);
            SocialNetworkService socialNetworkService = new SocialNetworkService(dbConnection);

            // Act and assert.
            Assert.DoesNotThrow(() => { socialNetworkService.PostToTimeline("Bodrul", "Unit testing this functionality."); });
        }

        [Test, Order(6)]
        public void GetsUsersWall()
        {
            // Arrange.
            string dbConnectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
            int timeoutSeconds = int.Parse(ConfigurationManager.AppSettings["TimeoutSeconds"]);
            DbConnection dbConnection = new DbConnection(dbConnectionString, timeoutSeconds);
            SocialNetworkService socialNetworkService = new SocialNetworkService(dbConnection);

            // Act and assert.
            Assert.DoesNotThrow(() =>
            {
                var timeline = socialNetworkService.GetUserWall("Bodrul");
                if (timeline.Count < 1) throw new ArgumentException();
            });
        }

        [Test, Order(7)]
        public void MakeUserFollowAnotherUser()
        {
            // Arrange.
            string dbConnectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
            int timeoutSeconds = int.Parse(ConfigurationManager.AppSettings["TimeoutSeconds"]);
            DbConnection dbConnection = new DbConnection(dbConnectionString, timeoutSeconds);
            SocialNetworkService socialNetworkService = new SocialNetworkService(dbConnection);

            // Act and assert.
            Assert.DoesNotThrow(() => { socialNetworkService.FollowUser("Bodrul", "Harry"); });
        }
    }
}
