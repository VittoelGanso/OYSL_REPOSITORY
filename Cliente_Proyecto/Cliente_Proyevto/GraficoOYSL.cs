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
    public partial class GraficoOYSL : Form
    {
        int nForm;
        Socket server;
        string nombreuser;



        public GraficoOYSL(int nForm, Socket server, string nombreuser)
        {
            InitializeComponent();
            this.nForm = nForm;
            this.server = server;
            this.nombreuser = nombreuser;

        }

        //List<Cartas> BarajaJugador1 = new List<Cartas>()
        //{
        //    new Cartas() {Nombre = "null", Image = "null"}
        //};

        //List<Cartas> BarajaJugador2 = new List<Cartas>()
        //{
        //    new Cartas() {Nombre = "null", Image = "null"}
        //};

        //List<Cartas> BarajaJugador3 = new List<Cartas>()
        //{
        //    new Cartas() {Nombre="null", Image="null"}
        //};

        //Random random = new Random();
        //List<int> cartasUsadas = new List<int>();
        //List<PictureBox> Jugador1Box = new List<PictureBox>();
        //List<PictureBox> Jugador2Box = new List<PictureBox>();
        //List<PictureBox> Jugador3Box = new List<PictureBox>();

        ////El juego tiene dos barajas, una de cartas de excusa y otras de cartas de accion
        ////Creamos las dos barajas y una baraja más con las miradas asesinas del señor oscuro
        //#region cartasExcusa
       
        //List<Cartas> barajaexcusa = new List<Cartas>()
        //{

        //    new Cartas() {Nombre = "Antiguismo_Lugar", Image = "antiquismo_lugar.png"},
        //    new Cartas() {Nombre = "Antorcha", Image = "antorcha.png"},
        //    new Cartas() {Nombre = "Arbol_Milenario", Image = "arbol_milenario.png"},
        //    new Cartas() {Nombre = "Artefacto_imposible", Image = "artefacto_imposible.png"},
        //    new Cartas() {Nombre = "Barco_Volador", Image = "barco_volador"},
        //    new Cartas() {Nombre = "Bella_exotica", Image = "bella_exotica"},
        //    new Cartas() {Nombre = "Bola_de_Cristal", Image="bola_de_cristal.png"},
        //    new Cartas() {Nombre="Bosque_oscuro", Image="bosque_oscuro.png"},
        //    new Cartas() {Nombre = "Caballero_negro", Image="caballero_negro.png"},
        //    new Cartas() {Nombre = "Cachorrito", Image="cahorrito.png"},
        //    new Cartas() {Nombre="Cadena_pesada", Image = "cadena_pesada.png"},
        //    new Cartas() {Nombre = "Calavera", Image = "calavera.png"},
        //    new Cartas() {Nombre = "Camino", Image = "camino.png"},
        //    new Cartas() {Nombre = "Capa_Archimago", Image="capa_del_archimago.png"},
        //    new Cartas() {Nombre="Cerveza_Enana", Image="cerveza_enana.png"},
        //    new Cartas() {Nombre="Ciudad_Flotante", Image ="ciudad_flotante.png"},
        //    new Cartas() {Nombre="Cofre_Cerrado", Image="cofre_cerrado.png"},
        //    new Cartas() {Nombre="Comerciante_Tramposo", Image="comerciante_tramposo.png"},
        //    new Cartas() {Nombre="Damisela_Peligro", Image="damisela_en_peligro.png"},
        //    new Cartas() {Nombre="Desolado_Desierto", Image="desolado_desierto.png"},
        //    new Cartas() {Nombre="Dragon", Image="dragon.png"},
        //    new Cartas() {Nombre="Elfa_Siniestra", Image="elfa_siniestra.png"},
        //    new Cartas() {Nombre="Escalera", Image="escalera.png"},
        //    new Cartas() {Nombre="Escudo_Redondo", Image="escudo_redondo.png"},
        //    new Cartas() {Nombre="EspadaInteligente", Image="espada_inteligente.png"},
        //    new Cartas() {Nombre="Espada_Soluble_Agua", Image="espada_soluble_en_agua.png"},
        //    new Cartas() {Nombre="Espantapajaros", Image="espantapajaros.png"},
        //    new Cartas() {Nombre="Fantasma_Aterrador", Image="fantasma_aterrador.png"},
        //    new Cartas() {Nombre="Gigante_Piedra", Image="gigante_piedra.png"},
        //    new Cartas() {Nombre="Goblin_lerdo", Image="goblin_lerdo.png"},
        //    new Cartas() {Nombre="Guardian_Diminuto", Image="guardian_diminuto.png"},
        //    new Cartas() {Nombre="Guerrero_Sin_Seso", Image="guerrero_sin_seso.png"},
        //    new Cartas() {Nombre="Hehicero_este", Image="hechicero_este.png"},
        //    new Cartas() {Nombre="Hierba_Sueño", Image="hierba_del_sueño.png"},
        //    new Cartas() {Nombre="Hombre_ViejoRapaz", Image="hombre_viejo_rapaz.png"},
        //    new Cartas() {Nombre="Isla_Salvaje", Image="isla_salvaje.png"},
        //    new Cartas() {Nombre="Llave", Image="llave.png"},
        //    new Cartas() {Nombre="Lugar_Equivocado", Image="lugar_equivocado.png"},
        //    new Cartas() {Nombre="Mansion_Oscura", Image="mansion_oscura.png"},
        //    new Cartas() {Nombre="Mapa", Image="mapa.png"},
        //    new Cartas() {Nombre="Mar", Image="mar.png"},
        //    new Cartas() {Nombre="Mazmorra_Oscura", Image="mazmorra_oscura.png"},
        //    new Cartas() {Nombre="Millon_Tentaculos", Image="millon_de_tentaculos.png"},
        //    new Cartas() {Nombre="Mina", Image="mina.png"},
        //    new Cartas() {Nombre="Momia", Image="momia.png"},
        //    new Cartas() {Nombre="Monstruo_Gloton", Image="monstruo_gloton.png"},
        //    new Cartas() {Nombre="Monstruo_Monstruo", Image="monstruo_mas_monstruo.png"},
        //    new Cartas() {Nombre="Murcielago", Image="murcielago.png"},
        //    new Cartas() {Nombre="Muro_Insuperable", Image="muro_insuperable.png"},
        //    new Cartas() {Nombre="Nieve", Image="nieve_caia.png"},
        //    new Cartas() {Nombre="Objetos", Image="objetos_sorprendentes.png"},
        //    new Cartas() {Nombre="Oro", Image="oro.png"},
        //    new Cartas() {Nombre="Otro_Señor_Oscuro", Image="otro_señor_oscuro.png"},
        //    new Cartas() {Nombre="Pantano", Image="pantano.png"},
        //    new Cartas() {Nombre="Pergamino", Image="pergamino_indescifrable.png"},
        //    new Cartas() {Nombre="Perversos", Image="perversos_sucubos.png"},
        //    new Cartas() {Nombre="Placido_Rio", Image="placido_rio.png"},
        //    new Cartas() {Nombre="Pocion", Image="pocion_misteriosa.png"},
        //    new Cartas() {Nombre="Pozo", Image="pozo.png"},
        //    new Cartas() {Nombre="Pueblo", Image="pueblo_feliz.png"},
        //    new Cartas() {Nombre="Puente", Image="puente_sobre_rio.png"},
        //    new Cartas() {Nombre="Puerta", Image="puerta.png"},
        //    new Cartas() {Nombre="Puntillas", Image="puntillas.png"},
        //    new Cartas() {Nombre="Rayo", Image="rayo.png"},
        //    new Cartas() {Nombre="Reina_Momia", Image="reina_momia.png"},
        //    new Cartas() {Nombre="Sirviente", Image="sirviente_rastrero.png"},
        //    new Cartas() {Nombre="Sol", Image="sol.png"},
        //    new Cartas() {Nombre="Taberna", Image="taberna.png"},
        //    new Cartas() {Nombre="Talisman", Image="talisman.png"},
        //    new Cartas() {Nombre="Templo", Image="templo_ruinas.png"},
        //    new Cartas() {Nombre="Tomo", Image="tomo_ululante.png"},
        //    new Cartas() {Nombre="Torre", Image="torre_solitaria.png"},
        //    new Cartas() {Nombre="Tropa", Image="tropa_prescindible.png"},
        //    new Cartas() {Nombre="Ventana", Image="ventana_al_mundo.png"},
        //    new Cartas() {Nombre="Viento", Image="viento.png"},
        //    new Cartas() {Nombre="Yelmo", Image="yelmo_con_cuernos.png"},
        //    new Cartas() {Nombre="Zapatos", Image="zapatos_de_moda.png"},
        //    new Cartas() {Nombre="Zombis", Image="zombis.png"},
        //};
        //#endregion

        //#region cartasAccion
        //List<Cartas> barajaAccion = new List<Cartas>()
        //{
        //    new Cartas() {Nombre="Manos1_1", Image="manos_1_1.png"},
        //    new Cartas() {Nombre="Manos1_2", Image="manos_1_2.png"},
        //    new Cartas() {Nombre="Manos1_3", Image="manos_1_3.png"},
        //    new Cartas() {Nombre="Manos1_4", Image="manos_1_4.png"},
        //    new Cartas() {Nombre="Manos1_5", Image="manos_1_5.png"},
        //    new Cartas() {Nombre="Manos1_6", Image="manos_1_6.png"},
        //    new Cartas() {Nombre="Manos1_7", Image="manos_1_7.png"},
        //    new Cartas() {Nombre="Manos1_8", Image="manos_1_8.png"},
        //    new Cartas() {Nombre="Manos1_9", Image="manos_1_9.png"},
        //    new Cartas() {Nombre="Manos1_10", Image="manos_1_10.png"},
        //    new Cartas() {Nombre="Manos1_11", Image="manos_1_11.png"},
        //    new Cartas() {Nombre="Manos1_12", Image="manos_1_12.png"},
        //    new Cartas() {Nombre="Manos2_1", Image="manos_2_1.png"},
        //    new Cartas() {Nombre="Manos2_2", Image="manos_2_2.png"},
        //    new Cartas() {Nombre="Manos2_3", Image="manos_2_3.png"},
        //    new Cartas() {Nombre="Manos2_4", Image="manos_2_4.png"},
        //    new Cartas() {Nombre="Manos2_5", Image="manos_2_5.png"},
        //    new Cartas() {Nombre="Manos2_6", Image="manos_2_6.png"},
        //    new Cartas() {Nombre="Manos2_7", Image="manos_2_7.png"},
        //    new Cartas() {Nombre="Manos2_8", Image="manos_2_8.png"},
        //    new Cartas() {Nombre="Manos2_9", Image="manos_2_9.png"},
        //    new Cartas() {Nombre="Manos2_10", Image="manos_2_10.png"},
        //    new Cartas() {Nombre="Manos2_11", Image="manos_2_11.png"},
        //    new Cartas() {Nombre="Manos2_12", Image="manos_2_12.png"},
        //    new Cartas() {Nombre="Manos3_1", Image="manos_3_1.png"},
        //    new Cartas() {Nombre="Manos3_2", Image="manos_3_2.png"},
        //    new Cartas() {Nombre="Manos3_3", Image="manos_3_3.png"},
        //    new Cartas() {Nombre="Manos3_4", Image="manos_3_4.png"},
        //    new Cartas() {Nombre="Manos3_5", Image="manos_3_5.png"},
        //    new Cartas() {Nombre="Manos3_6", Image="manos_3_6.png"},
        //    new Cartas() {Nombre="Manos3_7", Image="manos_3_7.png"},
        //    new Cartas() {Nombre="Manos3_8", Image="manos_3_8.png"},
        //    new Cartas() {Nombre="Manos3_9", Image="manos_3_9.png"},
        //    new Cartas() {Nombre="Manos3_10", Image="manos_3_10.png"},
        //    new Cartas() {Nombre="Manos3_11", Image="manos_3_11.png"},
        //    new Cartas() {Nombre="Manos3_12", Image="manos_3_12.png"},
        //};
        //#endregion

        //#region cartasMiradas
        //List<Cartas> barajaMiradas = new List<Cartas>()
        //{
        //    new Cartas() {Nombre="Señor1", Image="señor_1.png"},
        //    new Cartas() {Nombre="Señor2", Image="señor_2.png"},
        //    new Cartas() {Nombre="Señor3", Image="señor_3.png"},
        //};
        //#endregion

        
        //En el mazo para robar las cartas deben estar boca a bajo
 
        private void jugar_Click(object sender, EventArgs e)
        {

            if (Señor_oscuro.Checked)
            {
                Señor_Oscuro form = new Señor_Oscuro();
                form.ShowDialog();
            }
            else if (Lacayo_1.Checked)
            {
                Lacayos form = new Lacayos(1);
                form.ShowDialog();
            }
            else 
            {
                Lacayos form = new Lacayos(2);
                form.ShowDialog();
            }

        }


        private void Envio_Click(object sender, EventArgs e)
        {


            string mensaje = "8/" + Convert.ToString(nForm)+ "/" + nombreuser + ": " + informacion.Text;
            

            //Enviamos el nombre al servidor 
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void GraficoOYSL_Load(object sender, EventArgs e)
        {
            
        }
        delegate void MensajeChat(string mensaje);

        public void TomaRespuesta(string mensaje)
        {
            MensajeChat p = new MensajeChat(PonMensaje);
            chat.Invoke(p, new object[] { mensaje }); //Escribimos el mensaje en el chat
        }

        public void PonMensaje(string mensaje)
        {
            chat.Items.Add(mensaje);
        }




    }
}
