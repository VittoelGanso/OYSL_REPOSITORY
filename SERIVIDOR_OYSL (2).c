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

//Estructura para la lista de conectados, en la cual se 
//almacenaran los nombres y los sockets de los usuarios.
typedef struct{
	char user[20];
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



//FUNCIONES DE LA BASE DE DATOS  

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
			sprintf(answer,"1/%s","Si");
			row = mysql_fetch_row(result);
			return 0;
		}
		
	}
}
		
//Funcion que permite iniciar sesion y registrar los datos en la base de datos 
int SignIn(char username[25], char password[25],char email[40],char answer[100], MYSQL *conn, int sock_conn){
	
	MYSQL_ROW row;
	MYSQL_RES *result;
	
	char Query[100];
		
	sprintf(Query,"INSERT INTO userDB(username,password,email,wins) VALUES ('%s','%s','%s',0);",username,password,email);
	int err=mysql_query(conn,Query);
	if(err!=0)
	{
		printf("Failure trying to connect to DataBase %u %s\n",mysql_errno(conn),mysql_error(conn));
		sprintf(answer,"2/This account already exists try to log in. ",username);
		return 1;
	}
	else{
		sprintf(answer,"Si");
		return 1;
	}
	
}

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
	}
	else{ //Recogemos el resultado de la consulta 
			result = mysql_store_result(conn);
			row = mysql_fetch_row(result);
		if ( row == NULL ){
			printf(" hasta aquí funciona\n");
			sprintf(answer,"3/ this username is not registered yet");
		}
		else {
			printf("The winner is %s.\n",row[0]);
			sprintf(answer,"3/%s",row[0]);
		}
	}	
}

//Funcion que devuelve una tabla de puntuaciones ordenada de mas a menos partidas ganadas 
/*int ScoreChart(char answer[200],MYSQL *conn){*/
	
/*	MYSQL *result;*/
/*	MYSQL_ROW row;*/
	//pasamos la Query 
/*	char Query[500];*/
/*	sprintf ( Query,"SELECT userDB.username,userDB.wins FROM userDB ORDER BY wins DESC");*/
/*	int err = mysql_query(conn,Query);*/
	//control errores MYSQL
/*	if(err!=0)*/
/*	{*/
/*		printf("Fail trying to connect to DataBase %u %s",mysql_errno(conn),mysql_error(conn));*/
/*	}*/



int contador;
//Lista de conectados 
ListOnlineUsers ListO;
//Estructura necesaria para acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

int i;
int sockets[100];

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
	while (finish ==0)
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
			finish = LogIn(username,password,Answer,conn,sock_conn);
			printf("finish: %d\n",finish);
			write(sock_conn,Answer,strlen(Answer));
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
			finish = SignIn(username,password,email,Answer,conn,sock_conn);
			printf("finish: %d\n",finish);
			}
		//CODIGO3
		//Cuando el codigo es 3, se trata de una peticion para ver el numero de partidas ganadas por
		//el jugador cuyo nombres se recibe como parametro.
		else if (code == 3){
			
			p = strtok(NULL,"/");
			//obtenemos el nombre que deseamos buscar en la base de datos 
			strcpy(username,p);
			//llamamos a la funcion
			GamesWon(username,Answer,conn);
		}
			
		if((code == 1) || (code == 2))
		{
			printf("Answer:%s\n",Answer);
			// la respuesta
			write(sock_conn,Answer,strlen(Answer));
		}
		
	}
	// Se acabo el servicio para este cliente
	close(sock_conn); 
	
}


int main(int argc, char *argv[])
{
	
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;
	int gate = 9030;

	// INICIALITZACIONS
	// Abrimos el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	
	// establecemos el puerto de escucha
	serv_adr.sin_port = htons(9030);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		
		printf ("Error al bind");
	
	if (listen(sock_listen, 4) < 0) // la cola de peticiones no va a ser mayor de 4
	
		printf("Error en el Listen");
	
	contador =0;
	
	pthread_t thread;
	i=0;
	for (;;){
		
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		// Crear thead y decirle lo que tiene que hacer
		pthread_create (&thread, NULL, AtenderCliente,&sock_conn);
		i=i+1;
		
	}
	

	
}
