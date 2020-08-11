using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EstateApplication.Web.Models
{
    public class PropertiesModel
    {
        [Required]
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int NumberOfRooms { get; set; }
        [Required]
        public int NumberOfBaths { get; set; }
        [Required]
        public int NumberOfToilets { get; set; }
        public string Address { get; set; }
        [Required]
        public string ContactPhoneNumber { get; set; } 

        [DisplayName("Accept termes and conditions.")]
        [Range(typeof(bool), "true", "true", ErrorMessage = "Please Tick Terms and Condition box")]
        public bool TermsAndConditions { get; set; }
    }
}
