namespace version001
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pnlTablero = new System.Windows.Forms.Panel();
            this.msMenu = new System.Windows.Forms.MenuStrip();
            this.juegoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.terminarJuegoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configuracionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ayudaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblTemp = new System.Windows.Forms.Label();
            this.temp1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.lblPuntaje1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblPuntaje2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pbTurno = new System.Windows.Forms.PictureBox();
            this.nuevoJuegoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTurno)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTablero
            // 
            this.pnlTablero.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlTablero.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.pnlTablero.ForeColor = System.Drawing.SystemColors.ControlText;
            this.pnlTablero.Location = new System.Drawing.Point(250, 49);
            this.pnlTablero.Margin = new System.Windows.Forms.Padding(5);
            this.pnlTablero.Name = "pnlTablero";
            this.pnlTablero.Size = new System.Drawing.Size(600, 600);
            this.pnlTablero.TabIndex = 0;
            this.pnlTablero.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlTablero_Paint);
            this.pnlTablero.MouseClick += new System.Windows.Forms.MouseEventHandler(this.pnlTablero_MouseClick);
            // 
            // msMenu
            // 
            this.msMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.juegoToolStripMenuItem,
            this.ayudaToolStripMenuItem});
            this.msMenu.Location = new System.Drawing.Point(0, 0);
            this.msMenu.Name = "msMenu";
            this.msMenu.Size = new System.Drawing.Size(1112, 24);
            this.msMenu.TabIndex = 3;
            this.msMenu.Text = "menuStrip1";
            // 
            // juegoToolStripMenuItem
            // 
            this.juegoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoJuegoToolStripMenuItem,
            this.terminarJuegoToolStripMenuItem,
            this.configuracionToolStripMenuItem,
            this.salirToolStripMenuItem});
            this.juegoToolStripMenuItem.Name = "juegoToolStripMenuItem";
            this.juegoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J)));
            this.juegoToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.juegoToolStripMenuItem.Text = "Juego";
            // 
            // terminarJuegoToolStripMenuItem
            // 
            this.terminarJuegoToolStripMenuItem.Enabled = false;
            this.terminarJuegoToolStripMenuItem.Name = "terminarJuegoToolStripMenuItem";
            this.terminarJuegoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.terminarJuegoToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.terminarJuegoToolStripMenuItem.Tag = "Terminar";
            this.terminarJuegoToolStripMenuItem.Text = "Terminar Juego";
            this.terminarJuegoToolStripMenuItem.Click += new System.EventHandler(this.terminarJuegoToolStripMenuItem_Click);
            // 
            // configuracionToolStripMenuItem
            // 
            this.configuracionToolStripMenuItem.Name = "configuracionToolStripMenuItem";
            this.configuracionToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.configuracionToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.configuracionToolStripMenuItem.Tag = "Configuracion";
            this.configuracionToolStripMenuItem.Text = "Configuracion";
            this.configuracionToolStripMenuItem.Click += new System.EventHandler(this.configuracionToolStripMenuItem_Click);
            // 
            // salirToolStripMenuItem
            // 
            this.salirToolStripMenuItem.Name = "salirToolStripMenuItem";
            this.salirToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.salirToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.salirToolStripMenuItem.Tag = "Salir";
            this.salirToolStripMenuItem.Text = "Salir";
            this.salirToolStripMenuItem.Click += new System.EventHandler(this.salirToolStripMenuItem_Click);
            // 
            // ayudaToolStripMenuItem
            // 
            this.ayudaToolStripMenuItem.Name = "ayudaToolStripMenuItem";
            this.ayudaToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.ayudaToolStripMenuItem.Size = new System.Drawing.Size(53, 20);
            this.ayudaToolStripMenuItem.Text = "Ayuda";
            this.ayudaToolStripMenuItem.Click += new System.EventHandler(this.ayudaToolStripMenuItem_Click);
            // 
            // lblTemp
            // 
            this.lblTemp.BackColor = System.Drawing.Color.LimeGreen;
            this.lblTemp.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTemp.Font = new System.Drawing.Font("Courier New", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTemp.Location = new System.Drawing.Point(12, 49);
            this.lblTemp.Name = "lblTemp";
            this.lblTemp.Size = new System.Drawing.Size(230, 50);
            this.lblTemp.TabIndex = 0;
            this.lblTemp.Text = "TIEMPO";
            this.lblTemp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // temp1
            // 
            this.temp1.Interval = 1000;
            this.temp1.Tick += new System.EventHandler(this.temp_Tick);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arimo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(173, 18);
            this.label2.TabIndex = 6;
            this.label2.Text = "PUNTAJE JUGADOR 1";
            // 
            // lblPuntaje1
            // 
            this.lblPuntaje1.Location = new System.Drawing.Point(3, 54);
            this.lblPuntaje1.Name = "lblPuntaje1";
            this.lblPuntaje1.Size = new System.Drawing.Size(190, 60);
            this.lblPuntaje1.TabIndex = 7;
            this.lblPuntaje1.Text = "0";
            this.lblPuntaje1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arimo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(173, 18);
            this.label3.TabIndex = 8;
            this.label3.Text = "PUNTAJE JUGADOR 2";
            // 
            // lblPuntaje2
            // 
            this.lblPuntaje2.Location = new System.Drawing.Point(3, 242);
            this.lblPuntaje2.Name = "lblPuntaje2";
            this.lblPuntaje2.Size = new System.Drawing.Size(190, 60);
            this.lblPuntaje2.TabIndex = 9;
            this.lblPuntaje2.Text = "0";
            this.lblPuntaje2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LimeGreen;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.lblPuntaje2);
            this.panel1.Controls.Add(this.lblPuntaje1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(888, 49);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 335);
            this.panel1.TabIndex = 10;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LimeGreen;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.pbTurno);
            this.panel2.Location = new System.Drawing.Point(40, 139);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(172, 160);
            this.panel2.TabIndex = 11;
            // 
            // pbTurno
            // 
            this.pbTurno.Location = new System.Drawing.Point(34, 28);
            this.pbTurno.Name = "pbTurno";
            this.pbTurno.Size = new System.Drawing.Size(100, 100);
            this.pbTurno.TabIndex = 6;
            this.pbTurno.TabStop = false;
            // 
            // nuevoJuegoToolStripMenuItem
            // 
            this.nuevoJuegoToolStripMenuItem.Name = "nuevoJuegoToolStripMenuItem";
            this.nuevoJuegoToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.nuevoJuegoToolStripMenuItem.Size = new System.Drawing.Size(196, 22);
            this.nuevoJuegoToolStripMenuItem.Tag = "Nuevo";
            this.nuevoJuegoToolStripMenuItem.Text = "Nuevo Juego";
            this.nuevoJuegoToolStripMenuItem.Click += new System.EventHandler(this.nuevoJuegoToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 646);
            this.ControlBox = false;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblTemp);
            this.Controls.Add(this.pnlTablero);
            this.Controls.Add(this.msMenu);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMenu;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DRAGON BALL Z";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.msMenu.ResumeLayout(false);
            this.msMenu.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbTurno)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlTablero;
        private System.Windows.Forms.MenuStrip msMenu;
        private System.Windows.Forms.ToolStripMenuItem juegoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configuracionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ayudaToolStripMenuItem;
        private System.Windows.Forms.Label lblTemp;
        private System.Windows.Forms.Timer temp1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPuntaje1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblPuntaje2;
        private System.Windows.Forms.ToolStripMenuItem terminarJuegoToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pbTurno;
        private System.Windows.Forms.ToolStripMenuItem nuevoJuegoToolStripMenuItem;
    }
}

