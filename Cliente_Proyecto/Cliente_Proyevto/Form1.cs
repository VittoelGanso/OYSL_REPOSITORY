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
        string address = "192.168.56.101";
        int gate = 9070;
        Thread atender;
        int numForm;

        List<GraficoOYSL> formulario = new List<GraficoOYSL>();


        delegate void DelegadoParaEscribir(string mensaje, int pos1, int pos2, DataGridView datagrid);
        delegate void DelegadoParaVisualizar(bool visible, Panel p);
        delegate void GridVisible(bool visible, DataGridView datagrid);
        delegate void TamañoDataGrid(int num, string lugar, DataGridView datagrid);
        delegate void Titulo(string titulo, int pos, DataGridView datagrid);


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
            ListaConectados.Visible = visible;
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



        private void PonerEnMarchaFormulario()
        {
            int cont = formulario.Count;
            string nombreuser = this.Usuario.Text;
            GraficoOYSL f = new GraficoOYSL(cont, server, nombreuser);
            formulario.Add(f);
            f.ShowDialog();
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
                Console.WriteLine("Numero de trozos: " + Convert.ToString(trozos.Length));
                string mensaje = trozos[0].Split('\0')[0]; //Declaramos el mensaje recibido por el servidor
                Console.WriteLine(mensaje);
                int codigo = Convert.ToInt32(mensaje); //Donde tenemos el codigo del mensaje

                int i, j;
                
                string respuesta;
                DelegadoParaVisualizar delegado = new DelegadoParaVisualizar(PanelVisible);
                GridVisible listacon = new GridVisible(ListaConectadosVisible);
                GridVisible autosize = new GridVisible(ListaConectadosAutoSize);
                TamañoDataGrid tamaño = new TamañoDataGrid(ListaConectadosRow);
                Titulo titulo = new Titulo(ListaHeader);
                DelegadoParaEscribir añadir = new DelegadoParaEscribir(AñadirFila);


                switch (codigo)
                {
                    case 0:
                        panel1.Invoke(delegado, new object[] { true, panel1 }); //Vemos el panel 1
                        panel2.Invoke(delegado, new object[] { false, panel2 }); //Dejamos de ver las 
                        ListaConectados.Invoke(listacon, new object[] { false, ListaConectados }); //Dejamos de ver la lista
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
                        MessageBox.Show(respuesta);
                        break;
                    case 5: //Tabla de jugadores con sus partidas ganadas

                        Puntuaciones.Invoke(listacon, new object[] { true, Puntuaciones }); //Se hace visible
                        Puntuaciones.Invoke(autosize, new object[] { true, Puntuaciones}); //Ponemos el AutoSize
                        Puntuaciones.Invoke(tamaño, new object[] { (trozos.Length)/2, "r", Puntuaciones }); //Número de filas
                        Puntuaciones.Invoke(tamaño, new object[] { 2, "c", Puntuaciones }); //Número de columnas
                        Puntuaciones.Invoke(titulo, new object[] { "Nombre del jugador", 0, Puntuaciones }); //Título de la columna
                        Puntuaciones.Invoke(titulo, new object[] { "Partidas ganadas", 1, Puntuaciones }); //Título de la segunda columna

                        j = 1;
                        for (i = 0; i < (trozos.Length)/2 ; i++)
                        {
                            //Nombre:
                            respuesta = trozos[j].Split('\0')[0];
                            Console.WriteLine(respuesta);
                            Puntuaciones.Invoke(añadir, new object[] { respuesta, i, 0, Puntuaciones });

                            j = j + 1;
                            //Partidas ganadas:
                            respuesta = trozos[j].Split('\0')[0];
                            Console.WriteLine(respuesta);
                            Puntuaciones.Invoke(añadir, new object[] { respuesta, i, 1, Puntuaciones });
                            j = j + 1;
                        }
                        break;
                    case 6: //La notificación. Siempre que hay un cambio se nos enviará la notificación

                        j = 0;

                        for (i = 1; i < trozos.Length; i++)
                        {
                            respuesta = trozos[i].Split('\0')[0];
                            //Debemos ir aumentando la lista

                            ListaConectados.Invoke(tamaño, new object[] { (trozos.Length - 1), "r", ListaConectados });
                            ListaConectados.Invoke(añadir, new object[] { respuesta, j, 0, ListaConectados});  //Invocamos al thread que creó el objeto
                            j = j + 1;

                        }

                        break;
                    case 7: //Nos llega la notificacion del cliente para que podamos aceptar la partida
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
                    case 8: //Nos dice si se une
                        respuesta = trozos[1].Split('\0')[0];
                        if (respuesta == "Se juega la partida")
                        {
                            MessageBox.Show(respuesta);
                            ThreadStart ts = delegate { PonerEnMarchaFormulario(); };
                            Thread T = new Thread(ts);
                            T.Start();
                        }
                        else
                        {
                            MessageBox.Show(respuesta);
                        }
                        break;
                    case 9:
                        numForm = Convert.ToInt32(trozos[1].Split('\0')[0]);
                        Console.WriteLine(Convert.ToString(numForm));
                        respuesta = trozos[2].Split('\0')[0];
                        formulario[numForm].TomaRespuesta(respuesta);
                        //Enviamos el mensaje al formulario
                        break;

                }
            }
        }

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
            conectado= 0;
        }

        private void Invitacion_Click(object sender, EventArgs e)
        {
            //Mensaje que envia el cliente si es el el que está invitando

            string mensaje = "6/" + jugador1.Text + "/" + jugador2.Text;
            byte[] msg = System.Text.Encoding.ASCII.GetBytes(mensaje);
            server.Send(msg);
        }

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



        private void Form1_Load(object sender, EventArgs e)
        {
            
            GridVisible gr = new GridVisible(ListaConectadosVisible);
            Puntuaciones.Invoke(gr, new object[] { false, Puntuaciones });
        }

        public string GetUser (string nombreuser)
        {
            return nombreuser;
        }

    }
}
