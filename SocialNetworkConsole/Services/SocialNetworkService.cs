﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using SocialNetworkConsole.DataAccess;
using SocialNetworkConsole.Models;

namespace SocialNetworkConsole.Services
{
    /// <summary>
    /// Contains business logic for all user interactions with the Social Network.
    /// </summary>
    public class SocialNetworkService
    {
        private readonly DbConnection _dbConnection;

        /// <summary>
        /// Constructor takes a DbConnection object.
        /// </summary>
        public SocialNetworkService(DbConnection dbConnection) => _dbConnection = dbConnection;

        /// <summary>
        /// Tries to establish a connection to the database. Will throw a SqlException if not successful.
        /// </summary>
        public void CheckService() => _dbConnection.CheckConnection();

        /// <summary>
        /// Gets the id of the User given the User's name.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>User's id.</returns>
        public int GetUserId(string userName)
        {
            // Construct and execute query.
            string query = $"select top 1 id from [user] where [user].name = '{userName}';";
            DataSet result = _dbConnection.ExecuteQuery(query);

            // Get User's id.
            int userId = int.Parse(result.Tables[0].Rows[0]["id"].ToString());
            return userId;
        }

        /// <summary>
        /// Gets User's Timeline.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IList<Post> GetUserTimeline(string userName)
        {
            IList<Post> timeline = new List<Post>();

            // Construct query.
            string query =
                "select post.id, post.datecreated, post.text, post.userid " +
                "from post inner join [user] on [user].id = post.userid " +
                $"where [user].name = '{userName}' " +
                "order by datecreated desc;";

            // Execute query.
            DataSet result = _dbConnection.ExecuteQuery(query);

            // Cast results to list.
            foreach (DataRow row in result.Tables[0].Rows)
            {
                Post post = new Post
                {
                    Id = int.Parse(row["id"].ToString()),
                    DateCreated = DateTime.Parse(row["datecreated"].ToString()),
                    Text = row["text"].ToString(),
                    UserId = int.Parse(row["userid"].ToString())
                };
                timeline.Add(post);
            }

            return timeline;
        }

        /// <summary>
        /// Gets User's Wall.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>Dictionary containing the post and the associated user.</returns>
        public IDictionary<Post, string> GetUserWall(string userName)
        {
            IDictionary<Post, string> wall = new Dictionary<Post, string>();

            // First get User's own Timeline.
            IList<Post> userTimeline = GetUserTimeline(userName);
            foreach (Post userPost in userTimeline) wall.Add(userPost, userName);

            // Get all User's subscriptions.
            IList<string> subscriptionUsernames = GetSubscriptionUserNames(userName);

            // Get all the subscription's Timelines.
            foreach (string subscriptionUserName in subscriptionUsernames)
            {
                IList<Post> subscriptionTimeline = GetUserTimeline(subscriptionUserName);
                foreach (Post subscriptionPost in subscriptionTimeline) wall.Add(subscriptionPost, subscriptionUserName);
            }

            // Sort wall by date descending order.
            return wall.OrderByDescending(x => x.Key.DateCreated).ToDictionary(x => x.Key, x => x.Value);
        }

        /// <summary>
        /// Gets the Usernames for all the the User's subscriptions.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        public IList<string> GetSubscriptionUserNames(string userName)
        {
            IList<string> subscribedUserNames = new List<string>();

            // First, get the User's id.
            int userId = GetUserId(userName);

            // Construct query.
            string query =
                "select [user].name from dbo.[user] " +
                "inner join follow on follow.followinguserid = [user].id " +
                $"where follow.followeruserid = {userId};";

            // Execute query.
            DataSet result = _dbConnection.ExecuteQuery(query);

            // Cast results to list.
            foreach (DataRow row in result.Tables[0].Rows)
            {
                string name = row["name"].ToString();
                subscribedUserNames.Add(name);
            }

            return subscribedUserNames;
        }

        /// <summary>
        /// Inserts Post to a User's Timeline.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="text"></param>
        public void PostToTimeline(string userName, string text)
        {
            // First, get the User's id.
            int userId = GetUserId(userName);

            // Construct non query.
            string nonQuery =
                "insert into dbo.post (datecreated, userid, text) " +
                $"values ('{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}', {userId}, '{text}');";

            // Execute non query.
            _dbConnection.ExecuteNonQuery(nonQuery);
        }

        /// <summary>
        /// Make a User follow another User.
        /// </summary>
        /// <param name="followerUserName"></param>
        /// <param name="followingUserName"></param>
        public void FollowUser(string followerUserName, string followingUserName)
        {
            // First, get the ids for both Users.
            int followerUserId = GetUserId(followerUserName);
            int followingUserId = GetUserId(followingUserName);

            // Construct non query.
            string nonQuery =
                "insert into dbo.follow (datecreated, followeruserid, followinguserid) " +
                $"values ('{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}', {followerUserId}, {followingUserId});";

            // Execute non query.
            _dbConnection.ExecuteNonQuery(nonQuery);
        }
    }
}
