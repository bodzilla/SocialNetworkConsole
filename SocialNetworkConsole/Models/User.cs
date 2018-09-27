using System;
using System.ComponentModel.DataAnnotations;
using SocialNetworkConsole.Models.Interfaces;

namespace SocialNetworkConsole.Models
{
    public class User : IBaseModel
    {
        /// <inheritdoc />
        public int Id { get; set; }

        /// <inheritdoc />
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// User's name.
        /// </summary>
        [StringLength(50, ErrorMessage = "User's name cannot exceed 50 characters.")]
        public string Name { get; set; }
    }
}
