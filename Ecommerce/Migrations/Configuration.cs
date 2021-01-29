namespace Ecommerce.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Ecommerce.Models;
    using System.Collections.Generic;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<Ecommerce.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Ecommerce.Models.ApplicationDbContext context)
        {
            SeedCatagoProductos(context);           
            SeedEmpleados(context);
            SeedProveedores(context);
        }

        private void SeedEmpleados(ApplicationDbContext db) {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            roleManager.Create(new IdentityRole("Administrador"));
            roleManager.Create(new IdentityRole("Empleado"));


            var user_rh = new ApplicationUser {
                UserName = "recursoshumanos",
                Email = "recursoshumanos@gmail.com" };
            var rh = userManager.Create(user_rh, "recursoshumanos");

            if (rh.Succeeded) {

                userManager.AddToRole(user_rh.Id, "Empleado");

                Empleados w_rh = new Empleados();
                w_rh.Id_users = user_rh.Id;
                w_rh.Nombre = "Angel Becerril Calderon";
                w_rh.Sexo = false;
                w_rh.Salario = 9000;
                w_rh.Puesto = "Administrador de recursos humanos";
                w_rh.Area = "Recursos Humanos";
                w_rh.Fecha_Nacimeinto = new DateTime(1998, 8, 29);
                w_rh.Estado = "Mexico";
                w_rh.Municipio = "Toluca";
                w_rh.CodigoPostal = 51680;
                w_rh.Colonia = "Santin";
                w_rh.Calle = "Garza";
                w_rh.NoInterior = 1;
                w_rh.NoExterior = 350;
                w_rh.Referencia = "Puerta azul";
                w_rh.Registro_Completo= true;
                w_rh.Active = true;

                db.Empleados.AddOrUpdate(w_rh);
            }

            


        }

        private void SeedCatagoProductos(ApplicationDbContext db)
        {
            Catalogos msimple = new Catalogos { Id = 1, name = "Simples" };
            Catalogos mcareta = new Catalogos { Id = 2, name = "Caretas" };
            Catalogos manima = new Catalogos { Id = 3, name = "Animales" };
            Catalogos msuper = new Catalogos { Id = 4, name = "Superheroes" };
            Catalogos mimpor = new Catalogos { Id = 5, name = "Importacion" };


            db.Catalogos.AddOrUpdate(msimple);
            db.Catalogos.AddOrUpdate(mcareta);
            db.Catalogos.AddOrUpdate(manima);
            db.Catalogos.AddOrUpdate(msuper);
            db.Catalogos.AddOrUpdate(mimpor);
            
           

            Productos producto1 = new Productos
            {
                Id = 1,
                Nombre = "Mascara de carnaval negra",
                Descripcion = "Mascara para carnaval negra sencilla sin estilo",
                Url_image = "img/ms1.jpg",
                Sabor = "Negra",
                Marca = "Optima",
                Costo_unitario = 99,
                Porcentage_descuento = 0,
                Status = 1,
                Precio_final = 99,
                activo = true,
                Cantidad_ventas = 0,
                Time_Mount = 12,
                Time_Day = 31,
                Catalogos = new List<Catalogos> { msimple }
            };

            Productos producto2 = new Productos
            {
                Id = 2,
                Nombre = "Mascara de dali",
                Descripcion = "Mascara parecida a la de la serie de la casa de papel",
                Url_image = "img/ms2.jpg",
                Sabor = "Cafe",
                Marca = "Pintorest",
                Costo_unitario = 199,
                Porcentage_descuento = 0,
                Status = 1,
                Precio_final = 199,
                activo = true,
                Cantidad_ventas = 3,
                Time_Mount = 11,
                Time_Day = 4,
                Catalogos = new List<Catalogos> { mimpor }


            };

            Productos producto3 = new Productos
            {
                Id = 3,
                Nombre = "Mascara de tigre",
                Descripcion = "Mascara para niño de tigre",
                Url_image = "img/ms3.jpg",
                Sabor = "Naranja",
                Marca = "Optima",
                Costo_unitario = 119,
                Porcentage_descuento = 0,
                Status = 1,
                Precio_final = 119,
                activo = true,
                Cantidad_ventas = 3,
                Time_Mount = 1,
                Time_Day = 1,
                Catalogos = new List<Catalogos> { manima }
            };

            Productos producto4 = new Productos
            {
                Id = 4,
                Nombre = "Mascara de bufon",
                Descripcion = "Careta de bufon",
                Url_image = "img/ms4.jpg",
                Sabor = "Roja",
                Marca = "Pintorest",
                Costo_unitario = 129,
                Porcentage_descuento = 0,
                Status = 1,
                Precio_final = 129,
                activo = true,
                Cantidad_ventas = 3,
                Time_Mount = 3,
                Time_Day = 3,
                Catalogos = new List<Catalogos> { mcareta}
            };

            Productos producto5 = new Productos
            {
                Id = 5,
                Nombre = "Mascara de hulk",
                Descripcion = "Mascara del superheroe hulk",
                Url_image = "img/ms5.jpg",
                Sabor = "Verde",
                Marca = "Pintorest",
                Costo_unitario =119,
                Porcentage_descuento = 0,
                Status = 1,
                Precio_final = 119,
                activo = true,
                Cantidad_ventas = 3,
                Time_Mount = 5,
                Time_Day = 7,
                Catalogos = new List<Catalogos> { msuper }

            };

            Productos producto6 = new Productos
            {
                Id = 6,
                Nombre = "Mascara de spiderman",
                Descripcion = "Mascara del superheroe spiderman",
                Url_image = "img/ms6.jpg",
                Sabor = "Rojo",
                Marca = "Colors",
                Costo_unitario = 119,
                Porcentage_descuento = 0,
                Status = 1,
                Precio_final = 119,
                activo = true,
                Cantidad_ventas = 3,
                Time_Mount = 3,
                Time_Day = 23,
                Catalogos = new List<Catalogos> { msuper }
            };

            Productos producto7 = new Productos
            {
                Id = 7,
                Nombre = "Mascara de carnaval duo",
                Descripcion = "2 mascaras de carnaval simples",
                Url_image = "img/ms7.png",
                Sabor = "Varios",
                Marca = "Colors",
                Costo_unitario = 119,
                Porcentage_descuento = 0,
                Status = 1,
                Precio_final = 119,
                activo = true,
                Cantidad_ventas = 3,
                Time_Mount = 5,
                Time_Day = 23,
                Catalogos = new List<Catalogos> { msimple }
            };

            Productos producto8 = new Productos
            {
                Id = 8,
                Nombre = "Mascara de ojo rojo",
                Descripcion = "Mascara de exportacion de ojo rojo",
                Url_image = "img/ms8.jpg",
                Sabor = "Roja",
                Marca = "Takara",
                Costo_unitario = 199,
                Porcentage_descuento = 0,
                Status = 1,
                Precio_final = 199,
                activo = true,
                Cantidad_ventas = 0,
                Time_Mount = 10,
                Time_Day = 23,
                Catalogos = new List<Catalogos> { mimpor }
            };

            Productos producto9 = new Productos
            {
                Id = 9,
                Nombre = "Mascara de buho",
                Descripcion = "Mascara para niño de buho",
                Url_image = "img/ms9.jpg",
                Sabor = "Cafe",
                Marca = "Colors",
                Costo_unitario = 99,
                Porcentage_descuento = 0,
                Status = 1,
                Precio_final = 99,
                activo = true,
                Cantidad_ventas = 3,
                Time_Mount = 11,
                Time_Day = 13,
                Catalogos = new List<Catalogos> { manima}
            };

            Productos producto10 = new Productos
            {
                Id = 10,
                Nombre = "Mascara de susto",
                Descripcion = "Careta de susto",
                Url_image = "img/ms10.jpg",
                Sabor = "Blanca",
                Marca = "Pintorest",
                Costo_unitario = 119,
                Porcentage_descuento = 0,
                Status = 1,
                Precio_final = 119,
                activo = true,
                Cantidad_ventas = 10,
                Time_Mount = 12,
                Time_Day = 31,
                Catalogos = new List<Catalogos> { mcareta }
            };

            db.Productos.AddOrUpdate(producto1);
            db.Productos.AddOrUpdate(producto2);
            db.Productos.AddOrUpdate(producto3);
            db.Productos.AddOrUpdate(producto4);
            db.Productos.AddOrUpdate(producto5);
            db.Productos.AddOrUpdate(producto6);
            db.Productos.AddOrUpdate(producto7);
            db.Productos.AddOrUpdate(producto8);
            db.Productos.AddOrUpdate(producto9);
            db.Productos.AddOrUpdate(producto10);

        }
        
       
        private void SeedProveedores(ApplicationDbContext db)
        {
            Provedores prove1 = new Provedores();
            prove1.Id = 1;
            prove1.Nombre = "Mac";
            prove1.Telefono = "7224124088";
            prove1.Correo = "mac@hotmail.com";
            Provedores prove2 = new Provedores();
            prove2.Id = 2;
            prove2.Nombre = "Mas";
            prove2.Telefono = "7171717171";
            prove2.Correo = "mas@hotmail.com";
            Provedores prove3 = new Provedores();
            prove3.Id = 3;
            prove3.Nombre = "mascara";
            prove3.Telefono = "7223456709";
            prove3.Correo = "mascara@hotmail.com";
            Provedores prove4 = new Provedores();
            prove4.Id = 4;
            prove4.Nombre = "Bos";
            prove4.Telefono = "1234567891";
            prove4.Correo = "bos@hotmail.com";
            db.Provedores.AddOrUpdate(prove1);
            db.Provedores.AddOrUpdate(prove2);
            db.Provedores.AddOrUpdate(prove3);
            db.Provedores.AddOrUpdate(prove4);
        }
   
    }
}
