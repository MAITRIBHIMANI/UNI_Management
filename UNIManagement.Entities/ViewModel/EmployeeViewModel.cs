using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNIManagement.Entities.ViewModel
{
	public class EmployeeViewModel
	{
		public int EmployeeId { get; set; }
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Employeetype { get; set; }
		public string? ContactNumber1 { get; set; }
		public string? ContactNumber2 { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public string? State { get; set; }
        public DateTime? Birthdate { get; set; }
		public string? Education {  get; set; }
        public string? Photo { get; set; }
        public IFormFile EmployeeImage { get; set; }
        public string? Gender { get; set; }
        public string IsActive { get; set; }
        public DateTime? Joinningdate { get; set; }
        public string IsFresher { get; set; }
        public string? Resume {  get; set; }
		public IFormFile EmployeeResume {  get; set; }
        public string? Bond { get; set; }


        

    }
}
