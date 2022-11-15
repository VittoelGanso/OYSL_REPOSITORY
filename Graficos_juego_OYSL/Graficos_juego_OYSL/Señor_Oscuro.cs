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
    public partial class Señor_Oscuro : Form
    {
        public Señor_Oscuro()
        {
            InitializeComponent();
        }
        #region cartasMiradas
        List<Cartas> barajaMiradas = new List<Cartas>()
        {
            new Cartas() {Nombre="Señor1", Image="señor_1.png"},
            new Cartas() {Nombre="Señor2", Image="señor_2.png"},
            new Cartas() {Nombre="Señor3", Image="señor_3.png"},
        };
        #endregion

        private void MuestraReverso(PictureBox picturebox)
        {
            picturebox.ImageLocation = "reverso.png";
            picturebox.SizeMode = PictureBoxSizeMode.AutoSize;
        }


        private void Señor_Oscuro_Load(object sender, EventArgs e)
        {

        }
    }
}
