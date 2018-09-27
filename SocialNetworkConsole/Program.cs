using System;
using System.Collections.Generic;
using System.Configuration;
using SocialNetworkConsole.DataAccess;
using SocialNetworkConsole.Models;
using SocialNetworkConsole.Services;

namespace SocialNetworkConsole
{
    public class Program
    {
        private static SocialNetworkService _socialNetworkService;
        private static void Main()
        {
            // First, initialise all the settings and services.
            string dbConnectionString = ConfigurationManager.ConnectionStrings["DbConnectionString"].ConnectionString;
            int timeoutSeconds = int.Parse(ConfigurationManager.AppSettings["TimeoutSeconds"]);
            DbConnection dbConnection = new DbConnection(dbConnectionString, timeoutSeconds);

            // Make sure we can connect to the database.
            _socialNetworkService = new SocialNetworkService(dbConnection);
            _socialNetworkService.CheckService();

            // Now handle the user's request(s).
            PostToTimeline("Bodrul", "Posting this from the Social Network Console!");
            ReadTimeline("Bodrul");
            Console.Read();
        }

        /// <summary>
        /// Post text to a User's Timeline.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="text"></param>
        private static void PostToTimeline(string userName, string text) => _socialNetworkService.PostToTimeline(userName, text);

        /// <summary>
        /// Read a User's Timeline.
        /// </summary>
        /// <param name="userName"></param>
        private static void ReadTimeline(string userName)
        {
            IList<Post> timeline = _socialNetworkService.GetUserTimeline(userName);
            foreach (Post post in timeline) Console.WriteLine($"{post.Text} ({post.TimeAgo()})");
        }
    }
}
