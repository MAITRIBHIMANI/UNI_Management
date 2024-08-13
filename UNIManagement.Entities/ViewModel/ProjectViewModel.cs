using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UNIManagement.Entities.ViewModel
{
    public class ProjectViewModel
    {
        public long ProjectId { get; set; }
        public int ClientId { get; set; }
        public int DomainId { get; set; }
        public string? IsActive { get; set; }

        public DateTime? ArrivalDate { get; set; }
        public DateTime CommitmentDate { get; set; }
        public string GitRepo { get; set; }
        public string AdditionalInformation { get; set; }
   
     public string BusinessName {  get; set; }


    }
}
