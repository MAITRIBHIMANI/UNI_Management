using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNIManagement.Entities.ViewModel
{
    public class ProjectAttachmentViewModel
    {
        public long ProjectWiseAttachmentId { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Document {  get; set; }
        public IFormFile? Projectdocument { get; set; }
        public string DocDescription { get; set; }
        public string Description { get; set; }
        public string BusinessNumber {  get; set; }

    }
}
