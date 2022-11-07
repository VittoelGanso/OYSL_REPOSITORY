using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graficos_juego_OYSL
{
    class Baraja_Cartas : Cartas
    {
        const int Num_cartas = 117; //Numero total de cartas
        private Cartas[] baraja; //matriz de todas las cartas

        public Baraja_Cartas()
        {
            baraja = new Cartas[Num_cartas]; //Inicializamos la baraja de cartas
        }

        //Tenemos que saber cuál es nuestra baraja actual de cartas
        public Cartas[] getBaraja { get { return baraja; } }

        //Ahora creamos la baraja de cartas
        public void SetUpBaraja()
        {
            int i = 0;
            foreach (Valor v in Enum.GetValues(typeof(Valor)))
            {
                baraja[i] = new Cartas { MyValor = v }; //Ponemos el valor de las distintas cartas
                //Aquí las cartas no están mezcladas
                i++;
            }
            MezclarCartas();
        }

        //Ahora debemos mezclar las cartas
        public void MezclarCartas()
        {
            Random random = new Random();
            Cartas temp; //Necesitamos una carta temporaria

            //Mezclamos unas 1000 veces para que se mezcle correctamente
            for (int i= 0; i < 100; i++)
            {
                for(int j = 0; j<Num_cartas; j++)
                {
                    //Cojemos dos cartas y las intercambiamos
                    int IndexCarta = random.Next(117); //Mezclamos las 117 cartas
                    temp = baraja[i];
                    baraja[i] = baraja[IndexCarta];
                    baraja[IndexCarta] = temp;
                }
            }
        }
    }
}
