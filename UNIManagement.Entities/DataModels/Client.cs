using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UNIManagement.Entities.DataModels;

[Table("Client")]
public partial class Client
{
    [Key]
    public int ClientId { get; set; }

    [Column(TypeName = "character varying")]
    public string Name { get; set; } = null!;

    [Column(TypeName = "character varying")]
    public string? Number { get; set; }

    [Column(TypeName = "character varying")]
    public string? Email { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? BirthDate { get; set; }

    [Column(TypeName = "character varying")]
    public string? Address { get; set; }

    [Column(TypeName = "character varying")]
    public string? BusinessName { get; set; }

    [Column(TypeName = "character varying")]
    public string? BusinessNumber { get; set; }

    [Column(TypeName = "character varying")]
    public string? Category { get; set; }

    [Column(TypeName = "character varying")]
    public string? RefferenceDetails { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Created { get; set; }

    public int? CreatedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Modified { get; set; }

    public int? ModifiedBy { get; set; }

    [Column(TypeName = "character varying")]
    public string? AdditionInformation { get; set; }

    public string? Description { get; set; }

    public bool? IsDeleted { get; set; }

    [Column(TypeName = "character varying")]
    public string? IsActive { get; set; }
}
