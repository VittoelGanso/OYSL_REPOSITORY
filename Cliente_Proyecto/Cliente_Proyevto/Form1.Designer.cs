
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
            this.button5 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Correo = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.Password = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.Usuario = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.Nombre = new System.Windows.Forms.TextBox();
            this.Query3 = new System.Windows.Forms.RadioButton();
            this.Query2 = new System.Windows.Forms.RadioButton();
            this.Query1 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.ListaConectados_button = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button5);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.Correo);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.Password);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Usuario);
            this.panel1.Location = new System.Drawing.Point(25, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(153, 426);
            this.panel1.TabIndex = 0;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(26, 366);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(96, 43);
            this.button5.TabIndex = 2;
            this.button5.Text = "Connectar";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(26, 308);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 38);
            this.button2.TabIndex = 1;
            this.button2.Text = "Registrarse";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
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
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(35, 163);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(76, 38);
            this.button1.TabIndex = 1;
            this.button1.Text = "Iniciar sesión";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            // panel2
            // 
            this.panel2.Controls.Add(this.ListaConectados_button);
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
            this.panel2.Size = new System.Drawing.Size(198, 367);
            this.panel2.TabIndex = 1;
            this.panel2.Visible = false;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(48, 268);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(97, 39);
            this.button4.TabIndex = 7;
            this.button4.Text = "Desconectar";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(48, 216);
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
            this.Query3.Size = new System.Drawing.Size(179, 17);
            this.Query3.TabIndex = 3;
            this.Query3.TabStop = true;
            this.Query3.Text = "Mostrar la tabla de puntuaciones";
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
            // ListaConectados_button
            // 
            this.ListaConectados_button.Location = new System.Drawing.Point(48, 323);
            this.ListaConectados_button.Name = "ListaConectados_button";
            this.ListaConectados_button.Size = new System.Drawing.Size(97, 41);
            this.ListaConectados_button.TabIndex = 8;
            this.ListaConectados_button.Text = "Ver lista de usuarios conectados";
            this.ListaConectados_button.UseVisualStyleBackColor = true;
            this.ListaConectados_button.Click += new System.EventHandler(this.ListaConectados_button_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(735, 450);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox Password;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox Usuario;
        private System.Windows.Forms.Button button2;
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
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button ListaConectados_button;
    }
}

