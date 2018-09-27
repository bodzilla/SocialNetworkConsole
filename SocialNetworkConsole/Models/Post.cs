using System;
using System.ComponentModel.DataAnnotations;
using SocialNetworkConsole.Models.Interfaces;

namespace SocialNetworkConsole.Models
{
    public class Post : IBaseModel
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <inheritdoc />
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// The id of the user who posted this.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// The post text content.
        /// </summary>
        ///
        [StringLength(255, ErrorMessage = "User's post cannot exceed 255 characters.")]
        public string Text { get; set; }

        #region Helper Methods

        /// <summary>
        /// Formats DateTime to Time Ago format.
        /// Adapted from: https://dotnetthoughts.net/time-ago-function-for-c
        /// </summary>
        /// <returns></returns>
        public string TimeAgo()
        {
            string result;
            var timeSpan = DateTime.Now.Subtract(DateCreated);

            if (timeSpan <= TimeSpan.FromSeconds(60))
            {
                result = $"{timeSpan.Seconds} seconds ago";
            }
            else if (timeSpan <= TimeSpan.FromMinutes(60))
            {
                result = timeSpan.Minutes > 1 ? $"{timeSpan.Minutes} minutes ago" : "a minute ago";
            }
            else if (timeSpan <= TimeSpan.FromHours(24))
            {
                result = timeSpan.Hours > 1 ? $"{timeSpan.Hours} hours ago" : "an hour ago";
            }
            else if (timeSpan <= TimeSpan.FromDays(30))
            {
                result = timeSpan.Days > 1 ? $"{timeSpan.Days} days ago" : "yesterday";
            }
            else if (timeSpan <= TimeSpan.FromDays(365))
            {
                result = timeSpan.Days > 30 ? $"{timeSpan.Days / 30} months ago" : "a month ago";
            }
            else
            {
                result = timeSpan.Days > 365 ? $"{timeSpan.Days / 365} years ago" : "a year ago";
            }

            return result;
        }

        #endregion
    }
}
