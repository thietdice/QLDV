using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLDV.ActionFilter;
using QLDV.Data;

namespace QLDV.Areas.QuanLy.Controllers
{
    [CheckSession]
    public class LienChiDoanController : Controller
    {
        QldvContext qldvContext = new QldvContext();
        [Area("QuanLy")]
        [Route("[area]/[controller]/[action]/{id?}")]
        public IActionResult Index()
        {
            List<Faculty> fac = new List<Faculty>();

            using( var db = new QldvContext())
            {
                fac = db.Faculties.ToList();
            }
            return View(fac);
        }

        //public IActionResult LienChiDoan()
        // {
        //    return View("/Areas/QuanLy/Views/LienChiDoan/LienChiDoan.cshtml");
        // }

        public IActionResult Add()
        {
            return View("/Areas/QuanLy/Views/LienChiDoan/Add.cshtml");
        }

   
        public IActionResult Edit(int id)
        {

            Faculty fac = qldvContext.Faculties.Where(u => u.Id == id).First();
            
            return View("/Areas/QuanLy/Views/LienChiDoan/Edit.cshtml", fac);
        }

        public IActionResult Details(int id)
        {

			Faculty fac = qldvContext.Faculties.Where(u => u.Id == id).SingleOrDefault();
            var User = qldvContext.Users.ToList();
            ViewBag.User = User;
            return View("/Areas/QuanLy/Views/LienChiDoan/Details.cshtml", fac);
        }

		public IActionResult Delete (int id)
		{

			Faculty fac = qldvContext.Faculties.Where(u => u.Id == id).SingleOrDefault();

            qldvContext.Remove(fac);
            qldvContext.SaveChanges();

			return RedirectToAction("Index", "LienChiDoan", new { area = "QuanLy" });
		}

		[HttpPost]
        public IActionResult Add(Faculty input) 
        {
            QldvContext db = new QldvContext();

            var lcd = new Faculty()
            {
                
                Title = input.Title,
                ShortTitle = input.ShortTitle,
                Description = input.Description,
                CreatedAt = DateTime.Now,
                UseridCreated = HttpContext.Session.GetInt32("LoginId")
            };

			db.Faculties.Add(lcd);
			db.SaveChanges();

            return RedirectToAction("Index", "LienChiDoan", new { area = "QuanLy" });
        }
        [HttpPost]
        public IActionResult Edit(Faculty input)
        {

			Faculty fac = qldvContext.Faculties.Where(u => u.Id == input.Id).SingleOrDefault();
			if (fac != null) 
            {
                fac.Title = input.Title;
                fac.ShortTitle = input.ShortTitle;
                fac.Description = input.Description;
                fac.UpdatedAt = DateTime.Now;
                fac.UseridUpdated = HttpContext.Session.GetInt32("LoginId");
				qldvContext.Faculties.Update(fac);
			}

			
			qldvContext.SaveChanges();

			return RedirectToAction("Index", "LienChiDoan", new { area = "QuanLy" });
        }

    }
}
