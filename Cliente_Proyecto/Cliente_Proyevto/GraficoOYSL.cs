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
        public List<Lacayos> formulario = new List<Lacayos>();
        public List<Señor_Oscuro> f = new List<Señor_Oscuro>();
        delegate void DelegadoCheck(bool ver, CheckBox box);

        //Constructor del formulario
        public GraficoOYSL(int nForm, Socket server, string nombreuser)
        {
            InitializeComponent();
            this.nForm = nForm;
            this.server = server;
            this.nombreuser = nombreuser;
            
        }

        //En el mazo para robar las cartas deben estar boca a bajo

        private void jugar_Click(object sender, EventArgs e)
        {

            if (Señor_oscuro.Checked)
            {
                ThreadStart ts = delegate { PonerEnMarchaSeñor(); };
                Thread T = new Thread(ts);
                T.Start();
                string mensaje = "12/" + Convert.ToString(nForm) +  "/SO/" + nombreuser;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else if (Lacayo_1.Checked)
            {
                ThreadStart ts = delegate { PonerEnMarchaLacayos(1); };
                Thread T = new Thread(ts);
                T.Start();
                string mensaje = "12/"  + Convert.ToString(nForm) + "/lacayo1/" + nombreuser;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else 
            {
                ThreadStart ts = delegate { PonerEnMarchaLacayos(2); };
                Thread T = new Thread(ts);
                T.Start();
                string mensaje = "12/" + Convert.ToString(nForm) + "/lacayo2/" + nombreuser;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }

        }

        //Para enviar mensajes con el chat al servidor
        private void Envio_Click(object sender, EventArgs e)
        {


            string mensaje = "8/" + Convert.ToString(nForm)+ "/" + nombreuser + ": " + informacion.Text;
            

            //Enviamos el nombre al servidor 
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            informacion.Text = "";
        }

        //Que vemos al cargar el formulario
        private void GraficoOYSL_Load(object sender, EventArgs e)
        {
            Boton_Instrucciones.Visible = true;
            Salir.Visible = false;
        }

        delegate void MensajeChat(string mensaje);
        //Escribimos la respuesta que nos llega del servidor en el chat
        public void TomaRespuesta(string mensaje)
        {
            MensajeChat p = new MensajeChat(PonMensaje);
            chat.Invoke(p, new object[] { mensaje }); //Escribimos el mensaje en el chat
        }

        //Escribimos el mensaje que nos llega en el chat
        public void PonMensaje(string mensaje)
        {
            chat.Items.Add(mensaje);
        }

        //Abrimos el formulario de los lacayos
        private void PonerEnMarchaLacayos(int num)
        {
            int cont = formulario.Count;
            Lacayos f = new Lacayos(cont, server, num, nombreuser);
            formulario.Add(f);
            f.ShowDialog();

        }

        //Para abrir el formulario del Señor Oscuro
        private void PonerEnMarchaSeñor()
        {
            int cont = f.Count;
            Señor_Oscuro formulario = new Señor_Oscuro(cont, server, nombreuser);
            f.Add(formulario);
            formulario.ShowDialog();

        }

        //Pasamos la carta a los jugadores para que puedan ver que carta se ha jugado
        public void PasaCarta(int numero, string personaje, string carta)
        {
            if (personaje == "SO")
            {
                f[numero].PonCarta(carta);
            }
            else
            {
                formulario[numero].PasaCarta(carta);
            }
        }

        //Enviamos el cambio de turno a los jugadores
        public void CambioTurno(int numero, int l, string respuesta)
        {
            if (respuesta == "SO")
            {
                f[numero].MostrarCambioTurno(l);
            }
            else
            {
                formulario[numero].MostrarCambioTurno(l);
            }
        }

        //Al finalizar la partida, para enviarlo a los otros formularios
        public void AcabaPartida(int numero, string respuesta, string loser)
        {
            if (respuesta == "SO")
            {
                f[numero].FinalizaPartida(loser);
            }
            else
            {
                formulario[numero].FinalizarPartida(loser);
            }
        }

        //Enviamos al lacayo correspondiente la mirada fulminante
        public void PonMiradas(int numero, string respuesta)
        {
            formulario[numero].PonMiradas(respuesta);
        }

        //Para abrir las instrucciones del juego antes de empezar a jugar
        private void Boton_Instrucciones_Click(object sender, EventArgs e)
        {
            Instrucciones.Visible = true;
            Boton_Instrucciones.Visible = false;
            Salir.Visible = true;
        }

        //Una vez hemos leido las instrucciones las podemos esconder
        private void Salir_Click(object sender, EventArgs e)
        {
            Instrucciones.Visible = false;
            Boton_Instrucciones.Visible = true;
            Salir.Visible = false;
        }

        //Para el delegado de bloquear el personaje escogido
        public void CheckearPersonaje(bool check, CheckBox box)
        {
            box.Enabled = check;
        }

        //Bloqueamos el personaje elegido
        public void BloquearPersonaje(string rol)
        {
            DelegadoCheck del = new DelegadoCheck(CheckearPersonaje);
            if (rol == "lacayo1")
            {
                Lacayo_1.Invoke(del, new object[] { false, Lacayo_1 });
            }
            else if (rol == "lacayo2")
            {
                Lacayo_2.Invoke(del, new object[] { false, Lacayo_2 });
            }
            else
            {
                Lacayo_2.Invoke(del, new object[] { false, Señor_oscuro });
            }
        }
    }
}
