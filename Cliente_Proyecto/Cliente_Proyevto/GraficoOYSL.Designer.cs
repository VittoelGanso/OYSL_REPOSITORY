
namespace Graficos_juego_OYSL
{
    partial class GraficoOYSL
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraficoOYSL));
            this.label1 = new System.Windows.Forms.Label();
            this.Señor_oscuro = new System.Windows.Forms.CheckBox();
            this.Lacayo_1 = new System.Windows.Forms.CheckBox();
            this.Lacayo_2 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.jugar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.informacion = new System.Windows.Forms.TextBox();
            this.chat = new System.Windows.Forms.ListBox();
            this.Envio = new System.Windows.Forms.Button();
            this.Boton_Instrucciones = new System.Windows.Forms.Button();
            this.Instrucciones = new System.Windows.Forms.ListBox();
            this.Salir = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(228, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Escoge tu personaje";
            // 
            // Señor_oscuro
            // 
            this.Señor_oscuro.AutoSize = true;
            this.Señor_oscuro.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Señor_oscuro.Location = new System.Drawing.Point(42, 69);
            this.Señor_oscuro.Name = "Señor_oscuro";
            this.Señor_oscuro.Size = new System.Drawing.Size(133, 22);
            this.Señor_oscuro.TabIndex = 1;
            this.Señor_oscuro.Text = "Señor Oscuro";
            this.Señor_oscuro.UseVisualStyleBackColor = true;
            // 
            // Lacayo_1
            // 
            this.Lacayo_1.AutoSize = true;
            this.Lacayo_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lacayo_1.Location = new System.Drawing.Point(42, 114);
            this.Lacayo_1.Name = "Lacayo_1";
            this.Lacayo_1.Size = new System.Drawing.Size(95, 22);
            this.Lacayo_1.TabIndex = 1;
            this.Lacayo_1.Text = "Lacayo 1";
            this.Lacayo_1.UseVisualStyleBackColor = true;
            // 
            // Lacayo_2
            // 
            this.Lacayo_2.AutoSize = true;
            this.Lacayo_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lacayo_2.Location = new System.Drawing.Point(42, 158);
            this.Lacayo_2.Name = "Lacayo_2";
            this.Lacayo_2.Size = new System.Drawing.Size(95, 22);
            this.Lacayo_2.TabIndex = 1;
            this.Lacayo_2.Text = "Lacayo 2";
            this.Lacayo_2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.jugar);
            this.panel1.Controls.Add(this.Lacayo_2);
            this.panel1.Controls.Add(this.Lacayo_1);
            this.panel1.Controls.Add(this.Señor_oscuro);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(24, 76);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(239, 231);
            this.panel1.TabIndex = 0;
            // 
            // jugar
            // 
            this.jugar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jugar.Location = new System.Drawing.Point(62, 186);
            this.jugar.Name = "jugar";
            this.jugar.Size = new System.Drawing.Size(75, 35);
            this.jugar.TabIndex = 1;
            this.jugar.Text = "Jugar";
            this.jugar.UseVisualStyleBackColor = true;
            this.jugar.Click += new System.EventHandler(this.jugar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 1;
            this.label2.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.informacion);
            this.panel2.Controls.Add(this.chat);
            this.panel2.Location = new System.Drawing.Point(281, 76);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(196, 241);
            this.panel2.TabIndex = 2;
            // 
            // informacion
            // 
            this.informacion.Location = new System.Drawing.Point(0, 221);
            this.informacion.Name = "informacion";
            this.informacion.Size = new System.Drawing.Size(196, 20);
            this.informacion.TabIndex = 3;
            // 
            // chat
            // 
            this.chat.FormattingEnabled = true;
            this.chat.Location = new System.Drawing.Point(0, 0);
            this.chat.Name = "chat";
            this.chat.Size = new System.Drawing.Size(196, 225);
            this.chat.TabIndex = 0;
            // 
            // Envio
            // 
            this.Envio.Location = new System.Drawing.Point(328, 323);
            this.Envio.Name = "Envio";
            this.Envio.Size = new System.Drawing.Size(104, 39);
            this.Envio.TabIndex = 3;
            this.Envio.Text = "Enviar";
            this.Envio.UseVisualStyleBackColor = true;
            this.Envio.Click += new System.EventHandler(this.Envio_Click);
            // 
            // Boton_Instrucciones
            // 
            this.Boton_Instrucciones.Location = new System.Drawing.Point(718, 362);
            this.Boton_Instrucciones.Name = "Boton_Instrucciones";
            this.Boton_Instrucciones.Size = new System.Drawing.Size(140, 41);
            this.Boton_Instrucciones.TabIndex = 4;
            this.Boton_Instrucciones.Text = "Ver Instrucciones del juego";
            this.Boton_Instrucciones.UseVisualStyleBackColor = true;
            this.Boton_Instrucciones.Click += new System.EventHandler(this.Boton_Instrucciones_Click);
            // 
            // Instrucciones
            // 
            this.Instrucciones.FormattingEnabled = true;
            this.Instrucciones.Items.AddRange(new object[] {
            "Bienvenidos a Señor Oscuro:",
            "Este juego consiste en que los jugadores interpretan el papel de Lacayo o Señor O" +
                "scuro.",
            "",
            "Primero se debe decidir quien interpretará cada personaje. ",
            "En sucesivas partidas el papel de Señor Oscuro será interpretado por el perdedor." +
                "",
            "",
            "Como jugar:",
            "",
            "Trabajo de los Lacayos:",
            "Hay dos tipos de cartas, las de \"excusa\" y las de \"acción\".",
            "Las de excusa son los elementos narrativos que usa el Lacayo para crear una histo" +
                "ria para ",
            "justificarse frente a su amo. ",
            "Se juegan por si solas para introducir elementos narrativos en la historia que se" +
                " está contando. ",
            "Las cartas de acción deben jugarse con una carta de excusa para permitir una acci" +
                "ón ",
            "particular. ",
            "La carta del dedo permite echarle la culpa al otro jugador obligándole a empezar " +
                "su turno. ",
            "La de la palma de la mano levantada permite interferir con una nueva excusa en la" +
                " narración ",
            "del otro jugador durante su turno. ",
            "Hay cartas que permiten solo una de esas acciones y hay otras en la que se puede " +
                "elegir ",
            "la acción. ",
            "",
            "Trabajo del Señor Oscuro:",
            "El Señor Oscuro tiene tres cartas que se llaman \"miradas fulminantes\". ",
            "Estas miradas las asigna a los jugadores a lo largo de la partida a medida que su" +
                " enfado crece",
            " (no le gusta la historia). ",
            "El primer jugador que reciba las tres miradas fulminantes diferentes pierde la pa" +
                "rtida."});
            this.Instrucciones.Location = new System.Drawing.Point(483, 8);
            this.Instrucciones.Name = "Instrucciones";
            this.Instrucciones.Size = new System.Drawing.Size(492, 355);
            this.Instrucciones.TabIndex = 5;
            this.Instrucciones.Visible = false;
            // 
            // Salir
            // 
            this.Salir.Location = new System.Drawing.Point(718, 362);
            this.Salir.Name = "Salir";
            this.Salir.Size = new System.Drawing.Size(140, 45);
            this.Salir.TabIndex = 6;
            this.Salir.Text = "Salir";
            this.Salir.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.Salir.UseVisualStyleBackColor = true;
            this.Salir.Visible = false;
            this.Salir.Click += new System.EventHandler(this.Salir_Click);
            // 
            // GraficoOYSL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1015, 419);
            this.Controls.Add(this.Salir);
            this.Controls.Add(this.Instrucciones);
            this.Controls.Add(this.Boton_Instrucciones);
            this.Controls.Add(this.Envio);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Name = "GraficoOYSL";
            this.Text = "Grafico_Juego";
            this.Load += new System.EventHandler(this.GraficoOYSL_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox Señor_oscuro;
        private System.Windows.Forms.CheckBox Lacayo_1;
        private System.Windows.Forms.CheckBox Lacayo_2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button jugar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TextBox informacion;
        private System.Windows.Forms.ListBox chat;
        private System.Windows.Forms.Button Envio;
        private System.Windows.Forms.Button Boton_Instrucciones;
        private System.Windows.Forms.ListBox Instrucciones;
        private System.Windows.Forms.Button Salir;
    }
}

