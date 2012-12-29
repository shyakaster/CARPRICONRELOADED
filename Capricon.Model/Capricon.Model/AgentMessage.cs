using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Capricon.Model
{
    /// <summary>
    /// Represents an agent message
    /// </summary>
    public class AgentMessage
    {
        #region Ctor
        
        public AgentMessage()
        {

        }

        #endregion

        #region Public Properties
        
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        public int Id { get; set; }

        #endregion

        #region Associations
        
        /// <summary>
        /// Property that describes the agent associated to this <see cref="AgentMessage"/>
        /// </summary>
        [Required]
        public Agent Agent { get; set; }

        /// <summary>
        /// Property that describes the message associated to this <see cref="Message"/>
        /// </summary>
        [Required]
        public Message Message { get; set; }

        #endregion
    }
}
