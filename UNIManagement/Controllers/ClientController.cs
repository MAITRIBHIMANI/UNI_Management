 
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.Repository;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Controllers
{
    public class ClientController : BaseController
    {
       
        private readonly IClientRepository _clientRepository;
        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        #region List
        public IActionResult Index()
        {
            ViewBag.BusinessNameDropDown = _clientRepository.GetClientList();
            //List<ClientViewModel> list = _clientRepository.GetClientList();
            return View("ClientList");
        }
        #endregion
        public IActionResult View(int id)
        {
            var client = _clientRepository.GetClientDetails(id);
            return PartialView("_ClientView", client);
        }
       
        #region Client_List
        public IActionResult GetClientList(string filterName, string filterBusinessName, DateTime? filterBirthDate)
        {
            filterName = filterName == null ? "" : filterName;

            List<ClientViewModel> list = _clientRepository.GetClientListfilter(filterName, filterBusinessName, filterBirthDate);
            return PartialView("_Partial_ClientList", list);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            await _clientRepository.DeleteClientAsync(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region AddEdit
        public IActionResult AddEdit(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ClientId > 0)
                {
                    _clientRepository.UpdateClient(model);

                }
                else
                {
                    _clientRepository.AddClient(model);
                }
            }
            return RedirectToAction("Index");

        }
        #region Update
        public IActionResult Update(int id)
        {
            var Client = _clientRepository.GetClientDetails((int)id);
            return View("ClientForm", Client);

        }
        #endregion

        public IActionResult ClientForm()
        {
            return View();
        }
        #endregion

       
    }
}
