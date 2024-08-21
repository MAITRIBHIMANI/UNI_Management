using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNIManagement.Entities.DataContext;
using UNIManagement.Entities.DataModels;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.Repository.InterFace;
using Microsoft.AspNetCore.Http;
namespace UNIManagement.Repositories.Repository
{
    public class LoginRepository : ILoginRepository
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        public LoginRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion
        public bool GetUser(string email, string password)
        {
            var user = _context.Employees
           .FirstOrDefault(e => e.Email == email && e.Password == password);

            return user != null;
           

        }
    }
}
