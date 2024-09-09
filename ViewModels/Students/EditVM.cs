using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem.ViewModels.Students
{
    public class EditVM
    {
        public int Id { get; set; }

        [Display(Name = "First Name: ")]
        [Required(ErrorMessage = "This field is required.")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name: ")]
        [Required(ErrorMessage = "This field is required.")]
        public string LastName { get; set; }

        [Display(Name = "Specialty: ")]
        [Required(ErrorMessage = "This field is required.")]
        public string Specialty { get; set; }

        [Display(Name = "Year: ")]
        [Required(ErrorMessage = "This field is required.")]
        public int Year { get; set; }

        [Display(Name = "Username: ")]
        [Required(ErrorMessage = "This field is required.")]
        public string Username { get; set; }

        [Display(Name = "Password: ")]
        [Required(ErrorMessage = "This field is required.")]
        public string Password { get; set; }
    }
}
