using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graficos_juego_OYSL
{
    class BarajaAccion: Cartas
    {
        List<Cartas> vector = new List<Cartas>();
        
        public void AñadirCarta(Cartas c)
        {
            vector.Add(c);
        }



    }
}
