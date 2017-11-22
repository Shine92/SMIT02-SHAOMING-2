using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SMIT02_MVC5.Models;
namespace SMIT02_MVC5.Models {
    public class CItemService {
        private DB1006Entities db = new DB1006Entities();
        #region 新增商品
        public void CreateItem(Item Item){
            db.Items.Add(Item);
            db.SaveChanges();
        }
        #endregion
        #region 商品列表
        public List<Item> GetItemList() {
            var query = from o in db.Items
                        select o;
            return (query.ToList());
        }
        #endregion
        #region 商品詳情
        public List<Item> GetItemDetail(int Id) {
            var query = from o in db.Items
                        where o.Id == Id
                        select o;

            return query.ToList();
        }
        #endregion
    }
}