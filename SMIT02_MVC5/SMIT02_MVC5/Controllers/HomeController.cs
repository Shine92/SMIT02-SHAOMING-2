using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMIT02_MVC5.Models;
namespace SMIT02_MVC5.Controllers
{
    public class HomeController : Controller
    {
        CItemService itemService = new CItemService();
        // GET: Home
        public ActionResult Index()
        {

            return View(itemService.GetItemList());
        }
    }
}