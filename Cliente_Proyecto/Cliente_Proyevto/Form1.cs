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


namespace Cliente_Proyevto
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false; //Que permita al thread acceder al control de los threads del formulario
        }

        Socket server;
        int conectado = 0;  //Estamos desconectados
        Thread atender;

        private void AtenderServidor()
        {
            while (true) //bucle infinito
            {
                //Recibimos el mensaje del servidor
                byte[] msg2 = new byte[80];
                server.Receive(msg2);
                //Partimos por la barra para saber que servicio es
                string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                string mensaje = trozos[0].Split('\0')[0]; //Declaramos el mensaje recibido por el servidor
                Console.WriteLine(mensaje);
                int codigo = Convert.ToInt32(mensaje); //Donde tenemos el codigo del mensaje

                int i, j;
                string respuesta;

                switch (codigo)
                {
                    case 1: //Inicio de sesión
                        respuesta = trozos[1].Split('\0')[0];
                        
                        if (respuesta == "Si")
                        {
                            MessageBox.Show("Has iniciado sesión correctamente");
                            panel2.Visible = true; //Hcemos que aparezca el panel de las consultas
                            panel1.Visible = false; //Hacemos que se vaya el panel con el inicio de sesión y el registro
                            ListaConectados.Visible = true;
                            //ListaConectados.AutoSize = true;
                            ListaConectados.RowCount = (trozos.Length) - 1; //El primer número del mensaje nos indica cuántos jugadores hay en la lista
                            ListaConectados.ColumnCount = 1;
                            ListaConectados.Columns[0].HeaderText = "Nombre del jugador";
                        }
                        else
                        {
                            MessageBox.Show(respuesta);
                        }
                        break;
                    case 2: //Registro
                        respuesta = trozos[1].Split('\0')[0];
                        if (respuesta == "Si")
                        {
                            MessageBox.Show("Has sido registrado correctamente. ¡Ya puedes iniciar sesión!");
                        }
                        else
                        {
                            MessageBox.Show("No se ha podido registrar correctamente");
                        }
                        break;
                    case 3: //Partidas jugadas por un jugador
                        respuesta = trozos[1].Split('\0')[0];
                        MessageBox.Show("Ha jugado " + respuesta + " partidas.");
                        break;
                    case 4: //Partidas ganadas por un jugador
                        respuesta = trozos[1].Split('\0')[0];
                        MessageBox.Show(respuesta);
                        break;
                    case 5: //Tabla de jugadores con sus partidas ganadas
                        
                        //Haremos una tabla con el mensaje pasado por el servidor
                        Puntuaciones.Visible = true;
                        //string[] puntuaciones = mensaje.Split(',');
                        Puntuaciones.RowCount = (trozos.Length)-1; //Suponiendo que al principio se nos dice cuantos jugadores hay
                        Puntuaciones.ColumnCount = 2;
                        Puntuaciones.Columns[0].HeaderText = "Nombre del jugador";
                        Puntuaciones.Columns[1].HeaderText = "Partidas Ganadas";

                        j = 0;
                        for (i = 1; i < trozos.Length; i++)
                        {
                            respuesta = trozos[j].Split('\0')[0];
                            Puntuaciones.Rows[i].Cells[0].Value = respuesta;
                            Console.WriteLine(respuesta);
                            j = j + 1;
                            respuesta = trozos[j].Split('\0')[0];
                            Puntuaciones.Rows[i].Cells[1].Value = respuesta;
                            Console.WriteLine(respuesta);
                            j = j + 1;
                        }
                        break;
                    case 6: //La notificación. Siempre que hay un cambio se nos enviará la notificación

                        j = 0;

                        for (i = 1; i < trozos.Length; i++)
                        {
                            respuesta = trozos[i].Split('\0')[0];
                            ListaConectados.Rows[j].Cells[0].Value = respuesta;
                            j = j + 1;

                        }
                        MessageBox.Show("Bienvenido");
                        break;
                }
            }
        }

        private void IniciarSesion_Click(object sender, EventArgs e)
        {
            if (conectado == 0)
            {
                IPAddress direc = IPAddress.Parse("192.168.56.101");
                IPEndPoint ipep = new IPEndPoint(direc, 9090);

                try
                {
                    //Creamos el socket
                    server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); 
                    server.Connect(ipep); //Nos connectamos con el servidor

                    //Ponemos en marcha el thread que atenderá al servidor
                    ThreadStart ts = delegate { AtenderServidor(); };
                    atender = new Thread(ts);
                    atender.Start();
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
                IPAddress direc = IPAddress.Parse("192.168.56.101");
                IPEndPoint ipep = new IPEndPoint(direc, 9090);
                try
                {
                    //Creamos el socket
                    server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp); ;
                    server.Connect(ipep); //Nos connectamos con el servidor

                    //Ponemos en marcha el thread que atenderá al servidor
                    ThreadStart ts = delegate { AtenderServidor(); };
                    atender = new Thread(ts);
                    atender.Start();
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


                }
                else if (Query2.Checked)
                {
                    //Ahora queremos saber cuántas partidas ha ganado un determinado jugador
                    //Para ello el usuario escribirá el nombre de la persona que quiere saber las partidas ganadas en el textbox
                    string mensaje = "4/" + Nombre.Text;
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                }
                else
                {
                    //Finalmente queremos que nos aparezca la tabla con el nombre del jugador y las partidas ganadas
                    string mensaje = "5/";
                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                    server.Send(msg);

                       
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

            //Nos desconectamos
            atender.Abort();
            server.Shutdown(SocketShutdown.Both);
            server.Close();

        }

        

        
    }
}
