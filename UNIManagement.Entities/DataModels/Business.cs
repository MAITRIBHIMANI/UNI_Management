using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UNIManagement.Entities.DataModels;

[Table("Business")]
public partial class Business
{
    [Key]
    [Column("BusinessID")]
    public int BusinessId { get; set; }

    [Column(TypeName = "character varying")]
    public string? BusinessName { get; set; }

    [Column(TypeName = "character varying")]
    public string? BusinessNumber { get; set; }

    [Column(TypeName = "character varying")]
    public string? Category { get; set; }
}
