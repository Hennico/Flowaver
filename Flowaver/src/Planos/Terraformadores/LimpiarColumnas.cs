namespace Flowaver.Planos
{
    class LimpiarColumnas<T> : ITerraformador<T>
    {
        private T piso;
        private T pared;

        public LimpiarColumnas(T piso, T pared)
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
    }
}
