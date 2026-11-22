
using System;

namespace BibliotecaMesaPartes
{
    public static class UtilidadesExpediente
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

        // GENERAR CODIGO DEL EXPEDIENTE
        public static string GenerarCodigo(int totalRegistros)
        {
            int numero = totalRegistros + 1;
            int anioActual = DateTime.Now.Year;

            return "EXP-" + anioActual + "-" + numero.ToString("D3");
        }
    }
}
