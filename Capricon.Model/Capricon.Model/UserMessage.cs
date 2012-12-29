using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Capricon.Model
{
    /// <summary>
    /// Represents a User Message
    /// </summary>
    public class UserMessage
    {
        #region Ctor

        public UserMessage()
        {

        }

        #endregion

        #region Public Fields
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int Id { get; set; }

        #endregion

        #region Associations

        /// <summary>
        /// Property that describes the users associated with this <see cref="UserMessage"/>
        /// </summary>
        [Required]
        public virtual User User { get; set; }

        /// <summary>
        /// Property that describes the messages associated with this <see cref="UserMessage"/>
        /// </summary>
        [Required]
        public virtual Message Message { get; set; }

        #endregion
        
    }
}
