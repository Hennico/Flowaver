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

            do
            {
                Console.Clear();
                //Plano<char> mapa = PlanoFactory<char>.GenerarLaberinto(tamX, tamY, tamX / 2, tamY / 2, largo, '·', '#');
                Plano<char> mapa = PlanoFactory<char>.GenerarLaberintoMirror(tamX, tamY, tamX / 2, tamY / 2, largo, '·', '#');
                mapa[tamX / 2, tamY / 2] = new char[] { '@' };
                showMapa(mapa);
            }
            while (Console.ReadKey().Key != ConsoleKey.Spacebar);
        }

        public static void showMapa(Plano<char> mapa)
        {
            for (int i = 0; i < mapa.Ancho + 2; i++) Console.Write("-");
            Console.WriteLine();

            for (int j = 0; j < mapa.Alto; j++)
            {
                Console.Write("|");
                for (int i = 0; i < mapa.Ancho; i++)
                    Console.Write(mapa[i, j].Length > 0 ? mapa[i, j][0] : ' ');
                Console.WriteLine("|");
            }

            for (int i = 0; i < mapa.Ancho + 2; i++) Console.Write("-");
            Console.WriteLine();
        }
    }
}
