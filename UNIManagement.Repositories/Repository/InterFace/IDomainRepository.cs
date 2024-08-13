using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UNIManagement.Entities.DataModels;
using UNIManagement.Entities.ViewModel;

namespace UNIManagement.Repositories.Repository.InterFace
{
	public interface IDomainRepository
	{
        List<DomainViewModel> GetDomainList();
        Task DeleteDomainAsync(int Id);
        public void AddDomain(DomainViewModel model);
        public void UpdateDomain(DomainViewModel model);

        public DomainViewModel GetDomianDetails(int Id);
        List<DomainViewModel> GetDomainListfilter(string filterName, int? filterclientname, DateTime? filterPurchaseDate, string filterIsActive);

    }
}
