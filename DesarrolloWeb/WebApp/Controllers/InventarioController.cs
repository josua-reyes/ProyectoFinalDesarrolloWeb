using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApp;

namespace WebApp.Controllers
{
    public class InventarioController : Controller
    {
        private dbFacturasEntities db = new dbFacturasEntities();

        // GET: Inventario
        public async Task<ActionResult> ProductoXBodega(int? id)
        {
            var ProductoXBodega = await db.Inventario.Where(b => b.CodProducto == id).ToListAsync();

            return View(ProductoXBodega);
        }
        public async Task<ActionResult> ProductosXBodega(int? id)
        {
            var ProductoXBodega = await db.Inventario.Where(b => b.CodBodega == id).ToListAsync();

            return View(ProductoXBodega);
        }

        public async Task<ActionResult> Index()
        {
            var inventario = db.Inventario.Include(i => i.Bodega).Include(i => i.Producto);
            return View(await inventario.ToListAsync());
        }

        // GET: Inventario/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventario inventario = await db.Inventario.FindAsync(id);
            if (inventario == null)
            {
                return HttpNotFound();
            }
            return View(inventario);
        }

        // GET: Inventario/Create
        public ActionResult Create()
        {
            ViewBag.CodBodega = new SelectList(db.Bodega, "CodBodega", "Nombre");
            ViewBag.CodProducto = new SelectList(db.Producto, "CodProducto", "Nombre");
            return View();
        }

        // POST: Inventario/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodInventario,CodBodega,CodProducto,Existencia,FechaActualizacion,Precio")] Inventario inventario)
        {
            if (ModelState.IsValid)
            {
                db.Inventario.Add(inventario);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CodBodega = new SelectList(db.Bodega, "CodBodega", "Nombre", inventario.CodBodega);
            ViewBag.CodProducto = new SelectList(db.Producto, "CodProducto", "Nombre", inventario.CodProducto);
            return View(inventario);
        }

        // GET: Inventario/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventario inventario = await db.Inventario.FindAsync(id);
            if (inventario == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodBodega = new SelectList(db.Bodega, "CodBodega", "Nombre", inventario.CodBodega);
            ViewBag.CodProducto = new SelectList(db.Producto, "CodProducto", "Nombre", inventario.CodProducto);
            return View(inventario);
        }

        // POST: Inventario/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodInventario,CodBodega,CodProducto,Existencia,FechaActualizacion,Precio")] Inventario inventario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(inventario).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CodBodega = new SelectList(db.Bodega, "CodBodega", "Nombre", inventario.CodBodega);
            ViewBag.CodProducto = new SelectList(db.Producto, "CodProducto", "Nombre", inventario.CodProducto);
            return View(inventario);
        }

        // GET: Inventario/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Inventario inventario = await db.Inventario.FindAsync(id);
            if (inventario == null)
            {
                return HttpNotFound();
            }
            return View(inventario);
        }

        // POST: Inventario/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Inventario inventario = await db.Inventario.FindAsync(id);
            db.Inventario.Remove(inventario);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
