using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using QLDV.ActionFilter;
using QLDV.Data;

namespace QLDV.Areas.QuanLy.Controllers
{
    [CheckSession]
    public class ChiDoanController : Controller
    {
        QldvContext qldvContext = new QldvContext();
        [Area("QuanLy")]
        [Route("[area]/[controller]/[action]/{id?}")]
        public IActionResult Index()
        {
            List<Class> cl = new List<Class>();
            List<Faculty> fac = qldvContext.Faculties.ToList();
            ViewBag.Faculties = fac;
            

            var LogId = HttpContext.Session.GetInt32("LoginId");

            var LogRank = HttpContext.Session.GetInt32("Rank");

            if ( LogRank == 4)
            {
                
                cl = qldvContext.Classes.ToList();

                return View(cl);
            }
            else
            {
                var User = qldvContext.Users.Where(u => u.Id == LogId).First();
                var Classes = qldvContext.Classes.Where(c => c.Id == User.ClassId).First();

                cl = qldvContext.Classes.Where(c=>c.FacultyId == Classes.FacultyId).ToList();

                return View(cl);
            }

           
        }

        public IActionResult Add()
        {
           List<Faculty> fac = qldvContext.Faculties.ToList();

            ViewBag.Faculties = new SelectList(fac, "Id", "Title");
            
            return View("/Areas/QuanLy/Views/ChiDoan/Add.cshtml");
        }
        public IActionResult Edit(int id)
        {
            List<Faculty> fac = qldvContext.Faculties.ToList();

            ViewBag.Faculties = new SelectList(fac, "Id", "Title");
            var cl = qldvContext.Classes.Where(u => u.Id == id).First();

            return View("/Areas/QuanLy/Views/ChiDoan/Edit.cshtml", cl);
        }

        public IActionResult Delete(int id)
        {

            var cl = qldvContext.Classes.Where(u => u.Id == id).SingleOrDefault();

            qldvContext.Remove(cl);
            qldvContext.SaveChanges();

            return RedirectToAction("Index", "ChiDoan", new { area = "QuanLy" });
        }

        public IActionResult Details(int id)
        {
            Class cd = qldvContext.Classes.Where(u => u.Id == id).SingleOrDefault();
            var User = qldvContext.Users.ToList();
            ViewBag.User = User;
            var fac = qldvContext.Faculties.ToList();
            ViewBag.Faculties = fac;

            return View("/Areas/QuanLy/Views/ChiDoan/Details.cshtml", cd);
        }

        [HttpPost]
        public IActionResult Add(Class input)
        {

            var cd = new Class()
            {
                FacultyId = input.FacultyId,
                Title = input.Title,
                Description = input.Description,
                CreatedAt = DateTime.Now,
                UseridCreated = HttpContext.Session.GetInt32("LoginId")
            };

            qldvContext.Add(cd);
            qldvContext.SaveChanges();

            return RedirectToAction("Index", "ChiDoan", new { area = "QuanLy" });
        }

        [HttpPost]
        public IActionResult Edit(Class input)
        {
            
            var cd  = qldvContext.Classes.Where(u => u.Id == input.Id).SingleOrDefault();
            if (cd != null)
            {
                cd.Title = input.Title;
                cd.FacultyId = input.FacultyId;
                cd.Description = input.Description;
                cd.UpdatedAt = DateTime.Now;
                cd.UseridUpdated = HttpContext.Session.GetInt32("LoginId");
                qldvContext.Classes.Update(cd);
            }

            qldvContext.SaveChanges();

            return RedirectToAction("Index", "ChiDoan", new { area = "QuanLy" });
        }
    }
}
