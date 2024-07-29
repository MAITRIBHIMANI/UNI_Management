
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using UNIManagement.Entities.DataModels;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Controllers
{
    public class DomainController : BaseController
    {
        private readonly IDomainRepository _domainRepository;
        private readonly IClientRepository _clientRepository;
        public DomainController(IDomainRepository domainRepository, IClientRepository clientRepository)
        {
            _domainRepository = domainRepository;
            _clientRepository = clientRepository;
        }
        #region Index
        public IActionResult Index()
        {
            ViewBag.ClientDropDown = _clientRepository.GetClientList();
            //List<DomainViewModel> list = _domainRepository.GetDomainList();
            return View();
        }
        #endregion

        #region Domain_List
        public IActionResult GetDomainList(string filterName, string filterclientname, DateTime? filterPurchaseDate)
        {
            filterName = filterName == null ? "" : filterName;

            List<DomainViewModel> list = _domainRepository.GetDomainListfilter(filterName, filterclientname, filterPurchaseDate);
            return PartialView("_Partial_DomainList", list);
        }
        #endregion

        public IActionResult View(int id)
        {

            var domain = _domainRepository.GetDomianDetails(id);
            return PartialView("_DomainView", domain);
        } 
        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            await _domainRepository.DeleteDomainAsync(id);
            return RedirectToAction("Index");
        }
        #endregion
        #region AddEdit
        public IActionResult AddEdit(DomainViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.DomainId > 0)
                    _domainRepository.UpdateDomain(model);
                else
                    _domainRepository.AddDomain(model);
            }

            return RedirectToAction("Index");



        }
        #region Update
        public IActionResult Update(int id)
        {
            var Domain = _domainRepository.GetDomianDetails((int)id);
            return View("DomainForm", Domain);
        }

        #endregion
        public IActionResult DomainForm()
        {
            return View();
        }
        #endregion



    }
}
