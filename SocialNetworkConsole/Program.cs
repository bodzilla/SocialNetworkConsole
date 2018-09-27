using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
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
            ShowMenu();
        }

        /// <summary>
        /// Shows menu and actions a request.
        /// </summary>
        private static void ShowMenu()
        {
            // Get command.
            string input = Console.ReadLine();
            string[] inputArray = input?.Split(' ').ToArray();

            // Process command.
            if (inputArray != null)
            {
                if (input.Contains("->"))
                {
                    // They want to post something to a User's Timeline.
                    string[] splitString = input.Split(new[] { " -> " }, StringSplitOptions.None);
                    string userName = splitString[0];
                    string text = splitString[1];
                    PostToTimeline(userName, text);
                }
                else
                {
                    switch (inputArray.Length)
                    {
                        case 1:
                            if (inputArray[0].Equals("exit")) Environment.Exit(0);
                            // Otherwise, they want to read a User's Timeline.
                            ReadTimeline(inputArray[0]);
                            break;
                        case 2:
                            // They want to read a User's wall, including their subscriptions.
                            ReadUserWall(inputArray[0]);
                            break;
                        case 3:
                            // They want make a User Follow another User.
                            FollowUser(inputArray[0], inputArray[2]);
                            break;
                    }
                }
            }
            ShowMenu();
        }

        /// <summary>
        /// Read a User's Wall. This includes User's posts, as well as their subscriptions' posts.
        /// </summary>
        /// <param name="userName"></param>
        private static void ReadUserWall(string userName)
        {
            IDictionary<Post, string> wall = _socialNetworkService.GetUserWall(userName);
            foreach (KeyValuePair<Post, string> keyValuePair in wall)
            {
                Console.WriteLine($"{keyValuePair.Value} - {keyValuePair.Key.Text} ({keyValuePair.Key.TimeAgo()})");
            }
        }

        /// <summary>
        /// Make a User follow another User.
        /// </summary>
        /// <param name="followerUserName"></param>
        /// <param name="followingUserName"></param>
        private static void FollowUser(string followerUserName, string followingUserName) =>
            _socialNetworkService.FollowUser(followerUserName, followingUserName);

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

        /// <summary>
        /// Splits a string using another string.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="seperator"></param>
        /// <returns></returns>
        private static string[] Split(string value, string seperator) => value.Split(new string[] { seperator }, StringSplitOptions.None);
    }
}
