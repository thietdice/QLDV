using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI;
using QLDV.Data;
using QLDV.Models;

namespace QLDV.Controllers
{

	public class HomeController : Controller
	{
		private  readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}
       
        public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Index(LoginInformation loginf)
		{
			QldvContext DBContext = new QldvContext();
			User data = new User();
			var Rank = DBContext.UserCatalogues.ToList();

			if (int.Parse(loginf.Type) == 1)
			{
			 	data = DBContext.Users.Where(m=>m.Email == loginf.Email && m.Password == loginf.Password && m.UserCatalogueId == 1).FirstOrDefault();
				if ( data  != null )
				{
					foreach( var r in Rank)
					{
						if (r.Id == data.UserCatalogueId)
						{
							HttpContext.Session.SetString("RankTitle", r.Title);
						}
					}
					HttpContext.Session.SetInt32("Rank", data.UserCatalogueId);
					HttpContext.Session.SetInt32("LoginId", data.Id);
                    HttpContext.Session.SetString("LoginName", data.Fullname);
                    return RedirectToAction("Index", "Home", new { area = "DoanVien" });
				}
				else
				{
					ViewBag.LoginStatus = 0;
					return View();
				}
			}
			else
			{
				data = DBContext.Users.Where(m => m.Email == loginf.Email && m.Password == loginf.Password && m.UserCatalogueId != 1).FirstOrDefault();
				if (data != null)
				{
                    foreach (var r in Rank)
                    {
                        if (r.Id == data.UserCatalogueId)
                        {
                            HttpContext.Session.SetString("RankTitle", r.Title);
                        }
                    }
                    HttpContext.Session.SetInt32("Rank", data.UserCatalogueId);
                    HttpContext.Session.SetInt32("LoginId", data.Id);
                    HttpContext.Session.SetString("LoginName", data.Fullname);
                    return RedirectToAction("Index", "Home", new { area = "QuanLy" });
				}
				else
				{
					ViewBag.LoginStatus = 0;
					return View();
				}
			}
		}

		
	}
}