using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalCollege.Frontend.Models
{
    public class AppointmentViewModel
    {
        [Required(ErrorMessage = "Your name is required.")]
        [StringLength(200)]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Your email address is required.")]
        [StringLength(200)]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Your phone number is required.")]
        [StringLength(15)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string MobileNumber { get; set; }
        [Required(ErrorMessage = "Date for appointment is required.")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Your purpose for visit is required.")]
        [StringLength(200)]
        public string PurposeofVisit { get; set; }
        [Required(ErrorMessage = "Number of visitors is required.")]
        [Range(1, 6)]
        public int NumberofVisitors { get; set; }
        [StringLength(200)]
        public string ShortDescription { get; set; }
    }
}
