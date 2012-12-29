using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Capricon.Model
{
    /// <summary>
    /// Represents a User
    /// </summary>
    public class User : Person
    {
        #region Ctor
       
        public User()
        {

        }

        #endregion

        #region Public Fields
        
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        public int UserId { get; set; }

        #endregion

        #region Associations
       
        /// <summary>
        /// Property that shows the messages associated to this <see cref="User"/> 
        /// </summary>
        public virtual IList<UserMessage> UserMessages { get; set; }

        #endregion
    }
}
