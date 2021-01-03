using bilmek;
using bilmek.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace bilmek.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private bilmekDbContext db = new bilmekDbContext();
        public AdminController()
        {
        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        // GET: Admin
        public ActionResult HaberList()
        {
            return View(db.HaberIcerik.ToList());
        }

        // GET: Admin
        public ActionResult HaberEkle()
        {
            return View();
        }

        // POST: ilans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HaberEkle(HaberIcerikModel haber, String yayin, HttpPostedFileBase file)
        {
            //ilanim.UserId = kullaniciManager.FindByName(userName).Id;
            haber.IsPublished = yayin == "on" ? true : false;
            if (file != null && file.ContentLength > 0)
            {
                MemoryStream memoryStream = file.InputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    file.InputStream.CopyTo(memoryStream);
                }
                haber.Resim = memoryStream.ToArray();
            }
            db.HaberIcerik.Add(haber);
            db.SaveChanges();
            return RedirectToAction("HaberList");

        }
        // GET: Admin
        public ActionResult HaberDuzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HaberIcerikModel ilan = db.HaberIcerik.Find(id);
            if (ilan == null)
            {
                return HttpNotFound();
            }
            return View(ilan);
        }
        //GET: Admin
        public ActionResult HaberResimSil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HaberIcerikModel referans = db.HaberIcerik.Find(id);
            if (referans == null)
            {
                return HttpNotFound();
            }
            referans.Resim = null;
            db.SaveChanges();
            return Redirect("HaberDuzenle?id=" + id);
        }


        // POST: ilans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HaberDuzenle(HaberIcerikModel haber, String yayin, HttpPostedFileBase file)
        {
            var veri = db.HaberIcerik.Where(x => x.Id == haber.Id).FirstOrDefault();
            veri.CTitle = haber.CTitle;
            veri.CFullContent = haber.CFullContent;
            veri.IsPublished = yayin == "on" ? true : false;
            if (file != null && file.ContentLength > 0)
            {
                MemoryStream memoryStream = file.InputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    file.InputStream.CopyTo(memoryStream);
                }
                veri.Resim = memoryStream.ToArray();
            }
            veri.CreatedOn = haber.CreatedOn;
            db.SaveChanges();
            return RedirectToAction("HaberList");

        }

        // GET: kampanyas/Delete/5
        public ActionResult HaberSil(int? id)
        {
            ViewBag.Panel = "admin";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HaberIcerikModel haber = db.HaberIcerik.Find(id);
            if (haber == null)
            {
                return HttpNotFound();
            }
            db.HaberIcerik.Remove(haber);
            db.SaveChanges();
            return RedirectToAction("HaberList");
        }
        // GET: Admin
        public ActionResult IcerikList()
        {
            return View(db.GenelIcerik.ToList());
        }

        // GET: Admin
        public ActionResult IcerikEkle()
        {
            return View();
        }

        // POST: ilans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IcerikEkle(GenelIcerikModel genel, String yayin)
        {
            //ilanim.UserId = kullaniciManager.FindByName(userName).Id;
            genel.IsPublished = yayin == "on" ? true : false;
            db.GenelIcerik.Add(genel);
            db.SaveChanges();
            return RedirectToAction("IcerikList");

        }
        // GET: Admin
        public ActionResult IcerikDuzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenelIcerikModel genel = db.GenelIcerik.Find(id);
            if (genel == null)
            {
                return HttpNotFound();
            }
            return View(genel);
        }

        // POST: ilans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult IcerikDuzenle(GenelIcerikModel genel, String yayin, HttpPostedFileBase file)
        {
            var veri = db.GenelIcerik.Where(x => x.Id == genel.Id).FirstOrDefault();
            veri.CTitle = genel.CTitle;
            veri.CFullContent = genel.CFullContent;
            veri.IsPublished = yayin == "on" ? true : false;
            veri.CreatedOn = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("IcerikList");

        }

        // GET: kampanyas/Delete/5
        public ActionResult IcerikSil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GenelIcerikModel genel = db.GenelIcerik.Find(id);
            if (genel == null)
            {
                return HttpNotFound();
            }
            db.GenelIcerik.Remove(genel);
            db.SaveChanges();
            return RedirectToAction("IcerikList");
        }



        public ActionResult ReferansList()
        {
            return View(db.ReferansIcerik.ToList());
        }

        // GET: Admin
        public ActionResult ReferansEkle()
        {
            return View();
        }

        // POST: ilans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReferansEkle(ReferansIcerikModel referans, String yayin, HttpPostedFileBase file)
        {
            //ilanim.UserId = kullaniciManager.FindByName(userName).Id;
            referans.IsPublished = yayin == "on" ? true : false;
            if (file != null && file.ContentLength > 0)
            {
                MemoryStream memoryStream = file.InputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    file.InputStream.CopyTo(memoryStream);
                }
                referans.Resim = memoryStream.ToArray();
            }
            db.ReferansIcerik.Add(referans);
            db.SaveChanges();
            return RedirectToAction("ReferansList");


        }
        // GET: Admin
        public ActionResult ReferansDuzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReferansIcerikModel referans = db.ReferansIcerik.Find(id);
            if (referans == null)
            {
                return HttpNotFound();
            }
            return View(referans);
        }
        //GET: Admin
        public ActionResult ReferansResimSil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReferansIcerikModel referans = db.ReferansIcerik.Find(id);
            if (referans == null)
            {
                return HttpNotFound();
            }
            referans.Resim = null;
            db.SaveChanges();
            return Redirect("ReferansDuzenle?id="+id);
        }

        // POST: ilans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ReferansDuzenle(ReferansIcerikModel referans, String yayin, HttpPostedFileBase file)
        {
            var veri = db.ReferansIcerik.Where(x => x.Id == referans.Id).FirstOrDefault();
            veri.ReferansAdi = referans.ReferansAdi;
            if (file != null && file.ContentLength > 0)
            {
                MemoryStream memoryStream = file.InputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    file.InputStream.CopyTo(memoryStream);
                }
                veri.Resim = memoryStream.ToArray();
            }
            veri.Url = referans.Url;
            veri.IsPublished = yayin == "on" ? true : false;
            db.SaveChanges();
            return RedirectToAction("ReferansList");

        }

        // GET: kampanyas/Delete/5
        public ActionResult ReferansSil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReferansIcerikModel referans = db.ReferansIcerik.Find(id);
            if (referans == null)
            {
                return HttpNotFound();
            }
            db.ReferansIcerik.Remove(referans);
            db.SaveChanges();
            return RedirectToAction("ReferansList");
        }


        public ActionResult SliderList()
        {
            return View(db.Slider.ToList());
        }

        // GET: Admin
        public ActionResult SliderEkle()
        {
            return View();
        }

        // POST: ilans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SliderEkle(Slider slider, String yayin, HttpPostedFileBase file)
        {
            //ilanim.UserId = kullaniciManager.FindByName(userName).Id;
            slider.IsPublished = yayin == "on" ? true : false;
            if (file != null && file.ContentLength > 0)
            {
                MemoryStream memoryStream = file.InputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    file.InputStream.CopyTo(memoryStream);
                }
                slider.Resim = memoryStream.ToArray();
            }
            db.Slider.Add(slider);
            db.SaveChanges();
            return RedirectToAction("SliderList");


        }
        // GET: Admin
        public ActionResult SliderDuzenle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }
        //GET: Admin
        public ActionResult SliderResimSil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            slider.Resim = null;
            db.SaveChanges();
            return Redirect("SliderDuzenle?id=" + id);
        }

        // POST: ilans/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SliderDuzenle(Slider slider, String yayin, HttpPostedFileBase file)
        {
            var veri = db.Slider.Where(x => x.Id == slider.Id).FirstOrDefault();
            veri.Adi = slider.Adi;
            if (file != null && file.ContentLength > 0)
            {
                MemoryStream memoryStream = file.InputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    file.InputStream.CopyTo(memoryStream);
                }
                veri.Resim = memoryStream.ToArray();
            }
            veri.IsPublished = yayin == "on" ? true : false;
            db.SaveChanges();
            return RedirectToAction("SliderList");

        }

        // GET: kampanyas/Delete/5
        public ActionResult SliderSil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            db.Slider.Remove(slider);
            db.SaveChanges();
            return RedirectToAction("SliderList");
        }




        // GET: Admin
        public async Task<ActionResult> KullaniciList()
        {
            List<ApplicationUser> userList = await GetUsersAsync();
            //List<ApplicationUser> userlist=  UserManager.Users;
            return View(userList.ToList());
        }

        public async Task<List<ApplicationUser>> GetUsersAsync()
        {
             return await UserManager.Users.ToListAsync();
        }


        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult KullaniciEkle()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> KullaniciEkle(KayitEkleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Username, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    return RedirectToAction("KullaniciList");
                }
            }
            return View(model);
        }
        //
        // POST: /Manage/RemoveLogin
        public async Task<ActionResult> KullaniciSil(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (User.Identity.GetUserId()!=id.ToString())
            {

                var result = await UserManager.DeleteAsync(UserManager.Users.Where(m => m.Id == id).FirstOrDefault());
                ViewBag.sonuc = "Kaldirma Başarili";
            }
            else
            {
                ViewBag.sonuc = "Kaldirma Başarısız.";
            }
            return RedirectToAction("KullaniciList");
        }

        //
        // POST: /Manage/RemoveLogin
        public ActionResult KullaniciDuzenle(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser user = UserManager.Users.Where(m => m.Id == id).FirstOrDefault();
            var kayit = new KayitDuzenleViewModel { Id = user.Id, Username = user.UserName, Email = user.Email };
            return View(kayit);
        }
        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> KullaniciDuzenle(KayitDuzenleViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = UserManager.Users.Where(m=>m.Id== model.Id).FirstOrDefault();
                user.UserName = model.Username;
                user.Email = model.Email;
                var result=await UserManager.UpdateAsync(user);
                if(result.Succeeded)
                {
                    result = await UserManager.ResetPasswordAsync(model.Id,await UserManager.GeneratePasswordResetTokenAsync(model.Id),model.Password);
                    if (result.Succeeded)
                    {
                        //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToAction("KullaniciList");
                    }
                }
            }
            return View(model);
        }

        public ActionResult BasvuruList()
        {
            return View(db.BasvuruIcerik.ToList());
        }
        public ActionResult BasvuruSil(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BasvuruIcerikModel basvuru = db.BasvuruIcerik.Find(id);
            if (basvuru == null)
            {
                return HttpNotFound();
            }
            db.BasvuruIcerik.Remove(basvuru);
            db.SaveChanges();
            return RedirectToAction("BasvuruList");
        }
        //
        // POST: /Account/LogOff
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }
    }
}
