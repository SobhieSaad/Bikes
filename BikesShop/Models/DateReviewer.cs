using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BikesShop.Models
{
    public class DateReviewer
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="Please Fill start date Field")]
        [DataType(DataType.Date)]
        [Display(Name ="Start Date")]
        [DisplayFormat(DataFormatString ="{0:yyyy-MM-dd}",ApplyFormatInEditMode =true)]
        public Nullable<System.DateTime> StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> EndDate { get; set; }

   

    }
}