using System;
using Flowaver.Planos;
using Flowaver.DataMundo;

namespace Flowaver
{
    class Program
    {
        static void Main(string[] args)
        {
            Random mapaRnd = new Random();
            int tamX = 55;
            int tamY = 31;
            int largo = 200;

            char piso = '·';
            char pared = '#';
            char personaje = '@';

            Console.WindowWidth = tamX + 2;
            Console.WindowHeight = tamY + 3;

            int xIni = tamX/2;
            int yIni = mapaRnd.Next(tamY - 2) + 1;
            Plano<char> mapa = PlanoFactory<char>.GenerarLaberintoMirror(tamX, tamY, xIni, yIni, largo, mapaRnd, piso, pared);

            ConsoleKey tecla = ConsoleKey.Pa1;
            Posicion posicion = new Posicion(xIni, yIni);
            mapa.Agregar(posicion.x, posicion.y,personaje);
            do
            {
                switch (tecla)
                {
                    case ConsoleKey.UpArrow   : MoverPJ(mapa, personaje, pared, posicion,  0, -1); break;
                    case ConsoleKey.DownArrow : MoverPJ(mapa, personaje, pared, posicion,  0, +1); break;
                    case ConsoleKey.LeftArrow : MoverPJ(mapa, personaje, pared, posicion, -1,  0); break;
                    case ConsoleKey.RightArrow: MoverPJ(mapa, personaje, pared, posicion, +1,  0); break;

                    case ConsoleKey.R:
                        xIni = tamX / 2;
                        yIni = mapaRnd.Next(tamY - 2) + 1;
                        mapa = PlanoFactory<char>.GenerarLaberintoMirror(tamX, tamY, xIni, yIni, largo, mapaRnd, piso, pared);
                        posicion = new Posicion(xIni, yIni);
                        mapa.Agregar(posicion.x, posicion.y, personaje);
                        break;
                }

                Console.SetCursorPosition(0,0);
                showMapa(mapa);
                tecla = Console.ReadKey().Key;
            }
            while (tecla != ConsoleKey.Spacebar);
        }

        public static void MoverPJ(Plano<char> mapa, char personaje, char pared, Posicion posicion, int deltaX, int deltaY)
        {
            if (!mapa.LugarOcupado(posicion.x + deltaX, posicion.y + deltaY, pared))
            {
                mapa.Remover(posicion.x, posicion.y, personaje);
                posicion.x += deltaX;
                posicion.y += deltaY;
                mapa.Agregar(posicion.x, posicion.y, personaje);
            }
        }

        public static void showMapa(Plano<char> mapa)
        {
            for (int i = 0; i < mapa.Ancho + 2; i++) Console.Write("-");
            Console.WriteLine();

            for (int j = 0; j < mapa.Alto; j++)
            {
                Console.Write("|");
                for (int i = 0; i < mapa.Ancho; i++)
                    Console.Write(mapa[i, j].Length > 0 ? mapa[i, j][mapa[i, j].Length-1] : ' ');
                Console.WriteLine("|");
            }

            for (int i = 0; i < mapa.Ancho + 2; i++) Console.Write("-");
            Console.WriteLine();
        }
    }
}
