using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UNIManagement.Entities.DataModels;

[Table("Domain")]
public partial class Domain
{
    [Key]
    [Column("DomainID")]
    public int DomainId { get; set; }

    [Column(TypeName = "character varying")]
    public string DomainName { get; set; } = null!;

    [Column("URL", TypeName = "character varying")]
    public string? Url { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? PurchaseDate { get; set; }

    [Column(TypeName = "character varying")]
    public string? RenewDuration { get; set; }

    [Column(TypeName = "character varying")]
    public string? Platform { get; set; }

    [Column(TypeName = "character varying")]
    public string? CredentialDetails { get; set; }

    [Column(TypeName = "character varying")]
    public string? IsWorkshopPurchased { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? WorkshopPurchasedDate { get; set; }

    [Column(TypeName = "character varying")]
    public string? WorkshopRenewalDuration { get; set; }

    [Column(TypeName = "character varying")]
    public string? IsActive { get; set; }

    public bool? IsDeleted { get; set; }

    public string? Description { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Modified { get; set; }

    public int? ModifiedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Created { get; set; }

    public int? ClientId { get; set; }
}
