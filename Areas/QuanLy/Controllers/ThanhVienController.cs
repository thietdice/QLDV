using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using QLDV.ActionFilter;
using QLDV.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Drawing.Text;
using NuGet.Protocol;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing.Constraints;
using Org.BouncyCastle.Tls;

namespace QLDV.Areas.QuanLy.Controllers
{
    [CheckSession]
    public class ThanhVienController : Controller
    {
        
        private readonly IWebHostEnvironment webHostEnvironment;
        QldvContext qldvContext = new QldvContext();

        public ThanhVienController(IWebHostEnvironment webHost)
        {
            webHostEnvironment = webHost;
        }

        [Area("QuanLy")]
        [Route("[area]/[controller]/[action]/{id?}")]
        public IActionResult Index()
        {
            List<User> users = new List<User>();
            List<Class> cl = qldvContext.Classes.ToList();
            ViewBag.Class = cl;

            var LogId = HttpContext.Session.GetInt32("LoginId");

            var LogRank = HttpContext.Session.GetInt32("Rank");


            // Khac nhau giua cac nguoi dung
            if (LogRank == 4)
            {

                users = qldvContext.Users.ToList();
                return View(users);
            }
            else if (LogRank == 3)
            {
                var Us = qldvContext.Users.Where(u => u.Id == LogId).First();
                var Classes = qldvContext.Classes.Where(c => c.Id == Us.ClassId).First();
                var fac =qldvContext.Faculties.Where(f => f.Id == Classes.FacultyId).First();
                
                List<Class> ClassesList = qldvContext.Classes.Where(c => c.FacultyId == fac.Id).ToList();

                foreach ( var c in ClassesList)
                {
                    var temp = qldvContext.Users.Where(u => u.ClassId == c.Id).ToList();
                    if (temp != null)
                    {
                        users.AddRange(temp);
                    }
                }
            
                return View(users);
            }
            else
            {
                var Us = qldvContext.Users.Where(u => u.Id == LogId).First();
                var Classes = qldvContext.Classes.Where(c => c.Id == Us.ClassId).First();
                users = qldvContext.Users.Where(u => u.ClassId == Classes.Id).ToList();
                return View(users);
            }
        }

        public IActionResult Add() 
        {
            List<UserCatalogue> userCatalogues = qldvContext.UserCatalogues.ToList();
            List<Class> cl = qldvContext.Classes.ToList();
            var LogId = HttpContext.Session.GetInt32("LoginId");

            var LogRank = HttpContext.Session.GetInt32("Rank");

            if (LogRank == 4)
            {
                ViewBag.UserCata = new SelectList (userCatalogues , "Id" , "Title" );
                ViewBag.Classes = new SelectList(cl, "Id", "Title");
            }
            else if (LogRank == 3) 
            {
                var Us = qldvContext.Users.Where(u => u.Id == LogId).First();
                var Classes = qldvContext.Classes.Where(c => c.Id == Us.ClassId).First();
                var fac = qldvContext.Faculties.Where(f => f.Id == Classes.FacultyId).First();

                cl = qldvContext.Classes.Where(c => c.FacultyId == fac.Id).ToList();
                userCatalogues = qldvContext.UserCatalogues.Where(u => u.Id <= 3).ToList();

                ViewBag.UserCata = new SelectList(userCatalogues, "Id", "Title");
                ViewBag.Classes = new SelectList(cl, "Id", "Title");
            }
            else
            {
                var Us = qldvContext.Users.Where(u => u.Id == LogId).First();
                cl = qldvContext.Classes.Where(c => c.Id == Us.ClassId).ToList();
                userCatalogues = qldvContext.UserCatalogues.Where(u => u.Id <= 2).ToList();

                ViewBag.UserCata = new SelectList(userCatalogues, "Id", "Title");
                ViewBag.Classes = new SelectList(cl, "Id", "Title");

            }

            return View("/Areas/QuanLy/Views/ThanhVien/Add.cshtml");
        }
        public IActionResult Edit(int id)
        {
            List<UserCatalogue> userCatalogues = qldvContext.UserCatalogues.ToList();
            List<Class> cl = qldvContext.Classes.ToList();
            var LogId = HttpContext.Session.GetInt32("LoginId");

            var LogRank = HttpContext.Session.GetInt32("Rank");

            if (LogRank == 4)
            {
                ViewBag.UserCata = new SelectList(userCatalogues, "Id", "Title");
                ViewBag.Classes = new SelectList(cl, "Id", "Title");
            }
            else if (LogRank == 3)
            {
                var Us = qldvContext.Users.Where(u => u.Id == LogId).First();
                var Classes = qldvContext.Classes.Where(c => c.Id == Us.ClassId).First();
                var fac = qldvContext.Faculties.Where(f => f.Id == Classes.FacultyId).First();

                cl = qldvContext.Classes.Where(c => c.FacultyId == fac.Id).ToList();
                userCatalogues = qldvContext.UserCatalogues.Where(u => u.Id <= 3).ToList();

                ViewBag.UserCata = new SelectList(userCatalogues, "Id", "Title");
                ViewBag.Classes = new SelectList(cl, "Id", "Title");
            }
            else
            {
                var Us = qldvContext.Users.Where(u => u.Id == LogId).First();
                cl = qldvContext.Classes.Where(c => c.Id == Us.ClassId).ToList();
                userCatalogues = qldvContext.UserCatalogues.Where(u => u.Id <= 2).ToList();

                ViewBag.UserCata = new SelectList(userCatalogues, "Id", "Title");
                ViewBag.Classes = new SelectList(cl, "Id", "Title");

            }

            var user = qldvContext.Users.Where(u => u.Id >= id).FirstOrDefault();
            
            return View("/Areas/QuanLy/Views/ThanhVien/Edit.cshtml", user);
        }
        
        public IActionResult Delete(int id)
        {
            var user = qldvContext.Users.Where(u => u.Id == id).FirstOrDefault();

            //Xoa File Anh
            if (user.Image != null )
            {
                deleteFile(user.Image);
            }

            qldvContext.Users.Remove(user);
            qldvContext.SaveChanges();
            return RedirectToAction("Index", "ThanhVien", new { area = "QuanLy" });
        }

        [HttpPost] 
        public IActionResult Add( User input)
        {

                string FileName = UploadFile(input);
                   
            var user = new User()
            {
                UserCatalogueId = input.UserCatalogueId,
                Fullname = input.Fullname,
                Email = input.Email,
                Password = input.Password,
                IdStudent = input.IdStudent,
                ClassId = input.ClassId,
                Birthday = input.Birthday,
                Gender = input.Gender,
                Ethnic = input.Ethnic,
                Religion = input.Religion,
                IdCard = input.IdCard,
                Profession = input.Profession,
                LevelComputer = input.LevelComputer,
                LevelEducation = input.LevelEducation,
                LevelLanguage = input.LevelLanguage,
                LevelPolitics = input.LevelPolitics,
                LevelSpecialize = input.LevelSpecialize,
                DayInUnion = input.DayInUnion,
                Phone = input.Phone,
                ResidenceAddress = input.ResidenceAddress,
                ResidenceCity = input.ResidenceCity,
                ResidenceDistrict = input.ResidenceDistrict,
                ResidenceWard = input.ResidenceWard,
                Image = FileName,
                CreatedAt = DateTime.Now,
                UseridCreated = HttpContext.Session.GetInt32("LoginId")

            };

            qldvContext.Users.Add(user);
            qldvContext.SaveChanges();
            return RedirectToAction("Index", "ThanhVien", new { area = "QuanLy" });
        }

        [HttpPost]
        public IActionResult Edit(User input)
        {

            var user = qldvContext.Users.Where(u => u.Id == input.Id).SingleOrDefault();
    
            if (user != null)
            {
                string fileName = user.Image;
                if (input.imageFile != null)
                {
                    if (user.Image != null)
                    {
                        string uploadpath = Path.Combine(webHostEnvironment.WebRootPath, "Users/Images", fileName);
                        var stream = new FileStream(uploadpath, FileMode.Create);
                        input.imageFile.CopyTo(stream);
                        stream.Close();
                    }
                    else
                    {
                        fileName = UploadFile(input);
                    }

                }

                user.UserCatalogueId = input.UserCatalogueId;
                user.Fullname = input.Fullname;
                user.Email = input.Email;
                user.Password = input.Password;
                user.IdStudent = input.IdStudent;
                user.ClassId = input.ClassId;
                user.Birthday = input.Birthday;
                user.Gender = input.Gender;
                user.Ethnic = input.Ethnic;
                user.Religion = input.Religion;
                user.IdCard = input.IdCard;
                user.Profession = input.Profession;
                user.LevelComputer = input.LevelComputer;
                user.LevelEducation = input.LevelEducation;
                user.LevelLanguage = input.LevelLanguage;
                user.LevelPolitics = input.LevelPolitics;
                user.LevelSpecialize = input.LevelSpecialize;
                user.DayInUnion = input.DayInUnion;
                user.Phone = input.Phone;
                user.ResidenceAddress = input.ResidenceAddress;
                user.ResidenceCity = input.ResidenceCity;
                user.ResidenceDistrict = input.ResidenceDistrict;
                user.ResidenceWard = input.ResidenceWard;
                user.Image = fileName;
                user.UpdatedAt = DateTime.Now;
                user.UseridUpdated = HttpContext.Session.GetInt32("LoginId");

                qldvContext.Users.Update(user);
                qldvContext.SaveChanges();
            }

            

            return RedirectToAction("Index", "ThanhVien", new { area = "QuanLy" });
        }


        private string UploadFile (User user)
        {
            string fileName = null;
            if ( user.imageFile != null )
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

        private void deleteFile (string file)
        {
            string fileName = file;
            if (fileName != null)
            {
                string deletePath = Path.Combine(webHostEnvironment.WebRootPath, "Users/Images", fileName);
                if (System.IO.File.Exists(deletePath))
                {
                    System.IO.File.Delete(deletePath);
                }

            }
        }
    }
}
