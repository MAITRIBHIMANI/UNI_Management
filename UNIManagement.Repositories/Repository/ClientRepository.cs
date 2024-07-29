using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using UNIManagement.Entities.DataContext;
using UNIManagement.Entities.DataModels;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.CommanHelper;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Repositories.Repository
{
    public class ClientRepository : IClientRepository
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        #region Client All
        /// <summary>
        /// Retrive Lists from Client Table 
        /// </summary>
        /// <returns></returns>
        public List<ClientViewModel> GetClientList()
        {
            return _context.Clients
                           .Where(x => x.IsDeleted == false)
                           .Select(cont => new ClientViewModel()
                           {
                               ClientId = cont.ClientId,
                               Name = cont.Name,
                               Number = cont.Number,
                               BusinessName = cont.BusinessName,
                               BusinessNumber=cont.BusinessNumber,
                               IsActive = cont.IsActive,
                           }).ToList();
        }


   
      
        public List<ClientViewModel> GetClientListfilter(string filterName, string filterBusinessName, DateTime? filterBirthDate)
        {
            var clientList = _context.Clients.Where(x => x.IsDeleted == false
                                                 && (string.IsNullOrEmpty(filterName) || x.Name.ToLower().Contains(filterName.ToLower()))
                                                 && (string.IsNullOrEmpty(filterBusinessName) || x.BusinessName.ToLower().Contains(filterBusinessName.ToLower()))
                                                  && (!filterBirthDate.HasValue || x.BirthDate == filterBirthDate.Value)
                                                
                                                 ).Select(cont=> new ClientViewModel()
                                                 {
                                                     ClientId = cont.ClientId,
                                                     Name = cont.Name,
                                                     Number = cont.Number,
                                                     BusinessName = cont.BusinessName,
                                                     IsActive = cont.IsActive,
                                                 }



            ).ToList();
         

            return clientList;

        }

        #endregion

        #region Delete
        /// <summary>
        /// Delete Client details from database on ClientId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteClientAsync(int ClientId)
        {
            Client d = await _context.Clients.Where(x => x.ClientId == ClientId).FirstOrDefaultAsync();
            if (d != null)
            {
                d.IsDeleted = true;
                _context.Clients.Update(d);
                await _context.SaveChangesAsync();
            }
        }
        #endregion

        #region Client_Add
        /// <summary>
        /// Add Client Details
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void AddClient(ClientViewModel model)
        {
            try
            {
                var Client = new Client();
                Client.Name = model.Name;
                Client.Number = model.Number;
                Client.Email = model.Email;
                Client.BirthDate = model.BirthDate;
                Client.Address = model.Address;
                Client.BusinessName = model.BusinessName;
                Client.BusinessNumber = model.BusinessNumber;
                Client.Category = model.Category;
                Client.AdditionInformation = model.AdditionInformation;
                Client.RefferenceDetails = model.RefferenceDetails;
                Client.IsActive = model.IsActive;
                Client.IsDeleted = false;
                Client.Created = DateTime.Now;
                _context.Clients.Add(Client);
                _context.SaveChanges();

                Client.AdditionInformation = Helper.UploadClientAdditionalInfo(model.Additioninfo, Client.ClientId, "Client", "Information.pdf");
                Client.CreatedBy = Client.ClientId;

                _context.SaveChanges();

            }
            catch (Exception e)
            {
                return;
            }



        }
        #endregion

        #region Client_Edit
        /// <summary>
        /// Update Client Details By ClientId
        /// </summary>
        /// <param name="model"></param>
        public void UpdateClient(ClientViewModel model)
        {
            try
            {
                Client client = _context.Clients
                                .Where(x => x.ClientId == model.ClientId)
                                .FirstOrDefault();
                if (client != null)
                {
                    client.Name = model.Name;
                    client.Number = model.Number;
                    client.Email = model.Email;
                    client.BirthDate = model.BirthDate;
                    client.Address = model.Address;
                    client.BusinessName = model.BusinessName;
                    client.BusinessNumber = model.BusinessNumber;
                    client.Category = model.Category;
                    client.AdditionInformation = model.AdditionInformation;
                    client.RefferenceDetails = model.RefferenceDetails;
                    client.IsActive = model.IsActive;
                    client.Modified = DateTime.Now;
                    client.ModifiedBy = model.ClientId;
                    if (model.Additioninfo != null)
                        client.AdditionInformation = Helper.UploadClientAdditionalInfo(model.Additioninfo, client.ClientId, "Client", "Information.pdf");

                    _context.Clients.Update(client);
                    _context.SaveChanges();
                }

            }
            catch (Exception e) { return; }
        }
        #endregion

        #region GetClientById
        /// <summary>
        ///  Get Only one Client Details By ClientId
        /// </summary>
        /// <param name="ClientId"></param>
        /// <returns></returns>
        public ClientViewModel GetClientDetails(int ClientId)
        {

            var Client = _context.Clients
                         .FirstOrDefault(x => x.ClientId == ClientId);
            if (Client != null)
            {
                return new ClientViewModel()
                {
                    ClientId = Client.ClientId,
                    Name = Client.Name,
                    Number = Client.Number,
                    Email = Client.Email,
                    BirthDate = Client.BirthDate,
                    Address = Client.Address,
                    BusinessName = Client.BusinessName,
                    BusinessNumber = Client.BusinessNumber,
                    Category = Client.Category,
                    RefferenceDetails = Client.RefferenceDetails,
                    IsActive = Client.IsActive,
                    AdditionInformation=Client.AdditionInformation,
                };
            }
            return new ClientViewModel();


        }
        #endregion

      
    }
}
