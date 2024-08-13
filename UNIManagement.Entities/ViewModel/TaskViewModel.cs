using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNIManagement.Entities.ViewModel
{
    public class TaskViewModel
    {
    public int? TaskId { get; set; }
        public string? TokenNumber { get; set; }
        [Required(ErrorMessage ="select project")]
        public int ProjectId { get; set; }
        public string? ProjectName { get; set; }
        public int? ClientId { get; set; }
        public string? ClientName { get; set; }
        [Required(ErrorMessage = "select employee")]
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? Description { get; set; }
        public string? Document { get; set; }
        public IFormFile? TaskDocument { get; set; }
        public DateTime? TaskAssignDate { get; set; }
        public DateTime? DueDate { get; set; }
        public string? Status { get; set; }
        public bool IsDeleted { get; set; }
        public string? IsActive { get; set; }

    }
}
