using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Ofertas
    {
        public int Id { get; set; }
        public virtual Productos Producto { get; set; }
        public double Precio_anterior { get; set; }
        public int porcentaje { get; set; }
        public int status { get; set; }
    }
}