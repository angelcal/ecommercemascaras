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
    public class FacturaController : Controller
    {
        //ApplicationDbContext db = new ApplicationDbContext();

        // GET: Factura
        [Authorize(Roles = "Empleado")]
        public ActionResult Index()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Pago a Proveedores") || user.Puesto.Equals("Director Administrativo")))
                {
                    ViewBag.facturas = db.Compras.Where(p => p.Status.Equals(1)).ToList();
                    return View();
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            return View();
        }

        public async Task<ActionResult> Pago(int? id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            var client = db.Compras.Single(x => x.Id == id);
            // var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //var user = userManager.FindById(User.Identity.GetUserId());
            //Cliente cliente = db.Cliente.Where(c => c.Id_users == user.Id).FirstOrDefault();
            //Productos productos = db.Productos.Where(c => c.Id == id).FirstOrDefault();

            //db.Devoluciones.Add(dev);
            client.Status = 2;
            client.FechaCompra = DateTime.Now;
            //db.ExplicaDevolucion.Add(explicaDevolucion);
            db.SaveChanges();
            return RedirectToAction("Historial");
        }


        [Authorize(Roles = "Empleado")]
        public ActionResult Historial()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Pago a Proveedores") || user.Puesto.Equals("Director Administrativo")))
                {
                    ViewBag.facturas = db.Compras.Where(p => p.Status.Equals(2)).ToList();
                    return View();
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            return View();
        }

        public ActionResult Details(int? id)
        {
            ApplicationDbContext db = new ApplicationDbContext();
            if (User.Identity.IsAuthenticated)
            {
                var iduser = User.Identity.GetUserId();
                Empleados user = db.Empleados.Where(p => p.Id_users.Equals(iduser)).First();

                if (user.Active && (user.Puesto.Equals("Pago a Proveedores") || user.Puesto.Equals("Director Administrativo")))
                {
                    //ViewBag.compra = db.Compras.Where(p => p.Id.Equals(id)).ToList();
                    Compras compra = db.Compras.Find(id);
                    ViewBag.Detalles_Compra = compra.DetallesCompras.ToList();
                    return View();
                }
                return RedirectToAction("Denegate", "Empleados", user);

            }
            return View();
        }
    }
}