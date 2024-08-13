using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UNIManagement.Entities.DataModels;

[Table("Project")]
public partial class Project
{
    [Key]
    public int ProjectId { get; set; }

    public int? ClientId { get; set; }

    public int? DomainId { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime ArrivalDate { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime CommitmentDate { get; set; }

    [Column(TypeName = "character varying")]
    public string GitRepo { get; set; } = null!;

    [Column(TypeName = "character varying")]
    public string AdditionalInformation { get; set; } = null!;

    [Column(TypeName = "character varying")]
    public string? BusinessName { get; set; }

    [Column(TypeName = "character varying")]
    public string IsActive { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    [Column(TypeName = "character varying")]
    public string? Description { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime Created { get; set; }

    public int CreatedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Modified { get; set; }

    public int? ModifiedBy { get; set; }

    [Column(TypeName = "character varying")]
    public string? Name { get; set; }

    [InverseProperty("Project")]
    public virtual ICollection<EmployeeTask> EmployeeTasks { get; } = new List<EmployeeTask>();

    [InverseProperty("Project")]
    public virtual ICollection<ProjectAttachment> ProjectAttachments { get; } = new List<ProjectAttachment>();
}
