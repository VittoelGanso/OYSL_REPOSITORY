using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Cliente_Proyevto;

namespace Graficos_juego_OYSL
{
    public partial class Lacayos : Form
    {
        private int lacayo;
        int nForm;
        Socket server;
        int excusa;

        #region cartasExcusa

        List<Cartas> barajaexcusa = new List<Cartas>()
        {

            new Cartas() {Nombre = "Antiguismo_Lugar", Image = "antiquismo_lugar.png", ID = 1},
            new Cartas() {Nombre = "Antorcha", Image = "antorcha.png", ID =2},
            new Cartas() {Nombre = "Arbol_Milenario", Image = "arbol_milenario.png", ID=3},
            new Cartas() {Nombre = "Artefacto_imposible", Image = "artefacto_imposible.png", ID=4},
            new Cartas() {Nombre = "Barco_Volador", Image = "barco_volador", ID=5},
            new Cartas() {Nombre = "Bella_exotica", Image = "bella_exotica", ID=6},
            new Cartas() {Nombre = "Bola_de_Cristal", Image="bola_de_cristal.png", ID=7},
            new Cartas() {Nombre="Bosque_oscuro", Image="bosque_oscuro.png", ID=8},
            new Cartas() {Nombre = "Caballero_negro", Image="caballero_negro.png", ID=9},
            new Cartas() {Nombre = "Cachorrito", Image="cahorrito.png", ID=10},
            new Cartas() {Nombre="Cadena_pesada", Image = "cadena_pesada.png", ID=11},
            new Cartas() {Nombre = "Calavera", Image = "calavera.png", ID=12},
            new Cartas() {Nombre = "Camino", Image = "camino.png", ID=13},
            new Cartas() {Nombre = "Capa_Archimago", Image="capa_del_archimago.png", ID=14},
            new Cartas() {Nombre="Cerveza_Enana", Image="cerveza_enana.png", ID=15},
            new Cartas() {Nombre="Ciudad_Flotante", Image ="ciudad_flotante.png", ID=16},
            new Cartas() {Nombre="Cofre_Cerrado", Image="cofre_cerrado.png", ID=17},
            new Cartas() {Nombre="Comerciante_Tramposo", Image="comerciante_tramposo.png", ID=18},
            new Cartas() {Nombre="Damisela_Peligro", Image="damisela_en_peligro.png", ID=19},
            new Cartas() {Nombre="Desolado_Desierto", Image="desolado_desierto.png", ID=20},
            new Cartas() {Nombre="Dragon", Image="dragon.png", ID=21},
            new Cartas() {Nombre="Elfa_Siniestra", Image="elfa_siniestra.png", ID=22},
            new Cartas() {Nombre="Escalera", Image="escalera.png", ID=23},
            new Cartas() {Nombre="Escudo_Redondo", Image="escudo_redondo.png", ID=24},
            new Cartas() {Nombre="EspadaInteligente", Image="espada_inteligente.png", ID=25},
            new Cartas() {Nombre="Espada_Soluble_Agua", Image="espada_soluble_en_agua.png", ID=26},
            new Cartas() {Nombre="Espantapajaros", Image="espantapajaros.png", ID=27},
            new Cartas() {Nombre="Fantasma_Aterrador", Image="fantasma_aterrador.png", ID=28},
            new Cartas() {Nombre="Gigante_Piedra", Image="gigante_piedra.png", ID=29},
            new Cartas() {Nombre="Goblin_lerdo", Image="goblin_lerdo.png", ID=30},
            new Cartas() {Nombre="Guardian_Diminuto", Image="guardian_diminuto.png", ID=31},
            new Cartas() {Nombre="Guerrero_Sin_Seso", Image="guerrero_sin_seso.png", ID=32},
            new Cartas() {Nombre="Hehicero_este", Image="hechicero_este.png", ID=33},
            new Cartas() {Nombre="Hierba_Sueño", Image="hierba_del_sueño.png", ID=34},
            new Cartas() {Nombre="Hombre_ViejoRapaz", Image="hombre_viejo_rapaz.png", ID=35},
            new Cartas() {Nombre="Isla_Salvaje", Image="isla_salvaje.png", ID=36},
            new Cartas() {Nombre="Llave", Image="llave.png", ID=37},
            new Cartas() {Nombre="Lugar_Equivocado", Image="lugar_equivocado.png", ID=38},
            new Cartas() {Nombre="Mansion_Oscura", Image="mansion_oscura.png", ID=39},
            new Cartas() {Nombre="Mapa", Image="mapa.png", ID=40},
            new Cartas() {Nombre="Mar", Image="mar.png", ID=41},
            new Cartas() {Nombre="Mazmorra_Oscura", Image="mazmorra_oscura.png", ID=42},
            new Cartas() {Nombre="Millon_Tentaculos", Image="millon_de_tentaculos.png", ID=43},
            new Cartas() {Nombre="Mina", Image="mina.png", ID=44},
            new Cartas() {Nombre="Momia", Image="momia.png", ID=45},
            new Cartas() {Nombre="Monstruo_Gloton", Image="monstruo_gloton.png", ID=46},
            new Cartas() {Nombre="Monstruo_Monstruo", Image="monstruo_mas_monstruo.png", ID=47},
            new Cartas() {Nombre="Murcielago", Image="murcielago.png", ID=48},
            new Cartas() {Nombre="Muro_Insuperable", Image="muro_insuperable.png", ID=49},
            new Cartas() {Nombre="Nieve", Image="nieve_caia.png", ID=50},
            new Cartas() {Nombre="Objetos", Image="objetos_sorprendentes.png", ID=51},
            new Cartas() {Nombre="Oro", Image="oro.png", ID=52},
            new Cartas() {Nombre="Otro_Señor_Oscuro", Image="otro_señor_oscuro.png", ID=53},
            new Cartas() {Nombre="Pantano", Image="pantano.png", ID=54},
            new Cartas() {Nombre="Pergamino", Image="pergamino_indescifrable.png", ID=55},
            new Cartas() {Nombre="Perversos", Image="perversos_sucubos.png", ID=56},
            new Cartas() {Nombre="Placido_Rio", Image="placido_rio.png", ID=57},
            new Cartas() {Nombre="Pocion", Image="pocion_misteriosa.png", ID=58},
            new Cartas() {Nombre="Pozo", Image="pozo.png", ID=59},
            new Cartas() {Nombre="Pueblo", Image="pueblo_feliz.png", ID=60},
            new Cartas() {Nombre="Puente", Image="puente_sobre_rio.png", ID=61},
            new Cartas() {Nombre="Puerta", Image="puerta.png", ID=62},
            new Cartas() {Nombre="Puntillas", Image="puntillas.png", ID=63},
            new Cartas() {Nombre="Rayo", Image="rayo.png", ID=64},
            new Cartas() {Nombre="Reina_Momia", Image="reina_momia.png", ID=65},
            new Cartas() {Nombre="Sirviente", Image="sirviente_rastrero.png", ID=66},
            new Cartas() {Nombre="Sol", Image="sol.png", ID=67},
            new Cartas() {Nombre="Taberna", Image="taberna.png", ID=68},
            new Cartas() {Nombre="Talisman", Image="talisman.png", ID=69},
            new Cartas() {Nombre="Templo", Image="templo_ruinas.png", ID=70},
            new Cartas() {Nombre="Tomo", Image="tomo_ululante.png", ID=71},
            new Cartas() {Nombre="Torre", Image="torre_solitaria.png", ID=72},
            new Cartas() {Nombre="Tropa", Image="tropa_prescindible.png", ID=73},
            new Cartas() {Nombre="Ventana", Image="ventana_al_mundo.png", ID=74},
            new Cartas() {Nombre="Viento", Image="viento.png", ID=75},
            new Cartas() {Nombre="Yelmo", Image="yelmo_con_cuernos.png", ID=76},
            new Cartas() {Nombre="Zapatos", Image="zapatos_de_moda.png", ID=77},
            new Cartas() {Nombre="Zombis", Image="zombis.png", ID=78},
        };
        #endregion

        #region cartasAccion
        List<Cartas> barajaAccion = new List<Cartas>()
        {
            new Cartas() {Nombre="Manos1_1", Image="manos_1_1.png", ID=79},
            new Cartas() {Nombre="Manos1_2", Image="manos_1_2.png", ID=80},
            new Cartas() {Nombre="Manos1_3", Image="manos_1_3.png", ID=81},
            new Cartas() {Nombre="Manos1_4", Image="manos_1_4.png", ID=82},
            new Cartas() {Nombre="Manos1_5", Image="manos_1_5.png", ID=83},
            new Cartas() {Nombre="Manos1_6", Image="manos_1_6.png", ID=84},
            new Cartas() {Nombre="Manos1_7", Image="manos_1_7.png", ID=85},
            new Cartas() {Nombre="Manos1_8", Image="manos_1_8.png", ID=86},
            new Cartas() {Nombre="Manos1_9", Image="manos_1_9.png", ID=87},
            new Cartas() {Nombre="Manos1_10", Image="manos_1_10.png", ID=88},
            new Cartas() {Nombre="Manos1_11", Image="manos_1_11.png", ID=89},
            new Cartas() {Nombre="Manos1_12", Image="manos_1_12.png", ID=90},
            new Cartas() {Nombre="Manos2_1", Image="manos_2_1.png", ID=91},
            new Cartas() {Nombre="Manos2_2", Image="manos_2_2.png",ID=92},
            new Cartas() {Nombre="Manos2_3", Image="manos_2_3.png", ID=93},
            new Cartas() {Nombre="Manos2_4", Image="manos_2_4.png", ID=94},
            new Cartas() {Nombre="Manos2_5", Image="manos_2_5.png", ID=95},
            new Cartas() {Nombre="Manos2_6", Image="manos_2_6.png", ID=96},
            new Cartas() {Nombre="Manos2_7", Image="manos_2_7.png", ID=97},
            new Cartas() {Nombre="Manos2_8", Image="manos_2_8.png", ID=98},
            new Cartas() {Nombre="Manos2_9", Image="manos_2_9.png", ID=99},
            new Cartas() {Nombre="Manos2_10", Image="manos_2_10.png", ID=100},
            new Cartas() {Nombre="Manos2_11", Image="manos_2_11.png", ID=101},
            new Cartas() {Nombre="Manos2_12", Image="manos_2_12.png", ID=102},
            new Cartas() {Nombre="Manos3_1", Image="manos_3_1.png", ID=103},
            new Cartas() {Nombre="Manos3_2", Image="manos_3_2.png", ID=104},
            new Cartas() {Nombre="Manos3_3", Image="manos_3_3.png", ID=105},
            new Cartas() {Nombre="Manos3_4", Image="manos_3_4.png", ID=104},
            new Cartas() {Nombre="Manos3_5", Image="manos_3_5.png", ID=105},
            new Cartas() {Nombre="Manos3_6", Image="manos_3_6.png", ID=106},
            new Cartas() {Nombre="Manos3_7", Image="manos_3_7.png", ID=107},
            new Cartas() {Nombre="Manos3_8", Image="manos_3_8.png", ID=108},
            new Cartas() {Nombre="Manos3_9", Image="manos_3_9.png", ID=109},
            new Cartas() {Nombre="Manos3_10", Image="manos_3_10.png", ID=110},
            new Cartas() {Nombre="Manos3_11", Image="manos_3_11.png", ID=111},
            new Cartas() {Nombre="Manos3_12", Image="manos_3_12.png", ID=112},
        };
        #endregion

        public Lacayos(int nForm, Socket server, int lacayo)
        {
            InitializeComponent();
            this.nForm = nForm;
            this.server = server;
            this.lacayo = lacayo;
        }
        #region cartasMiradas
        List<Cartas> barajaMiradas = new List<Cartas>()
        {
            new Cartas() {Nombre="Señor1", Image="señor_1.png"},
            new Cartas() {Nombre="Señor2", Image="señor_2.png"},
            new Cartas() {Nombre="Señor3", Image="señor_3.png"},
        };
        #endregion

        private void MuestraImagen(PictureBox picturebox, string imagen)
        {
            picturebox.Image = Image.FromFile(imagen);
            picturebox.SizeMode = PictureBoxSizeMode.StretchImage;
            picturebox.ImageLocation = imagen;
        }
      


        private void Lacayos_Load(object sender, EventArgs e)
        {
            info.Text = "lacayo " + lacayo;

            Baraja.Visible = false; //Al principio no hay cartas en la baraja de descarte


            var random = new Random();
            int value;
            string carta1 = barajaexcusa[random.Next(0, barajaexcusa.Count - 1)].Image;
            string carta2 = barajaexcusa[random.Next(0, barajaexcusa.Count - 1)].Image;
            string carta3 = barajaexcusa[random.Next(0, barajaexcusa.Count - 1)].Image;

            MuestraImagen(Excusa1, carta1);
            MuestraImagen(Excusa2, carta2 );
            MuestraImagen(Excusa3, carta3);


            value = random.Next(0, barajaAccion.Count - 1);
            carta1 = barajaAccion[value].Image;
            value = random.Next(0, barajaAccion.Count - 1);
            carta2 = barajaAccion[value].Image;
            value = random.Next(0, barajaAccion.Count - 1);
            carta3 = barajaAccion[value].Image;

            MuestraImagen(Accion1, carta1);
            MuestraImagen(Accion2, carta2);
            MuestraImagen(Accion3, carta3);


            MuestraImagen(Jugador_2, "reverso.png");
            MuestraImagen(Jugador_1, "reverso.png");
            MuestraImagen(Accion, "reverso.png");
            MuestraImagen(Excusas, "reverso.png");

        }

        private void TirarAlMedio_Excusas(PictureBox picturebox)
        {
            if (Baraja.Visible == false) //Si no se ha tirado ninguna carta primero se hace visible 
            {
                Baraja.Visible = true;
            }
            MuestraImagen(Baraja, picturebox.ImageLocation);
            string mensaje = "13/" + Convert.ToString(nForm)  + "/" + picturebox.ImageLocation; //Enviamos el numero de la carta
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

        }

        private void TirarAlMedio_Accion(PictureBox picturebox)
        {
            if (Baraja.Visible == false) //Si no se ha tirado ninguna carta primero se hace visible 
            {
                Baraja.Visible = true;
            }
            MuestraImagen(Baraja, picturebox.ImageLocation);
            string mensaje = "13/" + Convert.ToString(nForm)  + "/" + picturebox.ImageLocation; //Enviamos el numero de la carta
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void Accion3_Click(object sender, EventArgs e)
        {
            TirarAlMedio_Accion(Accion3);
            string[] nombre = Accion3.ImageLocation.Split('_');
            if (nombre[1] == "3")
            {
                excusa = 3;
            }
            Accion3.Visible = false;
            CambiarTurno(excusa);
        }

        private void Accion2_Click(object sender, EventArgs e)
        {
            TirarAlMedio_Accion(Accion2);
            string[] nombre = Accion2.ImageLocation.Split('_');
            if (nombre[1] == "3")
            {
                excusa = 3;
            }
            Accion2.Visible = false;
            CambiarTurno(excusa);
        }

        private void Accion1_Click(object sender, EventArgs e)
        {
            TirarAlMedio_Accion(Accion1);
            string[] nombre = Accion1.ImageLocation.Split('_');
            if (nombre[1] == "3")
            {
                excusa = 3;
            }
            Accion1.Visible = false;
            CambiarTurno(excusa);
        }

        private void Excusa3_Click(object sender, EventArgs e)
        {
            TirarAlMedio_Excusas(Excusa3);
            excusa = excusa + 1;
            Excusa3.Visible = false;
            CambiarTurno(excusa);
        }

        private void Excusa2_Click(object sender, EventArgs e)
        {
            TirarAlMedio_Excusas(Excusa2);
            excusa = excusa + 1;
            Excusa2.Visible = false;
            CambiarTurno(excusa);
        }

        private void Excusa1_Click(object sender, EventArgs e)
        {
            TirarAlMedio_Excusas(Excusa1);
            excusa = excusa + 1;
            Excusa1.Visible = false;
            CambiarTurno(excusa);
        }

        private void Accion_Click(object sender, EventArgs e)
        {
            var random = new Random();
            string carta = barajaAccion[random.Next(0, barajaAccion.Count - 1)].Image;
            Console.WriteLine("Carta: " + carta);
            if (Accion1.Visible == false)
            {
                Accion1.Visible = true;
                MuestraImagen(Accion1, carta);
            }
            else if (Accion2.Visible == false)
            {
                Accion2.Visible = true;
                MuestraImagen(Accion2, carta );
            }
            else if (Accion3.Visible == false)
            {
                Accion3.Visible = true;
                MuestraImagen(Accion3, carta );
            }
            else
            {
                MessageBox.Show("Ya tienes todas las cartas de acción en tu mano");
            }
        }

        private void Excusas_Click(object sender, EventArgs e)
        {
            var random = new Random();
            string carta = barajaexcusa[random.Next(0, barajaexcusa.Count - 1)].Image;
            Console.WriteLine("Carta: " + carta);
            if (Excusa1.Visible == false)
            {
                Excusa1.Visible = true;
                MuestraImagen(Excusa1, carta);
            }
            else if (Excusa2.Visible == false)
            {
                Excusa2.Visible = true;
                MuestraImagen(Excusa2, carta);
            }
            else if (Excusa3.Visible == false)
            {
                Excusa3.Visible = true;
                MuestraImagen(Excusa3, carta );
            }
            else
            {
                MessageBox.Show("Ya tienes todas las cartas de excusa en tu mano");
            }
        }

        private void CambiarTurno(int excusa)
        {
            if(excusa == 3) //Se cambiará el turno
            {
                string mensaje = "10/" + Convert.ToString(nForm); //Enviamos al servidor el cambio de turno
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                excusa = 0; //Volvemos a empezar

            }
        }

        delegate void MiradasLabel(string numero);

        public void Miradas_Label(string numero)
        {
            miradas.Text = numero;
        }

        public void PonMiradas(string numero)
        {
            MiradasLabel label = new MiradasLabel(Miradas_Label);
            miradas.Invoke(label, new object[] { numero });
            MessageBox.Show("Te han enviado una mirada fulminante");
        }

        public void MostrarCambioTurno()
        {
            MessageBox.Show("Se ha cambiado el turno");
        }

        public void FinalizarPartida()
        {
            MessageBox.Show("Se ha acabado la partida");
            Close();
        }

        public void PasaCarta(string Imagen)
        {
            MuestraImagen(Baraja, Imagen);
        }
    }
}
