using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicioRestML
{
    public class DatosPago
    {
        public double Monto { get; set; }

        public MedioPago Tarjeta { get; set; }

        public BancosDTO Banco { get; set; }

        public int CuotaSeleccionada { get; set; }

        public List<CuotasDTO> Cuotas { get; set; }


        public DatosPago()
        {
            Tarjeta = new MedioPago();
            Banco = new BancosDTO();

            Cuotas = new List<CuotasDTO>();
        }
    }
}
