using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Tls;
using QLDV.ActionFilter;
using QLDV.Data;

namespace QLDV.Areas.DoanVien.Controllers
{
    [CheckSession]
    public class EventsController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        QldvContext qldvContext = new QldvContext();
        public EventsController(IWebHostEnvironment webHost)
        {
            webHostEnvironment = webHost;
        }
        [Area("DoanVien")]
        [Route("[area]/[controller]/[action]/{id?}")]
        public IActionResult Index()
        {
            string message = TempData["Message"] as string;
            // Truyền thông báo đến view
            ViewBag.Message = message;
            List<Event> events = new List<Event>();
            events = qldvContext.Events.ToList();
            return View("/Areas/DoanVien/Views/Event/Event.cshtml", events);
        }
        public IActionResult EventsInfo(int eventId)
        {
            Event @event = qldvContext.Events.FirstOrDefault(e => e.Id == eventId);
            if (@event == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View("/Areas/DoanVien/Views/Event/EventInfo.cshtml", @event);
        }

        [HttpPost]
        public IActionResult UploadEventUser(EventUser input, int eventId)
        {
            int id = HttpContext.Session.GetInt32("LoginId").GetValueOrDefault();
            var hasUploaded = qldvContext.EventUsers.FirstOrDefault(eu => eu.EventId == eventId && eu.UserId == id);
            if (hasUploaded != null)
            {
                TempData["Message"] = "Bạn đã upload hình ảnh cho sự kiện này.";
                return RedirectToAction("EventsInfo", "Events", new { area = "DoanVien", eventId = eventId });
            }

            try
            {
                string FileName = UploadFile(input);

                bool isEventExists = qldvContext.Events.Any(e => e.Id == input.EventId);
                if (!isEventExists)
                {
                    return RedirectToAction("Error", "Home");
                }

                var eventUser = new EventUser()
                {
                    EventId = input.EventId,
                    UserId = id,
                    Image = FileName,
                    Note = input.Note,
                    NoteReviewer = "",
                    CreatedAt = DateTime.Now,
                    Status = "Chưa duyệt",
                };
                qldvContext.EventUsers.Add(eventUser);
                qldvContext.SaveChanges();

                TempData["Message"] = "Upload hình ảnh thành công.";
                return RedirectToAction("EventsInfo", "Events", new { area = "DoanVien", eventId = eventId });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }



        private string UploadFile(EventUser eventUser)
        {
            string fileName = null;
            if (eventUser.imageFile != null)
            {
                Guid guid = Guid.NewGuid();
                string newfileName = guid.ToString();
                string fileextention = Path.GetExtension(eventUser.imageFile.FileName);
                fileName = newfileName + fileextention;
                string uploadpath = Path.Combine(webHostEnvironment.WebRootPath, "Users/Images/EventUser", fileName);
                var stream = new FileStream(uploadpath, FileMode.Create);
                eventUser.imageFile.CopyTo(stream);
                stream.Close();
            }
            return fileName;
        }

        
    }
}
