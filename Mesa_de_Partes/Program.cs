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

        }
        static void MostrarExpedientes(string[] codigos, string[] DNI, string[] remitentes, string[] tiposDoc, string[] asuntos, string[] estados, int totalRegistros)
        {
            
        }
        static void BuscarExpediente(string[] codigos, string[] DNI, string[] remitentes, string[] tiposDoc, string[] asuntos, string[] estados, int totalRegistros)
        {
           
        }
        static void OrdenarExpedientes(string[] codigos, string[] DNI, string[] remitentes, string[] tiposDoc, string[] asuntos, string[] estados, int totalRegistros)
        {
            Console.Clear();
            Console.WriteLine("==========================================================");
            Console.WriteLine("             ORDENAR EXPEDIENTES POR CÓDIGO");
            Console.WriteLine("==========================================================");
            if (totalRegistros <= 1)
            {
                Console.WriteLine("No hay suficientes expedientes para ordenar.");
            }
            else 
            {
                for (int i=0;i<totalRegistros-1;i++) 
                {
                    for (int j=0;j<totalRegistros-1-i;j++) 
                    {
                        if (codigos[j].CompareTo(codigos[j+1])>0) 
                        {
                            string auxCod = codigos[j];
                            codigos[j] = codigos[j + 1];
                            codigos[j + 1] = auxCod;

                            string auxDni = DNI[j];
                            DNI[j] = DNI[j + 1];
                            DNI[j + 1] = auxDni;

                            string auxRem = remitentes[j];
                            remitentes[j] = remitentes[j + 1];
                            remitentes[j + 1] = auxRem;

                            string auxTipo = tiposDoc[j];
                            tiposDoc[j] = tiposDoc[j + 1];
                            tiposDoc[j + 1] = auxTipo;

                            string auxAsu = asuntos[j];
                            asuntos[j] = asuntos[j + 1];
                            asuntos[j + 1] = auxAsu;

                            string auxEst = estados[j];
                            estados[j] = estados[j + 1];
                            estados[j + 1] = auxEst;
                        }
                    }
                }
                Console.WriteLine("Expedientes ordenados correctamente.");
            }
        }
        static void ActualizarEstado(string[] codigos, string[] estados, int totalRegistros)
        {

        }

    }
}

