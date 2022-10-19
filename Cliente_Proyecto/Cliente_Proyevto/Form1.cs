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

namespace Cliente_Proyevto
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }
        Socket server;

        private void button1_Click(object sender, EventArgs e)
        {


            try
            {


                //En este caso el mensaje será 1/Nombre de usuario/Contraseña
                //Así es como el cliente enviará los datos al servidor para que los procese
                string mensaje = "1/" + Usuario.Text + "/" + Password.Text + "/";
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Ahora recibe la respuesta de que el usuario se encuentra en la base de datos y entonces puede entrar al juego
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];
                //Ahora si la respuesta es sí o 0 podrá entrar en el juego (nuevo formulario)

                //Si el nombre de usuario o la contraseña no son correctos el usuario no podrá entrar en el juego
                //Desde el servidor se nos dice si los datos son correctos o no con el mensaje
                if (mensaje == "Ok")
                {
                    MessageBox.Show("Se ha iniciado sesión correctamente");
                }
                else
                {
                    MessageBox.Show(mensaje);
                }

            }
            catch (FormatException)
            {
                MessageBox.Show("Los datos introducidos no son correctos");
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                //Creamos el mensaje que enviaremos al servidor que tendrá la forma 2/Usuario/Contraseña/Correo
                string mensaje = "2/" + Usuario.Text + "/" + Password.Text + "/" + Correo.Text;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);

                //Una vez recibimos el mensaje
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                mensaje = Encoding.ASCII.GetString(msg2);

                if (mensaje == "Si")
                {
                    MessageBox.Show("Has sido registrado correctamente.");
                    Close();
                }
                else
                {
                    MessageBox.Show("No se ha podido registrar correctamente");
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Los datos introducidos no son correctos");
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                if (Query1.Checked)
                {
                    //Queremos ver el número de partidas jugadas por un jugador
                    //El usuario pone el nombre del jugador en la tabla de texto
                    string mensaje = "3/" + Nombre.Text;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    byte[] msg2 = new byte[80];
                    server.Receive(msg2);
                    mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

                    MessageBox.Show("El jugador " + Nombre.Text + " ha jugado " + mensaje + " partidas.");
                }
                else if (Query2.Checked)
                {
                    //Ahora queremos saber cuántas partidas ha ganado un determinado jugador
                    //Para ello el usuario escribirá el nombre de la persona que quiere saber las partidas ganadas en el textbox
                    string mensaje = "4/" + Nombre.Text;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    byte[] msg2 = new byte[80];
                    server.Receive(msg2);
                    mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

                    MessageBox.Show(mensaje);
                }
                else
                {
                    //Finalmente queremos que nos aparezca la tabla con el nombre del jugador y su puntuación
                    string mensaje = "5/";
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                    byte[] msg2 = new byte[80];
                    server.Receive(msg2);
                    mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

                    //Ahora debo saber que me va a devolver el servidor para luego poder mostrar la tabla con las puntuaciones
                }
            }
            catch (FormatException)
            {
                MessageBox.Show("Los datos introducidos no son correctos");
                return;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string mensaje = "0/";
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            server.Shutdown(SocketShutdown.Both);
            server.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                IPAddress direc = IPAddress.Parse("192.168.56.101");
                IPEndPoint ipep = new IPEndPoint(direc, 9080);

                //Creamos el socket
                server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); ;
                server.Connect(ipep); //Nos connectamos con el servidor
            }
            catch (SocketException)
            {
                MessageBox.Show("No se ha podido connectar con el servidor");
                return;

            }
        }
    }
}
