
namespace Graficos_juego_OYSL
{
    partial class Grafico_Juego
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Grafico_Juego));
            this.label1 = new System.Windows.Forms.Label();
            this.Señor_oscuro = new System.Windows.Forms.CheckBox();
            this.Lacayo_1 = new System.Windows.Forms.CheckBox();
            this.Lacayo_2 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.jugar = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Yu Gothic", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(277, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "Escoge tu personaje";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // Señor_oscuro
            // 
            this.Señor_oscuro.AutoSize = true;
            this.Señor_oscuro.Font = new System.Drawing.Font("Yu Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Señor_oscuro.Location = new System.Drawing.Point(56, 85);
            this.Señor_oscuro.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Señor_oscuro.Name = "Señor_oscuro";
            this.Señor_oscuro.Size = new System.Drawing.Size(154, 29);
            this.Señor_oscuro.TabIndex = 1;
            this.Señor_oscuro.Text = "Señor Oscuro";
            this.Señor_oscuro.UseVisualStyleBackColor = true;
            // 
            // Lacayo_1
            // 
            this.Lacayo_1.AutoSize = true;
            this.Lacayo_1.Font = new System.Drawing.Font("Yu Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lacayo_1.Location = new System.Drawing.Point(56, 140);
            this.Lacayo_1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Lacayo_1.Name = "Lacayo_1";
            this.Lacayo_1.Size = new System.Drawing.Size(113, 29);
            this.Lacayo_1.TabIndex = 1;
            this.Lacayo_1.Text = "Lacayo 1";
            this.Lacayo_1.UseVisualStyleBackColor = true;
            // 
            // Lacayo_2
            // 
            this.Lacayo_2.AutoSize = true;
            this.Lacayo_2.Font = new System.Drawing.Font("Yu Gothic", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Lacayo_2.Location = new System.Drawing.Point(56, 194);
            this.Lacayo_2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Lacayo_2.Name = "Lacayo_2";
            this.Lacayo_2.Size = new System.Drawing.Size(113, 29);
            this.Lacayo_2.TabIndex = 1;
            this.Lacayo_2.Text = "Lacayo 2";
            this.Lacayo_2.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.jugar);
            this.panel1.Controls.Add(this.Lacayo_2);
            this.panel1.Controls.Add(this.Lacayo_1);
            this.panel1.Controls.Add(this.Señor_oscuro);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(408, 95);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(319, 284);
            this.panel1.TabIndex = 0;
            // 
            // jugar
            // 
            this.jugar.BackColor = System.Drawing.SystemColors.ControlDark;
            this.jugar.Font = new System.Drawing.Font("Yu Gothic Medium", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.jugar.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.jugar.Location = new System.Drawing.Point(83, 229);
            this.jugar.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.jugar.Name = "jugar";
            this.jugar.Size = new System.Drawing.Size(100, 43);
            this.jugar.TabIndex = 1;
            this.jugar.Text = "Jugar";
            this.jugar.UseVisualStyleBackColor = false;
            this.jugar.Click += new System.EventHandler(this.jugar_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 11);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 17);
            this.label2.TabIndex = 1;
            this.label2.Visible = false;
            // 
            // Grafico_Juego
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.ClientSize = new System.Drawing.Size(1353, 516);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Grafico_Juego";
            this.Text = "Grafico_Juego";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
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
    }
}

