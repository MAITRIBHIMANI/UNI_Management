using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using UNIManagement.Entities.DataContext;
using UNIManagement.Entities.DataModels;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Repositories.Repository
{
    public class DomainRepository : IDomainRepository
    {
        private readonly ApplicationDbContext _context;
        public DomainRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #region List
        public List<DomainViewModel> GetDomainList()
        {
            return _context.Domains.Where(x => x.IsDeleted == false).Select(cont => new DomainViewModel()
            {
                DomainId = cont.DomainId,
                Name = cont.DomainName,
                Url = cont.Url,
                PurchaseDate = cont.PurchaseDate,
                RenewDuration = cont.RenewDuration,
                Platform = cont.Platform,
                CredentialDetails = cont.CredentialDetails,
                IsWorkshopPurchased= cont.IsWorkshopPurchased,
                WorkspacePurchaseDate=cont.WorkshopPurchasedDate,
                WorkshpaceRenewDuration=cont.WorkshopRenewalDuration,
                IsActive = cont.IsActive,
            }
            ).ToList();
        }
        public List<DomainViewModel> GetDomainListfilter(string filterName, string filterclientname, DateTime? filterPurchaseDate)
        {
            //List<DomainViewModel> domainList = (from domain in _context.Domains
            //                                    join client in _context.Clients
            //                                    on 
            //                                    )
            var domains = _context.Domains.Where(x => x.IsDeleted == false);
            if (!string.IsNullOrEmpty(filterName)) 
            {
                domains=domains.Where(x => x.DomainName.ToLower().Contains(filterName.ToLower()));
            }
            //if (!string.IsNullOrEmpty(filterclientname))
            //{
            //    domains=domains.Where(x=>x.ClientName)
            //}
            if (filterPurchaseDate.HasValue)
            {
                domains = domains.Where(x => x.PurchaseDate == filterPurchaseDate.Value);
            }

            var domainList = domains.Select(cont => new DomainViewModel()
            {
                DomainId = cont.DomainId,
                Name = cont.DomainName,
                Url = cont.Url,
                PurchaseDate=cont.PurchaseDate,               
                IsActive = cont.IsActive,
            }).ToList();


            return domainList;

        }
     
        #endregion

        #region Delete
        public async Task DeleteDomainAsync(int id)
        {
            Domain d = await _context.Domains.Where(x => x.DomainId == id).FirstOrDefaultAsync();
            if (d != null)
            {
                d.IsDeleted = true;
                _context.Domains.Update(d);
                await _context.SaveChangesAsync();
            }
        }
        #endregion

        #region Add

        public void AddDomain(DomainViewModel model)
        {
            try
            {
                var Domain = new Domain();
                Domain.DomainName = model.Name;
                Domain.Url = model.Url;
                Domain.PurchaseDate = model.PurchaseDate;
                Domain.RenewDuration = model.RenewDuration;
                Domain.Platform = model.Platform;
                Domain.CredentialDetails = model.CredentialDetails;
               // Domain.IsWorkshopPurchased=model.IsWorkshopPurchased;
                Domain.WorkshopPurchasedDate = model.WorkspacePurchaseDate;
                Domain.WorkshopRenewalDuration = model.WorkshpaceRenewDuration;
                Domain.Description = model.Description;
                Domain.IsDeleted = false;
                Domain.IsActive = model.IsActive;
                Domain.Created = DateTime.Now;
                _context.Domains.Add(Domain);
                _context.SaveChanges();
                Domain.CreatedBy = Domain.DomainId;
                _context.SaveChanges();

            }
            catch (Exception e)
            {
                return;
            }
        }

        public void UpdateDomain(DomainViewModel model)
        {
            try
            {
                Domain d = _context.Domains.Where(x => x.DomainId == model.DomainId).FirstOrDefault();
                if (d != null)
                {
                    d.DomainName = model.Name;
                    d.Url = model.Url;
                    d.PurchaseDate = model.PurchaseDate;
                    d.RenewDuration = model.RenewDuration;
                    d.Platform = model.Platform;
                    d.CredentialDetails = model.CredentialDetails;
                   
                    d.WorkshopPurchasedDate = model.WorkspacePurchaseDate;
                    d.WorkshopRenewalDuration = model.WorkshpaceRenewDuration;
                    d.Description = model.Description;
                    d.IsActive = model.IsActive;
                    d.IsWorkshopPurchased = model.IsWorkshopPurchased;
                    d.Modified = DateTime.Now;
                    d.ModifiedBy = model.DomainId;
                    _context.Domains.Update(d);
                    _context.SaveChanges();

                }
            }
            catch (Exception e)
            {
                return;
            }
        }
       
        #endregion

        #region Update
        public DomainViewModel GetDomianDetails(int Id)
        {

            var Domain = _context.Domains.FirstOrDefault(x => x.DomainId == Id);
            if (Domain != null)
            {
                return new DomainViewModel()
                {
                    DomainId = Domain.DomainId,
                    Name = Domain.DomainName,
                    Url = Domain.Url,
                    PurchaseDate = Domain.PurchaseDate,
                    RenewDuration = Domain.RenewDuration,
                    Platform = Domain.Platform,
                    CredentialDetails = Domain.CredentialDetails,
                    IsWorkshopPurchased=Domain.IsWorkshopPurchased,
                    WorkspacePurchaseDate = Domain.WorkshopPurchasedDate,
                    WorkshpaceRenewDuration = Domain.WorkshopRenewalDuration,
                    Description = Domain.Description,
                    IsActive = Domain.IsActive,
                };
            }
            return new DomainViewModel();
        }
        #endregion
    }
}
