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
        int conectado = 0;  //Estamos desconectados

        private void IniciarSesion_Click(object sender, EventArgs e)
        {
            if (conectado == 0)
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
                    MessageBox.Show("No se ha podido conectar con el servidor");
                    return;

                }
                conectado = 1;
            }

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
                Console.WriteLine(mensaje);
                //Ahora si la respuesta es sí o 0 podrá entrar en el juego (nuevo formulario)
                string[] respuesta = mensaje.Split('/');

                //Si el nombre de usuario o la contraseña no son correctos el usuario no podrá entrar en el juego
                //Desde el servidor se nos dice si los datos son correctos o no con el mensaje
                Console.WriteLine(respuesta[1]);
                if (respuesta[0] == "Si")
                {
                    MessageBox.Show("Se ha iniciado sesión correctamente");
                    panel2.Visible = true; //Hcemos que aparezca el panel de las consultas
                    panel1.Visible = false; //Hacemos que se vaya el panel con el inicio de sesión y el registro
                    ListaConectados.Visible = true;
                    //ListaConectados.AutoSize = true;
                    ListaConectados.RowCount = (respuesta.Length)-1; //El primer número del mensaje nos indica cuántos jugadores hay en la lista
                    ListaConectados.ColumnCount = 1;
                    ListaConectados.Columns[0].HeaderText = "Nombre del jugador";
                    int j = 0;

                    for (int i = 1; i < respuesta.Length; i++)
                    {
                        ListaConectados.Rows[j].Cells[0].Value = respuesta[i];
                        j = j + 1;

                    }
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

        private void Registrarse_Click(object sender, EventArgs e)
        {
            if (conectado == 0)
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
                    MessageBox.Show("No se ha podido conectar con el servidor");
                    return;

                }
                conectado = 1;
            }
            //Ahora nos registramos
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
                    MessageBox.Show("Has sido registrado correctamente. ¡Ya puedes iniciar sesión!");


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

                    //Haremos una tabla con el mensaje pasado por el servidor
                    Puntuaciones.Visible = true;
                    string[] puntuaciones = mensaje.Split(',');
                    Puntuaciones.RowCount = puntuaciones.Length; //Suponiendo que al principio se nos dice cuantos jugadores hay
                    Puntuaciones.ColumnCount = 2;
                    Puntuaciones.Columns[0].HeaderText = "Nombre del jugador";
                    Puntuaciones.Columns[1].HeaderText = "Partidas Ganadas";

                    int i;
                    int j = 0;
                    for (i = 0; i< puntuaciones.Length; i++)
                    {
                        Puntuaciones.Rows[i].Cells[0].Value = puntuaciones[j];
                        j = j + 1;
                        Puntuaciones.Rows[i].Cells[1].Value = puntuaciones[j];
                        j = j + 1;
                    }
                   
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

        

        private void ListaConectados_button_Click(object sender, EventArgs e)
        {
            string mensaje = "6/"; //Codigo para ver la lista de conectados
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);

            byte[] msg2 = new byte[80]; 
            server.Receive(msg2);
            mensaje = Encoding.ASCII.GetString(msg2).Split('\0')[0];

            //Se deberá usar un DataGridView para ver la lista de conectados
            ListaConectados.Visible = true;
            string[] jugadores = mensaje.Split(','); //Dividimos el mensaje por las comas
            ListaConectados.RowCount = jugadores.Length; //El primer número del mensaje nos indica cuántos jugadores hay en la lista
            ListaConectados.ColumnCount = 1;
            ListaConectados.Columns[0].HeaderText = "Nombre del jugador";

            for (int i =0; i<mensaje.Length; i++)
            {
                ListaConectados.Rows[i].Cells[0].Value = jugadores[i];
            }
        }
    }
}
