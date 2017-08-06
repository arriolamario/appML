using ServicioRestML;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AppML
{
    public partial class frmSeleccionMonto : Formulario
    {
        DatosPago datosPago;
        LogicaForm logicaForm;
        public frmSeleccionMonto()
        {
            InitializeComponent();
            
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            datosPago = new DatosPago();
            logicaForm = new LogicaForm();
            logicaForm.principal = this;
            SetDescripcion(datosPago);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double monto;
            bool res = Double.TryParse(tbMonto.Text, out monto);

            if (res)
            {
                datosPago.Monto = monto;
                new frmSeleccionPago(datosPago, logicaForm).Show();
                this.tbMonto.Clear();
            }
            else
            {
                MessageBox.Show("Ingrese un monto correcto");
            }

        }

        internal void Activar()
        {
            this.Visible = true;
            this.Focus();
            datosPago = new DatosPago();
            SetDescripcion(datosPago);
        }

        
    }
}
