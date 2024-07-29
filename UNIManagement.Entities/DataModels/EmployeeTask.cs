using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UNIManagement.Entities.DataModels;

[Table("EmployeeTask")]
public partial class EmployeeTask
{
    [Key]
    public int TaskId { get; set; }

    [Column(TypeName = "character varying")]
    public string TokenNumber { get; set; } = null!;

    public int? ProjectId { get; set; }

    public int? ClientId { get; set; }

    public int EmployeeId { get; set; }

    [Column(TypeName = "character varying")]
    public string? Document { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? TaskAssignDate { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? DueDate { get; set; }

    [Column(TypeName = "character varying")]
    public string? Status { get; set; }

    [Column(TypeName = "character varying")]
    public string? Description { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime Created { get; set; }

    public int CreatedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Modified { get; set; }

    public int? ModifiedBy { get; set; }

    public bool? IsDeleted { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("EmployeeTasks")]
    public virtual Employee Employee { get; set; } = null!;

    [ForeignKey("ProjectId")]
    [InverseProperty("EmployeeTasks")]
    public virtual Project? Project { get; set; }
}
