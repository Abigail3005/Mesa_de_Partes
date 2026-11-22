
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
            // PANEL DE INICIO DE SESION
            string usuarioCorrecto = "grupo4";
            string contrasenaCorrecta = "mesadepartes";

            string usuario;
            string contrasena;
            int intentos = 3;
            bool acceso = false;

            while (intentos > 0)
            {
                Console.BackgroundColor = ConsoleColor.Black;
                Console.Clear();

                Console.Write("\u001b[1;38;2;7;84;154m");
                Console.WriteLine();
                Console.WriteLine("       ===========================================");
                Console.WriteLine("              MESA DE PARTES DIGITAL");
                Console.WriteLine("                         UNC");
                Console.WriteLine("       ===========================================");
                Console.Write("\u001b[0m");

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("             SISTEMA DE GESTION DOCUMENTARIA");
                Console.WriteLine();

                Console.Write("\u001b[1;38;2;208;184;63m");
                Console.WriteLine("                 INICIO DE SESION");
                Console.WriteLine("       -------------------------------------------");
                Console.Write("\u001b[0m");
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("          INGRESE USUARIO: ");
                Console.ForegroundColor = ConsoleColor.White;
                usuario = Console.ReadLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("          INGRESE CONTRASEÑA: ");
                Console.ForegroundColor = ConsoleColor.White;

                contrasena = "";
                ConsoleKeyInfo tecla;

                do
                {
                    tecla = Console.ReadKey(true);

                    if (tecla.Key == ConsoleKey.Backspace && contrasena.Length > 0)
                    {
                        contrasena = contrasena.Substring(0, contrasena.Length - 1);
                        Console.Write("\b \b");
                    }
                    else if (tecla.Key != ConsoleKey.Backspace &&
                             tecla.Key != ConsoleKey.Enter &&
                             !char.IsControl(tecla.KeyChar))
                    {
                        contrasena += tecla.KeyChar;
                        Console.Write("*");
                    }

                } while (tecla.Key != ConsoleKey.Enter);

                if (usuario == usuarioCorrecto && contrasena == contrasenaCorrecta)
                {
                    acceso = true;

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("          =================================");
                    Console.WriteLine("                ACCESO AUTORIZADO");
                    Console.WriteLine("          =================================");
                    Console.ResetColor();

                    Console.WriteLine();
                    Console.WriteLine("          Bienvenido al sistema.");
                    Console.WriteLine("          Presione una tecla para continuar...");
                    Console.ReadKey();
                    break;
                }
                else
                {
                    intentos--;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine();
                    Console.WriteLine("          USUARIO O CONTRASEÑA INCORRECTOS.");
                    Console.WriteLine("          INTENTOS RESTANTES: " + intentos);
                    Console.ResetColor();

                    Console.WriteLine();
                    Console.WriteLine("          Presione una tecla para continuar...");
                    Console.ReadKey();
                }
            }

            if (!acceso)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();
                Console.WriteLine("       ===========================================");
                Console.WriteLine("                  ACCESO DENEGADO");
                Console.WriteLine("       ===========================================");
                Console.WriteLine("          Se agotaron los intentos.");
                Console.ResetColor();
                return;
            }

            // ARREGLOS DEL SISTEMA

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
                Console.WriteLine("             SISTEMA DE GESTION DOCUMENTARIA");
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
                Console.WriteLine("          [4] ORDENAR EXPEDIENTES");
                Console.WriteLine("          [5] ESTADO DEL EXPEDIENTE");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("          [0] SALIR DEL SISTEMA");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    opcion = -1;
                }

                // PROCESAMIENTO DE OPCIONES
                switch (opcion)
                {
                    case 1:
                        Expedientes.RegistrarExpediente(codigos, DNI, remitentes, tiposDoc, asuntos, estados, ref totalRegistros);
                        break;

                    case 2:
                        Expedientes.MostrarExpedientes(codigos, DNI, remitentes, tiposDoc, asuntos, estados, totalRegistros);
                        break;

                    case 3:
                        Expedientes.BuscarExpediente(codigos, DNI, remitentes, tiposDoc, asuntos, estados, totalRegistros);
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
    }
}
