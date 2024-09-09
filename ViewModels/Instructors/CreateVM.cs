using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.ViewModels.Instructors
{
    public class CreateVM
    {
        [Display(Name = "First Name: ")]
        [Required(ErrorMessage = "This field is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name: ")]
        [Required(ErrorMessage = "This field is required.")]
        public string LastName { get; set; }

        [Display(Name = "Rank: ")]
        [Required(ErrorMessage = "This field is required.")]
        public string Rank { get; set; }

        [Display(Name = "Work Experience: ")]
        [Required(ErrorMessage = "This field is required.")]
        [Range(0, 40, ErrorMessage = "Please enter a valid value.")]
        public int WorkExperience { get; set; }

    }
}
