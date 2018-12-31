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

        }
    }
}
