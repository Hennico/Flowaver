using System;
using Planos;

namespace Flowaver
{
    class Program
    {
        static void Main(string[] args)
        {
            int tamX = 55;
            int tamY = 31;
            int largo = 200;
            Console.WindowWidth = tamX + 2;
            Console.WindowHeight = tamY + 3;

            Plano<char> mapa = PlanoFactory<char>.GenerarLaberintoMirror(tamX, tamY, tamX / 2, tamY / 2, largo, '·', '#');

            ConsoleKey tecla = ConsoleKey.Pa1;
            int[] posicion = new int[] { tamX / 2, tamY /2 };
            mapa.Agregar(posicion[0], posicion[1],'@');
            do
            {
                switch (tecla)
                {
                    case ConsoleKey.UpArrow   : MoverPJ(mapa, posicion,  0, -1); break;
                    case ConsoleKey.DownArrow : MoverPJ(mapa, posicion,  0, +1); break;
                    case ConsoleKey.LeftArrow : MoverPJ(mapa, posicion, -1,  0); break;
                    case ConsoleKey.RightArrow: MoverPJ(mapa, posicion, +1,  0); break;
                }

                Console.Clear();
                showMapa(mapa);
                tecla = Console.ReadKey().Key;
            }
            while (tecla != ConsoleKey.Spacebar);
        }

        public static void MoverPJ(Plano<char> mapa, int[] posicion, int deltaX, int deltaY)
        {
            int j = 0;
            char[] contenidos = mapa[posicion[0], posicion[1]];
            char[] nuevosContenidos = new char[contenidos.Length - 1];
            for (int i = 0; i < contenidos.Length; i++)
                if (contenidos[i] != '@')
                    nuevosContenidos[j++] = contenidos[i];
            
            mapa[posicion[0], posicion[1]] = nuevosContenidos;
            posicion[0] += deltaX;
            posicion[1] += deltaY;
            mapa.Agregar(posicion[0], posicion[1], '@');
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
