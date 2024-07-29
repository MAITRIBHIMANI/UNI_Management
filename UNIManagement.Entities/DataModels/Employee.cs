using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UNIManagement.Entities.DataModels;

[Table("Employee")]
public partial class Employee
{
    [Key]
    [Column("EmployeeID")]
    public int EmployeeId { get; set; }

    [Column(TypeName = "character varying")]
    public string FirstName { get; set; } = null!;

    [Column(TypeName = "character varying")]
    public string MiddleName { get; set; } = null!;

    [Column(TypeName = "character varying")]
    public string LastName { get; set; } = null!;

    [Column(TypeName = "character varying")]
    public string UserName { get; set; } = null!;

    [Column("Password ", TypeName = "character varying")]
    public string Password { get; set; } = null!;

    [Column(TypeName = "character varying")]
    public string? ContactNumber1 { get; set; }

    [Column(TypeName = "character varying")]
    public string? ContactNumber2 { get; set; }

    [Column("Email ", TypeName = "character varying")]
    public string? Email { get; set; }

    public string? Address { get; set; }

    [Column(TypeName = "character varying")]
    public string? Country { get; set; }

    [Column(TypeName = "character varying")]
    public string? State { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Birthdate { get; set; }

    [Column(TypeName = "character varying")]
    public string? Education { get; set; }

    [Column(TypeName = "character varying")]
    public string? Photo { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Joinningdate { get; set; }

    [Column(TypeName = "character varying")]
    public string? IsFresher { get; set; }

    [Column(TypeName = "character varying")]
    public string? Resume { get; set; }

    [Column(TypeName = "character varying")]
    public string? Bond { get; set; }

    public bool? IsDeleted { get; set; }

    [Column(TypeName = "character varying")]
    public string IsActive { get; set; } = null!;

    [Column(TypeName = "character varying")]
    public string? Description { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime Created { get; set; }

    public int CreatedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Modified { get; set; }

    public int? ModifiedBy { get; set; }

    [Column(TypeName = "character varying")]
    public string? EmployeeType { get; set; }

    [Column(TypeName = "character varying")]
    public string? Gender { get; set; }

    [InverseProperty("Employee")]
    public virtual ICollection<EmployeeAttachment> EmployeeAttachments { get; } = new List<EmployeeAttachment>();

    [InverseProperty("Employee")]
    public virtual ICollection<EmployeeTask> EmployeeTasks { get; } = new List<EmployeeTask>();
}
