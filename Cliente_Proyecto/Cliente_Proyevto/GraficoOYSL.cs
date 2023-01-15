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
        //public delegate void pasarlacayo(List<Lacayos> lacayos);
        //public delegate void pasarSO(List<Señor_Oscuro> señor);
        //public event pasarlacayo pasado;
        //public event pasarSO pasa;

        public GraficoOYSL(int nForm, Socket server, string nombreuser)
        {
            InitializeComponent();
            this.nForm = nForm;
            this.server = server;
            this.nombreuser = nombreuser;
            
        }

        public GraficoOYSL(int nForm, Socket server, string nombreuser, Form1 parent)
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
                MessageBox.Show(mensaje);
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


        private void Envio_Click(object sender, EventArgs e)
        {


            string mensaje = "8/" + Convert.ToString(nForm)+ "/" + nombreuser + ": " + informacion.Text;
            

            //Enviamos el nombre al servidor 
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        private void GraficoOYSL_Load(object sender, EventArgs e)
        {
            Boton_Instrucciones.Visible = true;
            Salir.Visible = false;
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

        private void PonerEnMarchaLacayos(int num)
        {
            int cont = formulario.Count;
            Lacayos f = new Lacayos(cont, server, num);
            formulario.Add(f);
            ////pasado(formulario);
            ////this.Dispose();
            //Form1 formprincipal = Owner as Form1;
            //formprincipal.formlacayo[cont] = f;
            f.ShowDialog();

        }

        private void PonerEnMarchaSeñor()
        {
            int cont = f.Count;
            Señor_Oscuro formulario = new Señor_Oscuro(cont, server);
            f.Add(formulario);
            ////pasa(f);
            ////this.Dispose();
            //Form1 formprincipal = Owner as Form1;
            //formprincipal.formSeñor[cont] = formulario;
            formulario.ShowDialog();

        }

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

        public void CambioTurno(int numero, string respuesta)
        {
            if (respuesta == "SO")
            {
                f[numero].MostrarCambioTurno();
            }
            else
            {
                formulario[numero].MostrarCambioTurno();
            }
        }


        public void AcabaPartida(int numero, string respuesta, string winner)
        {
            if (respuesta == "SO")
            {
                f[numero].FinalizaPartida(winner);
            }
            else
            {
                formulario[numero].FinalizarPartida(winner);
            }
        }

        public void PonMiradas(int numero, string respuesta)
        {
            formulario[numero].PonMiradas(respuesta);
        }

        private void Boton_Instrucciones_Click(object sender, EventArgs e)
        {
            Instrucciones.Visible = true;
            Boton_Instrucciones.Visible = false;
            Salir.Visible = true;
        }

        private void Salir_Click(object sender, EventArgs e)
        {
            Instrucciones.Visible = false;
            Boton_Instrucciones.Visible = true;
            Salir.Visible = false;
        }

        //public void CheckearPersonaje(bool check, CheckBox box)
        //{
        //    box.Checked = check;
        //}
        ////Bloqueamos el personaje elegido
        //public void BloquearPersonaje(string rol)
        //{
        //    DelegadoCheck del = new DelegadoCheck(CheckearPersonaje);
        //    if (rol == "lacayo1")
        //    {
        //        Lacayo_1.Invoke(del, new object[] { true, Lacayo_1 });
        //    }
        //    else if (rol == "lacayo2")
        //    {
        //        Lacayo_2.Invoke(del, new object[] { true , Lacayo_2});
        //    }
        //    else
        //    {
        //        Lacayo_2.Invoke(del, new object[] { true , Señor_oscuro});
        //    }
        //}
    }
}
