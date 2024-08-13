using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNIManagement.Entities.DataModels;
using UNIManagement.Entities.ViewModel;

namespace UNIManagement.Repositories.Repository.InterFace
{
    public interface ILoginRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        Employee GetUser(string email, string password);
    }
}
