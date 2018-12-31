using System;

namespace Flowaver.Planos
{
    public static class PlanoFactory<T>
    {
        public static Plano<T> GenerarLaberinto(int ancho, int alto, int xIni, int yIni, int largo, T piso, T pared)
        {
            Plano<T> plano = new Plano<T>(ancho, alto);

            Random direcciones = new Random();
            int direccion = direcciones.Next(4);
            int x = xIni;
            int y = yIni;
            for (int i = 0; i < largo / 2; i++)
            {
                plano.Agregar(x, y, piso);
                x += deltaX(direccion);
                y += deltaY(direccion);

                if (!plano.PosicionValida(x + 2 * deltaX(direccion), y + 2 * deltaY(direccion)))
                {
                    direccion += 2;
                    x += deltaX(direccion);
                    y += deltaY(direccion);
                }

                plano.Agregar(x, y, piso);
                x += deltaX(direccion);
                y += deltaY(direccion);
                direccion = direcciones.Next(4);
            }

            AgregarParedes(plano, piso, pared);
            LimpiarColumnas(plano, piso, pared);

            return plano;
        }

        public static Plano<T> GenerarLaberintoMirror(int ancho, int alto, int xIni, int yIni, int largo, Random direcciones, T piso, T pared)
        {
            Plano<T> plano = new Plano<T>(ancho, alto);

            int direccion = direcciones.Next(4);
            int x = xIni;
            int y = yIni;
            for (int i = 0; i < largo / 4; i++)
            {
                plano.Agregar(x, y, piso);
                x += deltaX(direccion);
                y += deltaY(direccion);

                if (!plano.PosicionValida(x + 2 * deltaX(direccion), y + 2 * deltaY(direccion)))
                {
                    direccion += 2;
                    x += deltaX(direccion);
                    y += deltaY(direccion);
                }

                plano.Agregar(x, y, piso);
                x += deltaX(direccion);
                y += deltaY(direccion);
                direccion = direcciones.Next(4);
            }
            CopiarEnEspejo(plano, piso, true, false);
            CopiarEnEspejo(plano, piso, false, true);

            AgregarParedes(plano, piso, pared);
            //LimpiarColumnas(plano, piso, pared);

            return plano;
        }

        private static void CopiarEnEspejo(Plano<T> plano, T piso, bool horizontal, bool vertical)
        {
            for (int x = 0; x < plano.Ancho; x++)
            {
                for (int y = 0; y < plano.Alto; y++)
                {
                    if (plano.LugarVacio(x, y))
                    {
                        int xNew = horizontal ? plano.Ancho / 2 - (x - plano.Ancho / 2) : x;
                        int yNew = vertical ? plano.Alto / 2 - (y - plano.Alto / 2) : y;
                        if (plano.LugarOcupado(xNew, yNew, piso))
                            plano.Agregar(x, y, piso);
                    }
                }
            }
        }

        private static void AgregarParedes(Plano<T> plano, T piso, T pared)
        {
            for (int x = 0; x < plano.Ancho; x++)
            {
                for (int y = 0; y < plano.Alto; y++)
                {
                    if (plano.LugarVacio(x, y))
                    {
                        bool conPiso = false;

                        for (int varX = -1; varX < 2 && !conPiso; varX++)
                            for (int varY = -1; varY < 2 && !conPiso; varY++)
                                conPiso = plano.LugarOcupado(x + varX, y + varY, piso);

                        if (conPiso)
                            plano.Agregar(x, y, pared);
                    }
                }
            }
        }

        private static void LimpiarColumnas(Plano<T> plano, T piso, T pared)
        {
            for (int x = 0; x < plano.Ancho; x++)
            {
                for (int y = 0; y < plano.Alto; y++)
                {
                    if (plano.LugarOcupado(x, y, pared))
                    {
                        bool todoPiso = true;

                        for (int varX = -1; varX < 2 && todoPiso; varX++)
                            for (int varY = -1; varY < 2 && todoPiso; varY++)
                                todoPiso = varX == 0 && varY == 0 || plano.LugarOcupado(x + varX, y + varY, piso);

                        if (todoPiso)
                            plano[x, y] = new T[] { piso };
                    }
                }
            }
        }

        private static int deltaX(int direccion)
        {
            direccion = direccion % 4;
            if (direccion == 0)
                return 1;
            if (direccion == 2 || direccion == -2)
                return -1;
            return 0;
        }
        private static int deltaY(int direccion)
        {
            direccion = direccion % 4;
            if (direccion == 1 || direccion == -3)
                return 1;
            if (direccion == 3 || direccion == -1)
                return -1;
            return 0;
        }
    }
}