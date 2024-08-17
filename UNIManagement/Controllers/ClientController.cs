using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.Repository;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Controllers
{
    public class ClientController : BaseController
    {
        #region Constructor
        private readonly IClientRepository _clientRepository;
        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        #endregion

        #region List 
        /// <summary>
        /// Get DropDown Data
        /// </summary>
        /// <returns></returns>

        public IActionResult Index()
        {
            ViewBag.BusinessNameDropDown = _clientRepository.GetClientList();          
            return View("ClientList");
        }

        /// <summary>
        /// List of Data, Filtered Data
        /// </summary>
        /// <returns></returns>
        
        public IActionResult GetClientList(string filterName, string filterBusinessName, DateTime? filterBirthDate, string filterIsActive)
        {
            filterName = filterName == null ? "" : filterName;
            List<ClientViewModel> list = _clientRepository.GetClientListfilter(filterName, filterBusinessName, filterBirthDate, filterIsActive);
            return PartialView("_Partial_ClientList", list);
        }
        #endregion

        #region View
        /// <summary>
        /// View Model
        /// </summary>
        /// <returns></returns>
        public IActionResult View(int id)
        {
            var client = _clientRepository.GetClientDetails(id);
            return PartialView("_ClientView", client);
        }
        #endregion      

        #region Delete
        /// <summary>
        /// Delete Record By ClientId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int id)
        {
            await _clientRepository.DeleteClientAsync(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region AddEdit
        /// <summary>
        /// ClientFrom
        /// </summary>
        /// <returns></returns>
        public IActionResult ClientForm()
        {
            return View();
        }
        /// <summary>
        /// Add and Update Condition 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
         
        public IActionResult AddEdit(ClientViewModel model)
        {
            if (ModelState.IsValid)
                if (model.ClientId > 0)
                    _clientRepository.UpdateClient(model);
                else
                    _clientRepository.AddClient(model);
            else
                return View("ClientForm");

            return RedirectToAction("Index");
        }
        #region Update
        /// <summary>
        /// GetData by clientId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
           [HttpGet, Route("client/update", Name = "ClientUpdate")]

        public IActionResult Update(int id)
        {
            var Client = _clientRepository.GetClientDetails((int)id);
            return View("ClientForm", Client);
        }
        #endregion     

        #endregion

       
    }
}
