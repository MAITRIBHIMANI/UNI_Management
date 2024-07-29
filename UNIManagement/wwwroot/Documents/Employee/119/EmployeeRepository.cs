using Microsoft.EntityFrameworkCore;
using UNI_MANAGE.Entities.DataContext;
using UNI_MANAGE.Entities.DataModels;
using UNI_MANAGE.Entities.ViewModel;
using UNI_MANAGE.Repositories.Comman.Helper;
using UNI_MANAGE.Repositories.Repository.Interface;

namespace UNI_MANAGE.Repositories.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {

        #region Constructor 
        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region EmployeeList
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<EmployeeViewModel> GetEmployeeTbList()
        {
            return _context.EmployeeTbs
                            .Where(x => x.IsDeleted == false)
                            .OrderByDescending( x => x.Created)
                            .Select(cont => new EmployeeViewModel()
                            {
                                Id = cont.Id,
                                FirstName = cont.FirstName,
                                EmployeeTypeId = cont.EmployeeTypeId,
                                ContactNumber1 = cont.ContactNumber1,
                                Resume = cont.Resume,
                                IsActive = cont.IsActive,
                            })
                            .ToList();
        }
        #endregion

        #region GetEmployeeTbByEmployeeId
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public EmployeeDetailsViewModel? GetEmployeeTbByEmployeeId(int id)
        {
            return (from EmployeeTb in _context.EmployeeTbs
                                               join EmployeeAttachment in _context.EmployeeAttachments
                                               on EmployeeTb.Id equals EmployeeAttachment.EmployeeId
                                               into EmpGroup
                                               from EmployeeAttachment in EmpGroup.DefaultIfEmpty()
                                               where EmployeeAttachment.EmployeeId == id 
                                               select new EmployeeDetailsViewModel()
                                               {
                                                   Id = EmployeeTb.Id,
                                                   UserName = EmployeeTb.UserName,
                                                   FirstName = EmployeeTb.FirstName,
                                                   LastName = EmployeeTb.LastName,
                                                   EmployeeTypeId = EmployeeTb.EmployeeTypeId,
                                                   ContactNumber1 = EmployeeTb.ContactNumber1,
                                                   ContactNumber2 = EmployeeTb.ContactNumber2,
                                                   Resume = EmployeeTb.Resume,
                                                   IsActive = EmployeeTb.IsActive,
                                                   MiddleName = EmployeeTb.MiddleName,
                                                   Password = EmployeeTb.Password,
                                                   Email = EmployeeTb.Email,
                                                   Address = EmployeeTb.Address,
                                                   Country = EmployeeTb.Country,
                                                   State = EmployeeTb.State,
                                                   BirthDate = EmployeeTb.BirthDate,
                                                   Education = EmployeeTb.Education,
                                                   Photo = EmployeeTb.Photo,
                                                   Gender = EmployeeTb.Gender,
                                                   JoiningDate = EmployeeTb.JoiningDate,
                                                   IsFresher = EmployeeTb.IsFresher,
                                                   Bond = EmployeeTb.Bond,
                                                   IsDeleted = EmployeeTb.IsDeleted,
                                                   Description = EmployeeTb.Description,
                                                   EmployeeAttachmentId = EmployeeAttachment.EmployeeAttachmentId,
                                                   IsAdhar = EmployeeAttachment.IsAdhar,
                                                   AdharNo = EmployeeAttachment.AdharNo,
                                                   IsPassbook = EmployeeAttachment.IsPassbook,
                                                   AccountNumber = EmployeeAttachment.AccountNumber,
                                                   BankName = EmployeeAttachment.BankName,
                                                   Ifsc = EmployeeAttachment.Ifsc,
                                                   Upi = EmployeeAttachment.Upi,
                                                   IsDegree = EmployeeAttachment.IsDegree,
                                                   IsMarksheetUpload = EmployeeAttachment.IsMarksheetUpload,
                                                   OtherDocuments = EmployeeAttachment.OtherDocuments,
                                               })
                                          .FirstOrDefault();
        }
        #endregion

        #region AddEmployee
        public void AddEmployeeDetailsAsync(EmployeeDetailsViewModel employeeDetails)
        {
            EmployeeTb employee = new EmployeeTb();
            employee.FirstName = employeeDetails.FirstName;
            employee.MiddleName = employeeDetails.MiddleName;
            employee.LastName = employeeDetails.LastName;
            employee.Password = employeeDetails.Password;
            employee.Email = employeeDetails.Email;
            employee.Address = employeeDetails.Address;
            employee.Country = employeeDetails.Country;
            employee.State = employeeDetails.State;
            employee.BirthDate = employeeDetails.BirthDate;
            employee.Education = employeeDetails.Education;
            if (employeeDetails.EmployeeImage != null)
                employee.Photo = employeeDetails.EmployeeImage.FileName;
            employee.Gender = employeeDetails.Gender;
            employee.JoiningDate = employeeDetails.JoiningDate;
            employee.IsFresher = employeeDetails.IsFresher;
            employee.Bond = employeeDetails.Bond;
            employee.IsDeleted = employeeDetails.IsDeleted;
            employee.Description = employeeDetails.Description;
            employee.Created = employeeDetails.Created;
            employee.CreatedBy = employeeDetails.CreatedBy;
            employee.Modified = employeeDetails.Modified;
            employee.ModifiedBy = employeeDetails.ModifiedBy;
            employee.ContactNumber2 = employeeDetails.ContactNumber2;
            employee.UserName = employeeDetails.UserName;
            employee.EmployeeTypeId = employeeDetails.EmployeeTypeId;
            employee.ContactNumber1 = employeeDetails.ContactNumber1;

            if (employeeDetails.ResumeImage != null)
                employee.Resume = employeeDetails.ResumeImage.FileName;

            employee.IsActive = employeeDetails.IsActive;
            employee.EmployeeTypeId = employeeDetails.EmployeeTypeId;
            employee.UserName = employeeDetails.FirstName + " " + employeeDetails.LastName;
            employee.Password = employeeDetails.FirstName + " " + employeeDetails.LastName;
            employee.Photo = Helper.UploadDocument(employeeDetails.EmployeeImage, employee.Id, "Employee", "Image.pdf");
            _context.EmployeeTbs.Add(employee);
            _context.SaveChanges();

            EmployeeAttachment employeeAttachment = new EmployeeAttachment()
            {
                EmployeeId = employee.Id,
                AdharNo = employeeDetails.AdharNo,
                IsAdhar = employeeDetails.AdharDoc != null ? true : false,
                IsDegree = employeeDetails.IsDegree,
                IsMarksheetUpload = employeeDetails.IsMarksheetUpload,
                IsPassbook = employeeDetails.IsPassbook,
                AccountNumber = employeeDetails.AccountNumber,
                BankName = employeeDetails.BankName,
                Ifsc = employeeDetails.Ifsc,
                Upi = employeeDetails.Upi,
                OtherDocuments = employeeDetails.OtherDocuments,
                Description = employeeDetails.Description,
                Created = employeeDetails.Created,
                Modified = DateTime.Now,
            };

            _context.EmployeeAttachments.Add(employeeAttachment);
            _context.SaveChanges();


        }
        #endregion

        #region UpdateEmployee
        /// <summary>
        /// Update EmployeeDetails IN Employee Table and Employee Attachment Table Base on EmployeeId
        /// </summary>
        /// <param name="employeeDetails"></param>
        public void UpdateEmployeeDetailsAsync(EmployeeDetailsViewModel employeeDetails)
        {
            try
            {
                EmployeeTb employee = _context.EmployeeTbs.Where(x => x.Id == employeeDetails.Id).FirstOrDefault();

                if (employee != null)
                {
                    employee.UserName = employeeDetails.FirstName + " " + employeeDetails.LastName;
                    employee.Password = employeeDetails.FirstName + " " + employeeDetails.LastName;
                    employee.FirstName = employeeDetails.FirstName;
                    employee.MiddleName = employeeDetails.MiddleName;
                    employee.LastName = employeeDetails.LastName;
                    employee.Email = employeeDetails.Email;
                    employee.EmployeeTypeId = employeeDetails.EmployeeTypeId;
                    employee.Address = employeeDetails.Address;
                    employee.Country = employeeDetails.Country;
                    employee.State = employeeDetails.State;
                    employee.BirthDate = employeeDetails.BirthDate;
                    employee.Education = employeeDetails.Education;
                    employee.Gender = employeeDetails.Gender;
                    employee.JoiningDate = employeeDetails.JoiningDate;
                    employee.IsFresher = employeeDetails.IsFresher;
                    employee.Bond = employeeDetails.Bond;
                    employee.IsDeleted = employeeDetails.IsDeleted;
                    employee.Description = employeeDetails.Description;
                    employee.CreatedBy = employeeDetails.CreatedBy;
                    employee.ContactNumber2 = employeeDetails.ContactNumber2;
                    employee.ContactNumber1 = employeeDetails.ContactNumber1;
                    employee.IsActive = employeeDetails.IsActive;
                    employee.Created = DateTime.Now;

                    if (employeeDetails.EmployeeImage != null)
                        employee.Photo = Helper.UploadDocument(employeeDetails.EmployeeImage, employee.Id, "Employee", "Image.pdf");
                    if (employeeDetails.ResumeImage != null)
                        employee.Resume = Helper.UploadDocument(employeeDetails.EmployeeImage, employee.Id, "Employee", "Resume.pdf");

                    _context.EmployeeTbs.Update(employee);
                    _context.SaveChanges();

                    EmployeeAttachment employeeAttachment = _context.EmployeeAttachments.Where(x => x.EmployeeId == employeeDetails.Id).FirstOrDefault();

                    if (employeeAttachment != null)
                    {
                        employeeAttachment.EmployeeId = employee.Id;
                        employeeAttachment.AdharNo = employeeDetails.AdharNo;
                        employeeAttachment.IsAdhar = employeeDetails.AdharDoc != null ? true : false;
                        employeeAttachment.IsDegree = employeeDetails.IsDegree;
                        employeeAttachment.IsMarksheetUpload = employeeDetails.IsMarksheetUpload;
                        employeeAttachment.IsPassbook = employeeDetails.IsPassbook;
                        employeeAttachment.AccountNumber = employeeDetails.AccountNumber;
                        employeeAttachment.BankName = employeeDetails.BankName;
                        employeeAttachment.Ifsc = employeeDetails.Ifsc;
                        employeeAttachment.Upi = employeeDetails.Upi;
                        employeeAttachment.OtherDocuments = employeeDetails.OtherDocuments;
                        employeeAttachment.Description = employeeDetails.Description;
                        employeeAttachment.Created = employeeDetails.Created;
                        employeeAttachment.Modified = DateTime.Now;

                        _context.EmployeeAttachments.Update(employeeAttachment);
                        _context.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }

        }
        #endregion

        #region DeleteEmployee
        public async Task DeleteEmployeeAsync(int Id)
        {
            EmployeeTb db = await _context.EmployeeTbs.Where(x => x.Id.Equals(Id)).FirstOrDefaultAsync();
            if (db != null)
            {
                db.IsDeleted = true;
                _context.EmployeeTbs.Update(db);
                await _context.SaveChangesAsync();
            }
        }
        #endregion
    }
}