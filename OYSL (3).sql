DROP DATABASE IF EXISTS OYSL_DB;
CREATE DATABASE OYSL_DB;

USE OYSL_DB;

CREATE TABLE userDB(
	ID INT NOT NULL AUTO_INCREMENT,
	username VARCHAR(60),
	Password VARCHAR(60),
	email    VARCHAR(60),
	wins INT NOT NULL,
	PRIMARY KEY (ID)
)ENGINE=InnoDB;

CREATE TABLE Game(
	ID INT NOT NULL AUTO_INCREMENT,
	date VARCHAR(10),	/*YYYY-MM-DD*/
	hour VARCHAR(10),	/*HH: MM: SS*/
	duration INT,		/*Time in minutes*/
	winner VARCHAR(60),
	PRIMARY KEY (ID)
)ENGINE=InnoDB;

CREATE TABLE Games(
	ID_U INT,
	ID_G INT,
	score INT,
	FOREIGN KEY (ID_U) REFERENCES userDB(ID),
	FOREIGN KEY (ID_G) REFERENCES Game(ID)
)ENGINE=InnoDB;

INSERT INTO userDB(username, Password,email, wins) VALUES ('VittoelGanso','12','VittoelGanso@gmail.com',2); 
INSERT INTO userDB(username, Password,email, wins) VALUES ('Mario','34','Mario@gmail.com',2);
INSERT INTO userDB(username, Password,email, wins) VALUES ('Judit','56','judit@gmail.com',1);
INSERT INTO userDB(username, Password,email, wins) VALUES ('Miguel','78','miguel@gmail.com',0);
INSERT INTO userDB(username, Password,email, wins) VALUES ('Eda','51','eda@gmail.com',2);
INSERT INTO userDB(username, Password,email, wins) VALUES ('Pablo','88','pablo@gmail.com',0);
INSERT INTO userDB(username, Password,email, wins) VALUES ('Aitana','98','aitana@gmail.com',1);
INSERT INTO userDB(username, Password,email, wins) VALUES ('Aida','345','aida@gmail.com',0);

INSERT INTO Game(date, hour, duration, winner) VALUES ('2008-07-01','00: 01: 59',120,'VittoelGanso');
INSERT INTO Game(date, hour, duration, winner) VALUES ('2010-08-22','10: 59: 59',60,'Mario');
INSERT INTO Game(date, hour, duration, winner) VALUES ('2005-05-05','22: 30: 15',14,'VittoelGanso');
INSERT INTO Game(date, hour, duration, winner) VALUES ('2020-07-01','00: 17: 00',40,'Mario');
INSERT INTO Game(date, hour, duration, winner) VALUES ('2008-07-15','08: 00: 00',120,'Eda');
INSERT INTO Game(date, hour, duration, winner) VALUES ('2009-01-01','20: 00: 00',120,'Jordi');

INSERT INTO Games(ID_U, ID_G, score1,score2) VALUES (5,1,0);
INSERT INTO Games(ID_U, ID_G, score1,score2) VALUES (1,1,3);
INSERT INTO Games(ID_U, ID_G, score1,score2) VALUES (7,2,3);
INSERT INTO Games(ID_U, ID_G, score1,score2) VALUES (2,2,0);
INSERT INTO Games(ID_U, ID_G, score1,score2) VALUES (1,3,0);
INSERT INTO Games(ID_U, ID_G, score1,score2) VALUES (8,3,3);
INSERT INTO Games(ID_U, ID_G, score1,score2) VALUES (3,4,0);
INSERT INTO Games(ID_U, ID_G, score1,score2) VALUES (7,4,0);
INSERT INTO Games(ID_U, ID_G, score1,score2) VALUES (7,5,0);
INSERT INTO Games(ID_U, ID_G, score1,score2) VALUES (5,5,3);
INSERT INTO Games(ID_U, ID_G, score1,score2) VALUES (2,6,0);
INSERT INTO Games(ID_U, ID_G, score1,score2) VALUES (6,6,3);

/* DAME EL IDENTIFICADOR Y FECHA DE LAS PARTIDAS GANADAS POR X JUGADOR */
SELECT Game.date,Game.ID FROM Game
WHERE Game.winner='VittoelGanso';

/*MUESTRAME UNA TABLA DE PUNTUACIONES DONDE SE MUESTRE A LOS USUARIOS REGISTRADOS Y LAS PARTIDAS QUE HAN GANADO*/
SELECT userDB.username,userDB.wins FROM userDB ORDER BY wins DESC;

/*MUESTRAME TODAS LAS PARTIDAS JUGADAS POR X JUGADOR*/
SELECT *from (userDB, Games) WHERE userDB.username = 'VittoelGanso' AND userDB.ID = Games.ID_U;

