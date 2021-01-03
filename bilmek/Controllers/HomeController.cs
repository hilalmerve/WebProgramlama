using bilmek;
using bilmek.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace bilmek.Controllers
{
    [RoutePrefix("Home")]
    public class HomeController : Controller
    {

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private bilmekDbContext db = new bilmekDbContext(); //Veri tabanına erişim
        
        public HomeController()
        {
            
        }

        public HomeController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
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
        //bu method tekrar aynı sayfaya gelmek için
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }
        //Kullanıcı girişi için olan sayfa
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //Kullanıcı giriş işleminde buraya post edilir veriler
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            //Gönderilen isimde bir kullanıcı olup olmadığını kontrol eder.
            var kullanici = await UserManager.FindAsync(model.UserName, model.Password);
            if (kullanici == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(model);
            }
            else
            {
                //Kullanıcının bulunması durumunda authorization işlemi yapılır.
                await SignInManager.SignInAsync(kullanici, false, false);
                return RedirectToLocal(returnUrl);
            }

        }
        //Anasayfa 
        public ActionResult Index()
        {
            ViewBag.Header = "header";
            ViewBag.ModelType = "Slider";
            ViewBag.CarouselVisible = " ";
            return View(db.Slider.Where(x => x.IsPublished == true).ToList());
        }
        //hakkında sayfası için içerik veritabanından çekilir.
        public ActionResult Haberler()
        {
            ViewBag.Header = "header2";
            ViewBag.CarouselVisible = "hidden";
            return View(db.HaberIcerik.Where(x => x.IsPublished == true && x.Resim != null).ToList());
        }
        public ActionResult Referanslar()
        {
            ViewBag.Header = "header2";
            ViewBag.CarouselVisible = "hidden";
            return View(db.ReferansIcerik.Where(x => x.IsPublished == true && x.Resim != null).ToList());
        }
        public ActionResult Iletisim()
        {
            ViewBag.Header = "header2";
            ViewBag.CarouselVisible = "hidden";
            return View();
        }
        [Route("Basvuru/{BasvuruTuru=Cozum}")]
        public ActionResult Basvuru(string BasvuruTuru)
        {
            ViewBag.Header = "header2";
            ViewBag.CarouselVisible = "hidden";
            if (BasvuruTuru == "Cozum")
            {
                BasvuruTuru = "Çözüm";
            }
            else if (BasvuruTuru == "Destek" || BasvuruTuru == "Kariyer")
            {

            }
            else
            {

                return RedirectToAction("Index");
            }
            ViewBag.BasvuruTuru = BasvuruTuru;
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BasvuruEkle(BasvuruIcerikModel basvuru)
        {
            db.BasvuruIcerik.Add(basvuru);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}