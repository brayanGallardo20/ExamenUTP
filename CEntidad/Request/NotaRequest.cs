using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEntidad.Request
{
   public class NotaRequest
    {
        public int INT_ALUMNOID { get; set; }
        public int INT_CURSO_ID { get; set; }

        public decimal DEC_NOTA { get;set;}
    }
}
