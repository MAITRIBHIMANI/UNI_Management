
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using UNIManagement.Entities.DataModels;
using UNIManagement.Entities.ViewModel;
using UNIManagement.Repositories.Repository.InterFace;
using Newtonsoft.Json;
namespace UNIManagement.Controllers
{
    public class DomainController : BaseController
    {
        private readonly HttpClient _httpClient;
     
        #region Constructor
        private readonly IDomainRepository _domainRepository;
        private readonly IClientRepository _clientRepository;
        public DomainController(IDomainRepository domainRepository, IClientRepository clientRepository)
        {
            _domainRepository = domainRepository;
            _clientRepository = clientRepository;
            _httpClient = new HttpClient();
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
        public async Task<IActionResult> Demo()
        {
            string url = "https://drive.google.com/drive/folders/1TaWsSc9BCa3dRh_0mSALhxSHZw3G753a"; // Replace with your Google Apps Script URL
            var response = await _httpClient.GetStringAsync(url);

           ViewBag.demo = response;
            //var files = JsonConvert.DeserializeObject<List<GoogleDriveFile>>(response);

            return View(response);

        }
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
            else
            {
                return View("DomainForm");

            }

            return RedirectToAction("Index");
        }
        #region Update
        /// <summary>
        /// Get Client Details By CLientId
        /// </summary>
        /// <returns></returns>
    
        [HttpGet, Route("domain/update/", Name = "DomainUpdate")]
        public IActionResult Update(int id)
        {
            ViewBag.ClientDropDown = _clientRepository.GetClientList();
            var Domain = _domainRepository.GetDomianDetails((int)id);
            return View("DomainForm", Domain);
        }
        #endregion
     
        #endregion

    }
    public class GoogleDriveFile
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string MimeType { get; set; }
    }
}
