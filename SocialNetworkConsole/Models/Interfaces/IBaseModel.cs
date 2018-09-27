using System;

namespace SocialNetworkConsole.Models.Interfaces
{
    /// <summary>
    /// Base class for all models.
    /// </summary>
    public interface IBaseModel
    {
        /// <summary>
        /// Unique identifier.
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Datetime of creation.
        /// </summary>
        DateTime DateCreated { get; set; }
    }
}
