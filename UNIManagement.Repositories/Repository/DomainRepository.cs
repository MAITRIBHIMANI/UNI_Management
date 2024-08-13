using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Drawing;
using UNIManagement.Entities.DataContext;
using UNIManagement.Entities.DataModels;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Repositories.Repository
{
    public class DomainRepository : IDomainRepository
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        public DomainRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region List
        /// <summary>
        /// Retrive Data From Domain Table in List
        /// </summary>
        /// <returns></returns>
        public List<DomainViewModel> GetDomainList()
        {
            return _context.Domains.Where(x => x.IsDeleted == false)
                                   .OrderByDescending(x => x.Modified)
                                   .Select(cont => new DomainViewModel()
                                   {
                                       DomainId = cont.DomainId,
                                       Name = cont.DomainName,
                                       Url = cont.Url,
                                       PurchaseDate = cont.PurchaseDate,
                                       IsActive = cont.IsActive,
                                   }).ToList();
        }

        /// <summary>
        /// Retrive Filterd data from Domain Table
        /// </summary>
        /// <returns></returns>
        public List<DomainViewModel> GetDomainListfilter(string filterName, int? filterclientname, DateTime? filterPurchaseDate, string filterIsActive)
        {
            List<DomainViewModel> domainlist = (from domain in _context.Domains
                                                join client in _context.Clients
                                                on domain.ClientId equals client.ClientId into ClientGroup
                                                from client in ClientGroup.DefaultIfEmpty()
                                                where (domain.IsDeleted == false
                                                    && (string.IsNullOrEmpty(filterName) || domain.DomainName.ToLower().Trim().Contains(filterName.Trim().ToLower()))
                                                    && (!filterclientname.HasValue || domain.ClientId == filterclientname.Value)
                                                    && (!filterPurchaseDate.HasValue || domain.PurchaseDate == filterPurchaseDate.Value)
                                                    && (string.IsNullOrEmpty(filterIsActive) || domain.IsActive.ToLower() == filterIsActive.ToLower()))
                                                orderby domain.Modified descending
                                                select new DomainViewModel
                                                {
                                                    DomainId = domain.DomainId,
                                                    Name = domain.DomainName,
                                                    Url = domain.Url,
                                                    PurchaseDate = domain.PurchaseDate,
                                                    IsActive = domain.IsActive,
                                                }).ToList();
            return domainlist;
        }

        #endregion

        #region Delete
        /// <summary>
        /// Delete Domain By DomainId
        /// </summary>
        /// <returns></returns>
        public async Task DeleteDomainAsync(int id)
        {
            Domain? d = await _context.Domains
                                      .Where(x => x.DomainId == id)
                                      .FirstOrDefaultAsync();
            if (d != null)
            {
                d.IsDeleted = true;
                _context.Domains.Update(d);
                await _context.SaveChangesAsync();
            }
        }
        #endregion

        #region Add
        /// <summary>
        /// Add Domaian
        /// </summary>
        /// <param name="model"></param>
        public void AddDomain(DomainViewModel model)
        {
            try
            {
                var Domain = new Domain();
                Domain.DomainName = model.Name;
                Domain.Url = model.Url;
                Domain.ClientId = model.ClientId;
                Domain.PurchaseDate = model.PurchaseDate;
                Domain.RenewDuration = model.RenewDuration;
                Domain.Platform = model.Platform;
                Domain.CredentialDetails = model.CredentialDetails;
                Domain.IsWorkshopPurchased = model.IsWorkshopPurchased;
                if (Domain.IsWorkshopPurchased == "True")
                {
                    Domain.WorkshopPurchasedDate = model.WorkspacePurchaseDate;
                    Domain.WorkshopRenewalDuration = model.WorkshpaceRenewDuration;
                }
                Domain.IsActive = model.IsActive;
                Domain.IsDeleted = false;
                Domain.Created = DateTime.Now;
                Domain.Modified = Domain.Created;
                _context.Domains.Add(Domain);
                _context.SaveChanges();               
                Domain.CreatedBy = Domain.DomainId;
                Domain.ModifiedBy = Domain.DomainId;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                return;
            }
        }

        /// <summary>
        /// Update Domain
        /// </summary>
        public void UpdateDomain(DomainViewModel model)
        {
            try
            {
                Domain? d = _context.Domains.Where(x => x.DomainId == model.DomainId).FirstOrDefault();
                if (d != null)
                {
                    d.DomainName = model.Name;
                    d.Url = model.Url;
                    d.ClientId = model.ClientId;
                    d.PurchaseDate = model.PurchaseDate;
                    d.RenewDuration = model.RenewDuration;
                    d.Platform = model.Platform;
                    d.CredentialDetails = model.CredentialDetails;
                    d.IsWorkshopPurchased = model.IsWorkshopPurchased;
                    if (d.IsWorkshopPurchased == "True")
                    {
                        d.WorkshopPurchasedDate = model.WorkspacePurchaseDate;
                        d.WorkshopRenewalDuration = model.WorkshpaceRenewDuration;
                    }
                    d.IsActive = model.IsActive;
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

        /// <summary>
        /// Get Domain Details BY DomainId
        /// </summary>
        /// <returns></returns>
        public DomainViewModel GetDomianDetails(int id)
        {
            DomainViewModel? domainDetails = (from d in _context.Domains
                                              join c in _context.Clients
                                              on d.ClientId equals c.ClientId into ClientGroup
                                              from client in ClientGroup.DefaultIfEmpty()
                                              where d.DomainId == id
                                              select new DomainViewModel()
                                              {
                                                  DomainId = d.DomainId,
                                                  Name = d.DomainName,
                                                  Url = d.Url,
                                                  ClientId = client.ClientId,
                                                  ClientName = client.Name,
                                                  PurchaseDate = d.PurchaseDate,
                                                  RenewDuration = d.RenewDuration,
                                                  Platform = d.Platform,                                                
                                                  CredentialDetails = d.CredentialDetails,
                                                  IsWorkshopPurchased = d.IsWorkshopPurchased,
                                                  WorkspacePurchaseDate = d.WorkshopPurchasedDate,
                                                  WorkshpaceRenewDuration = d.WorkshopRenewalDuration,                                                 
                                                  IsActive = d.IsActive,
                                              }).FirstOrDefault();
            return domainDetails;
        }
        #endregion
    }
}
