namespace Planos
{
    public interface ITerraformador<T>
    {
        void Terraformar(Plano<T> plano);
    }
}
