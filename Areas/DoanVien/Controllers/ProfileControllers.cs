using Microsoft.AspNetCore.Mvc;
using QLDV.ActionFilter;
using QLDV.Data;

namespace QLDV.Areas.DoanVien.Controllers
{
    [CheckSession]
    public class ProfileController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        QldvContext qldvContext = new QldvContext();
        public ProfileController(IWebHostEnvironment webHost)
        {
            webHostEnvironment = webHost;
        }
        [Area("DoanVien")]
        [Route("[area]/[controller]/[action]/{id?}")]
        public IActionResult Index()
        {
            String userId = HttpContext.Session.GetString("LoginName");

            User user = qldvContext.Users.Where(u => u.Fullname.Equals(userId)).SingleOrDefault();
            return View("/Areas/DoanVien/Views/Profile/Profile.cshtml", user);
        }
        public IActionResult Edit()
        {
            String userId = HttpContext.Session.GetString("LoginName");
            User user = qldvContext.Users.Where(u => u.Fullname.Equals(userId)).First();

            return View("/Areas/DoanVien/Views/Profile/Edit.cshtml", user);
        }
        [HttpPost]
        public IActionResult Edit(User input)
        {

            var id = HttpContext.Session.GetInt32("LoginId");
            User user = qldvContext.Users.Where(u => u.Id == id).SingleOrDefault();
            string fileName = user.Image;
            if (input.imageFile != null)
            {
                fileName = UploadFile(input);
                string uploadpath = Path.Combine(webHostEnvironment.WebRootPath, "Users/Images", fileName);
                var stream = new FileStream(uploadpath, FileMode.Create);
                input.imageFile.CopyTo(stream);
            }
            if (user != null)
            {
                user.IdStudent = input.IdStudent;
                user.Fullname = input.Fullname;
                user.Birthday = input.Birthday;
                user.Gender = input.Gender;
                user.Ethnic = input.Ethnic;
                user.Religion = input.Religion;
                user.Profession = input.Profession;
                user.LevelEducation = input.LevelEducation;
                user.LevelLanguage = input.LevelLanguage;
                user.LevelSpecialize = input.LevelSpecialize;
                user.LevelPolitics = input.LevelPolitics;
                user.LevelComputer = input.LevelComputer;
                user.ResidenceAddress = input.ResidenceAddress;
                user.DayInUnion = input.DayInUnion;
                user.Phone = input.Phone;
                user.ResidenceAddress = input.ResidenceAddress;
                user.ResidenceCity = input.ResidenceCity;
                user.ResidenceDistrict = input.ResidenceDistrict;
                user.ResidenceWard = input.ResidenceWard;
                user.Image = fileName;
                user.UseridUpdated = HttpContext.Session.GetInt32("LoginId");
                qldvContext.Users.Update(user);
            }

            qldvContext.SaveChanges();

            return View("/Areas/DoanVien/Views/Profile/Profile.cshtml", user);
        }
        private string UploadFile(User user)
        {
            string fileName = null;
            if (user.imageFile != null)
            {
                Guid guid = Guid.NewGuid();
                string newfileName = guid.ToString();
                string fileextention = Path.GetExtension(user.imageFile.FileName);
                fileName = newfileName + fileextention;
                string uploadpath = Path.Combine(webHostEnvironment.WebRootPath, "Users/Images", fileName);
                var stream = new FileStream(uploadpath, FileMode.Create);
                user.imageFile.CopyTo(stream);
                stream.Close();
            }
            return fileName;
        }
    }
}
