using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class ExplicaDevolucion
    {
        public int Id { get; set; }
        [StringLength(120)]
        public string explicacion { get; set; }
        public virtual Devoluciones Devolucion { get; set; }
    }
}