using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ServicioRestML;

namespace AppML
{
    public partial class Formulario : Form
    {
        public Formulario()
        {
            InitializeComponent();
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {

        }

        private void UserControl1_ClientSizeChanged(object sender, EventArgs e)
        {
        }

        internal void SetDescripcion(DatosPago datos)
        {
            lblMontoSeleccionado.Text = datos.Monto == 0 ? "No Seleccionado" : String.Format("$ {0}", datos.Monto);
            lblTarjetaSeleccionada.Text = String.IsNullOrEmpty(datos.Tarjeta.name) ? "No Seleccionado" : datos.Tarjeta.name;
            lblBancoSeleccionado.Text = String.IsNullOrEmpty(datos.Banco.name) ? "No Seleccionado" : datos.Banco.name;
            lblCuotaSeleccionada.Text = datos.Cuotas.Count == 0 ? "No Seleccionado" : datos.Cuotas[0].payer_costs.FirstOrDefault(x => x.installments == datos.CuotaSeleccionada).recommended_message;
        }

        internal void SetCuotas(string cuotas)
        {
        }
    }
}
