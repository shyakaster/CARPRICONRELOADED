using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Capricon.Model
{
    /// <summary>
    /// Represents an agent
    /// </summary>
    public class Agent : Person
    {
        #region Ctor
        
        public Agent()
        {

        }

        #endregion

        #region Public Properties
        
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int AgentId { get; set; }

        #endregion

        #region Associations
        
        /// <summary>
        /// Property that describes messages associated with this <see cref="Agent"/>
        /// </summary>
        public IList<AgentMessage> AgentMessages { get; set; }

        #endregion
    }
}
