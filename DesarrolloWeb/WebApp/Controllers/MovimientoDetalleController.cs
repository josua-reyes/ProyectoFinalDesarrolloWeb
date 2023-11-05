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
    public class MovimientoDetalleController : Controller
    {
        private dbFacturasEntities db = new dbFacturasEntities();

        // GET: MovimientoDetalle
        public async Task<ActionResult> Index()
        {
            var movimientoDetalle = db.MovimientoDetalle.Include(m => m.Movimiento).Include(m => m.Producto);
            return View(await movimientoDetalle.ToListAsync());
        }
        public async Task<ActionResult> VerDetallesMovimientos(int? id)
        {
            var DetallesMovimientos = await db.MovimientoDetalle.Where(b => b.CodMovimiento == id).ToListAsync();

            return View(DetallesMovimientos);
        }


        // GET: MovimientoDetalle/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimientoDetalle movimientoDetalle = await db.MovimientoDetalle.FindAsync(id);
            if (movimientoDetalle == null)
            {
                return HttpNotFound();
            }
            return View(movimientoDetalle);
        }

        // GET: MovimientoDetalle/Create
        public ActionResult Create()
        {
            ViewBag.CodMovimiento = new SelectList(db.Movimiento, "CodMovimiento", "Descripcion");
            ViewBag.CodProducto = new SelectList(db.Producto, "CodProducto", "Nombre");
            return View();
        }

        // POST: MovimientoDetalle/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodMovimientoDetalle,CodMovimiento,CodProducto,Cantidad,Precio")] MovimientoDetalle movimientoDetalle)
        {
            if (ModelState.IsValid)
            {
                db.MovimientoDetalle.Add(movimientoDetalle);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CodMovimiento = new SelectList(db.Movimiento, "CodMovimiento", "Descripcion", movimientoDetalle.CodMovimiento);
            ViewBag.CodProducto = new SelectList(db.Producto, "CodProducto", "Nombre", movimientoDetalle.CodProducto);
            return View(movimientoDetalle);
        }

        // GET: MovimientoDetalle/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimientoDetalle movimientoDetalle = await db.MovimientoDetalle.FindAsync(id);
            if (movimientoDetalle == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodMovimiento = new SelectList(db.Movimiento, "CodMovimiento", "Descripcion", movimientoDetalle.CodMovimiento);
            ViewBag.CodProducto = new SelectList(db.Producto, "CodProducto", "Nombre", movimientoDetalle.CodProducto);
            return View(movimientoDetalle);
        }

        // POST: MovimientoDetalle/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodMovimientoDetalle,CodMovimiento,CodProducto,Cantidad,Precio")] MovimientoDetalle movimientoDetalle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movimientoDetalle).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CodMovimiento = new SelectList(db.Movimiento, "CodMovimiento", "Descripcion", movimientoDetalle.CodMovimiento);
            ViewBag.CodProducto = new SelectList(db.Producto, "CodProducto", "Nombre", movimientoDetalle.CodProducto);
            return View(movimientoDetalle);
        }

        // GET: MovimientoDetalle/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimientoDetalle movimientoDetalle = await db.MovimientoDetalle.FindAsync(id);
            if (movimientoDetalle == null)
            {
                return HttpNotFound();
            }
            return View(movimientoDetalle);
        }

        // POST: MovimientoDetalle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MovimientoDetalle movimientoDetalle = await db.MovimientoDetalle.FindAsync(id);
            db.MovimientoDetalle.Remove(movimientoDetalle);
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
