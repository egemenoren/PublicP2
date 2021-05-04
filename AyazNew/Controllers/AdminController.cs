using AyazNew.Entity;
using AyazNew.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;

namespace AyazNew.Controllers
{
    public class AdminController : BaseController
    {
        // GET: Admin
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.successtoindex = "Zaten Giris Yapmistiniz!";
                return View();
            }
            else
            {
                return View();
            }
        }
        [Authorize]
        public ActionResult SiteAyarlari()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult SiteAyarlari(HttpPostedFileBase imageicon, HttpPostedFileBase imagebanner, HttpPostedFileBase imagelogo)
        {
            WebImage fileLogo = null;
            if (imagelogo != null)
            {
                fileLogo = new WebImage(imagelogo.InputStream);
                if (fileLogo.Height != 768 && fileLogo.Width != 1280)
                {
                    ViewBag.ErrorMessage = "Logo boyutlari 1280x768 Olmali!";
                    return View();
                }

                var uzantiLogo = Path.GetExtension(imagelogo.FileName);
                var yol = Path.Combine(Server.MapPath("~/Content/img"), "logo" + uzantiLogo);
                fileLogo.Save(yol);
            }
            WebImage fileBanner = null;
            if (imagebanner != null)
            {
                fileBanner = new WebImage(imagebanner.InputStream);
                if (fileBanner.Height != 960 && fileBanner.Width != 1920)
                {
                    ViewBag.ErrorMessage = "Banner boyutlari 1920x960 Olmali!";
                    return View();
                }
                var uzantiBanner = Path.GetExtension(imagebanner.FileName);
                var yol = Path.Combine(Server.MapPath("~/Content/img"), "banner" + uzantiBanner);
                fileBanner.Save(yol);
            }
            WebImage fileIcon = null;
            if (imageicon != null)
            {
                fileIcon = new WebImage(imageicon.InputStream);
                if (fileIcon.Height != 256 && fileIcon.Width != 256)
                {
                    ViewBag.ErrorMessage = "Ikon boyutlari 256x256 Olmali!";
                    return View();
                }
                var uzantiIcon = Path.GetExtension(imageicon.FileName);
                var yol = Path.Combine(Server.MapPath("~/Content/img"), "favicon" + uzantiIcon);
                fileIcon.Save(yol);

            }
            ViewBag.success = "Site ayarlari basariyla guncellendi.";
            return View();


        }
        [Authorize]
        public ActionResult UrunDuzenle(int prodid)
        {
            var prodserv = new ProductService().GetById(prodid);
            return View(prodserv.Data);
        }
        [Authorize]
        [HttpPost]
        public ActionResult UrunDuzenle(Products prod, HttpPostedFileBase imagename)
        {
            WebImage file = null;
            if (imagename != null)
            {
                file = new WebImage(imagename.InputStream);
                if (file.Height != 512 && file.Width != 512)
                {
                    ViewBag.ErrorMessage = "Resim boyutlari 512x512 Olmali!";
                    var result = new ProductService().GetById(prod.Id);
                    prod.Image = result.Data.Image;
                    return View(prod);
                }
            }
            var prodserv = new ProductService();
            var isfeatured = new ProductService().GetById(prod.Id);
            bool alreadyFeatured = isfeatured.Data.isFeatured;

            if (GetProducts().Where(x => x.isFeatured == true).Count() == 3 && !alreadyFeatured && prod.isFeatured)
            {
                ViewBag.ErrorMessage = "Max. 3 tane one cikarilabilir.";
                return View(prod);
            }
            if (imagename != null)
            {
                var uzanti = Path.GetExtension(imagename.FileName);
                var yol = Path.Combine(Server.MapPath("~/Content/img"), prod.Id.ToString() + uzanti);
                file.Save(yol);
                prod.Image = prod.Id.ToString() + uzanti;
            }
            else
            {
                var result = new ProductService().GetById(prod.Id);
                prod.Image = result.Data.Image;
            }
            var editent = prodserv.Edit(prod);
            if (editent.HasError)
            {
                ViewBag.ErrorMessage = editent.ResultMessage;
                return View(prod);
            }
            ViewBag.successadminmenu = "Basariyla duzenlendi!";
            return View(prod);
        }
        [Authorize]
        public ActionResult Menu()
        {
            return View();
        }
        [Authorize]
        public ActionResult UrunEkle()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult UrunEkle(Products prod, HttpPostedFileBase imagename)
        {
            if (GetProducts().Where(x => x.ProductName == prod.ProductName).Count() >= 1)
            {
                ViewBag.ErrorMessage = "Bu isimde bir urun zaten var!";
                return View();
            }
            WebImage file = null;
            file = new WebImage(imagename.InputStream);
            if (file.Height != 512 && file.Width != 512)
            {
                ViewBag.ErrorMessage = "Resim boyutları 512x512 Olmalidir!";
                return View();
            }
            var bikeservice = new ProductService();
            if (GetProducts().Where(x => x.isFeatured == true).Count() >= 3 && prod.isFeatured == true)
            {
                ViewBag.ErrorMessage = "Max. 3 tane one cikarilabilir..";
                return View();
            }
            var getbikebb = bikeservice.New(prod);
            var getbike = bikeservice.Get(prod.Id);
            var uzanti = Path.GetExtension(imagename.FileName);
            var yol = Path.Combine(Server.MapPath("~/Content/img"), prod.Id.ToString() + uzanti);
            prod.Image = prod.Id.ToString() + uzanti;
            bikeservice.Edit(prod);
            file.Save(yol);
            ViewBag.successadminmenu = "Urun basariyla eklendi!";
            return RedirectToAction("Menu", "Admin");
        }
        [Authorize]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        public ActionResult KategoriDuzenle(int catid)
        {
            var catservice = new CategoryService().Get(catid);
            return View(catservice.Data);
        }
        [Authorize]
        [HttpPost]
        public ActionResult KategoriDuzenle(Categories cat)
        {
            var catserv = new CategoryService();
            catserv.Edit(cat);
            ViewBag.successadminmenu = "Kategori basariyla duzenlendi.";
            return View();
        }
        [Authorize]
        public ActionResult Kategoriler()
        {
            return View();
        }
        [Authorize]
        public ActionResult UrunSil(int prodid)
        {
            var result = new ProductService().Delete(prodid);
            if (result.HasError)
            {
                ViewBag.ErrorMessage = result.ResultMessage;
                return View();
            }
            ViewBag.successadminmenu = "Urun basariyla silindi";
            return RedirectToAction("Anasayfa", "Magaza");
        }
        [Authorize]
        [HttpGet]
        public ActionResult KategoriSil(int catid)
        {
            var categoryservice = new CategoryService();
            var remove = categoryservice.Delete(catid);
            ViewBag.successadminmenu = "Kategori basariyla silindi";
            return RedirectToAction("Menu", "Admin");
        }
        [Authorize]
        [HttpPost]
        public ActionResult KategoriEkle(Categories cat)
        {
            var catservice = new CategoryService();
            catservice.New(cat);
            ViewBag.successadminmenu = "Kategori basariyla eklendi";
            return RedirectToAction("Menu", "Admin");
        }
        [HttpPost]
        public ActionResult Login(Member memb)
        {
            var memberservice = new MemberService();
            var getMemberResult = memberservice.Auth(memb.UserName, memb.Password);
            if (getMemberResult.HasError)
            {
                ViewBag.mesaj = getMemberResult.ResultMessage;
                return View();
            }
            if (getMemberResult.Data.Status == DataStatus.Banned)
            {
                return RedirectToAction("BannedUser");
            }
            FormsAuthentication.SetAuthCookie(getMemberResult.Data.UserName, false);
            return RedirectToAction("Anasayfa", "Magaza");

        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Anasayfa", "Magaza");
        }
    }
}