using Microsoft.AspNetCore.Mvc;
using QLDV.Data;
using QLDV.ActionFilter;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using static System.Formats.Asn1.AsnWriter;

namespace QLDV.Areas.QuanLy.Controllers
{
    [CheckSession]

    public class SuKienController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        QldvContext qldvContext = new QldvContext();

        public SuKienController(IWebHostEnvironment webHost)
        {
            webHostEnvironment = webHost;
        }
        [Area("QuanLy")]
        [Route("[area]/[controller]/[action]/{id?}")]
        public IActionResult Index()
        {
            var Eve = qldvContext.Events.ToList();
            var semester = qldvContext.Semesters.ToList();
            ViewBag.Semesters = semester;
            return View(Eve);
        }

        public IActionResult Add()
        {
            var semester = qldvContext.Semesters.ToList();
            ViewBag.Semesters = new SelectList(semester, "Id" , "Title");
            return View("/Areas/QuanLy/Views/SuKien/Add.cshtml");
        }

        public IActionResult Edit(int id)
        {
            var Eve = qldvContext.Events.Where(a => a.Id == id).FirstOrDefault();
            var semester = qldvContext.Semesters.ToList();
            ViewBag.Semesters = new SelectList(semester, "Id", "Title");
            return View("/Areas/QuanLy/Views/SuKien/Edit.cshtml", Eve);
        }

        public IActionResult Active(int id)
        {
            var Eve = qldvContext.Events.Where(a => a.Id == id).FirstOrDefault();
            if (Eve.Publish == true)
            {
                Eve.Publish = false;
            }
            else { Eve.Publish = true; }

            qldvContext.Update(Eve);
            qldvContext.SaveChanges();

            return RedirectToAction("Index", "SuKien", new { area = "QuanLy" });
        }

        public IActionResult Delete(int id)
        {

            var Eve = qldvContext.Events.Where(a => a.Id == id).FirstOrDefault();
            if (Eve.Image != null)
            {
                deleteFile(Eve.Image);
            }
            qldvContext.Events.Remove(Eve);
            qldvContext.SaveChanges();


            return RedirectToAction("Index", "SuKien", new { area = "QuanLy" });
        }


        [HttpPost]

        public IActionResult Add (Event input)
        {
            string FileName = UploadFile(input);

            var Eve = new Event()
            {
                SemesterId = input.SemesterId,
                Title = input.Title,
                Description = input.Description,
                Content = input.Content,
                DayStart = input.DayStart,
                DayEnd = input.DayEnd,
                Score = input.Score,
                Image = FileName,
                CreatedAt = DateTime.Now,
                Publish = false,
                UseridCreated = HttpContext.Session.GetInt32("LoginId"),
            };

            qldvContext.Add(Eve);
            qldvContext.SaveChanges();


            return RedirectToAction("Index", "SuKien", new { area = "QuanLy" });
        }

        [HttpPost]
        public IActionResult Edit(Event input, int id)
        {

            Event Eve = qldvContext.Events.Where(a => a.Id == id).FirstOrDefault();
            if (Eve != null)
            {
                string fileName = Eve.Image;
                if (input.imageFile != null)
                {
                    if (Eve.Image != null)
                    {
                        string uploadpath = Path.Combine(webHostEnvironment.WebRootPath, "Events/Images", fileName);
                        var stream = new FileStream(uploadpath, FileMode.Create);
                        input.imageFile.CopyTo(stream);
                        stream.Close();
                    }
                    else
                    {
                        fileName = UploadFile(input);
                    }
                }

                Eve.Semester = input.Semester;
                Eve.Description = input.Description;
                Eve.Title = input.Title;
                Eve.Score = input.Score;
                Eve.DayStart = input.DayStart;
                Eve.DayEnd = input.DayEnd;
                Eve.Content = input.Content;
                Eve.UpdatedAt = DateTime.Now;
                Eve.UseridUpdated = HttpContext.Session.GetInt32("LoginId");

                Eve.Image = fileName;
                qldvContext.Events.Update(Eve);
                qldvContext.SaveChanges();
            }

            return RedirectToAction("Index", "SuKien", new { area = "QuanLy" });
        }

        private string UploadFile(Event eve)
        {
            string fileName = null;
            if (eve.imageFile != null)
            {
                Guid guid = Guid.NewGuid();
                string newfileName = guid.ToString();
                string fileextention = Path.GetExtension(eve.imageFile.FileName);
                fileName = newfileName + fileextention;
                string uploadpath = Path.Combine(webHostEnvironment.WebRootPath, "Events/Images", fileName);
                var stream = new FileStream(uploadpath, FileMode.Create);
                eve.imageFile.CopyTo(stream);
                stream.Close();
            }
            return fileName;
        }

        private void deleteFile(string file)
        {
            string fileName = file;
            if (fileName != null)
            {
                string deletePath = Path.Combine(webHostEnvironment.WebRootPath, "Events/Images", fileName);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }

            }
        }
    }
}
