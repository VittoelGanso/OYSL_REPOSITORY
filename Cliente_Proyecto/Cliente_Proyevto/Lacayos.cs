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

        public string[] cartas_excusa =
        {
            "antiquismo_lugar",
            "antorcha",
            "arbol_milenario",
            "artefacto_imposible",
            "barco_volador",
            "bella_exotica",
            "bola_de_cristal",
            "bosque_oscuro",
            "caballero_negro",
            "cachorrito",
            "cadena_pesada",
            "calavera",
            "camino",
            "capa_del_archimago",
            "cerveza_enana",
            "ciudad_flotante",
            "cofre_cerrado",
            "comerciante_tramposo",
            "desolado_desierto",
            "damisela_peligro",
            "dragon",
            "elfa_siniestra",
            "escalera",
            "escudo_redondo",
            "espada_inteligente",
            "espada_soluble_en_agua",
            "espantapajaros",
            "fantasma_aterrador",
            "gigante_de_piedra",
            "goblin_lerdo",
            "guardian_diminuto",
            "guerrero_sin_seso",
            "hechicero_este",
            "hierba_del_sueño",
            "hombre_viejo_rapaz",
            "isla_salvaje",
            "llave",
            "lugar_equivocado",
            "mansion_oscura",
            "mapa",
            "mar",
            "mazmorra_oscura",
            "millar_de_tentaculos",
            "mina",
            "momia",
            "monstruo_gloton",
            "monstruo_mas_monstruo",
            "murcielago",
            "nieve_caia",
            "objetos_sorprendentes",
            "oro",
            "otro_señor_oscuro",
            "pantano",
            "pergamino_indescifrable",
            "placido_rio",
            "pocion_misteriosa",
            "pozo",
            "pueblo_feliz",
            "puente_sobre_rio",
            "puerta",
            "puntillas",
            "rayo",
            "reina_momia",
            "sirviento_rastrero",
            "sol",
            "taberna",
            "templo_ruinas",
            "torre_solitaria",
            "tropa_perscindible",
            "talisman",
            "tomo_ululante",
            "viento",
            "ventana_al_mundo",
            "yelmo_con_cuernos",
            "zapatos_de_moda",
            "zombis"
            
        };

        public string[] cartas_accion =
        {
            "manos_2_1",
            "manos_2_2",
            "manos_2_3",
            "manos_2_4",
            "manos_2_5",
            "manos_2_6",
            "manos_2_7",
            "manos_2_8",
            "manos_2_9",
            "manos_2_10",
            "manos_2_11",
            "manos_2_12",
            "manos_3_1",
            "manos_3_2",
            "manos_3_3",
            "manos_3_4",
            "manos_3_5",
            "manos_3_6",
            "manos_3_7",
            "manos_3_8",
            "manos_3_9",
            "manos_3_10",
            "manos_3_11",
            "manos_3_12",
            "manos_1_1",
            "manos_1_2",
            "manos_1_3",
            "manos_1_4",
            "manos_1_5",
            "manos_1_6",
            "manos_1_7",
            "manos_1_8",
            "manos_1_9",
            "manos_1_10",
            "manos_1_11",
            "manos_1_12",

        };

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
            string carta1 = cartas_excusa[random.Next(0, cartas_excusa.Length - 1)];
            string carta2 = cartas_excusa[random.Next(0, cartas_excusa.Length - 1)];
            string carta3 = cartas_excusa[random.Next(0, cartas_excusa.Length - 1)];

            MuestraImagen(Excusa1, carta1 + ".png");
            MuestraImagen(Excusa2, carta2 + ".png");
            MuestraImagen(Excusa3, carta3 + ".png");


            value = random.Next(0, cartas_accion.Length - 1);
            carta1 = cartas_accion[value];
            value = random.Next(0, cartas_accion.Length - 1);
            carta2 = cartas_accion[value];
            value = random.Next(0, cartas_accion.Length - 1);
            carta3 = cartas_accion[value];

            MuestraImagen(Accion1, carta1 + ".png");
            MuestraImagen(Accion2, carta2 + ".png");
            MuestraImagen(Accion3, carta3 + ".png");


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

        }

        private void TirarAlMedio_Accion(PictureBox picturebox)
        {
            if (Baraja.Visible == false) //Si no se ha tirado ninguna carta primero se hace visible 
            {
                Baraja.Visible = true;
            }
            MuestraImagen(Baraja, picturebox.ImageLocation);
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
            string carta = cartas_accion[random.Next(0, cartas_accion.Length - 1)];
            Console.WriteLine("Carta: " + carta);
            if (Accion1.Visible == false)
            {
                Accion1.Visible = true;
                MuestraImagen(Accion1, carta + ".png");
            }
            else if (Accion2.Visible == false)
            {
                Accion2.Visible = true;
                MuestraImagen(Accion2, carta + ".png");
            }
            else if (Accion3.Visible == false)
            {
                Accion3.Visible = true;
                MuestraImagen(Accion3, carta + ".png");
            }
            else
            {
                MessageBox.Show("Ya tienes todas las cartas de acción en tu mano");
            }
        }

        private void Excusas_Click(object sender, EventArgs e)
        {
            var random = new Random();
            string carta = cartas_excusa[random.Next(0, cartas_excusa.Length - 1)];
            Console.WriteLine("Carta: " + carta);
            if (Excusa1.Visible == false)
            {
                Excusa1.Visible = true;
                MuestraImagen(Excusa1, carta + ".png");
            }
            else if (Excusa2.Visible == false)
            {
                Excusa2.Visible = true;
                MuestraImagen(Excusa2, carta + ".png");
            }
            else if (Excusa3.Visible == false)
            {
                Excusa3.Visible = true;
                MuestraImagen(Excusa3, carta + ".png");
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
                string mensaje = "10/"; //Enviamos al servidor el cambio de turno
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                excusa = 0; //Volvemos a empezar
            }
        }
    }
}
