using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMIT02_MVC5.Models;
namespace SMIT02_MVC5.Controllers
{
    public class ProductController : Controller
    {
        CItemService itemService = new CItemService();
        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(int Id) {
            
            return View(itemService.GetItemDetail(Id));
        }
        //[HttpPost]
        //public ActionResult Detail() {
        //    return Content("return Detail");
        //}
    }
}