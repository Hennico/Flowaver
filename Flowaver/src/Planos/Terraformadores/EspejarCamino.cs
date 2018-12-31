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
    }
}
