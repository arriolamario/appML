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
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.Timers;

namespace version001
{
    enum TipoFicha { rojo,verde,amarillo,azul}
    enum Direccion { derecha, izquierda, arriba, abajo, arrDer, arrIzq, abaDer, abaIzq }
    enum Empieza { jugador1, jugador2, aleatorio, ninguno }
    enum Opcion { ficha, atacar, mover, maquina}
    enum Dificultad { facil, medio, dificil }
    enum Fin { tiempo, jugador1, jugador2 }
    
    public partial class Form1 : Form
    {
        //codigo tipo de letra
        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [In] ref uint pcFonts);
        FontFamily ff;
        Font font;

        private void CargoPrivateFontCollection()
        {
            // CREO EL BYTE[] Y TOMO SU LONGITUD
            byte[] fontArray = Resources.alarm_clock;
            int dataLength = Resources.alarm_clock.Length;


            // ASIGNO MEMORIA Y COPIO BYTE[] EN LA DIRECCION
            IntPtr ptrData = Marshal.AllocCoTaskMem(dataLength);
            Marshal.Copy(fontArray, 0, ptrData, dataLength);


            uint cFonts = 0;
            AddFontMemResourceEx(ptrData, (uint)fontArray.Length, IntPtr.Zero, ref cFonts);

            PrivateFontCollection pfc = new PrivateFontCollection();
            //PASO LA FUENTE A LA PRIVATEFONTCOLLECTION
            pfc.AddMemoryFont(ptrData, dataLength);

            //LIBERO LA MEMORIA "UNSAFE"
            Marshal.FreeCoTaskMem(ptrData);

            ff = pfc.Families[0];
            font = new Font(ff, 15f, FontStyle.Bold);
        }
       
        //Constantes
        const int nada = 0;
        const int objeto = 99;
        const int jug1Ficha3 = 20;
        const int jug1Ficha2 = 21;
        const int jug1Ficha1 = 22;
        const int jug2Ficha3 = 10;
        const int jug2Ficha2 = 11;
        const int jug2Ficha1 = 12;
        const int obRoto = 98;
        const int posible = 90;
        const int sleep = 2;
        const int sleep2 = 2000;
        Pen pen;
        Graphics tablero;
        Graphics bufferTablero;
        int[,] matriz;
        Image imgTablero;

        //Cronometro
        Stopwatch cronometro;

        //coordenadas seleccionadas
        Point viejas, nuevas;
                        
        //Bandera para saber si selecciono la primer coordenada
        bool selec = false;
        
        //turno del jugador
        Empieza turno;

        //cronometro
        int min = 5, seg = 0, hora = 0;

        //Configuracion
        TipoFicha fichaCPU = TipoFicha.rojo;
        TipoFicha fichaHumano = TipoFicha.azul;
        DateTime tiempo;
        int cantBloques = 19;
        int cmin, cseg, chora;
        Empieza empieza = Empieza.aleatorio;
        Dificultad dificultad = Dificultad.medio;
        Bitmap[] arr1;
        Bitmap[] arr2;

        //listas;
        List<Point> listaAtaques = new List<Point>();
        List<Point> listaPsobles = new List<Point>();
        List<Point> jugadorUno = new List<Point>();
        List<Point> jugadorDos = new List<Point>();
        List<Point> objetos = new List<Point>();
        //Puntuacion
        int puntaje1 = 0;
        int puntaje2 = 0;

        

        public Bitmap[] Arr1
        {
            get { return arr1; }
            set { arr1 = value; }
        }

        public Bitmap[] Arr2
        {
            get { return arr2; }
            set { arr2 = value; }
        }

        internal Empieza Empieza
        {
            get { return empieza; }
            set { empieza = value; }
        }

        public int Chora
        {
            get { return chora; }
            set
            {
                chora = value;
                hora = value;
            }
        }

        public int Cseg
        {
            get { return cseg; }
            set
            {
                cseg = value;
                seg = value;
            }
        }

        public int Cmin
        {
            get { return cmin; }
            set
            {
                cmin = value;
                min = value;
            }
        }
        
        internal TipoFicha FichaCPU
        {
            get { return fichaCPU; }
            set { fichaCPU = value; }
        }
        
        internal TipoFicha FichaHumano
        {
            get { return fichaHumano; }
            set { fichaHumano = value; }
        }

        public DateTime Tiempo
        {
            get { return tiempo; }
            set { tiempo = value; }
        }

        public int CantBloques
        {
            get { return cantBloques; }
            set { cantBloques = value; }
        }

        internal Dificultad Dificultad
        {
            get { return dificultad; }
            set { dificultad = value; }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Icon = Resources.dragonBallRadar;
            tablero = pnlTablero.CreateGraphics();
            tiempo = new DateTime(2000, 12, 12, 0, 1, 0);
            cmin = min;
            cseg = seg;
            chora = hora;
            arr1 = crearArregloFichas(fichaHumano);
            arr2 = crearArregloFichas(fichaCPU);
            CargoPrivateFontCollection();
            float size = 30f;
            FontStyle fontStyle = FontStyle.Regular;

            lblTemp.Font = new Font(ff, size, fontStyle);
            lblPuntaje1.Font = new Font(ff, 50, fontStyle);
            lblPuntaje2.Font = new Font(ff, 50, fontStyle);
            lblPuntaje1.Text = "0";
            lblPuntaje2.Text = "0";
        }

        private void crearTablero(Graphics g)
        {
            pen = new Pen(Color.Black,2);
            
            for (int x = 0; x <= 600; x = x+50)
            {
                g.DrawLine(pen, x, 0, x, 600);
                g.DrawLine(pen, 0, x, 600, x);
                bufferTablero.DrawLine(pen, x, 0, x, 600);
                bufferTablero.DrawLine(pen, 0, x, 600, x);
            }
        }

        private void crearMatriz()
        {
            matriz = new int[12, 12];
        }

        private void posicionAleatoriaObjetos(int cant)
        {
            Random r = new Random();
            Size tam = new Size(50, 50);
            Bitmap img = new Bitmap(Resources.objetoZ,tam);
            
            int x,y,i;
            i = 0;
            while (i < cant)
            {
                x = r.Next(12);
                y = r.Next(2, 10);
                if (matriz[x, y] == nada)
                {
                    matriz[x, y] = objeto;
                    objetos.Add(new Point(x, y));
                    tablero.DrawImage(img,x*50,y*50);
                    bufferTablero.DrawImage(img, x * 50, y * 50);
                    i++;
                }
                
            }
            
        }

        private void posicionAleatoriaFichas(Bitmap[] ficha,bool cpu)
        {
            Random r = new Random();
            Size tam = new Size(50, 50);
            int x, y;

            if (cpu)
            {
                for (int i = 0; i < 3; i++)
                {
                    Bitmap img = new Bitmap(ficha[i], tam);
                    int j = 0;
                    while (j < i + 1)
                    {
                        x = r.Next(12);
                        y = r.Next(2);
                        if (matriz[x, y] == nada)
                        {
                            matriz[x, y] = i + 10;
                            jugadorDos.Add(new Point(x, y));
                            tablero.DrawImage(img, x * 50, y * 50);
                            bufferTablero.DrawImage(img, x * 50, y * 50);
                            j++;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < 3; i++)
                {
                    Bitmap img = new Bitmap(ficha[i], tam);
                    int j = 0;
                    while (j < i + 1)
                    {
                        x = r.Next(12);
                        y = r.Next(10,12);
                        if (matriz[x, y] == nada)
                        {
                            matriz[x, y] = i + 20;
                            jugadorUno.Add(new Point(x, y));
                            tablero.DrawImage(img, x * 50, y * 50);
                            bufferTablero.DrawImage(img, x * 50, y * 50);
                            j++;
                        }
                    }
                }
 
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            tablero.DrawImage(Resources.terminado, 0, 0, 600, 600);
            Bitmap fondo = new Bitmap(Resources.fondo_nameku, this.Size);
            this.BackgroundImage = fondo;
            pbTurno.Image = new Bitmap(Resources.ataque, pbTurno.Size);
        }

        private void limpiarMatriz()
        {
            crearMatriz();
            Size tam = new Size(50,50); 
            Bitmap img = new Bitmap(Resources.borrar,tam);

            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    tablero.DrawImage(img, i * 50, j * 50);
                    //bufferTablero.DrawImage(img, i * 50, j * 50);
                }
            }
        }

        private void pnlTablero_MouseClick(object sender, MouseEventArgs e)
        {
            if (Empieza.ninguno != turno)
            {


                int nroFicha = 0;
                if (!selec)
                {

                    viejas.X = e.X / 50;
                    viejas.Y = e.Y / 50;
                    try
                    {
                        nroFicha = matriz[viejas.X, viejas.Y];
                    }
                    catch (NullReferenceException)
                    {
                        return;
                    }
                    if (turno == Empieza.jugador1)
                    {
                        if (nroFicha >= 20 && nroFicha < 25)
                        {
                            nroFicha = nroFicha - 20;
                            marcarPosiblesMovimientos(nroFicha);
                            posiblesAtaques(nroFicha);
                            marcarPosiblesAtaques();
                            selec = true;


                        }
                    }
                    /*else if (turno == Empieza.jugador2)
                    {
                        if (nroFicha >= 10 && nroFicha < 15)
                        {
                            nroFicha = nroFicha - 10;
                            marcarPosiblesMovimientos(nroFicha);
                            selec = true;
                            posiblesAtaques(nroFicha);
                        }
                    }*/

                }
                else
                {
                    nuevas.X = e.X / 50;
                    nuevas.Y = e.Y / 50;
                    if (viejas == nuevas)
                    {
                        tablero.DrawImage(imgTablero, new Point(0, 0));
                        desmarcarPosiblesMovimientos();
                        tablero.DrawImage(imgTablero, new Point(0, 0));
                    }
                    else if (listaPsobles.Contains(nuevas))
                    {


                        if (turno == Empieza.jugador1)
                        {
                            moverFicha(viejas, nuevas, fichaHumano);
                            turno = Empieza.jugador2;
                            setTurno();
                            turnoMaquina();

                        }
                        /*else
                        {
                            moverFicha(viejas,nuevas, fichaCPU);
                            turno = Empieza.jugador1;
                            setTurno();
                        }*/





                    }
                    else if (romper())
                    {
                        tablero.DrawImage(imgTablero, new Point(0, 0));

                        if (turno == Empieza.jugador1)
                        {
                            if (jugadorDos.Count == 0)
                            {
                                finDelJuego(Fin.jugador1);
                            }
                            else
                            {
                                turno = Empieza.jugador2;
                                setTurno();
                                turnoMaquina();
                            }
                        }
                        /*else
                        {
                            turno = Empieza.jugador1;
                            setTurno();
                        }*/



                    }
                    else
                    {
                        MessageBox.Show("Movimiento Incorrecto", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        tablero.DrawImage(imgTablero, new Point(0, 0));
                    }

                    selec = false;




                }
            }

            

        }

        private void setPuntaje()
        {
            lblPuntaje1.Text = puntaje1.ToString();
            lblPuntaje2.Text = puntaje2.ToString();

        }

        private void configuracionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConfiguracion f = new frmConfiguracion(this);
            f.ShowDialog();
        }

        //andaaa
        private void moverFicha1(Point viejo, Point nuevo, TipoFicha tipo)
        {
            puntaje();
            Size tam = new Size(50, 50);
            Bitmap borrar = new Bitmap(Resources.borrar, tam);
            int valor = matriz[viejo.X, viejo.Y]; 
            int indice;
            Bitmap[] arr;
            if (turno == Empieza.jugador1)
            {
                arr = arr1;
                indice = valor - 20;
                jugadorUno.Remove(viejas);
                jugadorUno.Add(nuevas);

            }
            else
            {
                arr = arr2; 
                indice = valor - 10;
                jugadorDos.Remove(viejas);
                jugadorDos.Add(nuevas);
            }

            Bitmap ficha = new Bitmap(arr[indice], tam);
            
            //modificamos la matriz
            matriz[viejo.X, viejo.Y] = nada;
            matriz[nuevo.X, nuevo.Y] = valor;

            nuevo.X *= 50;
            nuevo.Y *= 50;
            viejo.X *= 50;
            viejo.Y *= 50;

            //hacemos el movimiento
            Direccion  dir = direccionMovimiento(viejo, nuevo);


            bufferTablero.DrawImage(Resources.borrar, viejo.X, viejo.Y, 50, 50);
            if (dir == Direccion.izquierda)
            {
                for (int x = viejo.X; x > nuevo.X; x= x-sleep)
                {
                    tablero.DrawImage(imgTablero, 0, 0);
                    tablero.DrawImage(ficha, x, viejo.Y);
                }
            }
            else if (dir == Direccion.derecha)
            {
                for (int x = viejo.X; x < nuevo.X; x=x+sleep)
                {
                    tablero.DrawImage(imgTablero, 0, 0);
                    tablero.DrawImage(ficha, x, viejo.Y);
                }
            }
            else if (dir == Direccion.abajo)
            {
                for (int y = viejo.Y; y < nuevo.Y; y= y+sleep)
                {
                    tablero.DrawImage(imgTablero, 0, 0);
                    tablero.DrawImage(ficha, viejo.X, y);
                }
            }
            else if (dir == Direccion.arriba)
            {
                for (int y = viejo.Y; y > nuevo.Y; y=y-sleep)
                {
                    tablero.DrawImage(imgTablero, 0, 0);
                    tablero.DrawImage(ficha, viejo.X, y);
                }
            }
            else if (dir == Direccion.abaDer)
            {
                int y = viejo.Y;
                for (int x = viejo.X; x < nuevo.X; x=x+sleep)
                {
                    tablero.DrawImage(imgTablero, 0, 0);
                    tablero.DrawImage(ficha, x, y);
                    y=y+sleep;
                }
            }
            else if (dir == Direccion.abaIzq)
            {
                int y = viejo.Y;
                for (int x = viejo.X; x >= nuevo.X; x = x - sleep)
                {
                    tablero.DrawImage(imgTablero, 0, 0);
                    tablero.DrawImage(ficha, x, y);
                    y = y + sleep;
                }
                                              
            }
            else if (dir == Direccion.arrDer)
            {
                int y = viejo.Y;
                for (int x = viejo.X; x < nuevo.X; x=x+sleep)
                {
                    tablero.DrawImage(imgTablero, 0, 0);
                    tablero.DrawImage(ficha, x, y);
                    y=y-sleep;
                }
            }
            else if (dir == Direccion.arrIzq)
            {
                int y = viejo.Y;
                for (int x = viejo.X; x > nuevo.X; x=x-sleep)
                {
                    tablero.DrawImage(imgTablero, 0, 0);
                    tablero.DrawImage(ficha, x, y);
                    y=y-sleep;
                }
            }
            tablero.DrawImage(imgTablero, 0, 0);
            tablero.DrawImage(ficha, nuevo.X, nuevo.Y);
            if (turno == Empieza.jugador1)
            {
                bufferTablero.DrawImage(arr1[indice], nuevo.X, nuevo.Y, 50, 50);
            }
            else
            {
                bufferTablero.DrawImage(arr2[indice], nuevo.X, nuevo.Y, 50, 50);
            }
            
           

        }

        private void moverFicha(Point viejo, Point nuevo, TipoFicha tipo)
        {
            puntaje();
            Size tam = new Size(50, 50);
            Bitmap borrar = new Bitmap(Resources.borrar, tam);
            int valor = matriz[viejo.X, viejo.Y];
            int indice;
            Bitmap[] arr;
            if (turno == Empieza.jugador1)
            {
                arr = arr1;
                indice = valor - 20;
                jugadorUno.Remove(viejas);
                jugadorUno.Add(nuevas);

            }
            else
            {
                arr = arr2;
                indice = valor - 10;
                jugadorDos.Remove(viejas);
                jugadorDos.Add(nuevas);
            }

            Bitmap ficha = new Bitmap(arr[indice], tam);

            //modificamos la matriz
            matriz[viejo.X, viejo.Y] = nada;
            matriz[nuevo.X, nuevo.Y] = valor;

            nuevo.X *= 50;
            nuevo.Y *= 50;
            viejo.X *= 50;
            viejo.Y *= 50;

            //hacemos el movimiento
            Direccion dir = direccionMovimiento(viejo, nuevo);

            bufferTablero.DrawImage(Resources.borrar, viejo.X, viejo.Y, 50, 50);
            Image aux = new Bitmap(imgTablero);
            if (dir == Direccion.izquierda)
            {
                for (int x = viejo.X; x > nuevo.X; x = x - sleep)
                {
                    bufferTablero.DrawImage(aux, new Point(0, 0));
                    bufferTablero.DrawImage(ficha, x, viejo.Y);
                    tablero.DrawImage(imgTablero, new Point(0, 0));
                }
            }
            else if (dir == Direccion.derecha)
            {
                for (int x = viejo.X; x < nuevo.X; x = x + sleep)
                {
                    bufferTablero.DrawImage(aux, new Point(0, 0));
                    bufferTablero.DrawImage(ficha, x, viejo.Y);
                    tablero.DrawImage(imgTablero, new Point(0, 0));
                }
            }
            else if (dir == Direccion.abajo)
            {
                for (int y = viejo.Y; y < nuevo.Y; y = y + sleep)
                {
                    bufferTablero.DrawImage(aux, new Point(0, 0));
                    bufferTablero.DrawImage(ficha, viejo.X, y);
                    tablero.DrawImage(imgTablero, new Point(0, 0));
                }
            }
            else if (dir == Direccion.arriba)
            {
                for (int y = viejo.Y; y > nuevo.Y; y = y - sleep)
                {
                    bufferTablero.DrawImage(aux, new Point(0, 0));
                    bufferTablero.DrawImage(ficha, viejo.X, y);
                    tablero.DrawImage(imgTablero, new Point(0, 0));
                }
            }
            else if (dir == Direccion.abaDer)
            {
                int y = viejo.Y;
                for (int x = viejo.X; x < nuevo.X; x = x + sleep)
                {
                    bufferTablero.DrawImage(aux, new Point(0, 0));
                    bufferTablero.DrawImage(ficha, x, y);
                    tablero.DrawImage(imgTablero, new Point(0, 0));
                    y = y + sleep;
                }
            }
            else if (dir == Direccion.abaIzq)
            {
                int y = viejo.Y;
                for (int x = viejo.X; x >= nuevo.X; x = x - sleep)
                {
                    bufferTablero.DrawImage(aux, new Point(0, 0));
                    bufferTablero.DrawImage(ficha, x, y);
                    tablero.DrawImage(imgTablero, new Point(0, 0));
                    y = y + sleep;
                }

            }
            else if (dir == Direccion.arrDer)
            {
                int y = viejo.Y;
                for (int x = viejo.X; x < nuevo.X; x = x + sleep)
                {
                    bufferTablero.DrawImage(aux, new Point(0, 0));
                    bufferTablero.DrawImage(ficha, x, y);
                    tablero.DrawImage(imgTablero, new Point(0, 0));
                    y = y - sleep;
                }
            }
            else if (dir == Direccion.arrIzq)
            {
                int y = viejo.Y;
                for (int x = viejo.X; x > nuevo.X; x = x - sleep)
                {
                    bufferTablero.DrawImage(aux, new Point(0, 0));
                    bufferTablero.DrawImage(ficha, x, y);
                    tablero.DrawImage(imgTablero, new Point(0, 0));
                    y = y - sleep;
                }
            }
            tablero.DrawImage(imgTablero, 0, 0);
            tablero.DrawImage(ficha, nuevo.X, nuevo.Y);
            if (turno == Empieza.jugador1)
            {
                bufferTablero.DrawImage(arr1[indice], nuevo.X, nuevo.Y, 50, 50);
            }
            else
            {
                bufferTablero.DrawImage(arr2[indice], nuevo.X, nuevo.Y, 50, 50);
            }



        }

        private Direccion direccionMovimiento(Point viejo, Point nuevo)
        {
            Direccion dir = Direccion.abaIzq;

            if (viejo.X < nuevo.X && viejo.Y == nuevo.Y)
            {
                dir = Direccion.derecha;
            }
            else if (viejo.X > nuevo.X && viejo.Y == nuevo.Y)
            {
                dir = Direccion.izquierda;
            }
            else if (viejo.X == nuevo.X && viejo.Y > nuevo.Y)
            {
                dir = Direccion.arriba;
            }
            else if (viejo.X == nuevo.X && viejo.Y < nuevo.Y)
            {
                dir = Direccion.abajo;
            }
            else if (viejo.X < nuevo.X && viejo.Y > nuevo.Y)
            {
                dir = Direccion.arrDer;
            }
            else if (viejo.X > nuevo.X && viejo.Y > nuevo.Y)
            {
                dir = Direccion.arrIzq;
            }
            else if (viejo.X < nuevo.X && viejo.Y < nuevo.Y)
            {
                dir = Direccion.abaDer;
            }
            else if (viejo.X > nuevo.X && viejo.Y < nuevo.Y)
            {
                dir = Direccion.abaIzq;
            }

            return dir;
        }

        private void nuevoJuegoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activarDesactivar(sender);
            pbTurno.Visible = true;
            listaPsobles = new List<Point>();
            listaAtaques = new List<Point>();
            jugadorUno = new List<Point>();
            jugadorDos = new List<Point>();
            objetos = new List<Point>();
            selec = false;
            imgTablero = new Bitmap(Resources.fondo, new Size(600, 600));
            bufferTablero = Graphics.FromImage(imgTablero);
            tablero.DrawImage(Resources.fondo, 0, 0, 600, 600);
            crearTablero(tablero);
            crearTablero(bufferTablero);
            limpiarMatriz();
            
            
            posicionAleatoriaObjetos(cantBloques);
            posicionAleatoriaFichas(arr1, false);
            posicionAleatoriaFichas(arr2, true);
            if (empieza == Empieza.aleatorio)
                randomTurno();
            else
                turno = empieza;

            if (temp1.Enabled)
            {
                temp1.Stop();
            }
            temp1.Start();

            setTextoTiempo();
            seg = cseg;
            min = cmin;
            hora = chora;
            setTextoTiempo();

            lblPuntaje1.Visible = true;
            lblPuntaje2.Visible = true;

            setTurno();

            lblPuntaje1.Text = "0";
            lblPuntaje2.Text = "0";

            cronometro = new Stopwatch();
            cronometro.Start();

            if (turno == Empieza.jugador2)
            {
                turnoMaquina();
            }
        }

        private void setTurno()
        {
            if (turno == Empieza.jugador1)
            {
                pbTurno.Image = new Bitmap(Resources.uno, pbTurno.Size);
            }
            else
            {
                pbTurno.Image = new Bitmap(Resources.dos, pbTurno.Size);
            }
        }

        private void randomTurno()
        {
            Random r = new Random();
            if (r.Next(1, 3) == 1)
            {
                turno = Empieza.jugador1;
            }
            else
            {
                turno = Empieza.jugador2;
            }
        }

        private void temp_Tick(object sender, EventArgs e)
        {
            if (seg == 0)
            {
                
                if (min == 0)
                {
                    
                    if (hora > 0)
                    {
                        hora--;
                    }
                    else if (hora == 0 && min == 0 && seg == 0)
                    {
                        temp1.Stop();
                        return;
                    }
                    
                    min = 59;
                }
                else
                {
                    min--;
                }

                seg = 60;

            }
            seg--;

            setTextoTiempo();

            if (hora == 0 & min == 0 & seg == 0)
            {
                finDelJuego(Fin.tiempo);

            }
        }

        private void finDelJuego(Fin op)
        {
            temp1.Stop();
            switch (op)
            {
                case Fin.tiempo:
                    if (puntaje1 > puntaje2)
                    {
                        tablero.DrawImage(Resources.ganadorUno, 0, 0, 600, 600);
                    }
                    else if(puntaje1 < puntaje2)
                    {
                        tablero.DrawImage(Resources.ganadorDos, 0, 0, 600, 600);
                    }
                    else
                    {
                        tablero.DrawImage(Resources.empate, 0, 0, 600, 600);
                    }

            
                    break;
                case Fin.jugador1:
                    tablero.DrawImage(Resources.ganadorUno, 0, 0, 600, 600);
                    break;
                case Fin.jugador2:
                    tablero.DrawImage(Resources.ganadorDos, 0, 0, 600, 600);
                    break;
                default:
                    break;
            }

            turno = Empieza.ninguno;
            crearMatriz();
            imgTablero = null;
            pbTurno.Image = new Bitmap(Resources.ataque, pbTurno.Size);
            

        }

        private void setTextoTiempo()
        {
            string s, m, h;
            if (seg < 10)
            {
                s = "0" + seg;
            }
            else
            {
                s = seg.ToString();
            }
            if (min < 10)
            {
                m = "0" + min;
            }
            else
            {
                m = min.ToString();
            }
            if (hora < 10)
            {
                h = "0" + hora;
            }
            else
            {
                h = hora.ToString();
            }

            this.lblTemp.Text = h + ":" + m + ":" + s;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Esta seguro que decea cerrar el juego?", "Dragon Ball Z", MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation) == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void marcarPosiblesMovimientos(int nroFicha)
        {
            tablero.DrawImage(Resources.seleccion, viejas.X * 50, viejas.Y * 50, 50, 50);
            listaPsobles.Clear();
            int max;
            switch (nroFicha)
            {
                case 0:
                    max = 3;
                    break;
                case 1:
                    max = 2;
                    break;
                case 2:
                    max = 1;
                    break;
                default:
        
                    max = 0;
                    break;
            }
            //posibles a la derecha
            try
            {
                for (int x = viejas.X; x < viejas.X + max; x++)
                {
                    if (matriz[x + 1, viejas.Y] == nada)
                    {
                        tablero.DrawImage(Resources.posibleMovimiento, (x + 1) * 50, viejas.Y * 50, 50, 50);
                        listaPsobles.Add(new Point(x+1,viejas.Y));
                        
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (IndexOutOfRangeException) { }
            //posibles a la izquierda
            try
            {
                for (int x = viejas.X; x > viejas.X - max; x--)
                {
                    if (matriz[x - 1, viejas.Y] == nada)
                    {
                        tablero.DrawImage(Resources.posibleMovimiento, (x - 1) * 50, viejas.Y * 50, 50, 50);
                        listaPsobles.Add(new Point(x - 1, viejas.Y));
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (IndexOutOfRangeException) { }
            //posible hacia arriba
            try
            {
                for (int y = viejas.Y; y > viejas.Y - max; y--)
                {
                    if (matriz[viejas.X, y - 1] == nada)
                    {

                        tablero.DrawImage(Resources.posibleMovimiento, viejas.X * 50, (y - 1) * 50, 50, 50);
                        listaPsobles.Add(new Point(viejas.X, y-1));
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (IndexOutOfRangeException) { }
            //posible hacia abajo
            try
            {
                for (int y = viejas.Y; y < viejas.Y + max; y++)
                {
                    if (matriz[viejas.X, y + 1] == nada)
                    {

                        tablero.DrawImage(Resources.posibleMovimiento, viejas.X * 50, (y + 1) * 50, 50, 50);
                        listaPsobles.Add(new Point(viejas.X, y+1));
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (IndexOutOfRangeException) { }
            //posible Arriba Derecha
            try
            {
                int y = viejas.Y;
                for (int x = viejas.X; x < viejas.X + max; x++)
                {
                    if (matriz[x + 1, y - 1] == nada)
                    {
                        tablero.DrawImage(Resources.posibleMovimiento, (x+1) * 50, (y - 1) * 50, 50, 50);
                        listaPsobles.Add(new Point(x+1,y-1));
                    }
                    else
                    {
                        break;
                    }
                    y--;
                }
            }
            catch (IndexOutOfRangeException) { }
            //posible Arriba Izquierda
            try
            {
                int y = viejas.Y;
                for (int x = viejas.X; x > viejas.X - max; x--)
                {
                    if (matriz[x - 1, y - 1] == nada)
                    {
                        tablero.DrawImage(Resources.posibleMovimiento, (x - 1) * 50, (y - 1) * 50, 50, 50);
                        listaPsobles.Add(new Point(x-1,y-1));
                    }
                    else
                    {
                        break;
                    }
                    y--;
                }
            }
            catch (IndexOutOfRangeException) { }

            //posible abajo derecha
            try
            {
                int y = viejas.Y;
                for (int x = viejas.X; x < viejas.X + max; x++)
                {
                    if (matriz[x + 1, y + 1] == nada)
                    {
                        tablero.DrawImage(Resources.posibleMovimiento, (x + 1) * 50, (y + 1) * 50, 50, 50);
                        listaPsobles.Add(new Point(x+1,y+1));
                    }
                    else
                    {
                        break;
                    }
                    y++;
                }
            }
            catch (IndexOutOfRangeException) { }

            //posible abajo izquierda
            try
            {
                int y = viejas.Y;
                for (int x = viejas.X; x > viejas.X - max; x--)
                {
                    if (matriz[x - 1, y + 1] == nada)
                    {
                        tablero.DrawImage(Resources.posibleMovimiento, (x - 1) * 50, (y + 1) * 50, 50, 50);
                        listaPsobles.Add(new Point(x-1,y+1));
                    }
                    else
                    {
                        break;
                    }
                    y++;
                }
            }
            catch (IndexOutOfRangeException) { }
            
        }

        private void desmarcarPosiblesMovimientos()
        {
            listaPsobles.Clear();
        }

        private void posiblesAtaques(int nroFicha)
        {
            listaAtaques.Clear();
            int max;
            switch (nroFicha)
            {
                case 0:
                    max = 1;
                    break;
                case 1:
                    max = 2;
                    break;
                case 2:
                    max = 3;
                    break;
                default:

                    max = 0;
                    break;
            }
            //posibles ataques a la derecha
            try
            {
                for (int x = viejas.X; x < viejas.X + max; x++)
                {
                    int valor = matriz[x + 1, viejas.Y];
                    if (agregarListaAtaques(valor, x + 1, viejas.Y))
                    {
                        break;
                    }
                    
                }
            }
            catch (IndexOutOfRangeException) { }
           //posibles a la izquierda
            try
            {
                for (int x = viejas.X; x > viejas.X - max; x--)
                {
                    int valor = matriz[x - 1, viejas.Y];
                    if (agregarListaAtaques(valor, x - 1, viejas.Y))
                    {
                        break;
                    }
                }
            }
            catch (IndexOutOfRangeException) { }
            //posible hacia arriba
            try
            {
                for (int y = viejas.Y; y > viejas.Y - max; y--)
                {
                    int valor = matriz[viejas.X, y - 1];
                    if (agregarListaAtaques(valor, viejas.X, y - 1))
                    {
                        break;
                    }
                }
            }
            catch (IndexOutOfRangeException) { }
            //posible hacia abajo
            try
            {
                for (int y = viejas.Y; y < viejas.Y + max; y++)
                {
                    int valor = matriz[viejas.X, y + 1];
                    if (agregarListaAtaques(valor, viejas.X, y + 1))
                        break;
                }
            }
            catch (IndexOutOfRangeException) { }
            //posible Arriba Derecha
            try
            {
                int y = viejas.Y;
                for (int x = viejas.X; x < viejas.X + max; x++)
                {
                    int valor = matriz[x + 1, y - 1];
                    if (agregarListaAtaques(valor, x + 1, y - 1))
                        break;
                    y--;
                }
            }
            catch (IndexOutOfRangeException) { }
            //posible Arriba Izquierda
            try
            {
                int y = viejas.Y;
                for (int x = viejas.X; x > viejas.X - max; x--)
                {
                    int valor = matriz[x - 1, y - 1];
                    if (agregarListaAtaques(valor, x - 1, y - 1))
                        break;
                    y--;
                }
            }
            catch (IndexOutOfRangeException) { }

            //posible abajo derecha
            try
            {
                int y = viejas.Y;
                for (int x = viejas.X; x < viejas.X + max; x++)
                {
                    int valor = matriz[x + 1, y + 1];
                    if (agregarListaAtaques(valor, x + 1, y + 1))
                        break;
                    y++;
                }
            }
            catch (IndexOutOfRangeException) { }

            //posible abajo izquierda
            try
            {
                int y = viejas.Y;
                for (int x = viejas.X; x > viejas.X - max; x--)
                {
                    int valor = matriz[x - 1, y + 1];
                    if (agregarListaAtaques(valor, x - 1, y + 1))
                        break;
                    y++;
                }
            }
            catch (IndexOutOfRangeException) { }
            
        }

        private void desmarcarAtaques()
        {
            listaAtaques.Clear();
        }

        private bool agregarListaAtaques(int valor, int x, int y)
        {
            bool res = false;
            if (turno == Empieza.jugador1)
            {
                if (valor > 19 & 30 > valor)
                {
                    return true;
                }
                if ((valor < 100 & valor > 97) | (valor > 9 & valor < 13))
                {
                    listaAtaques.Add(new Point(x, y));
                    res = true;
                }

            }
            else if (turno == Empieza.jugador2)
            {
                if (valor > 9 & 19 > valor)
                {
                    return true;
                }
                if ((19 < valor & valor < 23) | (97 < valor & valor < 100))
                {
                    listaAtaques.Add(new Point(x,y));
                    res = true;
                }

            }

            return res;
        }

        private bool romper()
        {
            bool res = false;
            
            foreach (Point item in listaAtaques)
            {
                if (nuevas.X == item.X & nuevas.Y == item.Y)
                {
                    
                    puntaje();
                    int valor = matriz[item.X, item.Y];
                    if (valor == objeto)
                    {
                        matriz[item.X, item.Y] = obRoto;
                        bufferTablero.DrawImage(Resources.borrar, item.X * 50, item.Y * 50, 50, 50);
                        bufferTablero.DrawImage(Resources.objetoZroto, item.X * 50, item.Y * 50, 50, 50);
                        explosion(valor);
                        tablero.DrawImage(imgTablero, new Point(0, 0));
                        res = true;

                    }
                    else if (turno == Empieza.jugador1)
                    {
                        if (valor == obRoto | valor == jug2Ficha1 | valor == jug2Ficha2 | valor == jug2Ficha3)
                        {
                            matriz[item.X, item.Y] = nada;
                            bufferTablero.DrawImage(Resources.borrar, item.X * 50, item.Y * 50, 50, 50);
                            explosion(valor);
                            tablero.DrawImage(imgTablero, new Point(0, 0));
                            res = true;
                            jugadorDos.Remove(nuevas);
                            

                        }
                    }
                    else if (turno == Empieza.jugador2)
                    {
                        if (valor == obRoto | valor == jug1Ficha1 | valor == jug1Ficha2 | valor == jug1Ficha3)
                        {
                            matriz[item.X, item.Y] = nada;
                            bufferTablero.DrawImage(Resources.borrar, item.X * 50, item.Y * 50, 50, 50); 
                            explosion(valor);
                            tablero.DrawImage(imgTablero, new Point(0, 0));
                            res = true;
                            jugadorUno.Remove(nuevas);
                           
                        }
                    }
                    break;
                }
                
            }

            return res;
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

        private void pnlTablero_Paint(object sender, PaintEventArgs e)
        {
            if (imgTablero != null)
            {
                tablero.DrawImage(imgTablero, new Point(0, 0));
            }
            else
            {
                tablero.DrawImage(Resources.terminado, 0, 0, 600, 600);
            }
        }

        private void puntaje()
        {
            cronometro.Stop();
            int valor = matriz[nuevas.X,nuevas.Y];
            int puntaje = 0;
            if (valor == jug1Ficha1 | valor == jug2Ficha1)
            {
                puntaje = 500;
            }
            else if (valor == jug1Ficha2 | valor == jug2Ficha2)
            {
                puntaje = 250;
            }
            else if (valor == jug1Ficha3 | valor == jug2Ficha3)
            {
                puntaje = 100 ;
            }
            else if (valor == objeto | valor == obRoto)
            {
                puntaje = 25;
            }

            if (turno == Empieza.jugador1)
            {
                puntaje1 += puntaje; 
            }
            else
            {
                puntaje2 += puntaje;
            }

            setPuntaje();
            cronometro.Reset();
            cronometro.Start();
        }

        private Point elegirRandom(Opcion op)
        {
            Random r = new Random();
            Point res;
            switch (op)
            {
                case Opcion.ficha:
                    res = jugadorDos.ElementAt(r.Next(jugadorDos.Count));
                    break;
                case Opcion.atacar:
                    res = listaAtaques.ElementAt(r.Next(listaAtaques.Count));
                    break;
                case Opcion.mover:
                    res = listaPsobles.ElementAt(r.Next(listaPsobles.Count));
                    break;
                default:
                    res = new Point();
                    break;
            }

            return res;
        }

        private void turnoMaquina()
        {
            
            if (Dificultad.facil == dificultad)
            {
                
                viejas = elegirRandom(Opcion.ficha);
                int ficha = matriz[viejas.X, viejas.Y];
                ficha -= 10;
                posiblesAtaques(ficha);
                marcarPosiblesAtaques();
                marcarPosiblesMovimientos(ficha);
                Application.DoEvents();
                System.Threading.Thread.Sleep(sleep2);
                if (listaAtaques.Count > 0)
                {
                    nuevas = elegirRandom(Opcion.atacar);
                    romper();
                    if (jugadorUno.Count == 0)
                    {
                        finDelJuego(Fin.jugador2);
                        return;
                    }
                    
                }
                else
                {
                    nuevas = elegirRandom(Opcion.mover);
                    moverFicha(viejas, nuevas, fichaCPU);
                }

                
            }
            else if (Dificultad.medio == dificultad)
            {
               
                
               
                if (buscarFichaEnAtaque())
                {
                    marcarPosiblesAtaques();
                    marcarPosiblesMovimientos(matriz[viejas.X, viejas.Y] - 10);
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(sleep2);
                    romper();
                    if (jugadorUno.Count == 0)
                    {
                        finDelJuego(Fin.jugador2);
                        return;
                    }
                }
                else
                {
                    viejas = elegirRandom(Opcion.ficha);
                    posiblesAtaques(matriz[viejas.X, viejas.Y]-10);
                    marcarPosiblesAtaques();
                    marcarPosiblesMovimientos(matriz[viejas.X, viejas.Y] - 10);
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(sleep2);
                    nuevas = elegirRandom(Opcion.mover);
                    moverFicha(viejas, nuevas, FichaCPU);
                }
            }
            else if (Dificultad.dificil == dificultad)
            {
                if (buscarFichaEnAtaque())
                {
                    marcarPosiblesAtaques();
                    marcarPosiblesMovimientos(matriz[viejas.X, viejas.Y] - 10);
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(sleep2);
                    romper();
                    if (jugadorUno.Count == 0)
                    {
                        finDelJuego(Fin.jugador2);
                        return;
                    }
                }
                else
                {
                    selectFichaCercana();
                    posiblesAtaques(matriz[viejas.X, viejas.Y] - 10);
                    marcarPosiblesAtaques();
                    marcarPosiblesMovimientos(matriz[viejas.X, viejas.Y] - 10);
                    Application.DoEvents();
                    System.Threading.Thread.Sleep(sleep2);
                    
                    seguir();
                    
                }
            }
            
            desmarcarPosiblesMovimientos();
            tablero.DrawImage(imgTablero, new Point(0, 0));
            turno = Empieza.jugador1;
            setTurno();
        }

        private bool buscarFichaEnAtaque()
        {
            
            foreach (Point ficha in jugadorDos)
            {
                viejas = ficha;
                posiblesAtaques(matriz[ficha.X, ficha.Y] - 10);
                foreach (Point ataque in listaAtaques)
                {
                    int valor = matriz[ataque.X,ataque.Y];
                    if (valor == jug1Ficha1 || valor == jug1Ficha2 || valor == jug1Ficha3)
                    {
                        nuevas = ataque;
                        return true;
                    }
                }
                
            }

            return false;
        }

        private void marcarPosiblesAtaques()
        {
            foreach (Point ataque in listaAtaques)
            {
                tablero.DrawImage(Resources.ataque, ataque.X * 50, ataque.Y * 50, 50, 50);
            }
        }

        private void terminarJuegoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            activarDesactivar(sender);
            crearMatriz();
            turno = Empieza.ninguno;
            imgTablero = null;
            tablero.DrawImage(Resources.terminado, 0, 0, 600, 600);
            temp1.Stop();
            lblTemp.Text = "TIEMPO";
            puntaje1 = 0;
            puntaje2 = 0;
            lblPuntaje1.Text = "0";
            lblPuntaje2.Text = "0";
            pbTurno.Image = new Bitmap(Resources.ataque, pbTurno.Size);

        }

        private void explosion(int valor)
        {
            List<Image> e = new List<Image>();
            if (valor == jug1Ficha1)
                e.Add(arr1[2]);
            else if (valor == jug1Ficha2)
                e.Add(arr1[1]);
            else if (valor == jug1Ficha3)
                e.Add(arr1[0]);
            else if (valor == jug2Ficha1)
                e.Add(arr2[2]);
            else if (valor == jug2Ficha2)
                e.Add(arr2[1]);
            else if (valor == jug2Ficha3)
                e.Add(arr2[0]);
            else if ( valor == obRoto){
                e.Add(Resources.objetoZroto);
            }


            e.Add(Resources.explocion1);
            e.Add(Resources.explocion2);
            e.Add(Resources.explocion3);
            e.Add(Resources.explocion4);
            e.Add(Resources.explocion5);
            e.Add(Resources.explocion6);
            e.Add(Resources.explocion7);

            if (valor == objeto)
            {
                tablero.DrawImage(Resources.objetoZ, 0, 0, 600, 600);
                System.Threading.Thread.Sleep(500);
                tablero.DrawImage(imgTablero, new Point(0, 0));
                tablero.DrawImage(Resources.objetoZroto, 0, 0, 600, 600);
                System.Threading.Thread.Sleep(500);
            }
            else
            {
                bool primero = true;
                int num = 1;
                Bitmap aa = new Bitmap(imgTablero);
                Graphics a;
                a = Graphics.FromImage(aa);

                foreach (Image item in e)
                {
                    tablero.DrawImage(imgTablero, new Point(0, 0));
                    tablero.DrawImage(item, 0, 0, 600, 600);
                    a.DrawImage(imgTablero, new Point(0, 0));
                    a.DrawImage(item, 0, 0, 600, 600);
                    
                    aa.Save("E:\\a\\img" + num + ".png", System.Drawing.Imaging.ImageFormat.Png);
                    num++;
                    if (!primero)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                    else
                    {
                        System.Threading.Thread.Sleep(500);
                        primero = false;
                    }

                }
            }

            tablero.DrawImage(imgTablero, new Point(0, 0));
        }

        private void ayudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            crAyuda rpt = new crAyuda();
            //Form2 f = new Form2();
            frmAyuda f = new frmAyuda(rpt);
            f.ShowDialog();
        }

        private void activarDesactivar(object sender)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            switch (item.Tag.ToString())
            {
                case "Nuevo":
                    terminarJuegoToolStripMenuItem.Enabled = true;
                    nuevoJuegoToolStripMenuItem.Enabled = false;
                    configuracionToolStripMenuItem.Enabled = false;
                    break;
                case "Terminar":
                    terminarJuegoToolStripMenuItem.Enabled = false;
                    nuevoJuegoToolStripMenuItem.Enabled = true;
                    configuracionToolStripMenuItem.Enabled = true;
                    break;
                default:
                    break;
            }
        }

        Point objetivo;

        private void selectFichaCercana()
        {
            double distancia = 0;
            bool primero = true;
            foreach (Point jug in jugadorUno)
            {
                foreach (Point comp in jugadorDos)
                {
                    double temp = Math.Sqrt(Math.Pow((comp.X - jug.X), 2) + Math.Pow((comp.Y - jug.Y), 2));
                    if (primero)
                    {
                        distancia = temp;
                        primero = !primero;
                    }
                    if (distancia >= temp)
                    {
                        viejas = comp;
                        objetivo = jug;
                        distancia = temp;
                    }                        
                }
            }
        }
        
        private void seguir()
        {
            Direccion dir = direccionMovimiento(viejas, objetivo);
            List<Point> aux = new List<Point>();
            
            foreach (Point p in listaPsobles)
            {
                if (dir == direccionMovimiento(viejas,p))
                {
                    aux.Add(p);
                }
            }

            if (aux.Count == 0)
            {
                foreach (Point p in listaAtaques)
                {
                    if (dir == direccionMovimiento(viejas, p))
                    {
                        nuevas = p;
                        romper();
                        break;
                    }
                    
                }
            }
            else
            {
                nuevas = aux.Last();
                if (peligro())
                {
                    if (jugadorDos.Count > 1)
                    {
                        Point v;
                        while (true)
                        {
                            v = elegirRandom(Opcion.ficha);
                            if (v != viejas)
                            {
                                break;
                            }
                        }
                        viejas = v;
                        desmarcarPosiblesMovimientos();
                        tablero.DrawImage(imgTablero, new Point(0, 0));
                        posiblesAtaques(matriz[viejas.X, viejas.Y] - 10);
                        marcarPosiblesAtaques();
                        marcarPosiblesMovimientos(matriz[viejas.X, viejas.Y] - 10);
                        Application.DoEvents();
                        System.Threading.Thread.Sleep(sleep2);
                           
                    }
                    nuevas = elegirRandom(Opcion.mover);                                
                    
                }
                moverFicha(viejas, nuevas, FichaCPU);
                
            }
        }

        private bool peligro()
        {
            List<Point> aux = listaAtaques;
            matriz[nuevas.X, nuevas.Y] = jug2Ficha2;
            Point vieAux = viejas;
            turno = version001.Empieza.jugador1;
            foreach (Point jug in jugadorUno)
            {
                viejas = jug;
                posiblesAtaques(matriz[jug.X, jug.Y] - 20);
                
                foreach (Point item in listaAtaques)
                {
                    if (nuevas == item)
                    {
                        listaAtaques = aux;
                        matriz[nuevas.X, nuevas.Y] = nada;
                        viejas = vieAux;
                        turno = version001.Empieza.jugador2;
                        return true;
                    }
                }
            }
            matriz[nuevas.X, nuevas.Y] = nada;
            viejas = vieAux;
            turno = version001.Empieza.jugador2;
            return false;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control & e.KeyCode == Keys.S)
            {
                salirToolStripMenuItem.PerformClick();
            }

            if (e.Control & e.KeyCode == Keys.N)
            {
                nuevoJuegoToolStripMenuItem.PerformClick();
            }
            if (e.Control & e.KeyCode == Keys.C)
            {
                configuracionToolStripMenuItem.PerformClick();
            }
            if (e.Control & e.KeyCode == Keys.T)
            {
                terminarJuegoToolStripMenuItem.PerformClick();
            }

            if (e.Control & e.KeyCode == Keys.A)
            {
                ayudaToolStripMenuItem.PerformClick();

            }
        }
    }
}
