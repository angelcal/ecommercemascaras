using Ecommerce.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class PaqueteriaController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Paqueteria
        [Authorize(Roles = "Empleado")]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control de ventas") || user.Puesto.Equals("Director Administrativo")))
                {
                    ViewBag.ventas = db.Ventas.ToList();
                    return View();
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            return View();
        }

        [Authorize(Roles = "Empleado")]
        public ActionResult Paquete(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control de ventas") || user.Puesto.Equals("Director Administrativo")))
                {
                    ViewBag.devoluciones = db.Ventas.Where(p => p.Id.Equals(id)).ToList();
                    return View();
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Paquete(int? id, Paqueteria pa)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var client = db.Ventas.Single(x => x.Id == id);

            // var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //var user = userManager.FindById(User.Identity.GetUserId());
            //Cliente cliente = db.Cliente.Where(c => c.Id_users == user.Id).FirstOrDefault();
            Ventas venta = db.Ventas.Where(c => c.Id == id).FirstOrDefault();
            Paqueteria paq = new Paqueteria
            {
                Venta = venta,
                nombre = pa.nombre,
                orden_paq = pa.orden_paq,
                status = 1,
                FechaEnvio = DateTime.Now,
                FechaEntrega = DateTime.Now
            };
            db.Paqueteria.Add(paq);
            await db.SaveChangesAsync();
            //db.Devoluciones.Add(dev);
            client.Status = 2;
            client.FechaVenta = DateTime.Now;
            //db.ExplicaDevolucion.Add(explicaDevolucion);
            db.SaveChanges();
            //await db.SaveChangesAsync();

            return RedirectToAction("Historial");

        }

        [Authorize(Roles = "Empleado")]
        public ActionResult Historial()
        {
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control de ventas") || user.Puesto.Equals("Director Administrativo")))
                {
                    ViewBag.paqueteria = db.Paqueteria.ToList();
                    return View();
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            return View();
        }

        public async Task<ActionResult> Entregar(int? id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var client = db.Ventas.Single(x => x.Id == id);
            var paquete = db.Paqueteria.Single(x => x.Venta.Id==id);
            // var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //var user = userManager.FindById(User.Identity.GetUserId());
            //Cliente cliente = db.Cliente.Where(c => c.Id_users == user.Id).FirstOrDefault();
            //Productos productos = db.Productos.Where(c => c.Id == id).FirstOrDefault();

            //db.Devoluciones.Add(dev);
            client.Status = 3;
            paquete.status = 2;
            paquete.FechaEntrega = DateTime.Now;
            //db.ExplicaDevolucion.Add(explicaDevolucion);
            db.SaveChanges();

            return RedirectToAction("Historial");
        }
    }
}