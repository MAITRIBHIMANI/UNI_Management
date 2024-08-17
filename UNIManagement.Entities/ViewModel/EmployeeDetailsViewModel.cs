using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations; 
namespace UNIManagement.Entities.ViewModel
{
    public class EmployeeDetailsViewModel
    {
       
        public int? EmployeeId { get; set; }

        [Required(ErrorMessage = "First Name Is Required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Middle Name Is Required")]
        public string? MiddleName { get; set; }

        [Required(ErrorMessage = "Last Name Is Required")]
        public string? LastName { get; set; }

        [Required]
        public string? Employeetype { get; set; }
        [StringLength(10, MinimumLength = 10, ErrorMessage = "The number must be 10 characters long")]
        public string? ContactNumber1 { get; set; }
        [StringLength(10, MinimumLength = 10, ErrorMessage = "The number must be 10 characters long")]

        public string? ContactNumber2 { get; set; }

        [Required]
        public string? Email { get; set; }

        public string? Address { get; set; }
        [Required]
        public string? Country { get; set; }
        [Required]
        public string? State { get; set; }

        public DateTime? Birthdate { get; set; }

        public string? Education { get; set; }

        public string? Photo { get; set; }

        public IFormFile? EmployeeImage { get; set; }
        [Required]
        public string Gender { get; set; }

        public string IsActive { get; set; }

        public DateTime? Joinningdate { get; set; }

        public string IsFresher { get; set; }

        public string? Resume { get; set; }

        public IFormFile? EmployeeResume { get; set; }

        public string? Bond { get; set; }

        [Required]

        public int EmployeeAttachmentId { get; set; }
    
        public bool? IsAdhar { get; set; }

        public IFormFile? AdharCard { get; set; }

        public string? AdharNo { get; set; }

        public bool? IsPassbook { get; set; }

        public IFormFile? PassBook { get; set; }

        public string? AccountNumber { get; set; }

        public string? BankName { get; set; }

        public string? Ifsc { get; set; }

        public string? Upi { get; set; }

        public bool? IsDegree { get; set; }

        public IFormFile? Degree { get; set; }

        public bool? IsMarksheetUpload { get; set; }

        public IFormFile? Marksheet { get; set; }

        public string? OtherDocuments { get; set; }

        public IFormFile? otherdocument { get; set; }

        public string? Description { get; set; }
        public DateTime? Modified {  get; set; }
       
        public string? Password { get; set; }

    }

}
