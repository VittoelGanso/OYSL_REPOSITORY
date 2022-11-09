using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Graficos_juego_OYSL
{
    public partial class Lacayo_1 : Form
    {
        public Lacayo_1()
        {
            InitializeComponent();
        }

        private void MuestraReverso(PictureBox picturebox)
        {
            picturebox.ImageLocation = "Pondremos el nombre de la foro";
            picturebox.SizeMode = PictureBoxSizeMode.AutoSize;
        }
    }
}
