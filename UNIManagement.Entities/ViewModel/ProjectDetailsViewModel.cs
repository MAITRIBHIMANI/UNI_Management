using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNIManagement.Entities.ViewModel
{
    public class ProjectDetailsViewModel
    {
        
        public int? ProjectId { get; set; }
        public int? ClientId { get; set; }       
        public string? ClientName { get; set; }      

        public int? DomainId { get; set; }
        public string? DomainName { get; set; }
        public DateTime ArrivalDate { get; set; }
        public DateTime CommitmentDate { get; set; }
        public string? GitRepo { get; set; }
        public string? AdditionalInformation { get; set; }
        public string? BusinessName { get; set; }
 
        public string? IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public long ProjectWiseAttachmentId { get; set; }       
        public string? Name { get; set; }
        public string? Document { get; set; }
        public IFormFile? Projectdocument { get; set; }
        public string? DocDescription { get; set; }
        public DateTime? Modified {  get; set; }


    }
  
}
