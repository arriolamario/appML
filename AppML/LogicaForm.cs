using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppML
{
    public class LogicaForm
    {
        public frmSeleccionMonto principal { get; set; }
        public frmSeleccionBanco seleccionBanco { get; set; }
        public frmSeleccionCuotas seleccionCuotas { get; set; }
        public frmSeleccionPago seleccionPago { get; set; }
        public bool cerrarPrincipal { get; set; }

        public LogicaForm()
        {
            this.cerrarPrincipal = true;
        }

    }
}
