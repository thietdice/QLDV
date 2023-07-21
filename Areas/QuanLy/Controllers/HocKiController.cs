using Microsoft.AspNetCore.Mvc;
using QLDV.Data;
using QLDV.ActionFilter;


namespace QLDV.Areas.QuanLy.Controllers
{
    [CheckSession]

    public class HocKiController : Controller
    {
        QldvContext qldvContext = new QldvContext();
        [Area("QuanLy")]
        [Route("[area]/[controller]/[action]/{id?}")]
        public IActionResult Index()
        {
            var hocki = qldvContext.Semesters.ToList();

            return View(hocki);
        }

        public IActionResult Add()
        {

            return View("/Areas/QuanLy/Views/HocKi/Add.cshtml");
        }

        public IActionResult Edit(int id)
        {
            var hocki = qldvContext.Semesters.Where(a => a.Id == id).FirstOrDefault();

            return View("/Areas/QuanLy/Views/HocKi/Edit.cshtml", hocki);
        }

        public IActionResult Details(int id)
        {
            var hocki = qldvContext.Semesters.Where(a => a.Id == id).FirstOrDefault();
            var Users = qldvContext.Users.ToList();
            ViewBag.User = Users;

            return View("/Areas/QuanLy/Views/HocKi/Details.cshtml", hocki);
        }

        public IActionResult Delete(int id)
        {

            var hocki = qldvContext.Semesters.Where(a => a.Id == id).FirstOrDefault();
     
            qldvContext.Semesters.Remove(hocki);
            qldvContext.SaveChanges();


            return RedirectToAction("Index", "HocKi", new { area = "QuanLy" });
        }


        [HttpPost]

        public IActionResult Add(Semester se)
        {
           

            Semester sem = new Semester()
            {
                Title = se.Title,
                DayStart = se.DayStart,
                DayEnd = se.DayEnd,
                CreatedAt = DateTime.Now,
                UseridCreated = HttpContext.Session.GetInt32("LoginId")
            };

            qldvContext.Add(sem);
            qldvContext.SaveChanges();

            return RedirectToAction("Index", "HocKi", new { area = "QuanLy" });
        }
        [HttpPost]
        public IActionResult Edit(Semester se, int id)
        {

            var hocki = qldvContext.Semesters.Where(a => a.Id == id).FirstOrDefault();

            hocki.Title = se.Title;
            hocki.DayStart = se.DayStart;
            hocki.DayEnd = se.DayEnd;
            hocki.UpdatedAt = DateTime.Now;
            hocki.UseridUpdated = HttpContext.Session.GetInt32("LoginId");

 
            qldvContext.Semesters.Update(hocki);
            qldvContext.SaveChanges();

            return RedirectToAction("Index", "HocKi", new { area = "QuanLy" });
        }
    }
}
