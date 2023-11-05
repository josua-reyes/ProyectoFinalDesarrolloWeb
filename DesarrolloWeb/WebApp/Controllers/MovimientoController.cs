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
    //[Authorize]
    public class MovimientoController : Controller
    {
        private dbFacturasEntities db = new dbFacturasEntities();

        // GET: Movimiento
        public async Task<ActionResult> Index()
        {
            var movimiento = db.Movimiento.Include(m => m.Bodega);
            return View(await movimiento.ToListAsync());
        }

        // GET: Movimiento/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimiento movimiento = await db.Movimiento.FindAsync(id);
            if (movimiento == null)
            {
                return HttpNotFound();
            }
            return View(movimiento);
        }

        // GET: Movimiento/Create
        public ActionResult Create()
        {
            ViewBag.CodBodega = new SelectList(db.Bodega, "CodBodega", "Nombre");
            return View();
        }

        // POST: Movimiento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodMovimiento,CodBodega,Fecha,Descripcion,TipoMovimiento")] Movimiento movimiento)
        {
            if (ModelState.IsValid)
            {
                db.Movimiento.Add(movimiento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CodBodega = new SelectList(db.Bodega, "CodBodega", "Nombre", movimiento.CodBodega);
            return View(movimiento);
        }

        // GET: Movimiento/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimiento movimiento = await db.Movimiento.FindAsync(id);
            if (movimiento == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodBodega = new SelectList(db.Bodega, "CodBodega", "Nombre", movimiento.CodBodega);
            return View(movimiento);
        }

        // POST: Movimiento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodMovimiento,CodBodega,Fecha,Descripcion,TipoMovimiento")] Movimiento movimiento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(movimiento).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CodBodega = new SelectList(db.Bodega, "CodBodega", "Nombre", movimiento.CodBodega);
            return View(movimiento);
        }

        // GET: Movimiento/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Movimiento movimiento = await db.Movimiento.FindAsync(id);
            if (movimiento == null)
            {
                return HttpNotFound();
            }
            return View(movimiento);
        }

        // POST: Movimiento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Movimiento movimiento = await db.Movimiento.FindAsync(id);
            db.Movimiento.Remove(movimiento);
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
