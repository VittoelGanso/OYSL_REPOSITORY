#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <pthread.h>
#include <mysql.h>

//ESTRUCTURAS USUARIOS Y LISTA DE LOS MISMOS

//Estructura necesaria para acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

//vector de threads para tener generar concurrencia.
pthread_t thread;


//Estructura para la lista de conectados, en la cual se 
//almacenaran los nombres y los sockets de los usuarios.
typedef struct{
	char username[20];
	int socket;
} OnlineUser;

typedef struct{
	OnlineUser online[100];
	int num;
} ListOnlineUsers;

//ESTRUCTURA DE PARTIDA 

//Estructura para la tabla de partidas, en el cual se alamacenaran 
//los nombres y los sockets de los usuarios.
typedef struct{
	char name1[20];
	char name2[20];
	char name3[20];
	int play1;
	int play2;
	int play3;
	int score1;
	int score2;
	int socket1;
	int socket2;
	int socket3;
}Game;

//FUNCIONES PARA LA LISTA DE USUARIOS CONECTADOS
 
ListOnlineUsers List; //creamos una lista de usuarios conectados.
	
//FUNCION : AñadiraLista funcion que añade un usuario a la lista de conectados.
//CODIGOS : 0 si el usuario ha sido añadido, -1 si el usuario ya estaba en la lista y -2 si no hay espacio en la lista.

int AddtoList(ListOnlineUsers *List, char username[20], int socket){
	
	int Added = 0;
	int indx1 = 0, indx2=0;
	
	//Primero de todo comprovamos que no exista ningún usuario conectado a la lista con el mismo nombre.
	while ( indx2 < List->num ){
		
		if (strcmp( List->online[indx2].username,username) == 0)
			return -1;
		else 
			indx2++;
	}
	
	//Si todo va bien añadimos el usuario a la lista.
	while (( indx1 < List->num ) && Added == 0){
		
		if ( List->online[indx1].socket == socket){
			strcpy( List->online[indx1].username, username);
			Added = 1;
			//Print de control de errores
			printf(" Socket: %d , nombre: %s , posicion: %d", socket,username,indx1);
		}
		else
			indx1++;
	}
	if (Added == 0)
		return -2;
	else 
		return 0;
}

//FUNCION : DamePosicion, funcion que nos retornara la posicion de un usuario dentro de la lista.
//CODIGOS : position si todo ha ido bien, -1 si no ha encontrado al usuario.

int GivemePosition (ListOnlineUsers *List, char username[20]){
	
	int position = 0;
	int found = 0;
	
	while ( position < List-> num){
		//comparamos el nombre pasado por parametro con los que hay en la lista.
		if( strcmp( List->online[position].username, username) == 0)
			found = 1;
		else 
			position ++;
	}
	if ( found == 0 )
		return -1;
	else 
		//Si todo ha salido bien la funcion nos retorna la posicion del usuario.
		return position;
}

//FUNCION : EliminardeLista funcion que elimina a un usuario de la lista de conectados.
//CODIGOS : 0 si todo ha salido bien.

int DeletefromList (ListOnlineUsers *List, char username[20]){
	
	//cogemos la posicion del usuario dentro de la lista.
	int position = GivemePosition(List, username);
	//generamos un bucle que mueva todos los elementos de la lista una posicion
	while(position < List->num )
	{
		List->online[position] = List->online[position-1];
		position ++;
	}
	//eliminamos la ultima posicion de la lista.
	List->num = List->num -1;
	return 0;
}

//FUNCION : Genera una string con todos los usuarios conectados.

int GivemeOnlineusers( ListOnlineUsers *List, char Ousers[512]){
	
	int i;
	for( i=0; i < List->num; i++){
		sprintf(Ousers,"%s%s/",Ousers,List->online[i].username);
	}
}

//FUNCIONES DE LA BASE DE DATOS  

//CODIGO 1
//Funcion que permite iniciar sesion comparando los datos de la base de datos 
int LogIn(char username[25], char password[25], char answer[100], MYSQL *conn, int sock_conn){
	
	MYSQL_ROW row;
	MYSQL_RES *result;
	
	//declaramos y hacemos la consulta
	char query[200];
	sprintf(query,"SELECT userDB.username, userDB.Password FROM userDB WHERE userDB.username = '%s' AND userDB.Password = '%s';",username,password);
	int err = mysql_query (conn, query);
	//Hacemos control de errores de la consulta 
	if(err!=0)
	{
		printf(" Failure trying to connecto to DataBase %u %s\n", mysql_errno(conn), mysql_error(conn));
		return 1;
	}
	else{//recogemos el resutado de la consulta 
		result = mysql_store_result(conn);
		row = mysql_fetch_row(result);
		
		if(row == NULL){
			
			sprintf(answer,"1/%s","No");
			return 1;
		}
		else {
			int res = AddtoList(&List, username,sock_conn);
			
			if (res == 0 ){// todo ha salido a pedir de milhouse.
			sprintf(answer,"1/%s","Si");
			row = mysql_fetch_row(result);
			return 0;
			}
			
			if (res == -1){ // ya hay alguien registrado con ese nombre.
				sprintf(answer, "1/Name in use, try another one");
				return 1;
			}
			if (res = -2){
				sprintf(answer,"1/The list is full");
				return 1;
			}
		}
		
	}
}

//CODIGO 2	
//Funcion que permite iniciar sesion y registrar los datos en la base de datos 
int SignIn(char username[25], char password[25],char email[40],char answer[100], MYSQL *conn, int sock_conn){
	
	MYSQL_ROW row;
	MYSQL_RES *result;
	
	char Query[100];
		
	sprintf(Query,"INSERT INTO userDB(username,password,email,wins, played) VALUES('%s','%s','%s',1, 0);",username,password,email);
	int err=mysql_query(conn,Query);
	if(err!=0){
		printf("Failure trying to connect to DataBase %u %s\n",mysql_errno(conn),mysql_error(conn));
		sprintf(answer,"2/This account already exists try to log in. ",username);
		return 1;
	}
	else{
		
		sprintf(answer,"2/Si");
		return 0;
	}
	
}

//CODIGO 3
//Funcion que nos devuelve las partidas jugadas por el jugador recibido como parametro.
int GamesPlayed(char name[25], char answer[100], MYSQL *conn, int sock_conn){
	
	MYSQL_RES *result;
	MYSQL_ROW row;

	char Query[500];
	sprintf(Query,"SELECT played FROM userDB WHERE userDB.username = '%s'",name);
	int err = mysql_query(conn,Query);
	if (err != 0)
	{
		printf("Failure trying to connect to DataBase %u %s\n",mysql_errno(conn),mysql_error(conn));
		return 1;
	}
	else{ //Recogemos el resultado de la consulta 
		result = mysql_store_result(conn);
		row = mysql_fetch_row(result);
		if ( row == NULL ){
			sprintf(answer,"3/this username is not registered yet");
		}
		else {
			printf("Ha jugado %s partidas.", row[0]);
			sprintf(answer,"3/%s",row[0]);
		}
		return 0;
	}	
}

//CODIGO 4
//Funcion que nos devuelve las partidas ganadas por el jugador recibido como parametro 
int GamesWon(char name[25],char answer[100],MYSQL *conn){
	
	MYSQL_RES *result;
	MYSQL_ROW row;
	int GW;
	char Query[500];
	sprintf(Query,"SELECT wins FROM userDB WHERE userDB.username = '%s'",name);
	int err = mysql_query(conn,Query);
	if (err!= 0)
	{
		printf("Failure trying to connect to DataBase %u %s\n",mysql_errno(conn),mysql_error(conn));
		return -1;
	}
	else{ //Recogemos el resultado de la consulta 
			result = mysql_store_result(conn);
			row = mysql_fetch_row(result);
		if ( row == NULL ){
			printf(" hasta aquí funciona\n");
			sprintf(answer,"4/ this username is not registered yet");
		}
		else {
			printf("The winner is %s.\n",row[0]);
			sprintf(answer,"4/s",row[0]);
		}
		return 0;
	}	
}

//CODIGO 5
//Funcion que nos devuelve un string con el siguiente formato, partidas Ganadas/nombre de usuario
//para posteriormente crear una tabla de puntuaciones.
int chart(MYSQL *conn, char answer[512]){
	
	MYSQL_RES *result;
	MYSQL_ROW row;
	
	
	char Query[200];
	sprintf(answer,"SELECT userDB.username,userDB.wins FROM (userDB) ORDER BY wins DESC");
	int err = mysql_query(conn,Query);
	
	if(err!=0){
		printf("Failure trying to connect to DataBase %u %s\n",mysql_errno(conn),mysql_error(conn));
		return -1;
	}
	else{ //Recogemos el resultado de la consulta
		 
		result = mysql_store_result(conn);
		row = mysql_fetch_row(result);
		
		if (row == NULL){
			printf("No se han obtenido datos");
			strcpy(answer, "No se han obtenido datos");
			return 0;
		}
		else{
			
			while( row != NULL){
				sprintf(answer, "5/%s/%s/%s",answer,row[0], row[1] ); 
				row=mysql_fetch_row(result);
			}
			return 0;
		}
	}
}




//Funcion que nos devuelve una string con las partidas ganadas de cada usuario que haya en la base de datos
//junto a su nombre de usuario separados por una /.
int Chart(MYSQL *conn, char answer[512]){
	
	MYSQL_RES *result;
	MYSQL_ROW row;
	
	char Query[200];
	sprintf(Query,"SELECT username, wins FROM userDB");
	int err = mysql_query(conn,Query);
	if (err!= 0){
		
		sprintf(answer,"Failure trying to connect to DataBase %u %s\n",mysql_errno(conn),mysql_error(conn));
	}
	else{
		
		while( row != NULL){
			
			printf("The player is: %s with games winned: %d\n", row[0],row[1]);
			sprintf(answer,"5/ %s %s \n", row[0],row[1]);
			
		}
		if( row == NULL){
			sprintf(answer,"5/There's nobody registered yet");
		}	
	}
}

//Usando las funciones creadas, buscamos atender al cliente.
void *AtenderCliente (void *socket)
{
	//Asignamos el socket que recibimos en un puntero 
	MYSQL *conn;
	int sock_conn;
	int *s;
	s= (int *) socket;
	sock_conn= *s;
	//int socket_conn = * (int *) socket;
	int finish =0;
	int code;
	
	char Request[512];
	char Answer[512];
	
	char *username[25];
	char *password[25];
	char *email[25];
	int error;
	
	int ret; // parametro que almaecena la informacion de los datos enviados por el usuario.
	
	conn = mysql_init(conn);
	
	if(conn == NULL){
		
		printf ("Failure Trying to innitialize: %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	conn = mysql_real_connect(conn, "localhost", "root", "mysql", "OYSL_DB", 0, NULL, 0);
	
	if(conn==NULL){
		printf("Error al inicializar la conexion %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit(1);
	}
	// Entramos en un bucle para atender todas las peticiones de este cliente
	// hasta que se desconecte
	while (finish == 0)
	{
		// Recibimos la peticion
		ret=read(sock_conn,Request, sizeof(Request));
		printf ("Recieved\n\n");
		
		// Marcamos final de string para evitar coger la redundancia aÃ±adida
		Request[ret]='\0';
		
		printf ("Peticion: %s\n",Request);
		
		// Miramos cual es la peticion 
		char *p = strtok( Request, "/");
		int code =  atoi (p);
		
		// Una vez conocemos la peticion, operamos con la informacion recibida.
		
		//CODIGO 0
		//Cuando el codigo es 0, se trata de una peticion de desconexion.
		if (code== 0){
			
			//DeletefromList(&List, username);
			
			finish = 1;
		}
		//CODIGO 1
		//Cuando el codigo es "1", se trata de una peticion de inicio de sesion.
		else if (code ==1){ 
			
			p = strtok(NULL, "/");
			//obtenemos el nombre de usuario 
			strcpy(username,p);
			
			p= strtok(NULL,"/");
			//obtenemos la password	
			strcpy(password,p);
			
			error = LogIn(username,password,Answer,conn,sock_conn);
			printf("finish: %d\n",finish);

		
			}
	
		//CODIGO 2
		//Cuando el codigo es "2", se trata de una peticion de registro.
		else if (code ==2){
			
			p = strtok(NULL, "/");
			//obtenemos el nombre de usuario 
			strcpy(username,p);
			
			p= strtok(NULL,"/");
			//obtenemos la password	
			strcpy(password,p);
			
			p= strtok(NULL,"/");
			//obtenemos el correo electronico
			strcpy(email,p);

			error = SignIn(username,password,email,Answer,conn,sock_conn);
			printf("finish: %d\n",finish);

			
			}
		//CODIGO 3
		//Cuando el codigo es 3, se trata de una peticion para ver el numero de partidas jugadas por
		//el jugador cuyo nombres se recibe como parametro.
		else if (code == 3){
			
			p = strtok(NULL,"/");
			//obtenemos el nombre que deseamos buscar en la base de datos 
			strcpy(username,p);
			//llamamos a la funcion
			error = GamesPlayed(username,Answer,conn, sock_conn);
			
		}
		//CODIGO 4
		//Cuando el codigo es 4, se trata de una peticion para ver el numero de partidas ganadas por 
		//el jugador cuyo nombre se recibe como paramentro.
		else if (code == 4){
			
			p = strtok(NULL,"/");
			//obtenemos el nombre que deseamos buscar en la base de datos 
			strcpy(username,p);
			//llamamos a la funcion
			error = GamesWon(username,Answer,conn);
			
		}
		else if(code ==5){
			
			error = chart(sock_conn, Answer);
			
			printf("No está disponible \n");
		}
		
		if((code==1)||(code == 2) || (code == 3)|| (code == 4) || (code==5))
		{
			printf("Answer:%s\n",Answer);
			// la respuesta
			write(sock_conn,Answer,strlen(Answer));
		}
		if ((code == 0) || (code == 1)){
			
			pthread_mutex_lock(&mutex);
			
			char users[512];
			
			error = GivemeOnlineusers(&List,users);
			printf("users: %s\n ",users);
			pthread_mutex_unlock(&mutex);
			
			//Notificar los usuarios conectados.
			users[strlen(users)-1] = '\0';
			char notificacion[512];
			sprintf(notificacion, "6/%s", users);
			
			//Se tiene que enviar a todos los clientes conectados
		
			for (int u=0; u < List.num ;u++){
				
				write(List.online[u].socket,notificacion,strlen(notificacion));
			}

		}
			
			
			
		
	}
	// Se acabo el servicio para este cliente
	close(sock_conn); 
	
}




int main(int argc, char *argv[]){
	
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;
	int gate = 9090;

	// INICIALITZACIONS
	// Abrimos el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Fail creating the socket");
	// Fem el bind al port
	
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	
	// establecemos el puerto de escucha
	serv_adr.sin_port = htons(gate);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		
		printf ("Error al bind");
	
	if (listen(sock_listen, 4) < 0) // la cola de peticiones no va a ser mayor de 4
		printf("Faiure listening");
	
	for (;;){
		printf ("Escuchando\n");
		
		//Obtenemos el socket y lo guardamos en un vector.
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		
		if(List.num < 100){
			
			//le atribuimos un socket a cada uno de los usuarios.
			List.online[List.num].socket = sock_conn;
			// Crear thead y decirle lo que tiene que hacer
			pthread_create (&thread, NULL, AtenderCliente,&List.online[List.num].socket);
			List.num++;
		}
		else 
			printf("There no more available sockets");
	}
	//Generamos un bucle de espera para que no finalize ningún thread hasta haber atendido al cliente

	
}
