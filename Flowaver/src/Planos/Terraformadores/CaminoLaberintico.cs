using System;

namespace Flowaver.Planos
{
    internal class CaminoLaberintico<T> : ITerraformador<T>
    {
        private T piso;
        private int xIni;
        private int yIni;
        private int largo;
        private Random direcciones;

        public CaminoLaberintico(int xIni, int yIni, int largo, T piso, Random direcciones)
        {
            this.xIni  = xIni;
            this.yIni  = yIni;
            this.largo = largo;
            this.piso = piso;
            this.direcciones = direcciones;
        }

        public void Terraformar(Plano<T> plano)
        {
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
        }

        private int deltaX(int direccion)
        {
            direccion = direccion % 4;
            if (direccion == 0)
                return 1;
            if (direccion == 2 || direccion == -2)
                return -1;
            return 0;
        }
        private int deltaY(int direccion)
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
