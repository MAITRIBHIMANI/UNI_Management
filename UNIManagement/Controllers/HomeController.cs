using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UNIManagement.Models;
using UNIManagement.Repositories.Repository.InterFace;
using UNIManagement.Entities.DataModels;
namespace UNIManagement.Controllers
{
	public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
		
		private readonly IDomainRepository _domainRepository;

		public HomeController(ILogger<HomeController> logger , IDomainRepository domainRepository)
		{
			_logger = logger;	
			_domainRepository = domainRepository;
		}

		public IActionResult Index()
		{
			return View();
		}
				
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
