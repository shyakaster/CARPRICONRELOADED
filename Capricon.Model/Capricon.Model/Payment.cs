using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Capricon.Model
{
    /// <summary>
    /// Represents a payment
    /// </summary>
    public class Payment
    {
        #region Ctor
       
        public Payment()
        {

        }

        #endregion

        #region Public Properties 
        
        /// <summary>
        /// Primary key
        /// </summary>
        [Key]
        public int PaymentId { get; set; }

        /// <summary>
        /// Property that describes the date of this <see cref="Payment"/>
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Payment date is required")]
        //Todo: make it generate date automatically from system date
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Property that describes the amount of this <see cref="Payment"/>
        /// </summary>
        public double Amount { get; set; }

        #endregion

        #region Associations
       
        /// <summary>
        /// Property that describes the message associated to this <see cref="Payment"/> 
        /// </summary>
        [Required]
        public Message Message { get; set; }

        #endregion
    }
}
