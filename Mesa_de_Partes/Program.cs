using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MesaDePartesDigital
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] codigos = new string[100];
            string[] DNI = new string[100];
            string[] remitentes = new string[100];
            string[] tiposDoc = new string[100];
            string[] asuntos = new string[100];
            string[] estados = new string[100];

            int totalRegistros = 0;
            int opcion;

            do
            {
                Console.Clear();

                Console.WriteLine("================================");
                Console.WriteLine("  MESA DE PARTES DIGITAL - UNC  ");
                Console.WriteLine("================================");
                Console.WriteLine("1. Registrar expediente");
                Console.WriteLine("2. Mostrar expedientes");
                Console.WriteLine("3. Buscar expediente");
                Console.WriteLine("4. Ordenar expedientes");
                Console.WriteLine("5. Estado del expediente");
                Console.WriteLine("0. Salir del sistema");
                Console.WriteLine("=========================================");
                Console.WriteLine("Expedientes registrados: " + totalRegistros);
                Console.Write("Seleccione una opción: ");

                opcion = Convert.ToInt32(Console.ReadLine());

                switch (opcion)
                {
                    case 1:
                        RegistrarExpediente
                        (codigos, DNI, remitentes, tiposDoc, asuntos, estados, ref totalRegistros);
                        break;

                    case 2:
                        MostrarExpedientes
                        (codigos, DNI, remitentes, tiposDoc, asuntos, estados, totalRegistros);
                        break;

                    case 3:
                        BuscarExpediente
                        (codigos, DNI, remitentes, tiposDoc, asuntos, estados, totalRegistros);
                        break;

                    case 4:
                        OrdenarExpedientes
                        (codigos, DNI, remitentes, tiposDoc, asuntos, estados, totalRegistros);
                        break;

                    case 5:
                        ActualizarEstado
                        (codigos, estados, totalRegistros);
                        break;

                    case 0:
                        Console.WriteLine();
                        Console.WriteLine("Cerrando sesión del sistema...");
                        break;

                    default:
                        Console.WriteLine();
                        Console.WriteLine("Opción inválida.");
                        break;
                }

                if (opcion != 0)
                {
                    Console.WriteLine();
                    Console.WriteLine("Presione cualquier tecla para volver al menú...");
                    Console.ReadKey();
                }

            } while (opcion != 0);
        }

        static void RegistrarExpediente(string[] codigos, string[] DNI, string[] remitentes, string[] tiposDoc, string[] asuntos, string[] estados, ref int totalRegistros)
        {
Console.WriteLine();
            Console.WriteLine("---- REGISTRAR NUEVO EXPEDIENTE ----");
            if (totalRegistros >= codigos.Length)
            {
                Console.WriteLine("No se pueden registrar más expedientes. Capacidad máxima alcanzada.");
                return;
            }
            string dniIngresado;
            bool dniValido;
            do
            {
                Console.Write("Ingrese DNI del remitente (8 dígitos): ");
                dniIngresado = Console.ReadLine();
                dniValido = EsDniValido(dniIngresado);

                if (!dniValido)
                {
                    Console.WriteLine("DNI inválido. Debe contener exactamente 8 números.");
                }

            } while (!dniValido);
            string remitenteIngresado;
            do
            {
                Console.Write("Ingrese nombre completo del remitente: ");
                remitenteIngresado = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(remitenteIngresado))
                {
                    Console.WriteLine("El nombre del remitente no puede estar vacío.");
                }

            } while (string.IsNullOrWhiteSpace(remitenteIngresado));
            string[] tiposPermitidos = { "OFICIO", "EXPEDIENTE", "DOCUMENTO EXTERNO", "SOLICITUD" };
            string tipoIngresado;
            bool tipoValido;
            do
            {
                Console.WriteLine("Tipos de documento disponibles: OFICIO, EXPEDIENTE, DOCUMENTO EXTERNO, SOLICITUD");
                Console.Write("Ingrese tipo de documento: ");
                tipoIngresado = Console.ReadLine().Trim().ToUpper();

                tipoValido = tiposPermitidos.Contains(tipoIngresado);

                if (!tipoValido)
                {
                    Console.WriteLine("Tipo de documento no válido. Intente nuevamente.");
                }

            } while (!tipoValido);
            string asuntoIngresado;
            do
            {
                Console.Write("Ingrese asunto del documento: ");
                asuntoIngresado = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(asuntoIngresado))
                {
                    Console.WriteLine("El asunto no puede estar vacío.");
                }

            } while (string.IsNullOrWhiteSpace(asuntoIngresado));
            string codigoGenerado = GenerarCodigo(totalRegistros);
            codigos[totalRegistros] = codigoGenerado;
            DNI[totalRegistros] = dniIngresado;
            remitentes[totalRegistros] = remitenteIngresado;
            tiposDoc[totalRegistros] = tipoIngresado;
            asuntos[totalRegistros] = asuntoIngresado;
            estados[totalRegistros] = "RECIBIDO";
            totalRegistros++;
            Console.WriteLine();
            Console.WriteLine("Expediente registrado con éxito.");
            Console.WriteLine("Código asignado: " + codigoGenerado);
        }
        static bool EsDniValido(string dni)
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
        static string GenerarCodigo(int totalRegistrosActual)
        {
            int numero = totalRegistrosActual + 1;
            int anioActual = DateTime.Now.Year;
            return "EXP-" + anioActual + "-" + numero.ToString("D3");
        }

        static void MostrarExpedientes(string[] codigos, string[] DNI, string[] remitentes, string[] tiposDoc, string[] asuntos, string[] estados, int totalRegistros)
        {
            Console.Clear();
            Console.WriteLine("================================================================================");
            Console.WriteLine("                        LISTA TOTAL DE EXPEDIENTES                              ");
            Console.WriteLine("================================================================================");
            if (totalRegistros == 0)
            {
                Console.WriteLine("No existen expedientes registrados en el sistema.");
            }
            else
            {
                for (int i = 0; i < totalRegistros; i++)
                {
                    Console.WriteLine("--------------------------------------------------------------------------------");
                    Console.WriteLine($"CÓDIGO: {codigos[i]}  |  ESTADO: {estados[i]}");
                    Console.WriteLine($"REMITENTE: {remitentes[i]}  |  DNI: {DNI[i]}");
                    Console.WriteLine($"TIPO: {tiposDoc[i]}");
                    Console.WriteLine($"ASUNTO: {asuntos[i]}");
                }
                Console.WriteLine("--------------------------------------------------------------------------------");
                Console.WriteLine($"Total registrados: {totalRegistros}");
            }
        }
      
        static void BuscarExpediente(string[] codigos, string[] DNI, string[] remitentes, string[] tiposDoc, string[] asuntos, string[] estados, int totalRegistros)
<<<<<<< HEAD
{
=======
        {
>>>>>>> origin/buscar
            string codigoBuscado;
            bool encontrado;
            int i, pos;

            Console.Clear();

            Console.WriteLine("==========================================================");
            Console.WriteLine("               BÚSQUEDA DE EXPEDIENTE");
            Console.WriteLine("==========================================================");

            if (totalRegistros == 0)
            {
                Console.WriteLine("No hay expedientes cargados en el sistema.");
            }
            else
            {
                Console.Write("Ingrese el código a buscar (ejemplo EXP-1): ");
                codigoBuscado = Console.ReadLine();

                encontrado = false;
                pos = -1;

                for (i = 0; i < totalRegistros; i++)
                {
                    if (codigos[i].ToUpper() == codigoBuscado.ToUpper())
                    {
                        encontrado = true;
                        pos = i;
                    }
                }

                if (encontrado)
                {
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine(">> EXPEDIENTE ENCONTRADO <<");
                    Console.WriteLine("Código: " + codigos[pos]);
                    Console.WriteLine("DNI: " + DNI[pos]);
                    Console.WriteLine("Remitente: " + remitentes[pos]);
                    Console.WriteLine("Tipo: " + tiposDoc[pos]);
                    Console.WriteLine("Asunto: " + asuntos[pos]);
                    Console.WriteLine("Estado actual: " + estados[pos]);
                    Console.WriteLine("----------------------------------------------------------");
                }
                else
                {
                    Console.WriteLine(">> No se encontró ningún expediente con el código: " + codigoBuscado);
                }
            }
        }
        static void OrdenarExpedientes(string[] codigos, string[] DNI, string[] remitentes, string[] tiposDoc, string[] asuntos, string[] estados, int totalRegistros)
        {

        }
        static void ActualizarEstado(string[] codigos, string[] estados, int totalRegistros)
        {

        }
    }
}
