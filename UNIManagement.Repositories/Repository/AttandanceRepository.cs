
using UNIManagement.Entities.DataContext;
using UNIManagement.Entities.DataModels;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.CommanHelper;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Repositories.Repository
{
    public class AttandanceRepository : IAttandanceRepository
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        public AttandanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Add
        public void AddAttandance(int day, int month, int year, short status)
        {
            try
            {
                var EmpAttandace = new EmployeeAttendance();
                EmpAttandace.EmployeeId = 131;
                EmpAttandace.Status = status;
                EmpAttandace.Day = day;
                EmpAttandace.Month = month;
                EmpAttandace.Year = year;
                EmpAttandace.Created = DateTime.Now;

                _context.EmployeeAttendances.Add(EmpAttandace);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return;
            }
        }
        #endregion

        #region GetAttandace
        public List<AttandanceViewModal> GetAttandace(int year, int month, int E)
        {
            var list =   _context.EmployeeAttendances
                     .Where(e => e.Month == (month+1) && e.Year == year && e.EmployeeId==E)
                     .Select(cont => new AttandanceViewModal()
                     {
                         EmployeeId = cont.EmployeeId,
                         AttendanceId = cont.AttendanceId,
                         Year = year,
                         Month = month,
                         Day = cont.Day,
                         Status = cont.Status,
                     })
                     .ToList();

            return list;
        }
        #endregion
    }
}
