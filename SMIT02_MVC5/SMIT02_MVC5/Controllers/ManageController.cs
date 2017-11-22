using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMIT02_MVC5.Models;
namespace SMIT02_MVC5.Controllers {
    public class ManageController : Controller {
        CItemService itemService = new CItemService();
        CMemberService memberService = new CMemberService();
        Item item = new Item();
        Member member = new Member();
        // GET: Manage
        #region 管理員首頁
        public ActionResult Index() {

            if (!Session["Account"].ToString().Equals("Admin")) {
                return RedirectToAction("Index", "Home");
            } else {
                return View();
            }
        }
        #endregion
        #region 會員管理
        public ActionResult MemberBoard() {
            return View(memberService.GetMemberList());
        }
        #endregion
        #region 新增商品
        public ActionResult Create() {

            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection frm) {
            HttpPostedFileBase file1 = Request.Files["ImageFile"];
            string UploadedPathFileName = Server.MapPath("~/Images") + "\\" + file1.FileName;

            file1.SaveAs(UploadedPathFileName);

            item.Id = Convert.ToInt32(frm["Id"]);
            item.Name = frm["Name"];
            item.Price = Convert.ToInt32(frm["Price"]);
            item.UnitStock = Convert.ToInt32(frm["UnitStock"]);
            item.Image = file1.FileName;

            itemService.CreateItem(item);
            return RedirectToAction("ItemList","Manage");
            //return Content(frm["Id"] + frm["Name"] + frm["Price"] + frm["UnitStock"] + UploadedPathFileName);
            //return Content("get:" + UploadedPathFileName);
        }
        #endregion
        #region 商品列表
        public ActionResult ItemList() {
            return View(itemService.GetItemList());
        }
        #endregion
    }
}