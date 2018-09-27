using System;
using SocialNetworkConsole.Models.Interfaces;

namespace SocialNetworkConsole.Models
{
    public class Follow : IBaseModel
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <inheritdoc />
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// The user id who is doing the following.
        /// </summary>
        public int FollowerUserId { get; set; }

        /// <summary>
        /// The user id who is being followed.
        /// </summary>
        public int FollowingUserId { get; set; }
    }
}
