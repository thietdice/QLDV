using System.Net;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using QLDV.ActionFilter;
using QLDV.Areas.DoanVien.Models;
using QLDV.Areas.QuanLy.Controllers;
using QLDV.Areas.QuanLy.Models;
using QLDV.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace QLDV.Areas.DoanVien.Controllers
{
    [CheckSession]
    [Area("DoanVien")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class XemDiemController : Controller
    {
        QldvContext qldvContext = new QldvContext();
        public IActionResult Index()
        {
            DiemRenLuyen result = new DiemRenLuyen();

            result.tab1s = Tongdiem();
            result.tab2s = Tab2();
            result.tab3s = Tab3();
            result.tab4s = Tab4();
         
            return View(result);
        }
        public List<Models.TongDiem> Tongdiem()
        {
            List<Models.TongDiem> view = new List<Models.TongDiem>();

            var id = HttpContext.Session.GetInt32("LoginId");
            var result = (from eu in qldvContext.EventUsers
                          join e in qldvContext.Events on eu.EventId equals e.Id
                          join s in qldvContext.Semesters on e.SemesterId equals s.Id
                          where eu.UserId == id
                          orderby eu.Id
                          select new
                          {
                              eu.Id,
                              semTitle = s.Title,
                              eveTitle = e.Title,
                              e.Score,
                              eu.Status
                          });


            var Sem = result.Select(x => x.semTitle).Distinct().ToList();

            for (var i = 0; i < Sem.Count; i++)
            {
                Models.TongDiem temp = new Models.TongDiem()
                {
                    Id = i,
                    SemTitle = Sem[i],
                    Count = 0,
                    Total = 0,
                    MaxTotal = 0

                };

                view.Add(temp);

            }

            foreach (var x in view)
            {
                foreach (var y in result)
                {
                    if (x.SemTitle == y.semTitle)
                    {
                        if (y.Status == "Thông qua")
                        {
                            x.Total = x.Total + y.Score;
                        }
                        x.Count = x.Count + 1;
                        x.MaxTotal = x.MaxTotal + y.Score;
                    }

                }
            }

            return (view);
        }

        public List<Models.SuKienThamGia> Tab2()
        {
            List<Models.SuKienThamGia> view = new List<Models.SuKienThamGia>();

            var id = HttpContext.Session.GetInt32("LoginId");
            var result = (from a in qldvContext.EventUsers
                          join b in qldvContext.Events on a.EventId equals b.Id
                          join c in qldvContext.Semesters on b.SemesterId equals c.Id
                          where a.UserId == id && a.Status == "Chưa duyệt"
                          orderby a.Id
                          select new
                          {
                              Id = a.Id,
                              Title = b.Title,
                              Semester = c.Title,
                              Score = b.Score,
                              Image = b.Image
                          });

            foreach( var a in result )
            {
                var temp = new Models.SuKienThamGia();
                temp.Id = a.Id;
                temp.Title = a.Title;
                temp.Semester = a.Semester;
                temp.Score = a.Score;
                temp.Image = a.Image;
                view.Add(temp);
            }     

            return (view);
        }

        public List<Models.SuKienThamGia> Tab3()
        {
            List<Models.SuKienThamGia> view = new List<Models.SuKienThamGia>();

            var id = HttpContext.Session.GetInt32("LoginId");
            var result = (from a in qldvContext.EventUsers
                          join b in qldvContext.Events on a.EventId equals b.Id
                          join c in qldvContext.Semesters on b.SemesterId equals c.Id
                          where a.UserId == id && a.Status == "Thông qua"
                          orderby a.Id
                          select new
                          {
                              Id = a.Id,
                              Title = b.Title,
                              Semester = c.Title,
                              Score = b.Score,
                              Image = b.Image
                          });

            foreach (var a in result)
            {
                var temp = new Models.SuKienThamGia();
                temp.Id = a.Id;
                temp.Title = a.Title;
                temp.Semester = a.Semester;
                temp.Score = a.Score;
                temp.Image = a.Image;
                view.Add(temp);
            }

            return (view);
        }

        public List<Models.SuKienThamGia> Tab4()
        {
            List<Models.SuKienThamGia> view = new List<Models.SuKienThamGia>();

            var id = HttpContext.Session.GetInt32("LoginId");
            var result = (from a in qldvContext.EventUsers
                          join b in qldvContext.Events on a.EventId equals b.Id
                          join c in qldvContext.Semesters on b.SemesterId equals c.Id
                          where a.UserId == id && a.Status == "Từ chối"
                          orderby a.Id
                          select new
                          {
                              Id = a.Id,
                              Title = b.Title,
                              Semester = c.Title,
                              Score = b.Score,
                              Image = b.Image
                          });

            foreach (var a in result)
            {
                var temp = new Models.SuKienThamGia();
                temp.Id = a.Id;
                temp.Title = a.Title;
                temp.Semester = a.Semester;
                temp.Score = a.Score;
                temp.Image = a.Image;
                view.Add(temp);
            }

            return (view);
        }
    }

    
}


