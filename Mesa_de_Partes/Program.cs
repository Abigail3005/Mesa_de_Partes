using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BibliotecaMesaPartes;

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

            int totalRegistros = Expedientes.CargarExpedientes(codigos, DNI, remitentes, tiposDoc, asuntos, estados);
            int opcion;

            do
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();

                // ENCABEZADO AZUL MEDIO EN NEGRITA
                Console.Write("\u001b[1;38;2;55;110;180m");
                Console.WriteLine();
                Console.WriteLine("       ===========================================");
                Console.WriteLine("              MESA DE PARTES DIGITAL");
                Console.WriteLine("                         UNC");
                Console.WriteLine("       ===========================================");
                Console.Write("\u001b[0m");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("          SISTEMA DE GESTION DOCUMENTARIA");
                Console.WriteLine();

                // MENU PRINCIPAL
                Console.Write("\u001b[1;38;2;55;110;180m");
                Console.WriteLine("                 MENU PRINCIPAL");
                Console.WriteLine("       -------------------------------------------");
                Console.Write("\u001b[0m");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("          [1] REGISTRAR EXPEDIENTE");

                Console.WriteLine("          [2] MOSTRAR EXPEDIENTES");

                Console.WriteLine("          [3] BUSCAR EXPEDIENTE");
                Console.Write("\u001b[0m");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("          [4] ORDENAR EXPEDIENTES");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("          [5] ESTADO DEL EXPEDIENTE");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("          [0] SALIR DEL SISTEMA");

                opcion = Convert.ToInt32(Console.ReadLine());

                // PROCESAMIENTO DE OPCIONES
                switch (opcion)
                {
                    case 1:
                        Expedientes.RegistrarExpediente
                        (codigos, DNI, remitentes, tiposDoc, asuntos, estados, ref totalRegistros);
                        break;

                    case 2:
                        Expedientes.MostrarExpedientes(codigos, DNI, remitentes, tiposDoc, asuntos, estados, totalRegistros);
                        break;

                    case 3:
                        BuscarExpediente
                        (codigos, DNI, remitentes, tiposDoc, asuntos, estados, totalRegistros);
                        break;

                    case 4:
                        Expedientes.OrdenarExpedientes(codigos, DNI, remitentes, tiposDoc, asuntos, estados, totalRegistros);
                        Expedientes.ActualizarArchivo(codigos, DNI, remitentes, tiposDoc, asuntos, estados, totalRegistros);
                        break;

                    case 5:
                        Expedientes.ActualizarEstado(codigos, estados, totalRegistros, DNI, remitentes, tiposDoc, asuntos);
                        Expedientes.ActualizarArchivo(codigos, DNI, remitentes, tiposDoc, asuntos, estados, totalRegistros);
                        break;

                    case 0:
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Cerrando sesion del sistema...");
                        Console.ResetColor();
                        break;

                    default:
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Opcion invalida.");
                        Console.ResetColor();
                        break;
                }

                if (opcion != 0)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Presione cualquier tecla para volver al menu...");
                    Console.ResetColor();
                    Console.ReadKey();
                }

            } while (opcion != 0);

            Console.ResetColor();
        }

        // BUSCAR EXPEDIENTE
        static void BuscarExpediente(string[] codigos, string[] DNI, string[] remitentes, string[] tiposDoc, string[] asuntos, string[] estados, int totalRegistros)
        {
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
                Console.Write("Ingrese el código a buscar (ejemplo EXP-2026-001): ");
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

    }
}