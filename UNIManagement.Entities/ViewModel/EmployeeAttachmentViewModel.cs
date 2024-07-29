using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNIManagement.Entities.ViewModel
{
    public class EmployeeAttachmentViewModel
    {
        public int EmployeeAttachmentId { get; set; }
        public int EmployeeId { get; set; }
        public bool IsAdhar {  get; set; }
        public IFormFile AdharCard { get; set; }
        public string AdharNo { get; set; }
        public bool IsPassbook {  get; set; }
        public IFormFile PassBook {  get; set; }
        public string AccountNumber {  get; set; }
        public string BankName {  get; set; }
        public string Ifsc {  get; set; }
        public string Upi {  get; set; }
        public bool IsDegree {  get; set; }
        public IFormFile Degree
        { get; set; }
        public bool IsMarksheetUpload {  get; set; }
        public IFormFile Marksheet {  get; set; }
        public string? OtherDocuments { get; set; }

        public string? Description { get; set; }


    }
}
