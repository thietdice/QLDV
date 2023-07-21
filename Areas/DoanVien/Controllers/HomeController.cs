using Microsoft.AspNetCore.Mvc;
using QLDV.Data;
using QLDV.Areas.DoanVien.Models;
using QLDV.ActionFilter;

namespace QLDV.Areas.DoanVien.Controllers
{
    [CheckSession]
    [Area("DoanVien")]
    [Route("[area]/[controller]/[action]/{id?}")]
    public class HomeController : Controller
	{

        private readonly IWebHostEnvironment webHostEnvironment;
        QldvContext qldvContext = new QldvContext();
        public HomeController(IWebHostEnvironment webHost)
        {
            webHostEnvironment = webHost;
        }
        
        
        public IActionResult Index()
		{
            List<Article> articles = new List<Article>();
            List<Event> events = new List<Event>();
            articles = qldvContext.Articles.ToList();
            events = qldvContext.Events.ToList();
          var viewModel = new ArticleEventViewModel
            {
                Articles = articles,
                Events = events
            };
      
            return View("~/Areas/DoanVien/Views/Home/Index.cshtml", viewModel);
        }
        public IActionResult Article()
        {
            List<Article> articles = new List<Article>();
            articles = qldvContext.Articles.ToList();
            return View("~/Areas/DoanVien/Views/Home/Article.cshtml", articles);
        }
        public IActionResult ArticleInfo(int articleId)
        {
            Article @article = qldvContext.Articles.FirstOrDefault(e => e.Id == articleId);
            if (@article == null)
            {
                return RedirectToAction("Error", "Home");
            }
            article.Viewed++;
            qldvContext.SaveChanges();
            return View("/Areas/DoanVien/Views/Home/ArticleInfo.cshtml", @article);
        }



    }
}
