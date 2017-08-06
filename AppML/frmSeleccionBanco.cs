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
    public partial class frmSeleccionBanco : Formulario
    {
        RestServices srv;
        DatosPago datosPago;
        List<BancosDTO> bancos;
        LogicaForm logicaForm;
        public frmSeleccionBanco(DatosPago datosPago, LogicaForm logicaForm)
        {
            InitializeComponent();
            this.srv = new RestServices();
            this.datosPago = datosPago;
            this.bancos = srv.GetBancos(this.datosPago.Tarjeta.id);
            cbBanco.DataSource = bancos;
            cbBanco.DisplayMember = "name";
            this.logicaForm = logicaForm;
            SetDescripcion(datosPago);

        }

        private void btnSiguiente_Click(object sender, EventArgs e)
        {
            datosPago.Banco = (BancosDTO)cbBanco.SelectedValue;

            new frmSeleccionCuotas(datosPago, logicaForm).Show();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            logicaForm.seleccionPago.Activar();
            logicaForm.seleccionBanco.Close();
        }

        private void frmSeleccionBanco_Load(object sender, EventArgs e)
        {
            logicaForm.seleccionPago.Visible = false;
            logicaForm.seleccionBanco = this;
        }

        internal void Activar()
        {
            this.Visible = true;
            this.Focus();
        }

        private void frmSeleccionBanco_FormClosed(object sender, FormClosedEventArgs e)
        {
            logicaForm.seleccionPago.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
