using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class Devoluciones
    {
        public int Id { get; set; }
        public virtual Productos Producto { get; set; }
        public virtual Cliente Cliente { get; set; }
        [StringLength(120)]
        public string motivo { get; set; }
        public int status { get; set; }
    }
}