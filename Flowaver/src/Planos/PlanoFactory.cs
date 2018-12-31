using System;

namespace Flowaver.Planos
{
    public static class PlanoFactory<T>
    {
        public static Plano<T> GenerarLaberinto(int ancho, int alto, int xIni, int yIni, int largo, Random direcciones, T piso, T pared)
        {
            Plano<T> plano = new Plano<T>(ancho, alto);
            new CaminoLaberintico<T>(xIni, yIni, largo, piso, direcciones).Terraformar(plano);
            new AmuradorDeCaminos<T>(piso, pared).Terraformar(plano);
            new LimpiarColumnas<T>(piso, pared).Terraformar(plano);
            return plano;
        }

        public static Plano<T> GenerarLaberintoMirror(int ancho, int alto, int xIni, int yIni, int largo, Random direcciones, T piso, T pared)
        {
            Plano<T> plano = new Plano<T>(ancho, alto);
            new CaminoLaberintico<T>(xIni, yIni, largo, piso, direcciones).Terraformar(plano);
            new EspejarCamino<T>(piso, true, false).Terraformar(plano);
            new EspejarCamino<T>(piso, false, true).Terraformar(plano);
            new AmuradorDeCaminos<T>(piso, pared).Terraformar(plano);
            return plano;
        }
    }
}