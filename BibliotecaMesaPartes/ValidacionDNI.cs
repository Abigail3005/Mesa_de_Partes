
using System;

namespace BibliotecaMesaPartes
{
    public static class ValidacionDNI
    {
        // VALIDAR DNI
        public static bool EsDniValido(string dni)
        {
            if (string.IsNullOrWhiteSpace(dni) || dni.Length != 8)
                return false;

            foreach (char c in dni)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }
    }
}
