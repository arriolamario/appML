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
    public partial class frmSeleccionCuotas : Formulario
    {
        RestServices srv;
        DatosPago datosPago;
        private LogicaForm logicaForm;
        public frmSeleccionCuotas(DatosPago datosPago, LogicaForm logicaForm)
        {
            InitializeComponent();
            this.datosPago = datosPago;
            this.srv = new RestServices();
            datosPago.Cuotas = srv.GetCantidadCuotas(datosPago.Banco.id, datosPago.Monto, datosPago.Tarjeta.id);
            
            if (datosPago.Cuotas.Count > 0)
            {
                cbCuotas.DataSource = datosPago.Cuotas[0].payer_costs;
                cbCuotas.DisplayMember = "installments";
            }
            this.logicaForm = logicaForm;
            SetDescripcion(datosPago);
        }

        private void frmSeleccionCuotas_Load(object sender, EventArgs e)
        {
            logicaForm.seleccionBanco.Visible = false;
        }

        private void cbCuotas_SelectedIndexChanged(object sender, EventArgs e)
        {
            Payer_Costs descripcion = (Payer_Costs)cbCuotas.SelectedValue;
            datosPago.CuotaSeleccionada = descripcion.installments;
            SetDescripcion(datosPago);
        }

        private void btnTerminar_Click(object sender, EventArgs e)
        {
            logicaForm.cerrarPrincipal = false;
            logicaForm.seleccionBanco.Close();
            logicaForm.seleccionPago.Close();
            logicaForm.principal.Activar();
            this.Close();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            logicaForm.seleccionBanco.Activar();
            logicaForm.seleccionCuotas.Close();
        }

        private void frmSeleccionCuotas_FormClosed(object sender, FormClosedEventArgs e)
        {
            logicaForm.seleccionBanco.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
