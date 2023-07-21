using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QLDV.ActionFilter;
using QLDV.Areas.QuanLy.Models;
using QLDV.Data;


namespace QLDV.Areas.QuanLy.Controllers
{
    [CheckSession]
    public class DuyetController : Controller
    {
        
        private readonly IWebHostEnvironment webHostEnvironment;
        QldvContext qldvContext = new QldvContext();
        public DuyetController(IWebHostEnvironment webHost)
        {
            webHostEnvironment = webHost;
        }
        [Area("QuanLy")]
        [Route("[area]/[controller]/[action]/{id?}")]

        public IActionResult Index()
        {
            List<DuyetViewModel> ViewModel = new List<DuyetViewModel>();
            var LogId = HttpContext.Session.GetInt32("LoginId");

            var LogRank = HttpContext.Session.GetInt32("Rank");
            if (LogRank == 4)
            {
               
                var query = (from a in qldvContext.EventUsers
                             join d in qldvContext.Users on a.UserId equals d.Id
                             join b in qldvContext.Events on a.EventId equals b.Id
                             join c in qldvContext.Semesters on b.SemesterId equals c.Id

                             select new
                             {
                               IDs =   a.Id,
                               Users =  d.Fullname,
                               Events =  b.Title,
                               Semsters =  c.Title,
                               Status = a.Status
                             });

                foreach  (var item in query)
                {
                    DuyetViewModel temp = new DuyetViewModel();
                    temp.Id = item.IDs;
                    temp.User = item.Users;
                    temp.Event = item.Events;
                    temp.Semester = item.Semsters;
                    temp.Status = item.Status;

                    ViewModel.Add(temp);
                }
                             
                
                return View(ViewModel);
            }
            else if (LogRank == 3)
            {
                
                var Us = qldvContext.Users.Where(u => u.Id == LogId).First();
                var Classes = qldvContext.Classes.Where(c => c.Id == Us.ClassId).First();
                var fac = qldvContext.Faculties.Where(f => f.Id == Classes.FacultyId).First();

                List<User> users = new List<User>();
                List<Class> ClassesList = qldvContext.Classes.Where(c => c.FacultyId == fac.Id).ToList();

                foreach (var c in ClassesList)
                {
                    var temp = qldvContext.Users.Where(u => u.ClassId == c.Id).ToList();
                    if (temp != null)
                    {
                        users.AddRange(temp);
                    }
                }

                foreach ( var u in users)
                {                 
                    var query = (from a in qldvContext.EventUsers
                                 join d in qldvContext.Users on a.UserId equals d.Id
                                 join b in qldvContext.Events on a.EventId equals b.Id
                                 join c in qldvContext.Semesters on b.SemesterId equals c.Id

                                 select new
                                 {
                                     IDs = a.Id,
                                     UserId = d.Id,
                                     Users = d.Fullname,
                                     Events = b.Title,
                                     Semsters = c.Title,
                                     Status = a.Status
                                 }).Where(x => x.UserId == u.Id).ToList();

                    foreach (var item in query)
                    {
                        DuyetViewModel temp = new DuyetViewModel();
                        temp.Id = item.IDs;
                        temp.User = item.Users;
                        temp.Event = item.Events;
                        temp.Semester = item.Semsters;
                        temp.Status = item.Status;

                        ViewModel.Add(temp);
                    }
                }


                return View(ViewModel);
            }
            else
            {
                var Us = qldvContext.Users.Where(u => u.Id == LogId).First();
                var Classes = qldvContext.Classes.Where(c => c.Id == Us.ClassId).First();
                List<User> users = new List<User>();
                users = qldvContext.Users.Where(u => u.ClassId == Classes.Id).ToList();
                foreach (var u in users)
                {
                    var query = (from a in qldvContext.EventUsers
                                 join d in qldvContext.Users on a.UserId equals d.Id
                                 join b in qldvContext.Events on a.EventId equals b.Id
                                 join c in qldvContext.Semesters on b.SemesterId equals c.Id

                                 select new
                                 {
                                     IDs = a.Id,
                                     UserId = d.Id,
                                     Users = d.Fullname,
                                     Events = b.Title,
                                     Semsters = c.Title,
                                     Status = a.Status
                                 }).Where(x => x.UserId == u.Id).ToList();

                    foreach (var item in query)
                    {
                        DuyetViewModel temp = new DuyetViewModel();
                        temp.Id = item.IDs;
                        temp.User = item.Users;
                        temp.Event = item.Events;
                        temp.Semester = item.Semsters;
                        temp.Status = item.Status;

                        ViewModel.Add(temp);
                    }
                }
                return View(ViewModel);
            }

            
        }

        public IActionResult Edit(int id)
        {
            var EventUser = qldvContext.EventUsers.Where(x => x.Id == id).FirstOrDefault();

            return View("/Areas/QuanLy/Views/Duyet/Edit.cshtml", EventUser);
        }

        public IActionResult Delete(int id)
        {
            var EventUser = qldvContext.EventUsers.Where(x => x.Id == id).FirstOrDefault();

            if (EventUser != null)
            {
                deleteFile(EventUser.Image);
                qldvContext.Remove(EventUser);
                qldvContext.SaveChanges();
            }
            return RedirectToAction("Index", "Duyet", new { area = "QuanLy" });
        }

        [HttpPost]
        public IActionResult Edit( EventUser input , int id)
        {
            var EventUser = qldvContext.EventUsers.Where(x => x.Id == id).FirstOrDefault();

            if (EventUser != null)
            {
                EventUser.NoteReviewer = input.NoteReviewer;
                EventUser.Status = input.Status;

                qldvContext.EventUsers.Update(EventUser);
                qldvContext.SaveChanges(); 
            }
            return RedirectToAction("Index", "Duyet", new { area = "QuanLy" });
        }

        private void deleteFile(string file)
        {
            string fileName = file;
            if (fileName != null)
            {
                string deletePath = Path.Combine(webHostEnvironment.WebRootPath, "Users/Images/EventUser", fileName);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }

            }
        }
    }
}
