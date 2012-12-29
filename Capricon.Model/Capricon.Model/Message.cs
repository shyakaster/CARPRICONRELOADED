using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Capricon.Model
{
    /// <summary>
    /// Represents a message
    /// </summary>
    public class Message
    {
        #region Ctor
        
        public Message()
        {

        }

        #endregion

        #region Public Properties
        
        /// <summary>
        /// Primary Key
        /// </summary>
        [Key]
        public int MessageId { get; set; }

        /// <summary>
        /// Property that describes the body of the <see cref="Message"/>
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Message body is required")]
        public string Body { get; set; }

        /// <summary>
        /// Property that describes date and time sent of the <see cref="Message"/>
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date sent is required")]
        //TODO: Make this be auto generated from System time
        public DateTime Sent { get; set; }

        /// <summary>
        /// Property that describes the status of this <see cref="Message"/>
        /// </summary>
        [Required]
        [CustomValidation(typeof(Message), "MessageStatusValidator")]
        public MessageStatus MessageStatus
        {
            get { return (MessageStatus)MessageStatusTypeValue; }
            set { MessageStatusTypeValue = (int)value; }
        }
        public int MessageStatusTypeValue { get; set; }

        #endregion

        #region Associations
        
        /// <summary>
        /// Property that describes the user messages associated with this <see cref="Message"/>
        /// </summary>
        public IList<UserMessage> UserMessages { get; set; }

        /// <summary>
        /// Property that describes the agent messages associated with this <see cref="Message"/>
        /// </summary>
        public IList<AgentMessage> AgentMessages { get; set; }

        #endregion

        #region Validation Methods

        /// <summary>
        /// Validates the Message Status property
        /// </summary>
        /// <param name="_MessageStatus"></param>
        public static ValidationResult MessageStatusValidator(MessageStatus _MessageStatus)
        {
            if (_MessageStatus != MessageStatus.Not_Specified)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Message status is required");
            }
        }

        #endregion
    }
}
