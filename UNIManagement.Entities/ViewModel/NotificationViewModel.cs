using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNIManagement.Entities.ViewModel
{
    public class NotificationViewModel
    {
        public int NotificationId { get; set; }

        public string Name { get; set; } = null!;

        public DateTime? Date { get; set; }

        public string? Document { get; set; }
        public IFormFile? DocumentFile { get; set; }
        public string? Duration { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDeleted { get; set; }

        public string? Description { get; set; }
    }
}
