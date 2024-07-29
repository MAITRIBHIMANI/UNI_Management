using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UNIManagement.Entities.DataModels;

[Table("Notification")]
public partial class Notification
{
    [Key]
    public int NotificationId { get; set; }

    [Column(TypeName = "character varying")]
    public string Name { get; set; } = null!;

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Date { get; set; }

    [Column(TypeName = "character varying")]
    public string? Document { get; set; }

    [Column(TypeName = "character varying")]
    public string? Duration { get; set; }

    public bool? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    [Column(TypeName = "character varying")]
    public string? Description { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Created { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Modified { get; set; }

    public int? ModifiedBy { get; set; }
}
