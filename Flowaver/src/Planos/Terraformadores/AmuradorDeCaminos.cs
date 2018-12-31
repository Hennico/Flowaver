namespace Flowaver.Planos
{
    public class AmuradorDeCaminos<T> : ITerraformador<T>
    {
        private T piso;
        private T pared;

        public AmuradorDeCaminos(T piso, T pared)
        {
            this.piso = piso;
            this.pared = pared;
        }

        public void Terraformar(Plano<T> plano)
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
    }
}
