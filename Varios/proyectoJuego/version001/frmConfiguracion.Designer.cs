namespace version001
{
    partial class frmConfiguracion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmConfiguracion));
            this.chTiempo = new Ejercicio01.controlHora();
            this.lblTiempo = new System.Windows.Forms.Label();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.rbFacil = new System.Windows.Forms.RadioButton();
            this.rbMedio = new System.Windows.Forms.RadioButton();
            this.rbDificil = new System.Windows.Forms.RadioButton();
            this.lblDificultad = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnCPU = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.btnJugador = new System.Windows.Forms.Button();
            this.cbEmpieza = new System.Windows.Forms.ComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // chTiempo
            // 
            this.chTiempo.Ahora = new System.DateTime(2015, 11, 10, 0, 0, 0, 0);
            this.chTiempo.Hora = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.chTiempo.Location = new System.Drawing.Point(25, 40);
            this.chTiempo.Minuntos = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.chTiempo.Name = "chTiempo";
            this.chTiempo.Segundos = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.chTiempo.Size = new System.Drawing.Size(120, 20);
            this.chTiempo.TabIndex = 0;
            // 
            // lblTiempo
            // 
            this.lblTiempo.AutoSize = true;
            this.lblTiempo.Font = new System.Drawing.Font("Arimo", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTiempo.Location = new System.Drawing.Point(37, 9);
            this.lblTiempo.Name = "lblTiempo";
            this.lblTiempo.Size = new System.Drawing.Size(94, 24);
            this.lblTiempo.TabIndex = 1;
            this.lblTiempo.Text = "Duracion";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "fichaRoja1.png");
            this.imageList1.Images.SetKeyName(1, "fichaVerde1.png");
            this.imageList1.Images.SetKeyName(2, "fichaAmarillo1.png");
            this.imageList1.Images.SetKeyName(3, "fichaAzul1.png");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arimo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(18, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 18);
            this.label1.TabIndex = 4;
            this.label1.Text = "CPU";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arimo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(82, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 18);
            this.label2.TabIndex = 5;
            this.label2.Text = "HUMANO";
            // 
            // rbFacil
            // 
            this.rbFacil.AutoSize = true;
            this.rbFacil.Checked = true;
            this.rbFacil.Font = new System.Drawing.Font("Arimo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbFacil.Location = new System.Drawing.Point(20, 47);
            this.rbFacil.Name = "rbFacil";
            this.rbFacil.Size = new System.Drawing.Size(60, 22);
            this.rbFacil.TabIndex = 6;
            this.rbFacil.TabStop = true;
            this.rbFacil.Tag = "19";
            this.rbFacil.Text = "Facil";
            this.rbFacil.UseVisualStyleBackColor = true;
            this.rbFacil.CheckedChanged += new System.EventHandler(this.rbFacil_CheckedChanged);
            // 
            // rbMedio
            // 
            this.rbMedio.AutoSize = true;
            this.rbMedio.Font = new System.Drawing.Font("Arimo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbMedio.Location = new System.Drawing.Point(20, 70);
            this.rbMedio.Name = "rbMedio";
            this.rbMedio.Size = new System.Drawing.Size(70, 22);
            this.rbMedio.TabIndex = 7;
            this.rbMedio.Tag = "28";
            this.rbMedio.Text = "Medio";
            this.rbMedio.UseVisualStyleBackColor = true;
            this.rbMedio.CheckedChanged += new System.EventHandler(this.rbFacil_CheckedChanged);
            // 
            // rbDificil
            // 
            this.rbDificil.AutoSize = true;
            this.rbDificil.Font = new System.Drawing.Font("Arimo", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbDificil.Location = new System.Drawing.Point(20, 93);
            this.rbDificil.Name = "rbDificil";
            this.rbDificil.Size = new System.Drawing.Size(65, 22);
            this.rbDificil.TabIndex = 8;
            this.rbDificil.Tag = "38";
            this.rbDificil.Text = "Dificil";
            this.rbDificil.UseVisualStyleBackColor = true;
            this.rbDificil.CheckedChanged += new System.EventHandler(this.rbFacil_CheckedChanged);
            // 
            // lblDificultad
            // 
            this.lblDificultad.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblDificultad.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDificultad.ImageIndex = 0;
            this.lblDificultad.Location = new System.Drawing.Point(95, 48);
            this.lblDificultad.Name = "lblDificultad";
            this.lblDificultad.Size = new System.Drawing.Size(55, 55);
            this.lblDificultad.TabIndex = 9;
            this.lblDificultad.Text = "66";
            this.lblDificultad.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.LimeGreen;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.rbFacil);
            this.panel1.Controls.Add(this.lblDificultad);
            this.panel1.Controls.Add(this.rbMedio);
            this.panel1.Controls.Add(this.rbDificil);
            this.panel1.Location = new System.Drawing.Point(39, 127);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(174, 129);
            this.panel1.TabIndex = 10;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Arimo", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(44, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 24);
            this.label4.TabIndex = 16;
            this.label4.Text = "Dificultad";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.LimeGreen;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel2.Controls.Add(this.btnCPU);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.btnJugador);
            this.panel2.Location = new System.Drawing.Point(262, 127);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(174, 129);
            this.panel2.TabIndex = 11;
            // 
            // btnCPU
            // 
            this.btnCPU.BackColor = System.Drawing.Color.Transparent;
            this.btnCPU.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnCPU.FlatAppearance.BorderSize = 0;
            this.btnCPU.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnCPU.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnCPU.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCPU.ImageIndex = 3;
            this.btnCPU.ImageList = this.imageList1;
            this.btnCPU.Location = new System.Drawing.Point(12, 68);
            this.btnCPU.Name = "btnCPU";
            this.btnCPU.Size = new System.Drawing.Size(50, 50);
            this.btnCPU.TabIndex = 2;
            this.btnCPU.TabStop = false;
            this.btnCPU.UseVisualStyleBackColor = false;
            this.btnCPU.Click += new System.EventHandler(this.btnCPU_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Arimo", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(52, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 24);
            this.label3.TabIndex = 15;
            this.label3.Text = "Ficha";
            // 
            // btnJugador
            // 
            this.btnJugador.BackColor = System.Drawing.Color.Transparent;
            this.btnJugador.FlatAppearance.BorderColor = System.Drawing.Color.Black;
            this.btnJugador.FlatAppearance.BorderSize = 0;
            this.btnJugador.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.btnJugador.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.btnJugador.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnJugador.ImageIndex = 3;
            this.btnJugador.ImageList = this.imageList1;
            this.btnJugador.Location = new System.Drawing.Point(99, 68);
            this.btnJugador.Name = "btnJugador";
            this.btnJugador.Size = new System.Drawing.Size(50, 50);
            this.btnJugador.TabIndex = 3;
            this.btnJugador.UseVisualStyleBackColor = false;
            this.btnJugador.Click += new System.EventHandler(this.btnCPU_Click);
            // 
            // cbEmpieza
            // 
            this.cbEmpieza.FormattingEnabled = true;
            this.cbEmpieza.Items.AddRange(new object[] {
            "CPU",
            "HUMANO",
            "ALEATORIO"});
            this.cbEmpieza.Location = new System.Drawing.Point(25, 39);
            this.cbEmpieza.Name = "cbEmpieza";
            this.cbEmpieza.Size = new System.Drawing.Size(121, 21);
            this.cbEmpieza.TabIndex = 12;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.LimeGreen;
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel3.Controls.Add(this.chTiempo);
            this.panel3.Controls.Add(this.lblTiempo);
            this.panel3.Location = new System.Drawing.Point(39, 32);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(174, 72);
            this.panel3.TabIndex = 13;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.LimeGreen;
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel4.Controls.Add(this.label5);
            this.panel4.Controls.Add(this.cbEmpieza);
            this.panel4.Location = new System.Drawing.Point(262, 32);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(174, 72);
            this.panel4.TabIndex = 14;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Arimo", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(41, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(89, 24);
            this.label5.TabIndex = 17;
            this.label5.Text = "Empieza";
            // 
            // frmConfiguracion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 289);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmConfiguracion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Tag = "33";
            this.Text = "CONFIGURACION";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmConfiguracion_FormClosing);
            this.Load += new System.EventHandler(this.frmConfiguracion_Load);
            this.Shown += new System.EventHandler(this.frmConfiguracion_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmConfiguracion_KeyDown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Ejercicio01.controlHora chTiempo;
        private System.Windows.Forms.Label lblTiempo;
        private System.Windows.Forms.Button btnCPU;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button btnJugador;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton rbFacil;
        private System.Windows.Forms.RadioButton rbMedio;
        private System.Windows.Forms.RadioButton rbDificil;
        private System.Windows.Forms.Label lblDificultad;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox cbEmpieza;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
    }
}