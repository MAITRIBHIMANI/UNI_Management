using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UNIManagement.Entities.DataModels;

[Table("EmployeeAttachment")]
public partial class EmployeeAttachment
{
    [Key]
    [Column("EmployeeAttachmentID")]
    public int EmployeeAttachmentId { get; set; }

    [Column("EmployeeID")]
    public int EmployeeId { get; set; }

    public bool? IsAdhar { get; set; }

    [Column(TypeName = "character varying")]
    public string? AdharNo { get; set; }

    public bool? IsPassbook { get; set; }

    [Column(TypeName = "character varying")]
    public string? AccountNumber { get; set; }

    [Column(TypeName = "character varying")]
    public string? BankName { get; set; }

    [Column("IFSC", TypeName = "character varying")]
    public string? Ifsc { get; set; }

    [Column("UPI", TypeName = "character varying")]
    public string? Upi { get; set; }

    public bool? IsDegree { get; set; }

    public bool? IsMarksheetUpload { get; set; }

    [Column(TypeName = "character varying")]
    public string? OtherDocuments { get; set; }

    public string? Description { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Created { get; set; }

    public int CreatedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Modified { get; set; }

    public int ModifiedBy { get; set; }

    [ForeignKey("EmployeeId")]
    [InverseProperty("EmployeeAttachments")]
    public virtual Employee Employee { get; set; } = null!;
}
