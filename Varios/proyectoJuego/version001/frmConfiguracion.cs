using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using version001.Properties;

namespace version001
{
    public partial class frmConfiguracion : Form
    {
        Form1 f;
        public frmConfiguracion(Form1 frm)
        {
            InitializeComponent();
            f = frm;
        }

        private void btnCPU_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int indice = btn.ImageIndex;
            if (indice == 3)
            {
                btn.ImageIndex = 0;
            }
            else
            {
                
                btn.ImageIndex = ++indice ;
            }
        }

        private void rbFacil_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radio = (RadioButton)sender;

            lblDificultad.Text = radio.Tag.ToString();
            
            
        }

        private void frmConfiguracion_Load(object sender, EventArgs e)
        {
            this.Icon = Resources.dragonBallRadar;
            if (f.CantBloques == 19)
            {
                rbFacil.Checked = true;
            }
            else if (f.CantBloques == 28)
            {
                rbMedio.Checked = true;
            }
            else
            {
                rbDificil.Checked = true;
            }

            btnCPU.ImageIndex = Convert.ToInt32(f.FichaCPU);
            btnJugador.ImageIndex = Convert.ToInt32(f.FichaHumano);

            marcarDificultad(f.Dificultad);
            

            chTiempo.Segundos = f.Cseg;
            chTiempo.Hora = f.Chora;
            chTiempo.Minuntos = f.Cmin;

            switch (f.Empieza)
            {
                case Empieza.jugador1:
                    cbEmpieza.SelectedIndex = 1;
                    break;
                case Empieza.jugador2:
                    cbEmpieza.SelectedIndex = 0;
                    break;
                case Empieza.aleatorio:
                    cbEmpieza.SelectedIndex = 2;
                    break;
                default:
                    break;
            }
        }

        private void frmConfiguracion_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (btnCPU.ImageIndex == btnJugador.ImageIndex)
            {
                MessageBox.Show("No se puede elegir el mismo color de ficha", "Dragon ball Z", MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Cancel = true;
            }
            //color ficha cpu
            switch (btnCPU.ImageIndex)
            {
                case 0:
                    f.FichaCPU = TipoFicha.rojo;
                    break;
                case 1:
                    f.FichaCPU = TipoFicha.verde;
                    break;
                case 2:
                    f.FichaCPU = TipoFicha.amarillo;
                    break;
                case 3:
                    f.FichaCPU = TipoFicha.azul;
                    break;
                default:
                    break;
            }
            //color ficha humano
            switch (btnJugador.ImageIndex)
            {
                case 0:
                    f.FichaHumano = TipoFicha.rojo;
                    break;
                case 1:
                    f.FichaHumano = TipoFicha.verde;
                    break;
                case 2:
                    f.FichaHumano = TipoFicha.amarillo;
                    break;
                case 3:
                    f.FichaHumano = TipoFicha.azul;
                    break;
                default:
                    break;
            }
            //tiempo de juego
            f.Cseg = Convert.ToInt32(chTiempo.Segundos);
            f.Chora = Convert.ToInt32(chTiempo.Hora);
            f.Cmin = Convert.ToInt32(chTiempo.Minuntos);

            //quien empieza
            switch (cbEmpieza.SelectedIndex)
            {
                case 0:
                    f.Empieza = Empieza.jugador2;
                    break;
                case 1:
                    f.Empieza = Empieza.jugador1;
                    break;
                case 2:
                    f.Empieza = Empieza.aleatorio;
                    break;
                default:
                    break;
            }

            //cargar arreglo de colores
            f.Arr1 = crearArregloFichas(f.FichaHumano);
            f.Arr2 = crearArregloFichas(f.FichaCPU);

            f.CantBloques = Convert.ToInt32(lblDificultad.Text);
            if (rbFacil.Checked)
            {
                f.Dificultad = Dificultad.facil;
            }
            else if (rbMedio.Checked)
            {
                f.Dificultad = Dificultad.medio;
            }
            else if (rbDificil.Checked)
            {
                f.Dificultad = Dificultad.dificil;
            }


        }

        private Bitmap[] crearArregloFichas(TipoFicha num)
        {
            Bitmap[] arr = new Bitmap[3];

            switch (num)
            {
                case TipoFicha.rojo:
                    arr[0] = Resources.fichaRoja3;
                    arr[1] = Resources.fichaRoja2;
                    arr[2] = Resources.fichaRoja1;
                    break;
                case TipoFicha.verde:
                    arr[0] = Resources.fichaVerde3;
                    arr[1] = Resources.fichaVerde2;
                    arr[2] = Resources.fichaVerde1;
                    break;
                case TipoFicha.amarillo:
                    arr[0] = Resources.fichaAmarillo3;
                    arr[1] = Resources.fichaAmarillo2;
                    arr[2] = Resources.fichaAmarillo1;
                    break;
                case TipoFicha.azul:
                    arr[0] = Resources.fichaAzul3;
                    arr[1] = Resources.fichaAzul2;
                    arr[2] = Resources.fichaAzul1;
                    break;
                default:
                    break;
            }

            return arr;
        }

        private void frmConfiguracion_Shown(object sender, EventArgs e)
        {
            Bitmap fondo = new Bitmap(Resources.fondo_nameku, this.Size);
            this.BackgroundImage = fondo;
        }

        private void marcarDificultad(Dificultad op)
        {
            switch (op)
            {
                case Dificultad.facil:
                    rbFacil.Checked = true;
                    lblDificultad.Text = rbFacil.Tag.ToString();
                    break;
                case Dificultad.medio:
                    rbMedio.Checked = true;
                    lblDificultad.Text = rbMedio.Tag.ToString();
                    break;
                case Dificultad.dificil:
                    rbDificil.Checked = true;
                    lblDificultad.Text = rbDificil.Tag.ToString();
                    break;
                default:
                    break;
            }

            
        }

        private void frmConfiguracion_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S)
            {
                this.Close();
            }
        }
    }
}
