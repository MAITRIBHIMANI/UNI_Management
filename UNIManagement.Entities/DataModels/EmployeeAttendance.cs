using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace UNIManagement.Entities.DataModels;

[Table("EmployeeAttendance")]
public partial class EmployeeAttendance
{
    [Key]
    public int AttendanceId { get; set; }

    public int EmployeeId { get; set; }

    public short? Status { get; set; }

    public int? Day { get; set; }

    public int? Month { get; set; }

    public int? Year { get; set; }

    public bool? IsHoliday { get; set; }

    [Column(TypeName = "character varying")]
    public string? Description { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime Created { get; set; }

    public int CreatedBy { get; set; }

    [Column(TypeName = "timestamp without time zone")]
    public DateTime? Modified { get; set; }

    public int? ModifiedBy { get; set; }
}
