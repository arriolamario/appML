using RestSharp;
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
    public partial class frmSeleccionPago : Formulario
    {
        RestServices srv;
        private DatosPago datosPago;
        private LogicaForm logicaForm;
        public frmSeleccionPago()
        {
        }

        public frmSeleccionPago(DatosPago datosPago, LogicaForm logicaForm)
        {
            InitializeComponent();
            // TODO: Complete member initialization
            this.datosPago = datosPago;
            srv = new RestServices();
            this.logicaForm = logicaForm;
            SetDescripcion(this.datosPago);
            
        }

        private void frmSeleccionPago_Load(object sender, EventArgs e)
        {
            var mediosDePago = srv.GetMedioPago();

            //cbFormaPago.Items.AddRange(mediosDePago.ToArray());

            cbFormaPago.DataSource = mediosDePago.Where(x => x.payment_type_id == "credit_card" & x.status == "active").ToList();
            cbFormaPago.DisplayMember = "name";
            logicaForm.principal.Visible = false;
            logicaForm.seleccionPago = this;
        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            datosPago.Tarjeta = (MedioPago)cbFormaPago.SelectedItem;

            new frmSeleccionBanco(datosPago, logicaForm).Show();

        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            logicaForm.principal.Activar();
            logicaForm.seleccionPago.Close();
        }




        internal void Activar()
        {
            this.Visible = true;
            this.Focus();
        }

        private void frmSeleccionPago_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (logicaForm.cerrarPrincipal)
            {
                logicaForm.principal.Close();
            }
            else
            {
                logicaForm.cerrarPrincipal = true;
            }
        }
    }
}
