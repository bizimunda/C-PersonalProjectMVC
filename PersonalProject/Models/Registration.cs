using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PersonalProject.Models
{
    public class Registration
    {
        public int Id { get; set; }

        [Required(ErrorMessage="Please enter user name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please enter your First name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter your Last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter your Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your Adress")]
        public string Adress { get; set; }

        [Required(ErrorMessage = "Please enter your Phone number")]
        public string Phone { get; set; }

        
        public string Phone1 { get; set; }
        
        
        public int Status { get; set; }

        
    }
}