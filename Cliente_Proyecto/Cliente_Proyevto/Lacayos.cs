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
    public partial class Lacayos : Form
    {
        private int lacayo;

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

        public Lacayos(int i)
        {
            InitializeComponent();
            lacayo = i;


            var random = new Random();
            int value = 15;
            string carta1 = cartas_excusa[random.Next(0, cartas_excusa.Length - 1)];
            value = 15;
            string carta2 = cartas_excusa[random.Next(0, cartas_excusa.Length - 1)];
            value = 15;
            string carta3 = cartas_excusa[random.Next(0, cartas_excusa.Length - 1)];
            Excusa1.Image = Image.FromFile( carta1 + ".png");
            Excusa2.Image = Image.FromFile( carta2 + ".png");
            Excusa3.Image = Image.FromFile(carta3 + ".png");
            Excusa1.SizeMode = PictureBoxSizeMode.StretchImage;
            Excusa2.SizeMode = PictureBoxSizeMode.StretchImage;
            Excusa3.SizeMode = PictureBoxSizeMode.StretchImage;

            value = random.Next(0, cartas_accion.Length - 1);
            carta1 = cartas_accion[value];
            value = random.Next(0, cartas_accion.Length - 1);
            carta2 = cartas_accion[value];
            value = random.Next(0, cartas_accion.Length - 1);
            carta3 = cartas_accion[value];

            Accion1.Image = Image.FromFile(carta1 + ".png");
            Accion2.Image = Image.FromFile(carta2 + ".png");
            Accion3.Image =Image.FromFile(carta3 + ".png");
            Accion1.SizeMode = PictureBoxSizeMode.StretchImage;
            Accion2.SizeMode = PictureBoxSizeMode.StretchImage;
            Accion3.SizeMode = PictureBoxSizeMode.StretchImage;

            MuestraReverso(Jugador_2);
            MuestraReverso(Jugador_1);
            MuestraReverso(Accion);
            MuestraReverso(Excusas);
            
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
            picturebox.Image = Image.FromFile("reverso.png");
            picturebox.SizeMode = PictureBoxSizeMode.StretchImage;
        }
      


        private void Lacayos_Load(object sender, EventArgs e)
        {
            info.Text = "lacayo " + lacayo;
        }

    }
}
