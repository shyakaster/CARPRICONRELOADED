using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Capricon.Model
{
    public abstract class Person
    {
        #region Ctor
        
        public Person()
        {
        
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Property that describes the first name of this <see cref="Person"/>
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        /// <summary>
        /// Property that describes the last name of this <see cref="Person"/>
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        /// <summary>
        /// Property that describes the other name of this <see cref="Person"/>
        /// </summary>
        public string OtherName { get; set; }

        /// <summary>
        /// Property that describes the gender of this <see cref="Person"/>
        /// </summary>
        [Required]
        [CustomValidation(typeof(Person), "GenderValidator")]
        public Gender Gender
        {
            get { return (Gender)GenderTypeValue; }
            set { GenderTypeValue = (int)value; }
        }
        public int GenderTypeValue { get; set; }

        /// <summary>
        /// Property that describes the religion of this <see cref="Person"/>
        /// </summary>
        public string Religion { get; set; }

        /// <summary>
        /// Property that describes the MobilePhone of this <see cref="Person"/>
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// Property that describes the _Email of this <see cref="Person"/>
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// property that describes the date of birth of this <see cref="Person"/>
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Property that describes the town of this <see cref=" cref="Person"/>
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "Town is required")]
        public string Town { get; set; }

        /// <summary>
        /// Property that describes the District of this <see cref="Person"/>
        /// </summary>
        [Required(AllowEmptyStrings = false, ErrorMessage = "District is required")]
        public string District { get; set; }

        #endregion

        #region Validation Methods

        /// <summary>
        /// Validates the Gender property
        /// </summary>
        /// <param name="_Gender"></param>
        public static ValidationResult GenderValidator(Gender _Gender)
        {
            if (_Gender != Gender.Not_Specified)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult("Gender is required");
            }
        }

        #endregion
    }
}



