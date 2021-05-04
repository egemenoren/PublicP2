using AyazNew.Entity;
using AyazNew.Service;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AyazNew.Controllers
{
    public class HomeController : BaseController
    {


        public int? category;
        public HomeController()
        {

        }
        [Route("Magaza")]
        public ActionResult Index()
        {

            return View();
        }
        //        [Route("hakkimizda")]
        //        public ActionResult Iletisim()
        //        {
        //            return View();
        //        }

        //        [HttpGet]
        //        public ActionResult Remove(int bikeid)
        //        {
        //            if (CheckAdmin())
        //            {
        //                var banbike = new ProductService();
        //                var result = banbike.Delete(bikeid);
        //                if (result.HasError)
        //                {
        //                    ViewBag.result = result.ResultMessage;
        //                }

        //                TempData["Success"] = "Successfully Removed";



        //                return RedirectToAction("Index", "Home");
        //            }
        //            return RedirectToAction("Index", "Home");
        //        }
        //        [HttpGet]
        //        public ActionResult Edit(int bikeid)
        //        {
        //            if (CheckAdmin())
        //            {
        //                var result = new ProductService().Get(bikeid);
        //                return View(result.Data);
        //            }
        //            return RedirectToAction("Index", "Home");
        //        }
        //        [HttpPost]
        //        public ActionResult Edit(Products entity)
        //        {
        //            if (CheckAdmin())
        //            {
        //                var EntityToUpdate = new ProductService().Edit(entity);
        //                TempData["Success"] = "Successfully Updated.";
        //                return RedirectToAction("Index", "Home");
        //            }
        //            return RedirectToAction("Index", "Home");
        //        }
        //        [HttpGet]
        //        [Route("urunara")]
        //        public ActionResult Ara(string q, int sayfa = 1, int pagesize = 18)
        //        {
        //            // if (!string.IsNullOrEmpty(q) && !string.IsNullOrWhiteSpace(q))

        //            var searching = new ProductService().GetAll().Where(x => x.ProductName.ToLower().Contains(q.ToLower()) && x.Status == DataStatus.Active);
        //            PagedList<Products> pagedlist = new PagedList<Products>(searching, sayfa, pagesize);
        //            return View(pagedlist);

        //        }
        //        [HttpGet]
        //        [Route("{kategoriadi}-{kategori}-{sayfa}")]
        //        public ActionResult Kategori(int? kategori, int sayfa = 1, int pagesize = 18)
        //        {
        //            var degerler = new ProductService().GetAll().Where((x => x.Status == DataStatus.Active && x.CategoryID == kategori));
        //            PagedList<Products> pagedlist = new PagedList<Products>(degerler, sayfa, pagesize);
        //            return View(pagedlist);
        //        }

        //        [HttpGet]
        //        [Route("Urunler/{sayfa}")]
        //        public ActionResult Urunler(int sayfa = 1, int pagesize = 18)
        //        {
        //            var degerler = new ProductService().GetAll().Where((x => x.Status == DataStatus.Active));
        //            PagedList<Products> pagedlist = new PagedList<Products>(degerler, sayfa, pagesize);
        //            return View(pagedlist);
        //        }


        //        [Route("urundetayi/{urunadi}-{id}")]
        //        public ActionResult Detay(int id)
        //        {
        //            if (User.Identity.IsAuthenticated)
        //            {
        //                var getmember = new MemberService().GetUserName(User.Identity.Name);
        //                if (getmember.Data.IsAdmin == 1)
        //                    ViewBag.admin = 1;
        //            }
        //            var result = new ProductService().Get(id);
        //            if (result.Data.Status != DataStatus.Active && !CheckAdmin())
        //                return RedirectToAction("Anasayfa", "Magaza");
        //            if (result.HasError)
        //            {
        //                ShowErrorMessage(result.ResultMessage);
        //                return View();
        //            }
        //            return View(result.Data);
        //        }
        //}
    }
} 
