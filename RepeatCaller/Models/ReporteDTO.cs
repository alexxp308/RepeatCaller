using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RepeatCaller.Models
{
    public class ReporteDTO
    { 
        public int campaniaId { get; set; }
        public string fechaBase { get; set; }
        public int tipo { get; set; }
        public string fechaFinal { get; set; }
    }
}