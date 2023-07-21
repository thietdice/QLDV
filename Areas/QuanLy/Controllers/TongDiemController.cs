using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using QLDV.Areas.QuanLy.Models;
using QLDV.Data;

namespace QLDV.Areas.QuanLy.Controllers
{
    public class TongDiemController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        QldvContext qldvContext = new QldvContext();
        public TongDiemController(IWebHostEnvironment webHost)
        {
            webHostEnvironment = webHost;
        }
        [Area("QuanLy")]
        [Route("[area]/[controller]/[action]/{id?}")]
        public IActionResult Index()
        {
            ViewBag.Semester = new SelectList (qldvContext.Semesters.ToList() , "Id" , "Title");
            
            return View();
        }

        [HttpPost]
        public IActionResult TimKiem (int Ki)
        {
            ViewBag.Semester = new SelectList(qldvContext.Semesters.ToList(), "Id", "Title");

            List<TongDiemModel> TongDiem = new List<TongDiemModel>();

            var query = (from a in qldvContext.EventUsers
                         join b in qldvContext.Users on a.UserId equals b.Id
                         join c in qldvContext.Events on a.EventId equals c.Id

                         select new
                         {

                             UserId = b.Id,
                             Users = b.Fullname,
                             Score = c.Score,
                             Status = a.Status,
                             Semsters = c.SemesterId,

                         }).Where(x=>x.Semsters == Ki).ToList();
                         
            var user = query.Select(x=>x.UserId).Distinct();
            
            foreach ( var item in user )
            {
                TongDiemModel temp = new TongDiemModel()
                {
                    UserId = item
                };
                TongDiem.Add(temp);
            }

            foreach (var item in TongDiem )
            {
               foreach (var x in query)
                {
                    if (item.UserId == x.UserId)
                    {
                        item.UserFullName = x.Users;
                        if (x.Status == "Thông qua")
                        {
                            item.Count = item.Count + 1;
                            item.TongDiem = item.TongDiem + x.Score;
                        }
                    }
                }
            }

                return View("/Areas/QuanLy/Views/TongDiem/Diem.cshtml", TongDiem);
        }

   
    }
}
