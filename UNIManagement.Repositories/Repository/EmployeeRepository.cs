using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNIManagement.Entities.DataContext;
using UNIManagement.Entities.DataModels;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.CommanHelper;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Repositories.Repository
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

        #region EmployeeAll
        /// <summary>
        /// Retrive Lists from Employee Table 
        /// </summary>
        /// <returns></returns>
        public List<EmployeeViewModel> GetEmployeeList()
        {
            return _context.Employees
                .Where(x=>x.IsDeleted==false)
                   .Select(cont => new EmployeeViewModel()
                     {
                         EmployeeId = cont.EmployeeId,
                         FirstName = cont.FirstName,
                         Employeetype = cont.EmployeeType,
                         ContactNumber1 = cont.ContactNumber1,
                         Resume = cont.Resume,
                         IsActive = cont.IsActive,
                     }
                     ).ToList();
        }
      
        public List<EmployeeDetailsViewModel> GetEmployeeListfilter(string filterName, string filterType, DateTime? filterJoinningDate)
        {
            var empList = _context.Employees.Where(x=>x.IsDeleted==false
                                                    && (string.IsNullOrEmpty(filterName) || x.FirstName.ToLower().Contains(filterName.ToLower()))
                                                    && (string.IsNullOrEmpty(filterType) || x.EmployeeType.ToLower().Contains(filterType.ToLower()))
                                                    && (!filterJoinningDate.HasValue || x.Joinningdate == filterJoinningDate.Value)
                                                    )                                     
                                            .Select(cont => new EmployeeDetailsViewModel()
                                             {
                                                 EmployeeId = cont.EmployeeId,
                                                 FirstName = cont.FirstName,
                                                 Employeetype = cont.EmployeeType,
                                                 ContactNumber1 = cont.ContactNumber1,
                                                 Resume = cont.Resume,
                                                 IsActive = cont.IsActive,
                                             }).ToList();         
                                                            
          
            return empList;

        }
    
        #endregion

        #region Delete
        /// <summary>
        /// Delete Emplyoee details from database on EmployeeId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteEmployeeAsync(int id)
        {
            Employee d = await _context.Employees.Where(x => x.EmployeeId == id).FirstOrDefaultAsync();
            if (d != null)
            {
                d.IsDeleted = true;
                _context.Employees.Update(d);
                await _context.SaveChangesAsync();
            }
        }
        #endregion

        #region Employee_Add
        /// <summary>
        /// Add Employee Details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void AddEmployee(EmployeeDetailsViewModel model)
        {
            //Employee
           
            try
            {
                var Employee = new Employee();
                Employee.FirstName = model.FirstName;
                Employee.MiddleName = model.MiddleName;
                Employee.LastName = model.LastName;
                Employee.ContactNumber1 = model.ContactNumber1;
                Employee.ContactNumber2 = model.ContactNumber2;
                Employee.Email = model.Email;               
                Employee.Address = model.Address;
                Employee.Country = model.Country;
                Employee.State = model.State;
                Employee.Birthdate = model.Birthdate;
                Employee.Education = model.Education;
                Employee.Gender = model.Gender;
                Employee.IsActive = model.IsActive;
                Employee.Joinningdate = model.Joinningdate;
                Employee.IsFresher = model.IsFresher;
                Employee.Bond = model.Bond;
                Employee.EmployeeType = model.Employeetype;
                Employee.Password = model.FirstName + " " + model.LastName;
                Employee.UserName = model.FirstName + " " + model.MiddleName + " " + model.LastName;
                Employee.Created= DateTime.Now;
                Employee.IsDeleted = false;
                _context.Employees.Add(Employee);
                _context.SaveChanges();
                
                Employee.Photo =
               Helper.Documents(model.EmployeeImage, Employee.EmployeeId, "Employee", "Image.jpg");
                Employee.Resume =
                Helper.Documents(model.EmployeeResume, Employee.EmployeeId, "Employee", "Resume.pdf");
                Employee.CreatedBy = Employee.EmployeeId;

                //EmployeeAttachment
                var employeeattachment = new EmployeeAttachment();
                employeeattachment.EmployeeId = Employee.EmployeeId;
                employeeattachment.AdharNo = model.AdharNo;
                employeeattachment.IsAdhar = model.IsAdhar != null ? true : false;
                employeeattachment.IsPassbook = model.IsPassbook != null ? true : false;
                employeeattachment.IsDegree = model.IsDegree != null ? true : false;
                employeeattachment.IsMarksheetUpload = model.IsMarksheetUpload != null ? true : false;
                employeeattachment.AccountNumber = model.AccountNumber;
                employeeattachment.BankName = model.BankName;
                employeeattachment.Ifsc = model.Ifsc;
                employeeattachment.Upi = model.Upi;
                employeeattachment.Created= DateTime.Now;
                employeeattachment.CreatedBy = Employee.EmployeeId;
                Helper.Documents(model.AdharCard, Employee.EmployeeId, "Employee", "AdharCard.pdf");
                Helper.Documents(model.PassBook, Employee.EmployeeId, "Employee", "Passbook.pdf");
                Helper.Documents(model.Degree, Employee.EmployeeId, "Employee", "Degree.pdf");
                Helper.Documents(model.Marksheet, Employee.EmployeeId, "Employee", "Marksheet.pdf");
                employeeattachment.OtherDocuments = Helper.Documents(model.otherdocument, Employee.EmployeeId, "Employee", "OtherDocument.pdf");

                _context.EmployeeAttachments.Add(employeeattachment);
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return;
            }


        }

        #endregion

        #region Employee_Edit
        /// <summary>
        /// Update Employee Details By EmployeeId
        /// </summary>
        /// <param name="model"></param>
        public void UpdateEmployee(EmployeeDetailsViewModel model)
        {
            try
            {
                Employee employee = _context.Employees
                                    .Where(x => x.EmployeeId == model.EmployeeId)
                                    .FirstOrDefault();

                if (employee != null)
                {
                    employee.UserName = model.FirstName + " " + model.LastName;
                    employee.Password = model.FirstName + " " + model.LastName;
                    employee.FirstName = model.FirstName;
                    employee.MiddleName = model.MiddleName;
                    employee.LastName = model.LastName;
                    employee.Email = model.Email;
                    employee.Address = model.Address;
                    employee.ContactNumber1 = model.ContactNumber1;
                    employee.ContactNumber2 = model.ContactNumber2;
                    employee.Country = model.Country;
                    employee.State = model.State;
                    employee.Birthdate = model.Birthdate;
                    employee.Education = model.Education;
                    employee.Gender = model.Gender;
                    employee.IsActive = model.IsActive;
                    employee.Joinningdate = model.Joinningdate;
                    employee.IsFresher = model.IsFresher;
                    employee.EmployeeType = model.Employeetype;
                    employee.Bond = model.Bond;
                    employee.ModifiedBy = model.EmployeeId;
                      
                    employee.Modified = DateTime.Now;
                    if (model.EmployeeImage != null)
                        employee.Photo = Helper.Documents(model.EmployeeImage, employee.EmployeeId, "Employee", "Image.pdf");
                    if (model.EmployeeResume != null)
                        employee.Resume = Helper.Documents(model.EmployeeImage, employee.EmployeeId, "Employee", "Resume.pdf");

                     _context.Employees.Update(employee);
                    _context.SaveChanges();

                    EmployeeAttachment employeeAttachment = _context.EmployeeAttachments
                                                            .Where(x => x.EmployeeId == model.EmployeeId)
                                                            .FirstOrDefault();

                    if (employeeAttachment != null)
                    {
                        employeeAttachment.EmployeeId = employee.EmployeeId;
                        employeeAttachment.AdharNo = model.AdharNo;
                        employeeAttachment.IsAdhar = model.AdharCard != null ? true : false;
                        employeeAttachment.IsDegree = model.Degree != null ? true : false;
                        employeeAttachment.IsMarksheetUpload = model.Marksheet != null ? true : false;
                        employeeAttachment.IsPassbook = model.PassBook != null ? true : false;
                        employeeAttachment.AccountNumber = model.AccountNumber;
                        employeeAttachment.BankName = model.BankName;
                        employeeAttachment.Ifsc = model.Ifsc;
                        employeeAttachment.Upi = model.Upi;
                        employeeAttachment.OtherDocuments = model.OtherDocuments;
                        employeeAttachment.Modified = DateTime.Now;
                        employeeAttachment.ModifiedBy = model.EmployeeId;
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

        #region GetEmployeeById
        /// <summary>
        /// Get Only one Employee Details By EmployeeId
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>

        public EmployeeDetailsViewModel GetEmployeeDetails(int Id)
        {
           
                EmployeeDetailsViewModel? EmployeeDetails = (from emp in _context.Employees
                                                            join empattach in _context.EmployeeAttachments
                                                            on emp.EmployeeId equals empattach.EmployeeId into EmployeeGroup
                                                            from EmployeeAttachment in EmployeeGroup.DefaultIfEmpty()
                                                            where EmployeeAttachment.EmployeeId == Id
                                                            select new EmployeeDetailsViewModel()
                                                            {
                                                                EmployeeId = emp.EmployeeId,
                                                                FirstName = emp.FirstName,
                                                                MiddleName = emp.MiddleName,
                                                                LastName = emp.LastName,
                                                                ContactNumber1 = emp.ContactNumber1,
                                                                ContactNumber2 = emp.ContactNumber2,
                                                                Email = emp.Email,
                                                                Address = emp.Address,
                                                                Country = emp.Country,
                                                                State = emp.State,
                                                                Birthdate = emp.Birthdate,
                                                                Education = emp.Education,
                                                                Photo = emp.Photo,
                                                                Gender = emp.Gender,
                                                                IsActive = emp.IsActive,
                                                                Joinningdate = emp.Joinningdate,
                                                                IsFresher = emp.IsFresher,
                                                                Resume = emp.Resume,
                                                                Bond = emp.Bond,
                                                                Employeetype = emp.EmployeeType,                                                               
                                                                AdharNo = EmployeeAttachment.AdharNo,
                                                                BankName = EmployeeAttachment.BankName,
                                                                AccountNumber = EmployeeAttachment.AccountNumber,
                                                                Upi = EmployeeAttachment.Upi,
                                                                Ifsc = EmployeeAttachment.Ifsc,
                                                               
                                                            }).FirstOrDefault();


           
          

            return EmployeeDetails;
        }


        #endregion

      

    }
}
