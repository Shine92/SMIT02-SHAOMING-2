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
    builder.EntitySet<Item>("Items");
    builder.EntitySet<CartBuy>("CartBuys"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ItemsController : ODataController
    {
        private DB1006Entities db = new DB1006Entities();

        // GET: odata/Items
        [EnableQuery]
        public IQueryable<Item> GetItems()
        {
            return db.Items;
        }

        // GET: odata/Items(5)
        [EnableQuery]
        public SingleResult<Item> GetItem([FromODataUri] int key)
        {
            return SingleResult.Create(db.Items.Where(item => item.Id == key));
        }

        // PUT: odata/Items(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Item> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Item item = db.Items.Find(key);
            if (item == null)
            {
                return NotFound();
            }

            patch.Put(item);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(item);
        }

        // POST: odata/Items
        public IHttpActionResult Post(Item item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Items.Add(item);
            db.SaveChanges();

            return Created(item);
        }

        // PATCH: odata/Items(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Item> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Item item = db.Items.Find(key);
            if (item == null)
            {
                return NotFound();
            }

            patch.Patch(item);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(item);
        }

        // DELETE: odata/Items(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Item item = db.Items.Find(key);
            if (item == null)
            {
                return NotFound();
            }

            db.Items.Remove(item);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Items(5)/CartBuys
        [EnableQuery]
        public IQueryable<CartBuy> GetCartBuys([FromODataUri] int key)
        {
            return db.Items.Where(m => m.Id == key).SelectMany(m => m.CartBuys);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ItemExists(int key)
        {
            return db.Items.Count(e => e.Id == key) > 0;
        }
    }
}
