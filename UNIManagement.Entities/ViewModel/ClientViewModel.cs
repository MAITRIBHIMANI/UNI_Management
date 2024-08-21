using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace UNIManagement.Entities.ViewModel
{
    public class ClientViewModel
    {
        public int? ClientId { get; set; }


        [Required(ErrorMessage = "Please enter your name"), MaxLength(50)]
        public string? Name { get; set; }

        [StringLength(10, MinimumLength = 10, ErrorMessage = "The number must be 10 characters long")]
        
        public string? Number { get; set; }

        [Required]      
         public string? Email { get; set; }

        [Required]
        public DateTime? BirthDate { get; set; }

        [Required]
        public string? Address { get; set; }

        [Required]
        public string? BusinessName { get; set; }

        [Required]
        public string? BusinessNumber { get; set; }

        [Required]
        public string? Category { get; set; }   
      
     
        public string? RefferenceDetails { get; set; }
       
        public string? AdditionInformation { get; set; }      

        public IFormFile? Additioninfo {  get; set; }       

        public bool? IsDeleted { get; set; }

        [Required]
        public string? IsActive { get; set; }
    }

}
