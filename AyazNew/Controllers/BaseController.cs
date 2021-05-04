using AyazNew.Entity;
using AyazNew.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AyazNew.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnException(ExceptionContext filterContext)
        {

            if (filterContext.ExceptionHandled)
                return;
            filterContext.ExceptionHandled = true;
            //todo:log
           // filterContext.Result = RedirectToAction("IslenirkenHata", "Hata");
        }
        public BaseController()
        {
            ViewBag.Cats = GetCategories();
            ViewBag.Prods = GetProducts();
            ViewBag.Description = "Ayaz Promosyon içinde hediyelik eşya veya promosyon ürünleri seni bekliyor.";
        }
        public IEnumerable<Products> FeaturedProducts()
        {
            var itemsFeatured = GetProducts().Where(x => x.Status == DataStatus.Active).OrderBy(x => Guid.NewGuid()).Take(4);
            return itemsFeatured.Take(4);
        }
        public IEnumerable<Products> LastProducts()
        {
           return GetProducts().Where(x => x.Status == DataStatus.Active).Reverse<Products>().Take(4);
        }
        internal void ShowErrorMessage(string message)
        {
            ViewBag.ErrorMessage = message;
        }
        public List<Categories> GetCategories()
        {
            var catserv = new CategoryService();
            return catserv.GetAll().Where(x => x.Status == DataStatus.Active).ToList();
        }
        public List<Products> GetProducts()
        {
            var prodserv = new ProductService();
            return prodserv.GetAll().ToList();
        }
        public bool CheckAdmin()
        {
            string username = User.Identity.Name;
            var member = new MemberService();
            var result = member.GetUserName(username);
            if (User.Identity.IsAuthenticated)
            {
                if (result.Data.IsAdmin != 1)
                    return false;
                else
                    return true;
            }
            else
                return false;
        }

        public string SamePath()
        {
            return Request.UrlReferrer.PathAndQuery.ToString();
        }


    }
}