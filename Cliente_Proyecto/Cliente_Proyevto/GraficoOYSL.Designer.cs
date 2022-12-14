
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
            this.panel2.Location = new System.Drawing.Point(337, 76);
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
            this.Envio.Location = new System.Drawing.Point(539, 278);
            this.Envio.Name = "Envio";
            this.Envio.Size = new System.Drawing.Size(104, 39);
            this.Envio.TabIndex = 3;
            this.Envio.Text = "Enviar";
            this.Envio.UseVisualStyleBackColor = true;
            this.Envio.Click += new System.EventHandler(this.Envio_Click);
            // 
            // GraficoOYSL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1015, 419);
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
    }
}

