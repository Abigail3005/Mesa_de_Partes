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

            int totalRegistros = 0;
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

                Console.Write("\u001b[1;37m");
                Console.WriteLine("          [1] REGISTRAR EXPEDIENTE");
                Console.WriteLine("          [2] MOSTRAR EXPEDIENTES");
                Console.WriteLine("          [3] BUSCAR EXPEDIENTE");
                Console.WriteLine("          [4] ORDENAR EXPEDIENTES");
                Console.Write("\u001b[0m");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("          [5] ESTADO DEL EXPEDIENTE");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("          [0] SALIR DEL SISTEMA");

                Console.Write("\u001b[1;38;2;55;110;180m");
                Console.WriteLine();
                Console.WriteLine("       ===========================================");
                Console.Write("\u001b[0m");

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("          EXPEDIENTES REGISTRADOS: ");

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine(totalRegistros);
                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("          SELECCIONE UNA OPCION: ");
                Console.ResetColor();

                opcion = Convert.ToInt32(Console.ReadLine());

                // PROCESAMIENTO DE OPCIONES
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

        // REGISTRAR EXPEDIENTE
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

                // VALIDACION DESDE LA BIBLIOTECA
                dniValido = Expedientes.EsDniValido(dniIngresado);

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

            string[] tiposPermitidos = {
                "OFICIO",
                "EXPEDIENTE",
                "DOCUMENTO EXTERNO",
                "SOLICITUD"
            };

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

            // GENERAR CODIGO DESDE LA BIBLIOTECA
            string codigoGenerado = Expedientes.GenerarCodigo(totalRegistros);

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

        // MOSTRAR EXPEDIENTES
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

        // ORDENAR EXPEDIENTES
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
                for (int i = 0; i < totalRegistros - 1; i++)
                {
                    for (int j = 0; j < totalRegistros - 1 - i; j++)
                    {
                        if (codigos[j].CompareTo(codigos[j + 1]) > 0)
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

        // ACTUALIZAR ESTADO
        static void ActualizarEstado(string[] codigos, string[] estados, int totalRegistros)
        {
            string codigoBuscado;
            int pos = -1, opEst = 0;

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

            Console.Write("\u001b[1;38;2;55;110;180m");
            Console.WriteLine("                 ACTUALIZAR ESTADO");
            Console.WriteLine("       -------------------------------------------");
            Console.Write("\u001b[0m");
            Console.WriteLine();

            if (totalRegistros == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("          No hay expedientes en el sistema.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("          Ingrese el código del expediente: ");

                codigoBuscado = Console.ReadLine();

                for (int i = 0; i < totalRegistros; i++)
                {
                    if (codigos[i].Equals(codigoBuscado,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        pos = i;
                        break;
                    }
                }

                if (pos == -1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("          No se encontró el expediente.");
                }
                else
                {
                    Console.WriteLine();
                    Console.Write("\u001b[1;38;2;55;110;180m");
                    Console.WriteLine("       -------------------------------------------");
                    Console.Write("\u001b[0m");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("          CODIGO: " + codigos[pos]);
                    Console.WriteLine("          ESTADO ACTUAL: " + estados[pos]);

                    Console.Write("\u001b[1;38;2;55;110;180m");
                    Console.WriteLine("       -------------------------------------------");
                    Console.WriteLine();
                    Console.WriteLine("              SELECCIONE EL NUEVO ESTADO");
                    Console.Write("\u001b[0m");
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("          [1] EN REVISION");
                    Console.WriteLine("          [2] DERIVADO A OFICINA");
                    Console.WriteLine("          [3] FINALIZADO / ATENDIDO");
                    Console.WriteLine("          [4] ARCHIVADO");

                    Console.WriteLine();

                    do
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("          Seleccione una opcion: ");

                        Console.ForegroundColor = ConsoleColor.White;

                        if (!int.TryParse(Console.ReadLine(), out opEst))
                        {
                            opEst = 0;
                        }

                        if (opEst < 1 || opEst > 4)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("          Opcion no valida. Intente nuevamente.");
                        }

                    } while (opEst < 1 || opEst > 4);

                    switch (opEst)
                    {
                        case 1:
                            estados[pos] = "EN REVISION";
                            break;

                        case 2:
                            estados[pos] = "DERIVADO A OFICINA";
                            break;

                        case 3:
                            estados[pos] = "FINALIZADO / ATENDIDO";
                            break;

                        case 4:
                            estados[pos] = "ARCHIVADO";
                            break;
                    }

                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("\u001b[1m");
                    Console.WriteLine("       ===========================================");
                    Console.WriteLine("               ESTADO ACTUALIZADO");
                    Console.WriteLine("       ===========================================");
                    Console.Write("\u001b[0m");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    Console.WriteLine("          Codigo: " + codigos[pos]);
                    Console.WriteLine("          Nuevo estado: " + estados[pos]);
                }
            }

            Console.ResetColor();
        }
    }
}
