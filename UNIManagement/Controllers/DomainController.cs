
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using UNIManagement.Entities.DataModels;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.Repository.InterFace;

namespace UNIManagement.Controllers
{
    public class DomainController : BaseController
    {
        #region Constructor
        private readonly IDomainRepository _domainRepository;
        private readonly IClientRepository _clientRepository;
        public DomainController(IDomainRepository domainRepository, IClientRepository clientRepository)
        {
            _domainRepository = domainRepository;
            _clientRepository = clientRepository;
        }
        #endregion

        #region Index
        /// <summary>
        /// Get Data in DropDown
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            ViewBag.ClientDropDown = _clientRepository.GetClientList();
            return View();
        }
        #endregion

        #region Domain_List
        /// <summary>
        /// get list of all records and filtered record
        /// </summary>
        /// <returns></returns>
        public IActionResult GetDomainList(string filterName, int? filterclientname, DateTime? filterPurchaseDate, string filterIsActive)
        {
            filterName = filterName == null ? "" : filterName;

            List<DomainViewModel> list = _domainRepository.GetDomainListfilter(filterName, filterclientname, filterPurchaseDate, filterIsActive);
            return PartialView("_Partial_DomainList", list);
        }
      
        #endregion

        #region ViewModal
        /// <summary>
        /// View Modal
        /// </summary>
        /// <returns></returns>
        public IActionResult View(int id)
        {
            var domain = _domainRepository.GetDomianDetails(id);
            return PartialView("_DomainView", domain);
        }
        #endregion

        #region Delete
        public async Task<IActionResult> Delete(int id)
        {
            await _domainRepository.DeleteDomainAsync(id);
            return RedirectToAction("Index");
        }
        #endregion

        #region AddEdit
        public IActionResult DomainForm()
        {
            ViewBag.ClientDropDown = _clientRepository.GetClientList();
            return View();
        }
        /// <summary>
        /// Add and Update Condition
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Get Client Details By CLientId
        /// </summary>
        /// <returns></returns>
        public IActionResult Update(int id)
        {
            ViewBag.ClientDropDown = _clientRepository.GetClientList();
            var Domain = _domainRepository.GetDomianDetails((int)id);
            return View("DomainForm", Domain);
        }
        #endregion
     
        #endregion

    }
}
