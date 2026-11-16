
using System;
using System.IO;


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

        //ORDENAR EXPEDIENTES
        public static void OrdenarExpedientes(string[] codigos, string[] DNI, string[] remitentes, string[] tiposDoc, string[] asuntos, string[] estados, int totalRegistros)
        {
            Console.Clear();
            Console.Write("\u001b[1;38;2;7;84;154m");
            Console.WriteLine("==========================================================");
            Console.WriteLine("             ORDENAR EXPEDIENTES POR CÓDIGO");
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
            if (File.Exists("archivo.txt"))
            {
                StreamReader lector = new StreamReader("archivo.txt");
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
            StreamWriter escritor = new StreamWriter("archivo.txt");
            for (int i = 0; i < totalRegistros; i++)
            {
                escritor.WriteLine(codigos[i] + "|" + DNI[i] + "|" + remitentes[i] + "|" + tiposDoc[i] + "|" + asuntos[i] + "|" + estados[i]);
            }
            escritor.Close();
        }
    }
}
