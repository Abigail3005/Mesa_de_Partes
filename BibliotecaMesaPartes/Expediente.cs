using System;
using System.IO;
using System.Linq;

namespace BibliotecaMesaPartes
{
    public class Expedientes
    {
        // Validar DNI
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
        // Generar código del expediente
        public static string GenerarCodigo(int totalRegistros)
        {
            int numero = totalRegistros + 1;
            int anioActual = DateTime.Now.Year;
            return "EXP-" + anioActual + "-" + numero.ToString("D3");
        }

        // REGISTRAR EXPEDIENTE
        public static void RegistrarExpediente(string[] codigos, string[] DNI, string[] remitentes, string[] tiposDoc, string[] asuntos, string[] estados, ref int totalRegistros)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            // ENCABEZADO INSTITUCIONAL (AZUL UNIVERSITARIO)
            Console.Write("\u001b[1;38;2;7;84;154m");
            Console.WriteLine();
            Console.WriteLine("    ===========================================");
            Console.WriteLine("              MESA DE PARTES DIGITAL");
            Console.WriteLine("                       UNC");
            Console.WriteLine("    ===========================================");
            Console.Write("\u001b[0m");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("          SISTEMA DE GESTION DOCUMENTARIA");
            Console.WriteLine();

            // TITULO
            Console.Write("\u001b[1;38;2;7;84;154m");
            Console.WriteLine("              REGISTRAR NUEVO EXPEDIENTE");
            Console.WriteLine("    -------------------------------------------");
            Console.Write("\u001b[0m");
            Console.WriteLine();

            if (totalRegistros >= codigos.Length)
            {
                Console.Write("\u001b[1;38;2;201;46;22m");
                Console.WriteLine("     No se pueden registrar mas expedientes.");
                Console.WriteLine("     Capacidad maxima alcanzada.");
                Console.Write("\u001b[0m");
                Console.ResetColor();
                return;
            }

            // DNI DEL REMITENTE
            string dniIngresado;
            bool dniValido;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("     Ingrese DNI del remitente (8 digitos): ");
                Console.ForegroundColor = ConsoleColor.White;

                dniIngresado = Console.ReadLine();
                dniValido = EsDniValido(dniIngresado);
                if (!dniValido)
                {
                    Console.Write("\u001b[1;38;2;201;46;22m");
                    Console.WriteLine("     DNI invalido. Debe contener exactamente 8 numeros.");
                    Console.Write("\u001b[0m");
                }
            } while (!dniValido);

            // NOMBRE DEL REMITENTE
            string remitenteIngresado;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("     Ingrese nombre completo del remitente: ");
                Console.ForegroundColor = ConsoleColor.White;
                remitenteIngresado = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(remitenteIngresado))
                {
                    Console.Write("\u001b[1;38;2;201;46;22m");
                    Console.WriteLine("     El nombre del remitente no puede estar vacio.");
                    Console.Write("\u001b[0m");
                }
            } while (string.IsNullOrWhiteSpace(remitenteIngresado));

            // TIPO DE DOCUMENTO
            int opcionTipo;
            string tipoIngresado = "";
            do
            {
                Console.WriteLine();
                Console.Write("\u001b[1;38;2;7;84;154m");
                Console.WriteLine("       TIPOS DE DOCUMENTO DISPONIBLES");
                Console.Write("\u001b[0m");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("     [1] OFICIO");
                Console.WriteLine("     [2] EXPEDIENTE");
                Console.WriteLine("     [3] DOCUMENTO EXTERNO");
                Console.WriteLine("     [4] SOLICITUD");
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("     Seleccione una opcion: ");
                Console.ForegroundColor = ConsoleColor.White;
                if (!int.TryParse(Console.ReadLine(), out opcionTipo))
                {
                    opcionTipo = 0;
                }

                switch (opcionTipo)
                {
                    case 1:
                        tipoIngresado = "OFICIO";
                        break;
                    case 2:
                        tipoIngresado = "EXPEDIENTE";
                        break;
                    case 3:
                        tipoIngresado = "DOCUMENTO EXTERNO";
                        break;
                    case 4:
                        tipoIngresado = "SOLICITUD";
                        break;
                    default:
                        Console.Write("\u001b[1;38;2;201;46;22m");
                        Console.WriteLine("     Opcion no valida. Intente nuevamente.");
                        Console.Write("\u001b[0m");
                        break;
                }
            } while (opcionTipo < 1 || opcionTipo > 4);
            // ASUNTO
            string asuntoIngresado;
            do
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("     Ingrese asunto del documento: ");
                Console.ForegroundColor = ConsoleColor.White;

                asuntoIngresado = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(asuntoIngresado))
                {
                    Console.Write("\u001b[1;38;2;201;46;22m");
                    Console.WriteLine("     El asunto no puede estar vacio.");
                    Console.Write("\u001b[0m");
                }
            } while (string.IsNullOrWhiteSpace(asuntoIngresado));

            // GENERAR CODIGO
            string codigoGenerado = GenerarCodigo(totalRegistros);
            codigos[totalRegistros] = codigoGenerado;
            DNI[totalRegistros] = dniIngresado;
            remitentes[totalRegistros] = remitenteIngresado;
            tiposDoc[totalRegistros] = tipoIngresado;
            asuntos[totalRegistros] = asuntoIngresado;
            estados[totalRegistros] = "RECIBIDO";

            totalRegistros++;
            string registro = codigos[totalRegistros - 1] + "|" + DNI[totalRegistros - 1] + "|" + remitentes[totalRegistros - 1] + "|" + tiposDoc[totalRegistros - 1] + "|" + asuntos[totalRegistros - 1] + "|" + estados[totalRegistros - 1];
            GuardarExpedientes(registro);

            // CONFIRMACION (DORADO INSTITUCIONAL)
            Console.WriteLine();
            Console.Write("\u001b[1;38;2;208;184;63m");
            Console.WriteLine("    ===========================================");
            Console.WriteLine("             EXPEDIENTE REGISTRADO");
            Console.WriteLine("    ===========================================");
            Console.Write("\u001b[0m");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine("     - CODIGO (EXP-2026-000): " + codigoGenerado);
            Console.WriteLine("     - DNI: " + dniIngresado);
            Console.WriteLine("     - REMITENTE: " + remitenteIngresado);
            Console.WriteLine("     - TIPO: " + tipoIngresado);
            Console.WriteLine("     - ASUNTO: " + asuntoIngresado);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("     - ESTADO: RECIBIDO");
            Console.Write("\u001b[1;38;2;208;184;63m");
            Console.WriteLine("    ===========================================");
            Console.Write("\u001b[0m");
            Console.ResetColor();
        }

        //ORDENAR EXPEDIENTES
        public static void OrdenarExpedientes(string[] codigos, string[] DNI, string[] remitentes, string[] tiposDoc, string[] asuntos, string[] estados, int totalRegistros)
        {
            Console.Clear();
            Console.Write("\u001b[1;38;2;7;84;154m");
            Console.WriteLine("==========================================================");
            Console.WriteLine("         ORDENAR EXPEDIENTES POR CÓDIGO");
            Console.WriteLine("==========================================================");
            Console.Write("\u001b[0m");

            if (totalRegistros <= 1)
            {
                Console.Write("\u001b[1;38;2;208;184;63m");
                Console.WriteLine("No hay suficientes expedientes para ordenar.");
                Console.Write("\u001b[0m");
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
                Console.Write("\u001b[1;38;2;208;184;63m");
                Console.WriteLine("Expedientes ordenados correctamente.");
                Console.Write("\u001b[0m");
            }
        }

        // MOSTRAR EXPEDIENTES
        public static void MostrarExpedientes(string[] codigos, string[] DNI, string[] remitentes, string[] tiposDoc, string[] asuntos, string[] estados, int totalRegistros)
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            Console.Write("\u001b[1;38;2;253;185;19m");
            Console.WriteLine("================================================================================");
            Console.WriteLine("                       LISTA TOTAL DE EXPEDIENTES             ");
            Console.WriteLine("================================================================================");  
            Console.Write("\u001b[0m");
            Console.WriteLine();
            if (totalRegistros == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("     No existen expedientes registrados en el sistema.");
                Console.ResetColor();
            }
            else
            {
                for (int i = 0; i < totalRegistros; i++)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("--------------------------------------------------------------------------------");
                    // Fila 1: Código y Estado
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" CÓDIGO: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"{codigos[i],-20}");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" | ESTADO: ");
                    if (estados[i] == "RECIBIDO") Console.ForegroundColor = ConsoleColor.Green;
                    else if (estados[i] == "EN REVISION") Console.ForegroundColor = ConsoleColor.Yellow;
                    else if (estados[i] == "DERIVADO A OFICINA") Console.ForegroundColor = ConsoleColor.DarkYellow;
                    else if (estados[i] == "FINALIZADO / ATENDIDO") Console.ForegroundColor = ConsoleColor.Cyan;
                    else if (estados[i] == "ARCHIVADO") Console.ForegroundColor = ConsoleColor.DarkGray;
                    else Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(estados[i]);

                    // Fila 2: Remitente y DNI
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" REMITENTE: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write($"{remitentes[i],-17}");

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" | DNI: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(DNI[i]);

                    // Fila 3: Tipo
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" TIPO: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(tiposDoc[i]);

                    // Fila 4: Asunto
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" ASUNTO: ");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine(asuntos[i]);
                }

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("--------------------------------------------------------------------------------");
                // Pie de lista
                Console.Write("\u001b[1;38;2;253;185;19m");
                Console.WriteLine($" Total registrados: {totalRegistros}");
                Console.ResetColor();
            }
        }

        // BUSCAR EXPEDIENTE
        public static void BuscarExpediente(string[] codigos, string[] DNI, string[] remitentes, string[] tiposDoc, string[] asuntos, string[] estados, int totalRegistros)
        {
            string codigoBuscado;
            int pos = -1;

            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
            // ENCABEZADO INSTITUCIONAL
            Console.Write("\u001b[1;38;2;7;84;154m");
            Console.WriteLine();
            Console.WriteLine("    ===========================================");
            Console.WriteLine("              MESA DE PARTES DIGITAL");
            Console.WriteLine("                      UNC");
            Console.WriteLine("    ===========================================");
            Console.Write("\u001b[0m");

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("       SISTEMA DE GESTION DOCUMENTARIA");
            Console.WriteLine();

            // TITULO
            Console.Write("\u001b[1;38;2;7;84;154m");
            Console.WriteLine("         BUSCAR EXPEDIENTE");
            Console.WriteLine("    -------------------------------------------");
            Console.Write("\u001b[0m");
            if (totalRegistros == 0)
            {
                Console.Write("\u001b[1;38;2;201;46;22m");
                Console.WriteLine("     No hay expedientes en el sistema.");
                Console.Write("\u001b[0m");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("     Ingrese el codigo del expediente (EXP-2026-000): ");
                codigoBuscado = Console.ReadLine();
                for (int i = 0; i < totalRegistros; i++)
                {
                    if (codigos[i].Equals(codigoBuscado, StringComparison.OrdinalIgnoreCase))
                    {
                        pos = i;
                        break;
                    }
                }

                if (pos != -1)
                {
                    Console.WriteLine();
                    Console.Write("\u001b[1;38;2;208;184;63m");
                    Console.WriteLine("    ===========================================");
                    Console.WriteLine("              EXPEDIENTE ENCONTRADO");
                    Console.WriteLine("    ===========================================");
                    Console.Write("\u001b[0m");

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine();
                    Console.WriteLine("     - CODIGO (EXP-2026-000): " + codigos[pos]);
                    Console.WriteLine("     - DNI: " + DNI[pos]);
                    Console.WriteLine("     - REMITENTE: " + remitentes[pos]);
                    Console.WriteLine("     - TIPO: " + tiposDoc[pos]);
                    Console.WriteLine("     - ASUNTO: " + asuntos[pos]);
                    Console.WriteLine("     - ESTADO: " + estados[pos]);

                    Console.Write("\u001b[1;38;2;208;184;63m");
                    Console.WriteLine("    ===========================================");
                    Console.Write("\u001b[0m");
                }
                else
                {
                    Console.Write("\u001b[1;38;2;201;46;22m");
                    Console.WriteLine();
                    Console.WriteLine("     No se encontro el expediente.");
                    Console.Write("\u001b[0m");
                }
            }
            Console.ResetColor();
        }

        // ACTUALIZAR ESTADO
        public static void ActualizarEstado(string[] codigos, string[] estados, int totalRegistros, string[] DNI, string[] remitentes, string[] tiposDoc, string[] asuntos)
        {
            string codigoBuscado;
            int pos = -1, opEst = 0;

            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();

            // ENCABEZADO AZUL MEDIO EN NEGRITA
            Console.Write("\u001b[1;38;2;55;110;180m");
            Console.WriteLine();
            Console.WriteLine("    ===========================================");
            Console.WriteLine("            MESA DE PARTES DIGITAL");
            Console.WriteLine("                     UNC");
            Console.WriteLine("    ===========================================");
            Console.Write("\u001b[0m");

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("       SISTEMA DE GESTION DOCUMENTARIA");
            Console.WriteLine();

            Console.Write("\u001b[1;38;2;55;110;180m");
            Console.WriteLine("         ACTUALIZAR ESTADO");
            Console.WriteLine("    -------------------------------------------");
            Console.Write("\u001b[0m");
            Console.WriteLine();

            if (totalRegistros == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("     No hay expedientes en el sistema.");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("     Ingrese el código del expediente (EXP-2026-001): ");
                codigoBuscado = Console.ReadLine();

                for (int i = 0; i < totalRegistros; i++)
                {
                    if (codigos[i].Equals(codigoBuscado, StringComparison.OrdinalIgnoreCase))
                    {
                        pos = i;
                        break;
                    }
                }
                if (pos == -1)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine();
                    Console.WriteLine("     No se encontró el expediente.");
                }
                else
                {
                    Console.WriteLine();
                    Console.Write("\u001b[1;38;2;55;110;180m");
                    Console.WriteLine("    -------------------------------------------");
                    Console.Write("\u001b[0m");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("     CODIGO: " + codigos[pos]);
                    Console.WriteLine("     ESTADO ACTUAL: " + estados[pos]);
                    Console.Write("\u001b[1;38;2;55;110;180m");
                    Console.WriteLine("    -------------------------------------------");
                    Console.WriteLine();
                    Console.WriteLine("       SELECCIONE EL NUEVO ESTADO");
                    Console.Write("\u001b[0m");
                    Console.WriteLine();

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("     [1] EN REVISION");
                    Console.WriteLine("     [2] DERIVADO A OFICINA");
                    Console.WriteLine("     [3] FINALIZADO / ATENDIDO");
                    Console.WriteLine("     [4] ARCHIVADO");
                    Console.WriteLine();
                    do
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("     Seleccione una opcion: ");
                        Console.ForegroundColor = ConsoleColor.White;
                        if (!int.TryParse(Console.ReadLine(), out opEst))
                        {
                            opEst = 0;
                        }

                        if (opEst < 1 || opEst > 4)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("     Opcion no valida. Intente nuevamente.");
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
                    Console.WriteLine("    ===========================================");
                    Console.WriteLine("               ESTADO ACTUALIZADO");
                    Console.WriteLine("    ===========================================");
                    Console.Write("\u001b[0m");

                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    Console.WriteLine("     - Codigo: " + codigos[pos]);
                    Console.WriteLine("     - Nuevo estado: " + estados[pos]);
                }
            }
            Console.ResetColor();
        }

        //Guardar los expedientes en txt
        public static void GuardarExpedientes(string registro)
        {
            StreamWriter escritor = new StreamWriter("expedientes.txt", true);
            escritor.WriteLine(registro);
            escritor.Close();
        }
        //Cargar expedientes desde el archivo txt
        public static int CargarExpedientes(string[] codigos, string[] DNI, string[] remitentes, string[] tiposDoc, string[] asuntos, string[] estados)
        {
            int totalRegistros = 0;
            if (File.Exists("expedientes.txt"))
            {
                StreamReader lector = new StreamReader("expedientes.txt");
                string linea;
                while ((linea = lector.ReadLine()) != null)
                {
                    string[] columnas = linea.Split('|');
                    if (columnas.Length == 6 && totalRegistros < codigos.Length)
                    {
                        codigos[totalRegistros] = columnas[0];
                        DNI[totalRegistros] = columnas[1];
                        remitentes[totalRegistros] = columnas[2];
                        tiposDoc[totalRegistros] = columnas[3];
                        asuntos[totalRegistros] = columnas[4];
                        estados[totalRegistros] = columnas[5];
                        totalRegistros++;
                    }
                }
                lector.Close();
            }
            return totalRegistros;
        }

        public static void ActualizarArchivo(string[] codigos, string[] DNI, string[] remitentes, string[] tiposDoc, string[] asuntos, string[] estados, int totalRegistros)
        {
            StreamWriter escritor = new StreamWriter("expedientes.txt");
            for (int i = 0; i < totalRegistros; i++)
            {
                escritor.WriteLine(codigos[i] + "|" + DNI[i] + "|" + remitentes[i] + "|" + tiposDoc[i] + "|" + asuntos[i] + "|" + estados[i]);
            }
            escritor.Close();
        }
    }
}