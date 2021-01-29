using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Paqueteria
    {
        public int Id { get; set; }
        public virtual Ventas Venta { get; set; }
        [StringLength(18)]
        public string orden_paq { get; set; }
        public string nombre { get; set; }
        public DateTime FechaEnvio { get; set; }
        public DateTime FechaEntrega { get; set; }
        public int status { get; set; }
    }
}