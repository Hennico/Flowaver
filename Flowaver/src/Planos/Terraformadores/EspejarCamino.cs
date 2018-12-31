namespace Flowaver.Planos
{
    class EspejarCamino<T> : ITerraformador<T>
    {
        private T piso;
        private bool vertical;
        private bool horizontal;

        public EspejarCamino(T piso, bool horizontal, bool vertical)
        {
            this.piso = piso;
            this.vertical = vertical;
            this.horizontal = horizontal;
        }

        public void Terraformar(Plano<T> plano)
        {
            int x,y;
            int izq = plano.Ancho;
            int der = 0;
            int arr = plano.Alto;
            int aba = 0;

            for (x = 0; x < plano.Ancho; x++)
            for (y = 0; y < plano.Alto; y++)
            {
                if (plano.LugarOcupado(x, y, piso))
                {
                    izq = izq > x ? x : izq;
                    arr = arr > y ? y : arr;
                    der = der < x ? x : der;
                    aba = aba < y ? y : aba;
                }
            }
            int xPibote = plano.Ancho / 2 > der ? der : (plano.Ancho / 2 < izq ? izq : plano.Ancho / 2);
            int yPibote = plano.Alto  / 2 > aba ? aba : (plano.Alto  / 2 < arr ? arr : plano.Alto  / 2);
            
            for (x = 0; x < plano.Ancho; x++)
            {
                for (y = 0; y < plano.Alto; y++)
                {
                    if (plano.LugarVacio(x, y))
                    {
                        int xNew = horizontal ? xPibote - (x - xPibote) : x;
                        int yNew = vertical   ? yPibote - (y - yPibote) : y;
                        if (plano.LugarOcupado(xNew, yNew, piso))
                            plano.Agregar(x, y, piso);
                    }
                }
            }
        }
    }
}
