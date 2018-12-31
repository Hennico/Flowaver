using System;
using System.Collections.Generic;

namespace Flowaver.Planos
{
    public class Plano<T>
    {
        public int Ancho { get { return grilla.Length; } }
        public int Alto { get { return grilla[0].Length; } }

        private List<T>[][] grilla;

        public Plano(int ancho, int alto)
        {
            if (alto < 1) throw new ArgumentOutOfRangeException("El valor {0} no es un alto valido.");
            if (ancho < 1) throw new ArgumentOutOfRangeException("El valor {0} no es un ancho valido.");

            grilla = new List<T>[ancho][];
            for (int i = 0; i < ancho; i++)
                grilla[i] = new List<T>[alto];
            for (int i = 0; i < ancho * alto; i++)
                grilla[i % ancho][i / ancho] = new List<T>();
        }

        public T[] this[int x, int y]
        {
            get
            {
                if (PosicionValida(x, y))
                    return grilla[x][y].ToArray();
                return new T[0];
            }
            set
            {
                grilla[x][y] = new List<T>(value ?? new T[0]);
            }
        }

        public bool LugarVacio(int x, int y)
        {
            return PosicionValida(x, y) ? (grilla[x][y].Count == 0) : false;
        }

        public bool LugarOcupado(int x, int y, T ocupador)
        {
            return this[x, y].Length > 0 ? this[x, y][0].Equals(ocupador) : false;
        }

        public void Agregar(int x, int y, T valor)
        {
            if (PosicionValida(x, y))
                grilla[x][y].Add(valor);
        }

        public void Remover(int x, int y, T valor)
        {
            if (PosicionValida(x, y))
                grilla[x][y].Remove(valor);
        }
        public void Remover(int x, int y, T valor, Func<T,T,bool> comparador)
        {
            if (PosicionValida(x, y))
            {
                List<T> actual = new List<T>(grilla[x][y]);
                grilla[x][y] = new List<T>();
                foreach (T item in actual)
                {
                    if (!comparador(valor, item))
                    {
                        grilla[x][y].Add(item);
                        break;
                    }
                }
            }
        }

        public bool PosicionValida(int x, int y)
        {
            return x < Ancho && x >= 0 && y < Alto && y >= 0;
        }
    }
}