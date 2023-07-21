using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using QLDV.ActionFilter;
using QLDV.Data;

namespace QLDV.Areas.QuanLy.Controllers
{
    [CheckSession]
    public class BaiDangController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        QldvContext qldvContext = new QldvContext();

        public BaiDangController(IWebHostEnvironment webHost)
        {
            webHostEnvironment = webHost;
        }
        [Area("QuanLy")]
        [Route("[area]/[controller]/[action]/{id?}")]
        public IActionResult Index()
        {

            List<Article> semes = qldvContext.Articles.ToList();
            
            return View(semes);
        }

        public IActionResult Add()
        {

            return View("/Areas/QuanLy/Views/BaiDang/Add.cshtml");
        }

        public IActionResult Edit(int id)
        {
            var article = qldvContext.Articles.Where(a => a.Id == id).FirstOrDefault();

            return View("/Areas/QuanLy/Views/BaiDang/Edit.cshtml", article);
        }

        public IActionResult Delete(int id)
        {

            var article = qldvContext.Articles.Where(a => a.Id == id).FirstOrDefault();

            if (article.Pdf != null) 
            {
                deleteFile(article.Pdf);
            }

            qldvContext.Articles.Remove(article);
            qldvContext.SaveChanges();



            return RedirectToAction("Index", "BaiDang", new { area = "QuanLy" });
        }

        [HttpPost]

        public IActionResult Add(Article art)
        {
            string pdfName = UploadFile(art);

            Article article = new Article()
            {
                Viewed = 0,
                Title = art.Title,
                Content = art.Content,
                Pdf = pdfName,
                CreatedAt = DateTime.Now,
                UseridCreated = HttpContext.Session.GetInt32("LoginId")
            };

            qldvContext.Add(article);
            qldvContext.SaveChanges();

            return RedirectToAction("Index", "BaiDang", new { area = "QuanLy" });
        }

        [HttpPost]
        public IActionResult Edit(Article art , int id)
        {

            Article article = qldvContext.Articles.Where(a => a.Id == id).FirstOrDefault();
            if (article != null)
            {
                string pdfName = article.Pdf;
                if (art.PdfFile != null)
                {
                    if (article.Pdf != null)
                    {
                        string uploadpath = Path.Combine(webHostEnvironment.WebRootPath, "Articles", pdfName);
                        var stream = new FileStream(uploadpath, FileMode.Create);
                        art.PdfFile.CopyTo(stream);
                        stream.Close();
                    }
                    else
                    {
                        pdfName = UploadFile(art);
                    }
                }
                

                article.Title = art.Title;
                article.Content = art.Content;
                article.UpdatedAt = DateTime.Now;
                article.UseridUpdated = HttpContext.Session.GetInt32("LoginId");

                article.Pdf = pdfName;
                qldvContext.Articles.Update(article);
                qldvContext.SaveChanges();
            }
            
            return RedirectToAction("Index", "BaiDang", new { area = "QuanLy" });
        }

        private string UploadFile(Article art)
        {
            string fileName = null;
            if (art.PdfFile != null)
            {
                Guid guid = Guid.NewGuid();
                string newfileName = guid.ToString();
                string fileextention = Path.GetExtension(art.PdfFile.FileName);
                fileName = newfileName + fileextention;
                string uploadpath = Path.Combine(webHostEnvironment.WebRootPath, "Articles", fileName);
                var stream = new FileStream(uploadpath, FileMode.Create);
                art.PdfFile.CopyTo(stream);
                stream.Close();
            }
            return fileName;
        }

        private void deleteFile(string file)
        {
            string fileName = file;
            if (fileName != null)
            {
                string deletePath = Path.Combine(webHostEnvironment.WebRootPath, "Articles", fileName);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }

            }
        }
    }
}
