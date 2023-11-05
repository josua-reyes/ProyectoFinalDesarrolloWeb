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
    public class FacturaController : Controller
    {
        private dbFacturasEntities db = new dbFacturasEntities();

        // GET: Facturas
        public async Task<ActionResult> Index()
        {
            var factura = db.Factura.Include(f => f.Bodega);
            return View(await factura.ToListAsync());
        }

        // GET: Facturas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = await db.Factura.FindAsync(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // GET: Facturas/Create
        public ActionResult Create()
        {
            ViewBag.CodBodega = new SelectList(db.Bodega, "CodBodega", "Nombre");
            return View();
        }

        // POST: Facturas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CodFactura,Serie,Numero,CodBodega,Fecha,NIT,NombreCliente,DireccionCliente,Estado")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Factura.Add(factura);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CodBodega = new SelectList(db.Bodega, "CodBodega", "Nombre", factura.CodBodega);
            return View(factura);
        }

        // GET: Facturas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = await db.Factura.FindAsync(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.CodBodega = new SelectList(db.Bodega, "CodBodega", "Nombre", factura.CodBodega);
            return View(factura);
        }

        // POST: Facturas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CodFactura,Serie,Numero,CodBodega,Fecha,NIT,NombreCliente,DireccionCliente,Estado")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                db.Entry(factura).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CodBodega = new SelectList(db.Bodega, "CodBodega", "Nombre", factura.CodBodega);
            return View(factura);
        }

        // GET: Facturas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Factura factura = await db.Factura.FindAsync(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Factura factura = await db.Factura.FindAsync(id);
            db.Factura.Remove(factura);
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
