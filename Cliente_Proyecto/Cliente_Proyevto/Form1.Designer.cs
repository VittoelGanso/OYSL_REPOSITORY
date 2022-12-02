
namespace Cliente_Proyevto
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
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
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panel1 = new System.Windows.Forms.Panel();
            this.Registrarse = new System.Windows.Forms.Button();
            this.Correo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.IniciarSesion = new System.Windows.Forms.Button();
            this.Password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Usuario = new System.Windows.Forms.TextBox();
            this.Chat = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.Invitacion = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.jugador2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.jugador1 = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.Nombre = new System.Windows.Forms.TextBox();
            this.Query3 = new System.Windows.Forms.RadioButton();
            this.Query2 = new System.Windows.Forms.RadioButton();
            this.Query1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.ListaConectados = new System.Windows.Forms.DataGridView();
            this.Puntuaciones = new System.Windows.Forms.DataGridView();
            this.message = new System.Windows.Forms.TextBox();
            this.Enviar = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListaConectados)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Puntuaciones)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Registrarse);
            this.panel1.Controls.Add(this.Correo);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.IniciarSesion);
            this.panel1.Controls.Add(this.Password);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Usuario);
            this.panel1.Location = new System.Drawing.Point(23, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(153, 367);
            this.panel1.TabIndex = 0;
            // 
            // Registrarse
            // 
            this.Registrarse.Location = new System.Drawing.Point(26, 308);
            this.Registrarse.Name = "Registrarse";
            this.Registrarse.Size = new System.Drawing.Size(98, 38);
            this.Registrarse.TabIndex = 1;
            this.Registrarse.Text = "Registrarse";
            this.Registrarse.UseVisualStyleBackColor = true;
            this.Registrarse.Click += new System.EventHandler(this.Registrarse_Click);
            // 
            // Correo
            // 
            this.Correo.Location = new System.Drawing.Point(22, 268);
            this.Correo.Name = "Correo";
            this.Correo.Size = new System.Drawing.Size(100, 20);
            this.Correo.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 241);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(97, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Introduce tu correo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(23, 216);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Create una cuenta";
            // 
            // IniciarSesion
            // 
            this.IniciarSesion.Location = new System.Drawing.Point(35, 163);
            this.IniciarSesion.Name = "IniciarSesion";
            this.IniciarSesion.Size = new System.Drawing.Size(76, 38);
            this.IniciarSesion.TabIndex = 1;
            this.IniciarSesion.Text = "Iniciar sesión";
            this.IniciarSesion.UseVisualStyleBackColor = true;
            this.IniciarSesion.Click += new System.EventHandler(this.IniciarSesion_Click);
            // 
            // Password
            // 
            this.Password.Location = new System.Drawing.Point(18, 124);
            this.Password.Name = "Password";
            this.Password.Size = new System.Drawing.Size(100, 20);
            this.Password.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 94);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Contraseña";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Nombre de usuario";
            // 
            // Usuario
            // 
            this.Usuario.Location = new System.Drawing.Point(18, 56);
            this.Usuario.Name = "Usuario";
            this.Usuario.Size = new System.Drawing.Size(100, 20);
            this.Usuario.TabIndex = 1;
            // 
            // Chat
            // 
            this.Chat.FormattingEnabled = true;
            this.Chat.Location = new System.Drawing.Point(0, 0);
            this.Chat.Name = "Chat";
            this.Chat.Size = new System.Drawing.Size(153, 238);
            this.Chat.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label10);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.Invitacion);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.jugador2);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.jugador1);
            this.panel2.Controls.Add(this.button4);
            this.panel2.Controls.Add(this.button3);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.Nombre);
            this.panel2.Controls.Add(this.Query3);
            this.panel2.Controls.Add(this.Query2);
            this.panel2.Controls.Add(this.Query1);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Location = new System.Drawing.Point(525, 12);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(198, 439);
            this.panel2.TabIndex = 1;
            this.panel2.Visible = false;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(25, 284);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(153, 13);
            this.label10.TabIndex = 14;
            this.label10.Text = "a los que quieres invitar a jugar";
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 271);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(180, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Introduce el usuario de los jugadores";
            // 
            // Invitacion
            // 
            this.Invitacion.Location = new System.Drawing.Point(48, 356);
            this.Invitacion.Name = "Invitacion";
            this.Invitacion.Size = new System.Drawing.Size(97, 35);
            this.Invitacion.TabIndex = 12;
            this.Invitacion.Text = "Invitar";
            this.Invitacion.UseVisualStyleBackColor = true;
            this.Invitacion.Click += new System.EventHandler(this.Invitacion_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 329);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(54, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Jugador 2";
            // 
            // jugador2
            // 
            this.jugador2.Location = new System.Drawing.Point(94, 326);
            this.jugador2.Name = "jugador2";
            this.jugador2.Size = new System.Drawing.Size(100, 20);
            this.jugador2.TabIndex = 10;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 307);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 9;
            this.label7.Text = "Jugador 1";
            // 
            // jugador1
            // 
            this.jugador1.Location = new System.Drawing.Point(94, 300);
            this.jugador1.Name = "jugador1";
            this.jugador1.Size = new System.Drawing.Size(100, 20);
            this.jugador1.TabIndex = 8;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(48, 397);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(97, 39);
            this.button4.TabIndex = 7;
            this.button4.Text = "Desconectar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(48, 221);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(97, 38);
            this.button3.TabIndex = 6;
            this.button3.Text = "Enviar";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(23, 63);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(155, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Introduce el usuario del jugador";
            // 
            // Nombre
            // 
            this.Nombre.Location = new System.Drawing.Point(48, 91);
            this.Nombre.Name = "Nombre";
            this.Nombre.Size = new System.Drawing.Size(110, 20);
            this.Nombre.TabIndex = 4;
            // 
            // Query3
            // 
            this.Query3.AutoSize = true;
            this.Query3.Location = new System.Drawing.Point(15, 184);
            this.Query3.Name = "Query3";
            this.Query3.Size = new System.Drawing.Size(110, 17);
            this.Query3.TabIndex = 3;
            this.Query3.TabStop = true;
            this.Query3.Text = "El mejor mentiroso";
            this.Query3.UseVisualStyleBackColor = true;
            // 
            // Query2
            // 
            this.Query2.AutoSize = true;
            this.Query2.Location = new System.Drawing.Point(15, 151);
            this.Query2.Name = "Query2";
            this.Query2.Size = new System.Drawing.Size(179, 17);
            this.Query2.TabIndex = 2;
            this.Query2.TabStop = true;
            this.Query2.Text = "¿Cuántas partidas ha ganado...?";
            this.Query2.UseVisualStyleBackColor = true;
            // 
            // Query1
            // 
            this.Query1.AutoSize = true;
            this.Query1.Location = new System.Drawing.Point(15, 124);
            this.Query1.Name = "Query1";
            this.Query1.Size = new System.Drawing.Size(175, 17);
            this.Query1.TabIndex = 1;
            this.Query1.TabStop = true;
            this.Query1.Text = "¿Cuántas partidas ha jugado...?";
            this.Query1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(70, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 18);
            this.label3.TabIndex = 0;
            this.label3.Text = "Consultas";
            // 
            // ListaConectados
            // 
            this.ListaConectados.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.ListaConectados.Location = new System.Drawing.Point(23, 3);
            this.ListaConectados.Name = "ListaConectados";
            this.ListaConectados.Size = new System.Drawing.Size(169, 145);
            this.ListaConectados.TabIndex = 2;
            this.ListaConectados.Visible = false;
            // 
            // Puntuaciones
            // 
            this.Puntuaciones.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.Puntuaciones.Location = new System.Drawing.Point(235, 39);
            this.Puntuaciones.Name = "Puntuaciones";
            this.Puntuaciones.Size = new System.Drawing.Size(240, 307);
            this.Puntuaciones.TabIndex = 3;
            this.Puntuaciones.Visible = false;
            // 
            // message
            // 
            this.message.Location = new System.Drawing.Point(0, 241);
            this.message.Name = "message";
            this.message.Size = new System.Drawing.Size(150, 20);
            this.message.TabIndex = 5;
            // 
            // Enviar
            // 
            this.Enviar.Location = new System.Drawing.Point(394, 280);
            this.Enviar.Name = "Enviar";
            this.Enviar.Size = new System.Drawing.Size(59, 23);
            this.Enviar.TabIndex = 6;
            this.Enviar.Text = "envio";
            this.Enviar.UseVisualStyleBackColor = true;
            this.Enviar.Visible = false;
            this.Enviar.Click += new System.EventHandler(this.Enviar_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.Chat);
            this.panel3.Controls.Add(this.message);
            this.panel3.Location = new System.Drawing.Point(235, 39);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(153, 264);
            this.panel3.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(735, 450);
            this.Controls.Add(this.Enviar);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.Puntuaciones);
            this.Controls.Add(this.ListaConectados);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ListaConectados)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Puntuaciones)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button IniciarSesion;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Usuario;
        private System.Windows.Forms.Button Registrarse;
        private System.Windows.Forms.TextBox Correo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton Query3;
        private System.Windows.Forms.RadioButton Query2;
        private System.Windows.Forms.RadioButton Query1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Nombre;
        private System.Windows.Forms.DataGridView ListaConectados;
        private System.Windows.Forms.DataGridView Puntuaciones;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button Invitacion;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox jugador2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox jugador1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ListBox Chat;
        private System.Windows.Forms.TextBox message;
        private System.Windows.Forms.Button Enviar;
        private System.Windows.Forms.Panel panel3;
    }
}

