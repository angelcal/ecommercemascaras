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
    public class DevolucionController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Devolucion
        [Authorize(Roles = "Empleado")]
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control de ventas") || user.Puesto.Equals("Director Administrativo")))
                {
                    ViewBag.devoluciones = db.Devoluciones.ToList();
                    return View();
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            return View();
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
                    ViewBag.devolucionesexp = db.ExplicaDevolucion.ToList();
                    return View();
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            return View();
        }

        
        public async Task<ActionResult> Aceptar(int? id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var client = db.Devoluciones.Single(x => x.Id == id);
            // var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //var user = userManager.FindById(User.Identity.GetUserId());
            //Cliente cliente = db.Cliente.Where(c => c.Id_users == user.Id).FirstOrDefault();
            //Productos productos = db.Productos.Where(c => c.Id == id).FirstOrDefault();

            //db.Devoluciones.Add(dev);
            client.status = 2;
            //db.ExplicaDevolucion.Add(explicaDevolucion);
            db.SaveChanges();
            return RedirectToAction("MotivosAdmin", id);
        }

        
        public async Task<ActionResult> Rechazar(int? id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var client = db.Devoluciones.Single(x => x.Id == id);
            // var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //var user = userManager.FindById(User.Identity.GetUserId());
            //Cliente cliente = db.Cliente.Where(c => c.Id_users == user.Id).FirstOrDefault();
            //Productos productos = db.Productos.Where(c => c.Id == id).FirstOrDefault();

            //db.Devoluciones.Add(dev);
            client.status = 3;
            //db.ExplicaDevolucion.Add(explicaDevolucion);
            db.SaveChanges();
            return RedirectToAction("MotivosAdmin", id);
        }

        [Authorize(Roles = "Empleado")]
        public ActionResult MotivosAdmin(int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Control de ventas") || user.Puesto.Equals("Director Administrativo")))
                {
                    ViewBag.devoluciones = db.Devoluciones.Where(p => p.Id.Equals(id)).ToList();
                    return View();
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MotivosAdmin(int? id, ExplicaDevolucion de)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var client = db.ExplicaDevolucion.Single(x => x.Id == id);
            // var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //var user = userManager.FindById(User.Identity.GetUserId());
            //Cliente cliente = db.Cliente.Where(c => c.Id_users == user.Id).FirstOrDefault();
            //Productos productos = db.Productos.Where(c => c.Id == id).FirstOrDefault();

            //db.Devoluciones.Add(dev);
            client.explicacion = de.explicacion;
            //db.ExplicaDevolucion.Add(explicaDevolucion);
            db.SaveChanges();
            //await db.SaveChangesAsync();

            return RedirectToAction("Historial");

        }
    }
}