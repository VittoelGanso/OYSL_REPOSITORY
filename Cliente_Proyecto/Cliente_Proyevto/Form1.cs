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
using Graficos_juego_OYSL;



namespace Cliente_Proyevto
{
    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
        }

        Socket server;
        string nombreuser;
        int conectado = 0;  //Estamos desconectados
        string address = "147.83.117.22";
        int gate = 50004;
        Thread atender;
        int numForm;

        List<GraficoOYSL> formulario = new List<GraficoOYSL>();


        delegate void DelegadoParaEscribir(string mensaje, int pos1, int pos2, DataGridView datagrid);
        delegate void DelegadoParaVisualizar(bool visible, Panel p);
        delegate void GridVisible(bool visible, DataGridView datagrid);
        delegate void TamañoDataGrid(int num, string lugar, DataGridView datagrid);
        delegate void Titulo(string titulo, int pos, DataGridView datagrid);
        delegate void BloquearBoton(bool bloq);
        delegate void EscribirLabel(string texto);
        delegate void DejarVerLabel(bool ver);
        delegate void VerBoton(bool ver);


        //Intentar hacer las funciones globales, que sirvan para todos los paneles o datagrids
        public void AñadirFila(string nombre, int pos1, int pos2, DataGridView datagrid)
        {
            datagrid.Rows[pos1].Cells[pos2].Value = nombre;
        }

        public void PanelVisible(bool visible, Panel p)
        {
            p.Visible = visible;
        }

        public void ListaConectadosVisible(bool visible, DataGridView datagrid)
        {
            datagrid.Visible = visible;
        }

        public void ListaConectadosAutoSize(bool visible, DataGridView datagrid)
        {
            ListaConectados.AutoSize = visible;
        }

        public void ListaConectadosRow(int num, string lugar, DataGridView datagrid)
        {
            if (lugar == "c")
            {
                datagrid.ColumnCount = num;
            }
            else
            {
                datagrid.RowCount = num;
            }
 
        }

        public void ListaHeader(string titulo, int pos, DataGridView datagrid)
        {
            datagrid.Columns[pos].HeaderText = titulo;
        }



        private void PonerEnMarchaFormulario(string nombreuser)
        {
            int cont = formulario.Count;
            GraficoOYSL f = new GraficoOYSL(cont, server, nombreuser);
            formulario.Add(f);
            f.ShowDialog();
            
        }

        //Función para bloquear el boton de invitar, ya que solo se puede crear una partida
        private void Bloquear_Boton(bool bloq)
        {
            Invitacion.Enabled = bloq;
        }

        //Funcion para escribir en una label
        private void Escribir_Label(string texto)
        {
            Partidas.Text = texto;
        }

        //Funcion para ver o dejar de ver una label
        private void Ver_Label(bool ver)
        {
            Partidas.Visible = ver;
        }

        //Funcion para ver o dejar de ver un boton
        private void Ver_Boton(bool ver)
        {
            Cerrar.Visible = ver;
        }

        //
        //COMPLEJO DE FUNCIONES PARA ATENDER LAS PETICIONES QUE LLEGAN DEL SERVIDOR.
        //

        private void AtenderServidor()
        {
            int terminar = 0;
            while (terminar == 0) //bucle infinito
            {
                //Recibimos el mensaje del servidor
                byte[] msg2 = new byte[96];
                server.Receive(msg2);

                //Partimos por la barra para saber que servicio es
                string[] trozos = Encoding.ASCII.GetString(msg2).Split('/');
                Console.WriteLine("Numero de trozos: " + Convert.ToString(trozos.Length) + "el trozo es" + Convert.ToString(trozos[0]));
                string mensaje = trozos[0].Split('\0')[0]; //Declaramos el mensaje recibido por el servidor
                Console.WriteLine(mensaje);
                int codigo = Convert.ToInt32(mensaje); //Donde tenemos el codigo del mensaje

                int i, j;
                int numero;
                
                string respuesta;
                DelegadoParaVisualizar delegado = new DelegadoParaVisualizar(PanelVisible);
                GridVisible listacon = new GridVisible(ListaConectadosVisible);
                GridVisible autosize = new GridVisible(ListaConectadosAutoSize);
                TamañoDataGrid tamaño = new TamañoDataGrid(ListaConectadosRow);
                Titulo titulo = new Titulo(ListaHeader);
                DelegadoParaEscribir añadir = new DelegadoParaEscribir(AñadirFila);
                EscribirLabel escribir = new EscribirLabel(Escribir_Label);
                DejarVerLabel ver = new DejarVerLabel(Ver_Label);
                VerBoton boton = new VerBoton(Ver_Boton);
                BloquearBoton bloq = new BloquearBoton(Bloquear_Boton);


                switch (codigo)
                {
                    case 0:
                        conectado = 0;
                        atender.Abort();
                        server.Shutdown(SocketShutdown.Both);
                        server.Close();
                        terminar = 1;
                        break;
                    case 1: //Inicio de sesión
                        respuesta = trozos[1].Split('\0')[0];

                        if (respuesta == "Si")
                        {
                            
                            MessageBox.Show("Has iniciado sesión correctamente");

                            panel1.Invoke(delegado, new object[] { false, panel1 }); //Dejamos de ver el panel 1
                            panel2.Invoke(delegado, new object[] { true, panel2 }); //Visualizamos las querys
                            ListaConectados.Invoke(listacon, new object[] { true, ListaConectados }); //Vemos el datagrid de la lista

                            ListaConectados.Invoke(autosize, new object[] { true, ListaConectados }); //Ponemos el método de autosize

                            ListaConectados.Invoke(tamaño, new object[] { trozos.Length - 1, "r", ListaConectados }); //Número de filas
                            ListaConectados.Invoke(tamaño, new object[] { 1, "c" , ListaConectados }); //Número de columnas
                            ListaConectados.Invoke(titulo, new object[] { "Nombre del jugador", 0, ListaConectados }); //Título de la columna

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
                        MessageBox.Show("Ha ganado " + respuesta + " partidas");
                        break;
                    case 5: //Tabla de jugadores que han ganado menos partidas
                        Console.WriteLine("He entrado en el codigo 5");
                        Puntuaciones.Invoke(listacon, new object[] { true, Puntuaciones }); //Se hace visible
                        Puntuaciones.Invoke(autosize, new object[] { true, Puntuaciones }); //Ponemos el AutoSize
                        Puntuaciones.Invoke(tamaño, new object[] { (trozos.Length) / 2, "r", Puntuaciones }); //Número de filas
                        Puntuaciones.Invoke(tamaño, new object[] { 1, "c", Puntuaciones }); //Número de columnas
                        Puntuaciones.Invoke(titulo, new object[] { "Nombre del jugador", 0, Puntuaciones }); //Título de la columna
                        respuesta = trozos[1].Split('\0')[0];
                        Partidas.Invoke(escribir, new object[] {"Los jugadores han ganado: " + respuesta + " partidas" });
                        j = 2;
                        for (i = 0; i < (trozos.Length) / 2; i++)
                        {
                            //Nombre:
                            respuesta = trozos[j].Split('\0')[0];
                            if (respuesta == "-1")
                            {
                                MessageBox.Show("Hubo un error al conectar con la base de datos");
                                break;
                            }
                            Console.WriteLine("Nombre: "+ respuesta);
                            Puntuaciones.Invoke(añadir, new object[] { respuesta, i, 0, Puntuaciones });
                            j = j + 1;
                        }
                        Cerrar.Invoke(boton, new object[] { true });
                        break;
                    case 6: //La notificación. Siempre que hay un cambio se nos enviará la notificación

                        j = 0;

                        for (i = 1; i < trozos.Length; i++)
                        {
                            respuesta = trozos[i].Split('\0')[0];
                            Console.WriteLine("Nombre lista: " + respuesta);
                            //Debemos ir aumentando la lista
                            if (respuesta != nombreuser)
                            {
                                ListaConectados.Invoke(tamaño, new object[] { (trozos.Length -2), "r", ListaConectados });
                                ListaConectados.Invoke(añadir, new object[] { respuesta, j, 0, ListaConectados });  //Invocamos al thread que creó el objeto
                                j = j + 1;
                            }
                        }

                        break;
                    case 7: //Nos llega la notificacion del cliente para que podamos aceptar la partida
                        
                        Invitacion.Invoke(bloq, new object[] { false });
                        respuesta = trozos[1].Split('\0')[0];
                        if(respuesta=="No estan conectados")
                        {
                            MessageBox.Show(respuesta);
                        }
                        else
                        {
                            string invitacion = "¿Quieres unirte a mi partida?";
                            string caption = "Invitación";
                            var result = MessageBox.Show(invitacion, caption, MessageBoxButtons.YesNo);
                            string contesta;
                            if (result == DialogResult.Yes)
                            {
                                contesta = "Si";

                            }
                            else
                            {
                                contesta = "No";
                            }
                            ThreadStart t = delegate { Invitar(contesta, respuesta); };
                            Thread ts = new Thread(t);
                            ts.Start();
                        }
                        
                        break;
                    case 8: //Nos dice si se une para abrir el nuevo formulario
                        respuesta = trozos[1].Split('\0')[0];
                        if (respuesta == "Se juega la partida")
                        {
                            MessageBox.Show(respuesta);
                            ThreadStart ts = delegate { PonerEnMarchaFormulario(nombreuser); };
                            Thread T = new Thread(ts);
                            T.Start();
                        }
                        else
                        {
                            MessageBox.Show(respuesta);
                            Invitacion.Invoke(bloq, new object[] { true });
                        }
                        break;
                    case 9: //Se envia el mensaje del chat
                        numForm = Convert.ToInt32(trozos[1].Split('\0')[0]);
                        Console.WriteLine("En el chat numform: " + Convert.ToString(numForm));
                        respuesta = trozos[2].Split('\0')[0];
                        formulario[numForm].TomaRespuesta(respuesta);
                        break;
                    case 10: //se envia la mirada fulminante al cliente
                        numForm = Convert.ToInt32(trozos[1].Split('\0')[0]);
                        numero = Convert.ToInt32(trozos[2].Split('\0')[0]);
                        respuesta = trozos[3].Split('\0')[0];
                        formulario[numForm].PonMiradas(numero, respuesta);
                        break;
                    case 11:  //Cambio de turno
                        numForm = Convert.ToInt32(trozos[1].Split('\0')[0]);
                        numero = Convert.ToInt32(trozos[2].Split('\0')[0]);
                        int l = Convert.ToInt32(trozos[3].Split('\0')[0]);
                        respuesta = trozos[4].Split('\0')[0];
                        formulario[numForm].CambioTurno(numero, l, respuesta);
                        break;
                    case 12: //Acaba la partida
                        numForm = Convert.ToInt32(trozos[1].Split('\0')[0]);
                        numero = Convert.ToInt32(trozos[2].Split('\0')[0]);
                        respuesta = trozos[3].Split('\0')[0];
                        string loser = trozos[4].Split('\0')[0];
                        formulario[numForm].AcabaPartida(numero, respuesta, loser);
                        break;
                    case 13: //Para poner las cartas en los picturebox de todos los clientes
                        numForm = Convert.ToInt32(trozos[1].Split('\0')[0]);
                        Console.WriteLine("Numero de formulario: " + Convert.ToString(numForm));
                        numero = Convert.ToInt32(trozos[2].Split('\0')[0]);
                        respuesta = trozos[3].Split('\0')[0];
                        string carta = trozos[4].Split('\0')[0];
                        formulario[numForm].PasaCarta(numero, respuesta, carta);
                        break;
                    case 14: //Bloquear elección de personajes
                        numForm = Convert.ToInt32(trozos[1].Split('\0')[0]);
                        respuesta = trozos[2].Split('\0')[0];
                        formulario[numForm].BloquearPersonaje(respuesta);
                        break;
                    case 15: //Eliminar un usuario de la base de datos
                        numero= Convert.ToInt32(trozos[1].Split('\0')[0]);
                        if (numero == 0)
                        {
                            MessageBox.Show("Tu usuario se ha eliminado con éxito");
                            conectado = 0; //Nos desconectamos
                            panel1.Invoke(delegado, new object[] { true, panel1 }); //Vemos el panel 1
                            panel2.Invoke(delegado, new object[] { false, panel2 }); //Dejamos de ver el panel 2
                            ListaConectados.Invoke(listacon, new object[] { false, ListaConectados }); //Dejamos de ver la lista
                            Puntuaciones.Invoke(listacon, new object[] { false, Puntuaciones }); //Dejamos de ver la tabla de los jugadores que menos partidas han ganado
                            Partidas.Invoke(ver, new object[] { false }); //Dejamos de ver la label que nos indica las partidas ganadas
                            Cerrar.Invoke(boton, new object[] { false }); //Dejamos de ver el boton para cerrar las tablas, ya que se cierran solas
                        }
                        else
                        {
                            MessageBox.Show("No se ha podido eliminar");
                        }
                       break;


                }
            }
        }

        //Boton para iniciar sesion
        private void IniciarSesion_Click(object sender, EventArgs e)
        {
            if (conectado == 0)
            {
                IPAddress direc = IPAddress.Parse(address);
                IPEndPoint ipep = new IPEndPoint(direc, gate);

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
                nombreuser = Usuario.Text;

                //En este caso el mensaje será 1/Nombre de usuario/Contraseña
                //Así es como el cliente enviará los datos al servidor para que los procese
                string mensaje = "1/" + Usuario.Text + "/" + Password.Text + "/";
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
                Usuario.Text = "";
                Password.Text = "";

            }
            catch (FormatException)
            {
                MessageBox.Show("Los datos introducidos no son correctos");
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Introduce datos");
            }
        }

        //Boton para registrarnos
        private void Registrarse_Click(object sender, EventArgs e)
        {
            if (conectado == 0)
            {
                IPAddress direc = IPAddress.Parse(address);
                IPEndPoint ipep = new IPEndPoint(direc, gate);
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
                Usuario.Text = "";
                Password.Text = "";
                Correo.Text = "";

            }
            catch (FormatException)
            {
                MessageBox.Show("Los datos introducidos no son correctos");
                return;
            }
            catch (Exception)
            {
                MessageBox.Show("Introduce datos");
            }
        }

        //Boton para enviar las tres consultas a la base de datos
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
                    //Queremos conocer los jugadores que van perdiendo, así que nos aparecerá una label que nos indicará
                    //las partidas canadas por todos los jugadores que nos apareceran en la tabla
                    //En la tabla tendremos todos los jugadores que habrán ganado X partidas, que será el número más bajo de partidas ganadas
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

        //Boton para desconectarnos del servidor
        private void button4_Click(object sender, EventArgs e)
        {
            string mensaje = "0/" + nombreuser;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            DelegadoParaVisualizar delegado = new DelegadoParaVisualizar(PanelVisible);
            GridVisible listacon = new GridVisible(ListaConectadosVisible);
            DejarVerLabel ver = new DejarVerLabel(Ver_Label);
            VerBoton boton = new VerBoton(Ver_Boton);
            panel1.Invoke(delegado, new object[] { true, panel1 }); //Vemos el panel 1
            panel2.Invoke(delegado, new object[] { false, panel2 }); //Dejamos de ver las 
            ListaConectados.Invoke(listacon, new object[] { false, ListaConectados }); //Dejamos de ver la lista
            Puntuaciones.Invoke(listacon, new object[] { false, Puntuaciones }); //Dejamos de ver la tabla de los jugadores que menos partidas han ganado
            Partidas.Invoke(ver, new object[] { false }); //Dejamos de ver la label que nos indica las partidas ganadas
            Cerrar.Invoke(boton, new object[] { false });
        }

        //Para invitar a los demás jugadores a jugar una partida
        private void Invitacion_Click(object sender, EventArgs e)
        {
            //Mensaje que envia el cliente si es el el que está invitando

            string mensaje = "6/" + jugador1.Text + "/" + jugador2.Text;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
            BloquearBoton bot = new BloquearBoton(Bloquear_Boton);
            Invitacion.Invoke(bot, new object[] { false });
            jugador1.Text = "";
            jugador2.Text = "";
        }

        //Cuando nos llega la notificacion de la invitación y tenemos que aceptar o denegarla
        public void Invitar(string respuesta, string nombre)
        {
            if(respuesta == "Si")
            {
                string mensaje = "7/Si/" + nombre;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
            else
            {
                string mensaje = "7/No/" + nombre;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }


        //Al cargar el formulario hay objetos que queremos que esten ocultos
        private void Form1_Load(object sender, EventArgs e)
        {
            GridVisible gr = new GridVisible(ListaConectadosVisible);
            VerBoton boton = new VerBoton(Ver_Boton);
            Puntuaciones.Invoke(gr, new object[] { false, Puntuaciones });
            Cerrar.Invoke(boton, new object[] { false });
        }

        //Cuando cerramos el formulario por la cruz también nos desconectaremos
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            string mensaje = "0/" + nombreuser;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

        //Para dar de baja a un usuario
        private void Baja_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Esta seguro de que quiere eliminar su usuario ??",
                                    "Confirma!!",
                                    MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                string mensaje = "11/" + nombreuser;
                byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
                server.Send(msg);
            }
        }

        //Cerramos la tabla de los que más partidas han perdido
        private void Cerrar_Click(object sender, EventArgs e)
        {
            DejarVerLabel ver = new DejarVerLabel(Ver_Label);
            GridVisible listacon = new GridVisible(ListaConectadosVisible);
            Puntuaciones.Invoke(listacon, new object[] { false, Puntuaciones }); //Dejamos de ver la tabla de los jugadores que menos partidas han ganado
            Partidas.Invoke(ver, new object[] { false }); //Dejamos de ver la label que nos indica las partidas ganadas
            VerBoton boton = new VerBoton(Ver_Boton);
            Cerrar.Invoke(boton, new object[] { false });
        }
    }
}
