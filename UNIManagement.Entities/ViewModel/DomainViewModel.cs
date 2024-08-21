using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNIManagement.Entities.ViewModel
{
	public class DomainViewModel
	{
		public int? DomainId { get; set; }
		[Required(ErrorMessage = "Domain Name IS Required")]
		public string? Name { get; set; }
		
		public string? Url { get; set; }

		public DateTime? PurchaseDate { get; set; }

		public string? RenewDuration { get; set; }

		public string? Platform { get; set; }
		public string? CredentialDetails { get; set; }
        public string IsWorkshopPurchased { get; set; }
        public DateTime? WorkspacePurchaseDate { get; set; }

		public string? WorkshpaceRenewDuration { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "Client Name IS Required")]
        public string? ClientName {  get; set; }
		[Required]
        public string? IsActive { get; set; } = "True";

        [Required(ErrorMessage = "Client Id Name IS Required")]
        public int? ClientId { get; set; }
    }
}
