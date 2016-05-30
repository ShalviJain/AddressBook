using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AddressBook.Models
{
    public class ContactValidation : Contact
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Please provide First Name", AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Please provide Phone Numnber", AllowEmptyStrings = false)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone number")]
        public string PhoneNumber 
        {
            get
            {
                return this.phoneNumber;
            }
            set
            {
                if (phoneNumber != null)
                {
                    this.phoneNumber = Regex.Replace(phoneNumber, @"\s", "");
                }
                else
                    this.phoneNumber = value;
            }

        }
        [Display(Name = "Street Name")]
        [Required(ErrorMessage = "Please provide Street Name", AllowEmptyStrings = false)]
        public string StreetName { get; set; }
        [Display(Name = "City")]
        [Required(ErrorMessage = "Please provide City", AllowEmptyStrings = false)]
        public string City { get; set; }
        [Display(Name = "Province")]
        [Required(ErrorMessage = "Please provide Province", AllowEmptyStrings = false)]
        public string Province { get; set; }
        [Display(Name = "Postal Code")]
        [Required(ErrorMessage = "Please provide Postal Code", AllowEmptyStrings = false)]
        public string PostalCode { get; set; }
        [Display(Name = "Country")]
        [Required(ErrorMessage = "Please provide Country", AllowEmptyStrings = false)]
        public string Country { get; set; }

        private string phoneNumber;
    }

}