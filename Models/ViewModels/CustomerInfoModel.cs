using ECommerce.Project.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Project.Models
{
    public class CustomerInfoModel
    {
        public long CustomerInfoID { get; set; }
        [Required(ErrorMessage ="Please enter your fullname.")]
        [MaxLength(100, ErrorMessage ="Full name not exceed 100 characters.")]
        public string FullName { get; set; }
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your address.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Please enter your phone number.")]
        public string PhoneNumber { get; set; }
        [Required]
        public int? ProvinceID { get; set; }
        [Required]
        public int? DistrictID { get; set; }
        [Required]
        public int? WardID { get; set; }

        public string Avatar { get; set; }

        public string Note { get; set; }

        public double Total { get; set; }

        public double ShipFee { get; set; }


    }
}
