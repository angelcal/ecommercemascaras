using Ecommerce.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    [Authorize]
    public class DevolucionesController : Controller
    {
        public ActionResult Index(int id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string usid = User.Identity.GetUserId();
            var cliente = db.Cliente.Single(x => x.Id_users == usid);
            ViewBag.id = id;
            var compra = (from x in db.Productos
                          where x.Id == id
                          select x).ToList();
            ViewBag.producto = compra;

            //var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //var user = userManager.FindById(User.Identity.GetUserId());
            //Cliente cliente2 = db.Cliente.Where(c => c.Id_users == user.Id).FirstOrDefault();
            //ICollection<Devoluciones> detalle = new List<Devoluciones>();
            //Devoluciones devo = new Devoluciones
            //{
            //  Cliente = cliente2,
            //Producto = ViewBag.id,
            //status = 1,

            //};
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(int? id, Devoluciones de)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var user = userManager.FindById(User.Identity.GetUserId());
            Cliente cliente = db.Cliente.Where(c => c.Id_users == user.Id).FirstOrDefault();
            Productos productos = db.Productos.Where(c => c.Id == id).FirstOrDefault();
            Devoluciones dev = new Devoluciones
            {
                Cliente = cliente,
                Producto = productos,
                status = 1,
                motivo = de.motivo
            };
            db.Devoluciones.Add(dev);
            ExplicaDevolucion explicaDevolucion = new ExplicaDevolucion
            {
                Devolucion = dev,
                explicacion = "N"
            };
            db.ExplicaDevolucion.Add(explicaDevolucion);
            await db.SaveChangesAsync();

            return RedirectToAction("Lista");
            
        }
        public ActionResult Lista()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            string usid = User.Identity.GetUserId();
            var cliente = db.Cliente.Single(x => x.Id_users == usid);
            var clienteId = cliente.Id;
            //var devolucionesCliente = (from a in db.Devoluciones
            //                     where a.Cliente.Id == clienteId
            //                     select a).ToList();
            var explicadevo = (from e in db.ExplicaDevolucion
                               join d in db.Devoluciones on e.Devolucion.Id equals d.Id
                               where d.Cliente.Id == clienteId
                               select new devolucionexplica() { devoluciones = d, Explica = e }).ToList();
            ViewBag.explicadevo = explicadevo;
            return View();
        }
    }
}