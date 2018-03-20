using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepeatCaller.Models
{
    public class BaseDTO
    {
        public int baseId { get; set; }
        public int userId { get; set; }
        public int campaniaId { get; set; }
        public string fechaHora { get; set; }
        public string nombreArchivo { get; set; }
        public string tipo { get; set; }
    }
}