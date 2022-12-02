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

        public string[] cartas_excusa =
     {
            "antiquismo_lugar",
            "arbol_milenario",
            "artefacto_imposible",
            "barco_volador",
            "bosque_oscuro",
            "caballero_negro",
            "cachorrito",
            "camino",
            "cofre_cerrado",
            "comerciante_tramposo",
            "desolado_desierto",
            "dragon",
            "escudo_redondo",
            "espantapajaros",
            "fantasma_aterrador",
            "gigante_de_piedra",
            "goblin_lerdo",
            "guardian_diminuto",
            "guerrero_sin_seso",
            "hechicero_este",
            "hombre_viejo_rapaz",
            "lugar_equivocado",
            "mapa",
            "mar",
            "mosntruo_gloton",
            "monstruo_mas_monstruo",
            "murcielago",
            "muro_insuperable",
            "otro_señor_oscuro",
            "pantano",
            "pergamino_indescifrable",
            "placido_rio",
            "pozo",
            "pueblo_feliz",
            "puente_sobre_rio",
            "rayo",
            "sirviento_rastrero",
            "sol",
            "talisman",
            "templo_en_ruinas",
            "tomo_ululante",
            "viento",
            "yelmo_con_cuernos",
            "antorcha",
            "bella_exotica",
            "bola_cristal",
            "cadena_pesada",
            "calavera",
            "capa_archimago",
            "cerveza_enana",
            "ciudad_flotante",
            "damisela_peligro",
            "elfa_siniestra",
            "escalera",
            "espada_inteligente",
            "espada_soluble_agua",
            "hierba_sueño",
            "isla_salvaje",
            "llave",
            "mansion_oscura",
            "mazmorra_oscura",
            "mina",
            "momia",
            "nieve_caia",
            "pocion_misteriosa",
            "puerta",
            "reina_momia",
            "taberna",
            "torre_solitaria",
            "tropa_prescindible",
            "objetos_sorprendentes",
            "perversos_sucubos",
            "zapatos_moda",
             "oro",
            "puntillas",
            "señor_1",
            "señor_2",
            "señor_3",
            "millar_tentaculos",
            "ventana_mundo",
            "zombies"
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

        public Señor_Oscuro()
        {
            InitializeComponent();


            var random = new Random();
            int value = 15;
            string carta1 = cartas_excusa[random.Next(0, cartas_excusa.Length - 1)];
            value = 15;
            string carta2 = cartas_excusa[random.Next(0, cartas_excusa.Length - 1)];
            value = 15;
            string carta3 = cartas_excusa[random.Next(0, cartas_excusa.Length - 1)];
            Excusa1.ImageLocation = @"..\..\" + carta1 + ".png";
            Excusa2.ImageLocation = @"..\..\" + carta2 + ".png";
            Excusa3.ImageLocation = @"..\..\" + carta3 + ".png";
            Excusa1.SizeMode = PictureBoxSizeMode.StretchImage;
            Excusa2.SizeMode = PictureBoxSizeMode.StretchImage;
            Excusa3.SizeMode = PictureBoxSizeMode.StretchImage;

            value = random.Next(0, cartas_accion.Length - 1);
            carta1 = cartas_accion[value];
            value = random.Next(0, cartas_accion.Length - 1);
            carta2 = cartas_accion[value];
            value = random.Next(0, cartas_accion.Length - 1);
            carta3 = cartas_accion[value];

            Accion1.ImageLocation = @"..\..\" + carta1 + ".png";
            Accion2.ImageLocation = @"..\..\" + carta2 + ".png";
            Accion3.ImageLocation = @"..\..\" + carta3 + ".png";
            Accion1.SizeMode = PictureBoxSizeMode.StretchImage;
            Accion2.SizeMode = PictureBoxSizeMode.StretchImage;
            Accion3.SizeMode = PictureBoxSizeMode.StretchImage;
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
