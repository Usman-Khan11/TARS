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

    public partial class tbl_center
    {
        public int id { get; set; }

        [Required(ErrorMessage = "Center City Required")]
        [Display(Name = "Center City")]
        [MinLength(6, ErrorMessage = "Min 6 Character Required"), MaxLength(25, ErrorMessage = "Max 25 Character allow")]
        public string city { get; set; }


        [Required(ErrorMessage = "Center Address Required")]
        [Display(Name = "Center Address")]
        [MinLength(10, ErrorMessage = "Min 10 Character Required"), MaxLength(50, ErrorMessage = "Max 50 Character allow")]
        public string address { get; set; }


        [Required(ErrorMessage = "Center Pincode Required")]
        [Display(Name = "Center Pincode")]
        public Nullable<int> pincode { get; set; }


        [Required(ErrorMessage = "Center Contact Required")]
        [Display(Name = "Center Contact")]
        [MinLength(11, ErrorMessage = "Min 11 Character Required"), MaxLength(15, ErrorMessage = "Max 15 Character allow")]
        public string contact { get; set; }


        [Required(ErrorMessage = "Center Area Required")]
        [Display(Name = "Center Area")]
        [MinLength(4, ErrorMessage = "Min 4 Character Required"), MaxLength(20, ErrorMessage = "Max 20 Character allow")]
        public string area { get; set; }
    }
}
