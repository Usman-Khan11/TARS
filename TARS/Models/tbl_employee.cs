//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TARS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    public partial class tbl_employee
    {
        public int e_id { get; set; }



        [Required(ErrorMessage = "Employee Name Required")]
        [Display(Name = "Employee Name")]
        [MinLength(4, ErrorMessage = "Min 4 Character Required"), MaxLength(15, ErrorMessage = "Max 15 Character allow")]
        public string e_name { get; set; }


        [Required(ErrorMessage = "Employee Email Required")]
        [Display(Name = "Employee Email")]
        [MinLength(10, ErrorMessage = "Min 10 Character Required"), MaxLength(50, ErrorMessage = "Max 50 Character allow")]
        public string e_email { get; set; }



        [Required(ErrorMessage = "Employee Password Required")]
        [Display(Name = "Employee Password")]
        [MinLength(8, ErrorMessage = "Min 8 Character Required"), MaxLength(16, ErrorMessage = "Max 16 Character allow")]
        public string e_password { get; set; }



        [Required(ErrorMessage = "Employee Address Required")]
        [Display(Name = "Employee Address")]
        [MinLength(10, ErrorMessage = "Min 10 Character Required"), MaxLength(50, ErrorMessage = "Max 50 Character allow")]
        public string e_address { get; set; }


        [Required(ErrorMessage = "Employee Contact Required")]
        [Display(Name = "Employee Contact")]
        [MinLength(11, ErrorMessage = "Min 11 Character Required"), MaxLength(13, ErrorMessage = "Max 13 Character allow")]
        public string e_contact { get; set; }


        [Display(Name = "Employee Picture")]
        public string e_picture { get; set; }



        [Required(ErrorMessage = "Employee City Required")]
        [Display(Name = "Employee City")]
        [MinLength(6, ErrorMessage = "Min 6 Character Required"), MaxLength(20, ErrorMessage = "Max 20 Character allow")]
        public string city { get; set; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }

        public HttpPostedFileBase ImageFile { get; set; }


    }
}