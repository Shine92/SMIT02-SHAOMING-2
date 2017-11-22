using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using SMIT02_MVC5.Models;

namespace SMIT02_MVC5.Controllers
{
    /*
    WebApiConfig 類別可能需要其他變更以新增此控制器的路由，請將這些陳述式合併到 WebApiConfig 類別的 Register 方法。注意 OData URL 有區分大小寫。

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using SMIT02_MVC5.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<CartBuy>("CartBuys");
    builder.EntitySet<Item>("Items"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class CartBuysController : ODataController
    {
        private DB1006Entities db = new DB1006Entities();

        // GET: odata/CartBuys
        [EnableQuery]
        public IQueryable<CartBuy> GetCartBuys()
        {
            return db.CartBuys;
        }

        // GET: odata/CartBuys(5)
        [EnableQuery]
        public SingleResult<CartBuy> GetCartBuy([FromODataUri] string key)
        {
            return SingleResult.Create(db.CartBuys.Where(cartBuy => cartBuy.Cart_Id == key));
        }

        // PUT: odata/CartBuys(5)
        public IHttpActionResult Put([FromODataUri] string key, Delta<CartBuy> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CartBuy cartBuy = db.CartBuys.Find(key);
            if (cartBuy == null)
            {
                return NotFound();
            }

            patch.Put(cartBuy);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartBuyExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(cartBuy);
        }

        // POST: odata/CartBuys
        public IHttpActionResult Post(CartBuy cartBuy)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CartBuys.Add(cartBuy);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CartBuyExists(cartBuy.Cart_Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(cartBuy);
        }

        // PATCH: odata/CartBuys(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] string key, Delta<CartBuy> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CartBuy cartBuy = db.CartBuys.Find(key);
            if (cartBuy == null)
            {
                return NotFound();
            }

            patch.Patch(cartBuy);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartBuyExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(cartBuy);
        }

        // DELETE: odata/CartBuys(5)
        public IHttpActionResult Delete([FromODataUri] string key)
        {
            CartBuy cartBuy = db.CartBuys.Find(key);
            if (cartBuy == null)
            {
                return NotFound();
            }

            db.CartBuys.Remove(cartBuy);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/CartBuys(5)/Item
        [EnableQuery]
        public SingleResult<Item> GetItem([FromODataUri] string key)
        {
            return SingleResult.Create(db.CartBuys.Where(m => m.Cart_Id == key).Select(m => m.Item));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CartBuyExists(string key)
        {
            return db.CartBuys.Count(e => e.Cart_Id == key) > 0;
        }
    }
}
