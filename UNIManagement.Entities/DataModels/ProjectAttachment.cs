using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UNIManagement.Entities.DataModels;

[Table("ProjectAttachment")]
public partial class ProjectAttachment
{
    [Key]
    public int ProjectAttachmentId { get; set; }

    public int ProjectId { get; set; }

    [Column(TypeName = "character varying")]
    public string Document { get; set; } = null!;

    [Column(TypeName = "character varying")]
    public string? DocDescription { get; set; }

    public bool? IsDeleted { get; set; }

    [Column(TypeName = "character varying")]
    public string? Description { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime Created { get; set; }

    public int CreatedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Modified { get; set; }

    public int? ModifiedBy { get; set; }

    [ForeignKey("ProjectId")]
    [InverseProperty("ProjectAttachments")]
    public virtual Project Project { get; set; } = null!;
}
