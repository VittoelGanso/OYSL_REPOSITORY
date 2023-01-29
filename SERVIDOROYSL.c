#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <pthread.h>
#include <mysql.h>
#include <my_global.h>




//ESTRUCTURAS USUARIOS Y LISTA DE LOS MISMOS

//Estructura necesaria para acceso excluyente
pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER;

//vector de threads para tener generar concurrencia.
pthread_t thread;

//Para ir sumando los jugadores que contestaran
int jugadores=0;
int idp; //El ID de las partidas

//Estructura para la lista de conectados, en la cual se 
//almacenaran los nombres y los sockets de los usuarios.
typedef struct{
	char username[20];
	int socket;
	char rol[20];
	int MF;
	/*int nform;*/
} OnlineUser;

typedef struct{
	OnlineUser online[100];
	int num;
} ListOnlineUsers;

//ESTRUCTURA DE PARTIDA 
typedef struct{
	OnlineUser user[3];
	int playersnum;
	int full;
	int id;
} Game;



//Estructura para la tabla de partidas, en el cual se alamacenaran 
//los nombres y los sockets de los usuarios.
//*typedef struct{*/
/*	char name1[20];*/
/*	char name2[20];*/
/*	char name3[20];*/
/*	int play1;*/
/*	int play2;*/
/*	int play3;*/
/*	int score1;*/
/*	int score2;*/
/*	int socket1;*/
/*	int socket2;*/
/*	int socket3;*/
/*}Game;*/

 
ListOnlineUsers List; //creamos una lista de usuarios conectados.
typedef Game TGames [20]; //Creamos la lista de las partidas
TGames Table;
// int sock_conn;
// MYSQL *conn;

//
//
//FUNCIONES PARA LA TABLA DE PARTIDAS.
//
//



//BUSCAMOS UNA PARTIDA QUE NO ESTE LLENA PARA GENERARLA.
int FreeGame(TGames Table){
	int found = 0;
	int id = 0;
	while ((id<20)&&(found==0)){
		if(Table[id].full ==0){
			found = 1;
		}
		else{
			id=id+1;
		}
	}
	if (found == 1){
		return id; //Que ser  la posici n en la que haya una partida libre
	}
	else{
		return -1; //Ha habido un error
	}
}

//ELIMINAMOS LA PARTIDA CUANDO SE ACABA.
void DeleteGame(TGames Table, int id){
	Table[id].playersnum=0; //Ponemos a 0 el n mero de jugadores
	for(int i=0; i<3; i++){
		Table[id].user[i].socket=-1;
		strcpy(Table[id].user[i].username, "");
	}
}

//A ADIMOS Y GENERAMOS UNA PARTIDA 
void AddtoGame(TGames Table, char username[20], int id){
	
	if(Table[id].full==0){
		DeleteGame(Table, id); //Eliminamos la partida por si ya habia alguien
		Table[id].full=1; //Ponemos que la partida ahora est  ocupada
	}
	
	int socket = GivemeSocket(&List, username);
	printf("Socket: %d \n", socket);
	
	//copiamos los datos del socket y del user dentro de la tabla.
	Table[id].user[Table[id].playersnum].socket = socket;
	strcpy(Table[id].user[Table[id].playersnum].username, username);
	
	//Avanzamos una posicion de la tabla.
	printf("Usuario: %s \n", Table[id].user[Table[id].playersnum].username);
	Table[id].playersnum = Table[id].playersnum +1;
	printf("Numero: %d \n", Table[id].playersnum);
}

//FUNCION QUE ELIMINA UNA PARTIDA DE LA TABLA DE PARTIDAS Y ACTUALIZA EL RESULTADO.
void FinishGame(TGames Table, int idp, char loser[20], MYSQL* conn,int sock_conn) {

	MYSQL_ROW row;
	MYSQL_RES* result;
	char query[200];
	char answer[100];
	char winner[20];
	char rol[20];
	char SO[20];
	int err;
	strcpy(rol, "lacayo");
	char rol2[20];
	strcpy(rol, "SO");


	for (int i = 0; i < 3; i++) {

		if ((strcmp(Table[idp].user[i].username, loser) != 0) && (strcmp(Table[idp].user[i].rol, rol) == 0)) {

			strcpy(winner, Table[idp].user[i].username);

		}
	}

	//Actualizamos las partidas ganadas del jugador que ha ganado la partida.
	int GamesWon1 = GamesWon(winner, answer, conn) + 1;
	//Hacemos la consulta y comprovamos que la conexion sea correcta.
	sprintf(query, "INSERT INTO userDB(wins) VALUES ('%d');", GamesWon1);
	err = mysql_query(conn, query);
	//control de errores.
	if (err != 0)
	{
		printf(" Failure trying to connecto to DataBase %u %s\n", mysql_errno(conn), mysql_error(conn));
		return 1;
	}
	else {
		//Recogemos el resultado para comprovar si es correcto
		result = mysql_store_result(conn);
		row = mysql_fetch_row(result);

		if (row == NULL) {

			sprintf(answer, "11/%s", "No");
			return 1;
		}
		else {//comprovamos que el resultado insertado Ã©s el mismo que hemos recogido de la base de datos.

			if (GamesWon1 == result) {
				printf(result);
				sprintf(answer, "11/%s", "Se ha actualizado la informacion de usuario");
				
			}
			else {
				sprintf(answer, "11/%s", "No se ha actualizado la informacion de usuario");
				return 1;
			}
		}
	}
	//Actualizamos las partidas jugadas tanto del ganador como del perdedor.
	int GamesPlayed1 = GamesPlayed(loser, answer, conn, sock_conn) + 1;
	int GamesPlayed2 = GamesPlayed(winner, answer, conn, sock_conn) + 1;

	//Actualizamos la informacion del perdedor.
	sprintf(query, "INSERT INTO userDB(played) WHERE userDB.username = '%s' VALUES ('%d')", loser, GamesPlayed1);
	err = mysql_query(conn, query);
	//control de errores.
	if (err != 0)
	{
		printf(" Failure trying to connect to DataBase %u %s\n", mysql_errno(conn), mysql_error(conn));
		return 1;
	}
	else{
		//Recogemos el resultado para comprovar si es correcto
		result = mysql_store_result(conn);
		row = mysql_fetch_row(result);

		if (row == NULL) {

			sprintf(answer, "11/%s", "No");
			return 1;
		}
		else {//comprovamos que el resultado insertado Ã©s el mismo que hemos recogido de la base de datos.

			if (GamesPlayed1 == result) {
				printf(result);
				sprintf(answer, "11/%s", "Se ha actualizado la informacion de usuario");

			}
			else {
				sprintf(answer, "11/%s", "No se ha actualizado la informacion de usuario");
				return 1;
			}
		}
	}

	//Actualizamos la informacion del ganador.
	sprintf(query, "INSERT INTO userDB(played) WHERE userDB.username = '%s' VALUES ('%d')", winner, GamesPlayed2);
	err = mysql_query(conn, query);
	//control de errores.
	if (err != 0)
	{
		printf(" Failure trying to connecto to DataBase %u %s\n", mysql_errno(conn), mysql_error(conn));
		return 1;
	}
	else {
		//Recogemos el resultado para comprovar si es correcto
		result = mysql_store_result(conn);
		row = mysql_fetch_row(result);

		if (row == NULL) {

			sprintf(answer, "11/%s", "No");
			return 1;
		}
		else {//comprovamos que el resultado insertado Ã©s el mismo que hemos recogido de la base de datos.

			if (GamesPlayed2 == result) {
				printf(result);
				sprintf(answer, "11/%s", "Se ha actualizado la informacion de usuario");

			}
			else {
				sprintf(answer, "11/%s", "No se ha actualizado la informacion de usuario");
				return 1;
			}
		}
	}


	//Buscamos entre los jugadores quien es el seÃ±or oscuro.
	for (int u = 0; u < 3; u++) {

		if (strcmp(Table[idp].user[u].rol, rol2) == 0) 
		strcpy(SO,Table[idp].user[u].username);
		
	}

	//Actualizamos las partidas jugadas por el seÃ±or oscuro.
	int GamesPlayed3 = GamesPlayed(SO, answer, conn, sock_conn) + 1;

	//Actualizamos la informacion del seÃ±or oscuro.
	sprintf(query, "INSERT INTO userDB(played) WHERE userDB.username = '%s' VALUES ('%d')", SO, GamesPlayed3);
	err = mysql_query(conn, query);
	//control de errores.
	if (err != 0)
	{
		printf(" Failure trying to connecto to DataBase %u %s\n", mysql_errno(conn), mysql_error(conn));
		return 1;
	}
	else {
		//Recogemos el resultado para comprovar si es correcto
		result = mysql_store_result(conn);
		row = mysql_fetch_row(result);

		if (row == NULL) {

			sprintf(answer, "11/%s", "No");
			return 1;
		}
		else {//comprovamos que el resultado insertado Ã©s el mismo que hemos recogido de la base de datos.

			if (GamesPlayed3 == result){
				printf(result);
				sprintf(answer, "11/%s", "Se ha actualizado la informacion de usuario");
			}
			else {
				sprintf(answer, "11/%s", "No se ha actualizado la informacion de usuario");
				return 1;
			}
		}
	}
	DeleteGame(Table, idp);

}





int GivemePositionTabla (TGames Table, char username[20], int idp){
	
	int position = 0;
	int found = 0;
	
	while ( (position < 20)&&(found==0)){
		//comparamos el nombre pasado por parametro con los que hay en la lista.
		if( strcmp( Table[idp].user[position].username, username) == 0)
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
	


//
// 
// 
//FUNCIONES PARA LA LISTA DE USUARIOS CONECTADOS.
//
//
//

//A ADIR LISTA
//FUNCION : A adiraLista funcion que a ade un usuario a la lista de conectados.
//CODIGOS : 0 si el usuario ha sido a adido, -1 si el usuario ya estaba en la lista y -2 si no hay espacio en la lista.

int AddtoList(ListOnlineUsers *List, char username[20], int socket){
	
	/*int Added = 0;*/
	int indx2=0;
	printf("Usuarios en la lista antes de añadir: %d\n", List->num);
	//Primero de todo comprovamos que no exista ningún usuario conectado a la lista con el mismo nombre.
	while ( indx2 < List->num ){
		
		if (strcmp( List->online[indx2].username,username) == 0){
			List->num = List->num -1;
			return -1;
		}
		else {
			indx2++;
	
		}
	}
	//Ahora añadimos el nuevo usuario en la lista
	if (List->num == 100){
		return -1;
	}
	else{
		strcpy(List->online[List->num-1].username, username);
		List->online[List->num-1].socket = socket;
		printf("Añadidos: Usuario: %s, Socket: %d, Personas que hay: %d \n", List->online[List->num-1].username, List->online[List->num-1].socket, List->num);
		return 0;
	}
	

}

//DAME POSICION
//FUNCION : DamePosicion, funcion que nos retornara la posicion de un usuario dentro de la lista.
//CODIGOS : position si todo ha ido bien, -1 si no ha encontrado al usuario.

int GivemePosition (ListOnlineUsers *List, char username[20]){
	
	int position = 0;
	int found = 0;
	printf("Username en la busqueda: %s \n", username);
	printf("Numero usuarios en la lista %d \n", List->num);
	
	while ( (position < List-> num)&&(found==0)){
		//comparamos el nombre pasado por parametro con los que hay en la lista.
		printf("Usuario lista: %s \n", List->online[position].username);
		if( strcmp( List->online[position].username, username) == 0){
			found = 1;
		}
		else {
			position ++;
		}
	}
	printf("Value of found: %d \n", found);
	if ( found == 0 )
		return -1;
	else 
		//Si todo ha salido bien la funcion nos retorna la posicion del usuario.
		return position;
}

//ELIMINA DE LA LISTA
//FUNCION : EliminardeLista funcion que elimina a un usuario de la lista de conectados.
//CODIGOS : 0 si todo ha salido bien.

int DeletefromList (ListOnlineUsers *List, char username[20]){
	
	//cogemos la posicion del usuario dentro de la lista.
	printf("Usuarios Lista: %d\n", List->num);
	int position = GivemePosition(List, username);
	printf("Posicion %d \n", position);
	//generamos un bucle que mueva todos los elementos de la lista una posicion
	while(position < List->num )
	{
		List->online[position] = List->online[position+1];
		printf("Nueva posicion en la lista: %s \n", List->online[position].username);
		position ++;
	}
	//eliminamos la ultima posicion de la lista.
	List->num = List->num -1;
	return 0;
}

//DAME EL SOCKET
//FUNCION: Nos da el socket de un usuario que se encuentra en la lista de conectados
//CODIGOS : -1 si no ha encontrado el socket o bien el socket.
int GivemeSocket (ListOnlineUsers *List, char username[20]){
	int j=0;
	int found=0;
	while((j<List->num)&&(found==0)){
		if(strcmp(List->online[j].username, username)==0){
			found=1;
		}
		else{
			j=j+1;
		}
	}
	if (found==1){
		return List->online[j].socket;
	}
	else{
		return -1;
	}
}

//FUNCION : Genera una string con todos los usuarios conectados.
int GivemeOnlineusers( ListOnlineUsers *List, char Ousers[512]){
	
	int i;
	printf("Usuarios en la lista: %d \n", List->num);
	strcpy(Ousers, List->online[0].username);
	for( i=1; i < List->num; i++){
		sprintf(Ousers,"%s/%s",Ousers,List->online[i].username);
	}
}


//
// 
// 
//FUNCIONES DE LA BASE DE DATOS  
//
// 
// 


//CODIGO 1
//Funcion que permite iniciar sesion comparando los datos de la base de datos 
int LogIn( char username[25], char password[25], char answer[100], MYSQL *conn, int sock_conn){
	
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
			
			sprintf(answer,"1/%s","Tu usuario no se encuentra en la base de datos. Crea un usuario y vuelve a iniciar sesion");
			List.num = List.num -1;
			return 1;
		}
		else {
			
			pthread_mutex_lock(&mutex);
			int res = AddtoList(&List, username,sock_conn);
			pthread_mutex_unlock(&mutex);
			
			if (res == 0 ){// todo ha salido a pedir de milhouse.
				printf("Value res: %d \n", res);
				sprintf(answer,"1/%s","Si");
				row = mysql_fetch_row(result);
				return 0;
			
			}
			
			if (res == -1){ // ya hay alguien registrado con ese nombre.
				sprintf(answer, "1/El nombre ya está en uso, prueba otro");
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
		
	sprintf(Query,"INSERT INTO userDB(username,password,email,wins) VALUES ('%s','%s','%s',0);",username,password,email);
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
	sprintf(Query,"SELECT COUNT(Games.ID_G) FROM(userDB, Games) WHERE userDB.username = '%s' AND userDB.ID=Games.ID_U",name);
	int err = mysql_query(conn,Query);
	if (err != 0)
	{
		printf("Failure trying to connect to DataBase %u %s\n",mysql_errno(conn),mysql_error(conn));
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
	}
	else{ //Recogemos el resultado de la consulta 
			result = mysql_store_result(conn);
			row = mysql_fetch_row(result);
		if ( row == NULL ){
			printf(" hasta aqu  funciona\n");
			sprintf(answer,"4/ this username is not registered yet");
		}
		else {
			printf("The winner is %s.\n",row[0]);
			sprintf(answer,"4/%s",row[0]);
		}
	}	
}


int DeleteUser(char username[20], MYSQL *conn)
{
	MYSQL_ROW row;
	MYSQL_RES resultado;
	
	char query[100];
	
	sprintf (query, "DELETE FROM userDB WHERE userDB.username = '%s'", username);
	int err = mysql_query (conn, query);
	
	if (err!=0)
	{
		printf ("Error al introducir datos la base %u %s.\n", mysql_errno(conn), mysql_error(conn));
		return -1;
	}
	else
	{
		printf("El usuario %s se ha eliminado con exito.\n", username);
		return 0;
	}
	
}
//CODIGO 5
//Funcion que nos devuelve los jugadores que menos partidas han ganado.
//Nos devuelve 5/partidas perdidas/Nombre usuario 1/ Nombre usuario 2 y así con todos los usuarios que han ganado las mismas pocas partidas
int chart(MYSQL *conn, char answer[512]){
	
	MYSQL_RES *result;
	MYSQL_ROW row;
	printf("He entrado\n");
	
	char Query[200];
	sprintf(Query,"SELECT userDB.username, userDB.wins FROM userDB WHERE wins = (SELECT MIN(wins) FROM userDB);");
	printf("Query: %s\n", Query);
	int err = mysql_query(conn,Query);
	printf("Hola %d\n",err);
	
	if(err!=0){
		printf("Failure trying to connect to DataBase %u %s\n", mysql_errno(conn),mysql_error(conn));
		return -1;
	}
	
	else{ //Recogemos el resultado de la consulta
		printf("Estoy en el else\n");
		result = mysql_store_result(conn);
		row = mysql_fetch_row(result);
		printf("Rows: %s, %s \n", row[0], row[1]);
		if(row ==NULL){
			sprintf(answer, "5/No se han obtenido datos");
			printf("Respuesta: %s\n", answer);
		}
		else{
			sprintf(answer, "5/%s/%s", row[1], row[0]);
			row = mysql_fetch_row(result);
			while( row != NULL){
				sprintf(answer, "%s/%s",answer, row[0]); // row[0] --> nombre usuario
				// row[1] --> nombre usuario
				row = mysql_fetch_row(result);
			}
			printf("%s\n",answer);
			return 0;
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
	int numform;
	int desconexion = 0;
	
	char Request[512];
	char Answer[512];
	char Nombre[20];
	
	char username[25];
	char password[25];
	char email[25];
	char notificacion[512];
	char rol[20];
	char picture[512];
	char contesta[512];
	
	int ret; // parametro que almaecena la informacion de los datos enviados por el usuario.
	
	conn = mysql_init(conn);
	
	if(conn == NULL){
		
		printf ("Failure Trying to innitialize: %u %s\n", mysql_errno(conn), mysql_error(conn));
		exit (1);
	}
	
	conn = mysql_real_connect(conn, "shiva2.upc.es", "root", "mysql", "MG1OYSL_DB", 0, NULL, 0);
	
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
		printf("Request: %s \n", Request);
		printf("%s\n", p);
		int code =  atoi (p);
		printf("Codigo: %d \n", code);
		p=strtok(NULL, "\0");
		if(p!=NULL){
			strcpy(Request, p);
			printf("Request: %s\n", Request);
		}
		
		// Una vez conocemos la peticion, operamos con la informacion recibida.
		
		//CODIGO 0
		//Cuando el codigo es 0, se trata de una peticion de desconexion.
		if (code== 0){
			p=strtok(Request, "/");
			strcpy(username, p);
			pthread_mutex_lock(&mutex);
			printf("Usuarios lista: %d \n", List.num);
			DeletefromList(&List, username);
			pthread_mutex_unlock(&mutex);
			sprintf(Answer, "0/");
			printf("Answer: %s \n", Answer);
			finish = 1;
		}
		//CODIGO 1
		//Cuando el codigo es "1", se trata de una peticion de inicio de sesion.
		else if (code ==1){ 
			
			p = strtok(Request, "/");
			//obtenemos el nombre de usuario 
			strcpy(username,p);
			
			p= strtok(NULL,"/");
			//obtenemos la password	
			strcpy(password,p);
			
			desconexion = LogIn(username,password,Answer,conn,sock_conn);
			strcpy(Nombre,username);
			printf("finish: %d\n",desconexion);

		
			}
	
		//CODIGO 2
		//Cuando el codigo es "2", se trata de una peticion de registro.
		else if (code ==2){
			
			p = strtok(Request, "/");
			//obtenemos el nombre de usuario 
			strcpy(username,p);
			
			p= strtok(NULL,"/");
			//obtenemos la password	
			strcpy(password,p);
			
			p= strtok(NULL,"/");
			//obtenemos el correo electronico
			strcpy(email,p);
			desconexion = SignIn(username,password,email,Answer,conn,sock_conn);
			printf("finish: %d\n",finish);

			
			}
		//CODIGO 3
		//Cuando el codigo es 3, se trata de una peticion para ver el numero de partidas jugadas por
		//el jugador cuyo nombres se recibe como parametro.
		else if (code == 3){
			
			p = strtok(Request,"/");
			//obtenemos el nombre que deseamos buscar en la base de datos 
			strcpy(username,p);
			//llamamos a la funcion
			GamesPlayed(username,Answer,conn, sock_conn);
			
		}
		//CODIGO 4
		//Cuando el codigo es 4, se trata de una peticion para ver el numero de partidas ganadas por 
		//el jugador cuyo nombre se recibe como paramentro.
		else if (code == 4){
			
			p = strtok(Request,"/");
			//obtenemos el nombre que deseamos buscar en la base de datos 
			strcpy(username,p);
			//llamamos a la funcion
			GamesWon(username,Answer,conn);
			
		}
		else if(code ==5){
			printf("Codigo 5 entrada \n");
			int resultado = chart(conn, Answer);
			if(resultado == -1){
				printf("No se pudo conectar con la base de datos");
				strcpy(Answer,"5/-1");
			}
		}
		
		//CODIGO 6
		//Codigo que gestiona las invitaciones a jugadores y la 
		//busqueda de partidas libres para generar una.
		else if (code==6){
			idp = FreeGame(Table); //Buscamos una partida libre
			if (idp == -1)
				break;
			else{
			printf("ID Partida: %d \n", idp);
			pthread_mutex_lock(&mutex);
			jugadores = jugadores+1;
			AddtoGame(Table, Nombre, idp); //A adimos a la partida al cliente que est  invitando
			pthread_mutex_unlock(&mutex);
			//Realizamos la invitacion
			}
			p=strtok(Request, "/");
			while (p!=NULL){
				strcpy(username, p);
				printf("Usuario: %s\n", username);
				int pos = GivemePosition(&List, username); //Obtenemos la posici n en la lista del primer usuario1
				printf("Posicion: %d \n", pos);
				if(pos==-1){
					sprintf(notificacion, "7/No estan conectados");
					write(sock_conn, notificacion, strlen(notificacion));
					break;
				}
				else{
	
					sprintf(notificacion, "7/%s", username); //Mensaje que enviaremos al cliente por si quiere aceptar la invitacion
					printf("Notificacion: %s\n", notificacion);

					write(List.online[pos].socket,notificacion,strlen(notificacion));

				}

				p=strtok(NULL, "/"); //Ahora cogemos al siguiente usuario y hacemos lo mismo
			}
		  
		}
		
		//CODIGO 7 
		//Codigo que sirve para aceptar la solicitud de invitacion y iniciar la partida.
		else if (code == 7){ 

			char decision[512];
			p = strtok(Request, "/");
			strcpy(decision, p);
			printf("Decision: %s\n", decision);
			printf("ID PARTIDA: %d \n", idp);
			char usuario[512];
			p=strtok(NULL, "/");
			strcpy(usuario, p);
			printf("Usuario: %s\n", usuario);
			if (strcmp(decision,"Si")==0){
				pthread_mutex_lock(&mutex);
				AddtoGame(Table, usuario, idp);
				pthread_mutex_unlock(&mutex);
			}
			pthread_mutex_lock(&mutex);
			jugadores=jugadores+1;
			pthread_mutex_unlock(&mutex);
			printf("Jugadores: %d\n", jugadores);
			if (Table[idp].playersnum==3){
				sprintf(notificacion, "8/Se juega la partida");
				for(int u =0;u<3;u++){
					Table[idp].user[u].MF = 0;
					printf("Notificacion: %s\n", notificacion);
					write(Table[idp].user[u].socket, notificacion, strlen(notificacion));
				}
			}
			else{
				printf("Jugadores: %d \n", jugadores);
				if((Table[idp].playersnum>1)&&(jugadores==3)){
					printf("JUgadores: %d \n", jugadores);
					pthread_mutex_lock(&mutex);
					DeleteGame(Table, idp); //Como la partida no se jugará la eliminamos de la tabla
					jugadores = 0;
					pthread_mutex_unlock(&mutex);
					sprintf(notificacion, "8/No se juega la partida");
					printf("Notificacion: %s \n", notificacion);
					
					for (int u=0; u<3; u++){
						printf("Notificacion: %s\n", notificacion);
						write(List.online[u].socket,notificacion,strlen(notificacion));;
					}
				}
				else{
					printf("Esperamos a que acepten los demas jugadores. \n");
				}
			}
		}
		//CODIGO 8 (APTO PARA 1 PARTIDA SIMULTANEA).
		//Codigo que gestiona el chat.
		//Se recibe 8/mensaje.
		else if (code == 8)
		{
			p=strtok(Request, "/");
			numform=atoi(p);
			char mensaje[512];
			p = strtok(NULL, "/");

			strcpy(mensaje, p);
			sprintf(notificacion, "9/%d/%s", numform, mensaje);
			printf("Num Form chat: %d\n", numform);

			for (int u = 0; u < 3; u++) {
				printf("Notificacion: %s \n", notificacion);
				write(Table[idp].user[u].socket, notificacion, strlen(notificacion));
			}
		}
		//CODIGO 9
		//MIRADA FULMINANTE Recibe: 9/num/lacayo/username
		//					Envia: 10/num/lacayo
		else if (code == 9){
			
			p = strtok(Request, "/");
			numform = atoi(p);
			int PartidaAcabada = 0;
			int pos;
			p=strtok(NULL,"/");
			strcpy(username,p);
			int socket = GivemeSocket(&List, username);
			
			for(int i = 0; i<3;i++){
				if(strcmp(Table[idp].user[i].username,username)==0){
					Table[idp].user[i].MF++;
					if(Table[idp].user[i].MF == 3){
						PartidaAcabada = 1;
						pos = i;
					}
					else{
						sprintf(Answer,"10/0/%d/%d",numform, Table[idp].user[i].MF);
						printf("Answer: %s \n", Answer);
						write(socket,Answer,strlen(Answer));
					}
				}
			}
			if(PartidaAcabada == 1){
				char loser[20];
				strcpy(loser,Table[idp].user[pos].username);
				for(int j =0; j<3; j++){
					sprintf(notificacion, "12/0/%d/%s/%s", numform, Table[idp].user[j].rol, loser);
					printf("Notificacion %s \n", notificacion);
					write(Table[idp].user[j].socket, notificacion, strlen(notificacion));
				}
				FinishGame(Table, idp, loser, conn, sock_conn);
			}
		}
		//CODIGO 10
		//CAMBIAR TURNO Recibe: 10/?
		//				Envia: 11/?
		else if (code == 10){
			p=strtok(Request, "/");
			numform = atoi(p);
			p = strtok(NULL, "/");
			int l = atoi(p);
			if (l ==1){
				l=2;
			}
			else{
				l=1;
			}
			for (int i=0; i<3; i++){
				sprintf(notificacion, "11/0/%d/%d/%s", numform, l,  Table[idp].user[i].rol);
				printf("Notificacion: %s\n", notificacion);
				write(Table[idp].user[i].socket, notificacion, strlen(notificacion));
				strcpy(notificacion, "");
			}
		}
		
		//CODIGO 11
		//CODIGO QUE ELIMINA UN USUARIO DE LA BASE DE DATOS
		else if (code ==11){
			strcpy(username, strtok(Request, "/"));
			pthread_mutex_lock(&mutex);
			DeletefromList(&List, username);
			pthread_mutex_unlock(&mutex);
			int error = DeleteUser(username, conn);
			if ( error == 0){
				sprintf(Answer, "15/%d", error);
				printf("Answer: %s \n", Answer);
				desconexion = 1;
			}
			else{
				sprintf(Answer, "15/%d", error);
				printf("Answer: %s \n", Answer);
			}
		}
		
		//CODIGO 12 
		//CODIGO QUE ACTUALIZA LA INFORMACION DE USUARIO PARA AÑADIRLE UN ROL
		// Recibe: 12/numform/rol/username
		else if (code == 12) 
		{
			p=strtok(Request, "/");
			numform = atoi(p);
			strcpy(rol,strtok(NULL, "/"));
			printf("%s\n", rol);
			strcpy(username,strtok(NULL, "/"));
			printf("%s\n", username);
			
			int posicion = GivemePositionTabla(Table, username, idp);
			strcpy(Table[idp].user[posicion].rol,rol);
			printf("Rol del usuario: %s\n", Table[idp].user[posicion].rol);
			sprintf(notificacion, "14/%d/%s", numform, rol);
			for (int i = 0; i<3; i++){
				printf("Notificacion: %s\n", notificacion);
				write(Table[idp].user[i].socket, notificacion, strlen(notificacion));
			}
		}
		
		//CODIGO 13 
		//Codigo que reenvia las cartas que se estan usando.
		//Recibe: 13/numform/nomcarta
		else if (code==13){
			p=strtok(Request, "/");
			numform = atoi(p);
			printf("Num form %d\n", numform);
			strcpy(picture, strtok(NULL, "/"));
			
			for (int i=0; i<3; i++){
				sprintf(notificacion, "13/0/%d/%s/%s", numform, Table[idp].user[i].rol, picture);
				printf("Notificacion: %s\n", notificacion);
				write(Table[idp].user[i].socket, notificacion, strlen(notificacion));
				strcpy(notificacion, "");
			}
			
		}
		
		
		//Funcion que envia respuestas.
		if((code==0)||(code==1)||(code == 2) || (code == 3)|| (code == 4) || (code==5) || (code ==11))
		{
			printf("Answer:%s\n",Answer);
			// la respuesta
			write(sock_conn,Answer,strlen(Answer));
		}
		
		//Para desconectarnos en el cliente una vez hemos eliminado nuestro usuario
		if ( desconexion ==1){
			strcpy(contesta, "0/");
			printf("Answer desconexion: %s \n",contesta);
			finish=1;
			write(sock_conn, contesta, strlen(contesta));
		}

		//funcion que envia notificacon a todos los usuarios.
		if ((code == 0) || (code == 1) || (code ==11)){
			
			char users[512];
			
			pthread_mutex_lock(&mutex);
			GivemeOnlineusers(&List,users);
			printf("users: %s\n ",users);
			pthread_mutex_unlock(&mutex);
			
			//Notificar los usuarios conectados.
			/*users[strlen(users)-1] = '\0';*/

			sprintf(notificacion, "6/%s", users);
			
			
			//Se tiene que enviar a todos los clientes conectados
		
			for (int u=0; u < List.num ;u++){
				printf("Notificacion: %s \n", notificacion);
				write(List.online[u].socket,notificacion,strlen(notificacion));
				printf("Iteraciones: %d \n", u);
			}

		}

	
	}
	// Se acabo el servicio para este cliente
	close(sock_conn); 
	
}




int main(int argc, char *argv[]){
	
	int sock_conn, sock_listen;
	struct sockaddr_in serv_adr;


	// INICIALITZACIONS
	// Abrimos el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0){
		printf("Fail creating the socket");
	}
	// Fem el bind al port
	
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	
	// establecemos el puerto de escucha
	serv_adr.sin_port = htons(50004);
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
		else{ 
			printf("There no more available sockets");
		}
	}
	//Generamos un bucle de espera para que no finalize ning n thread hasta haber atendido al cliente

	
}
