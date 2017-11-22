using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SMIT02_MVC5.Models;
namespace SMIT02_MVC5.Controllers {
    public class MemberController : Controller {


        private DB1006Entities db = new DB1006Entities();

        [HttpPost]
        public ActionResult test(int num) {
            var query = from o in db.Items
                        orderby o.Name.Take(num)
                        select o;
            return Json(query.ToList());
        }


        CMemberService memberSerivce = new CMemberService();
        // GET: Member

        #region 註冊
        public ActionResult Register() {
            Member member = new Member();

            if (User.Identity.IsAuthenticated) {
                return RedirectToAction("Index", "Home");
            }
            return View(member);
        }

        [HttpPost]
        public ActionResult Register(Member newMember) {
            if (memberSerivce.AccountCheck(newMember.Account)) {
                memberSerivce.Register(newMember);
                TempData["Account"] = newMember.Account;
                TempData["Name"] = newMember.Name;
                TempData["Email"] = newMember.Email;
                return RedirectToAction("Index","Home");
            } else {
                TempData["CheckResult"] = "帳號已被註冊";
                return View(newMember);
            }
            //return View();
        }
        #endregion
        #region 會員資料
        public ActionResult MemberCenter(Member Member) {
            Session["Account"] = TempData["Account"];
            if (Session["Account"] == null) {
                return RedirectToAction("Login", "Member");
            }
            //Session["Name"] = TempData["Name"];
            //Session["Email"] = TempData["Email"];
            return View();
        }
        #endregion
        #region 登入
        public ActionResult Login() {

            TempData["Account"] = "";
            TempData["Password"] = "";
            TempData["ErrorMsg"] = "";
            return View();
        }

        [HttpPost]
        public ActionResult Login(string Account, string Password) {
            if (memberSerivce.AccountLogin(Account, Password)) {
                Session["Account"] = Account;
                if (memberSerivce.GetRole(Account)) {
                    return RedirectToAction("Index", "Manage");
                    //return Content("OK");
                } else {
                    var MemberId = memberSerivce.GetMemberId(Account);
                     Response.Cookies["id"].Value = MemberId.ToString();
                    TempData["MemberId"] = Request.Cookies["id"];
                    Session["Account"] = Account;
                    return RedirectToAction("Index","Home");
                }
            } else {
                TempData["Account"] = Account;
                TempData["Password"] = Password;
                TempData["ErrorMsg"] = "帳號或密碼錯誤，請重新輸入";
            }
            return View();
        }
        #endregion
        #region 登出
        public ActionResult Logout() {
            //FormsAuthentication.SignOut();
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Member");
        }
        #endregion
        #region 購物車
        public ActionResult Cart() {

            return View();
        }
        #endregion
    }
}