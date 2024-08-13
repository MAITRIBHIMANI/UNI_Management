using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNIManagement.Entities.ViewModel;

namespace UNIManagement.Repositories.Repository.InterFace
{
    public interface IClientRepository
    {
        List<ClientViewModel> GetClientList();
        Task DeleteClientAsync(int Id);
        public void AddClient(ClientViewModel model);
        public void UpdateClient(ClientViewModel model);
        public ClientViewModel GetClientDetails(int Id);
        List<ClientViewModel> GetClientListfilter(string filterName, string filterBusinessName, DateTime? filterBirthDate, string filterIsActive);
  
      
    }

}
