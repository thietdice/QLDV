using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QLDV.ActionFilter;
using QLDV.Areas.DoanVien.Models;
using QLDV.Data;

namespace QLDV.Areas.QuanLy.Controllers
{


    [CheckSession]
    public class HomeController : Controller
	{
        QldvContext qldvContext = new QldvContext();
        [Area("QuanLy")]
        [Route("QuanLy/[controller]/[action]/{id?}")]
        // GET: HomeController
        public ActionResult Index()
		{
            var SoNguoi = qldvContext.Users.Count();
            var SoBaiDang = qldvContext.Articles.Count();
            var SuKien = qldvContext.Events.Count();
            var LienChi = qldvContext.Faculties.Count();
            var ChiDoan = qldvContext.Classes.Count();
            int LuongTruyCap = 0;
            var BaiDang = qldvContext.Articles.ToList();
            foreach ( var item in BaiDang) 
            {
                LuongTruyCap += item.Viewed.Value;
            }

            ViewBag.SoNguoi = SoNguoi;
            ViewBag.SoBaiDang = SoBaiDang;
            ViewBag.SuKien = SuKien;
            ViewBag.LuongTruyCap = LuongTruyCap;
            ViewBag.SoLien = LienChi;
            ViewBag.SoChi = ChiDoan;

            return View();
		}

      



        public IActionResult DangXuat()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home", new { area = "" });
        }

    }
}
