using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UNIManagement.Entities.ViewModel
{
    public class AttandanceViewModal
    {
        public int AttendanceId { get; set; }
        public int EmployeeId { get; set; }
        public short? Status { get; set; }
        public int? Day {  get; set; }
        public int? Month { get; set; }
        public int? Year { get; set; }


    }
}
