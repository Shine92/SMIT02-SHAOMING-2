using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMIT02_MVC5.Models;
namespace SMIT02_MVC5.Models {
    public class CMemberService {
        private DB1006Entities db = new DB1006Entities();
        
        #region 會員資料上傳資料庫
        public void Register(Member newMember) {
            db.Members.Add(newMember);
            db.SaveChanges();
            
        }
        #endregion
        #region 檢查會員帳號是否重複
        public bool AccountCheck(string Account) {
            //Member search = db.Members.Find(Account);
            bool result = false;
            var query = from o in db.Members
                        where o.Account == Account
                        select o;
            if(query.Count() == 1) {
                result = false;
            }
            return true;
        }
        #endregion
        #region 會員登入檢查
        public bool AccountLogin(string Account, string Password) {
            bool result = false;

            var query = from o in db.Members
                        where o.Account == Account && o.Password == Password
                        select o.Account;

            if (query.Count() > 0) {
                result = true;
            }

            return result;
        }
        #endregion
        #region 取得角色
        //取得會員的權限角色資料
        public bool GetRole(string Account) {
            //宣告初始角色字串
            bool RoleAdmin = false;
            //取得傳入帳號的會員資料
            //Member LoginMember = db.Members.Find(Account);
            var query = (from o in db.Members
                         where o.Account == Account && o.IsAdmin == true
                         select o).Any();     

            //判斷資料庫欄位，用以確認是否為Admon
                RoleAdmin = query; //添加Admin

            //回傳最後結果
            return RoleAdmin;
        }
        #endregion
        #region 會員列表
        public List<Member> GetMemberList() {
            var query = from o in db.Members
                        select o;
            return (query.ToList());
        }
        #endregion
        #region 取得會員ID
        public int GetMemberId(string Account) {
            var query = from o in db.Members
                        where o.Account == Account
                        select o.Id;
            return query.Single();
        }
        #endregion
        #region 購物車項目

        #endregion
    }
}