using System;
using MySql.Data.MySqlClient;

namespace Progra3Card.Administrativo
{
    class Program
    {
        private static string connectionString = "Server=localhost;Port=3306;Database=mi_banco_db;Uid=root;Pwd=root;";

        static void Main(string[] args)
        {
            bool salir = false;
            while (!salir)
            {
                Console.Clear();
                Console.WriteLine("========================================");
                Console.WriteLine("    SISTEMA ADMINISTRATIVO PROGRA3CARD   ");
                Console.WriteLine("========================================");
                Console.WriteLine("1. Emitir Nueva Tarjeta (Alta de Cliente)");
                Console.WriteLine("2. Listar Tarjetas");
                Console.WriteLine("3. Ver Detalle de una Tarjeta / Cliente");
                Console.WriteLine("4. Eliminar Tarjeta (Baja de Sistema)");
                Console.WriteLine("5. Emitir Nueva Liquidación Mensual");
                Console.WriteLine("6. Salir");
                Console.WriteLine("========================================");
                Console.Write("Seleccione una opción: ");

                switch (Console.ReadLine())
                {
                    case "1": MenuEmitirTarjeta(); break;
                    case "2": MenuListarTarjetas(); break;
                    case "3": MenuVerDetalleTarjeta(); break;
                    case "4": MenuEliminarTarjeta(); break;
                    case "5": MenuEmitirLiquidacion(); break;
                    case "6": salir = true; break;
                    default:
                        Console.WriteLine("Opción no válida. Presione una tecla para continuar...");
                        Console.ReadKey();
                        break;
                }
            }
        }

        // Funciones a completar:

        // -- MenuEmitirTarjeta() NO ESTABA EN EL ARCHIVO BASE! --
        static void MenuEmitirTarjeta()
        {
            Console.Clear();
            Console.WriteLine("--- EMITIR NUEVA TARJETA ---");
            //-- Datos para la tabla Usuarios
            Console.WriteLine("-- +-8 Digitos --");
            Console.Write("Documento del Usuario : ");
            string documento = Console.ReadLine() ?? "";

            //--> DIVISION POR DATO <--
            Console.Clear();
            Console.WriteLine("--- EMITIR NUEVA TARJETA ---");
            Console.WriteLine("--- PASAPORTE o DNI ---");
            Console.Write("Tipo del Documento: ");
            string tipoDocumento = Console.ReadLine() ?? "";
            //-- Verificacion para que el tipo de documento sea PASAPORTE o DNI
            while (!(tipoDocumento == "DNI" || tipoDocumento == "PASAPORTE"))
            {
                Console.Clear();
                Console.WriteLine("--- EMITIR NUEVA TARJETA ---");
                Console.WriteLine("--> El tipo de documento es erroneo, tiene que ser PASAPORTE o DNI obligatoriamente <--");
                Console.Write("Tipo del Documento: ");
                tipoDocumento = Console.ReadLine() ?? "";
            }

            //--> DIVISION POR DATO <--
            Console.Clear();
            Console.WriteLine("--- EMITIR NUEVA TARJETA ---");
            Console.Write("Nombre del Usuario: ");
            string nombre = Console.ReadLine() ?? "";

            //--> DIVISION POR DATO <--
            Console.Clear();
            Console.WriteLine("--- EMITIR NUEVA TARJETA ---");
            Console.Write("Apellido del Usuario: ");
            string apellido = Console.ReadLine() ?? "";
            //--> DIVISION POR DATO <--
            Console.Clear();
            Console.WriteLine("--- EMITIR NUEVA TARJETA ---");
            Console.WriteLine("--- Formato YYYY-MM-DD ---");
            Console.Write("Fecha de Nacimiento del Usuario: ");

            //-- Tipo DateTime, el parse es para convertir el string del ReadLine en una fecha
            DateTime fechaNacimiento = DateTime.Parse(Console.ReadLine() ?? "");

            //--> DIVISION POR DATO <--
            Console.Clear();
            Console.WriteLine("--- EMITIR NUEVA TARJETA ---");
            Console.Write("Email del Usuario: ");
            string email = Console.ReadLine() ?? "";

            //-- Datos para la tabla Tarjetas

            //--> DIVISION POR DATO <--
            Console.Clear();
            Console.WriteLine("--- EMITIR NUEVA TARJETA ---");
            Console.WriteLine("-- 16 Digitos --");
            Console.Write("Numero de la tarjeta: ");
            string numTarjeta = Console.ReadLine() ?? "";

            //--> DIVISION POR DATO <--
            Console.Clear();
            Console.WriteLine("--- EMITIR NUEVA TARJETA ---");
            Console.WriteLine("-- Banco Nación - Banco Provincia - Banco Galicia - Banco Santander - Banco BBVA - Banco Macro --");
            Console.Write("Banco emisor: ");
            string banco = Console.ReadLine() ?? "";
            //-- Validacion para que el banco sea alguno valido
            while (!(banco == "Banco Nación" || banco == "Banco Provincia" || banco == "Banco Galicia" || banco == "Banco Santander" || banco == "Banco BBVA" || banco == "Banco Macro"))
            {
                Console.Clear();
                Console.WriteLine("Banco invalido. Opciones: Banco Nación, Banco Provincia, Banco Galicia, Banco Santander, Banco BBVA, Banco Macro");
                Console.Write("Banco emisor: ");
                banco = Console.ReadLine() ?? "";
            }

            //--> DIVISION POR DATO <--
            Console.Clear();
            Console.WriteLine("--- EMITIR NUEVA TARJETA ---");
            Console.WriteLine("-- Activa o Bloqueada --");
            Console.Write("Estado de la tarjeta: ");
            string estado = Console.ReadLine() ?? "";
            //-- Validacion para que el estado de la tarjeta sea alguno valido
            while (!(estado == "Activa" || estado == "Bloqueada"))
            {
                Console.Clear();
                Console.WriteLine("--- EMITIR NUEVA TARJETA ---");
                Console.WriteLine("--> El valor del Estado de la tarjeta es erroneo, tiene que ser Activa o Bloqueada obligatoriamente <--");
                Console.Write("Estado de la tarjeta: ");
                estado = Console.ReadLine() ?? "";
            }

            //--> DIVISION POR DATO <--
            Console.Clear();
            Console.WriteLine("--- EMITIR NUEVA TARJETA ---");
            Console.Write("Saldo inicial: ");
            //-- Convert a decimal, el ReadLine siempre espera un string
            decimal saldo = Convert.ToDecimal(Console.ReadLine());

            //--> DIVISION POR DATO <--
            Console.Clear();
            Console.WriteLine("--- EMITIR NUEVA TARJETA ---");
            Console.WriteLine("-- +-8 Digitos --");
            Console.WriteLine("-- Documento previo: " + documento);
            Console.Write("DNI del titular: ");
            string dni = Console.ReadLine() ?? "";

            if (InsertarTarjeta(documento, tipoDocumento, nombre, apellido, fechaNacimiento, email, numTarjeta, banco, estado, saldo, dni))
            {
                Console.Clear();
                Console.WriteLine("--- EMITIR NUEVA TARJETA ---");
                Console.WriteLine("\nUsuario y Tarjeta creados con exito!");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("--- EMITIR NUEVA TARJETA ---");
                Console.WriteLine("Error: El usuario y la Tarjeta no fueron agregados correctamente");
            }


            Console.WriteLine("\nPresione una tecla para volver al menú...");
            Console.ReadKey();
        }
        // -- MenuEmitirTarjeta() NO ESTABA EN EL ARCHIVO BASE! --

        static void MenuListarTarjetas()
        {
            Console.Clear();
            Console.WriteLine("--- LISTADO GENERAL DE TARJETAS ---");
            Console.WriteLine("{0,-12} {1,-18} {2,-20} {3,-15}", "Nro Cuenta", "Nro Tarjeta", "Banco Emisor", "DNI Titular");
            Console.WriteLine("----------------------------------------------------------------------");

            // === A realizar ===
            // Aquí deben implementar un SELECT sobre la tabla 'tarjetas'
            // para recorrer las filas e imprimirlas en la consola.

            ObtenerYMostrarTarjetas();

            Console.WriteLine("\nPresione una tecla para volver al menú...");
            Console.ReadKey();
        }

        static void MenuVerDetalleTarjeta()
        {
            Console.Clear();
            Console.WriteLine("--- DETALLE DE TARJETA Y CLIENTE ---");
            Console.Write("Ingrese el Número de Cuenta a consultar: ");
            int numCuenta = Convert.ToInt32(Console.ReadLine());

            // === A realizar ===
            // Aquí deben realizar un SELECT con un JOIN entre 'tarjetas' y 'usuarios' 
            // filtrando por el numCuenta para traer todos los campos (Nombre, Apellido, Email, Saldo, etc.)

            MostrarDetalleCompleto(numCuenta);

            Console.WriteLine("\nPresione una tecla para volver al menú...");
            Console.ReadKey();
        }

        static void MenuEliminarTarjeta()
        {
            Console.Clear();
            Console.WriteLine("--- ELIMINAR TARJETA DEL SISTEMA ---");
            Console.Write("Ingrese el Número de Cuenta de la tarjeta a dar de baja: ");
            int numCuenta = Convert.ToInt32(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n⚠️ ADVERTENCIA: Se eliminará la tarjeta, sus liquidaciones y los datos de acceso web vinculados.");
            Console.ResetColor();
            Console.Write("¿Está seguro de continuar? (S/N): ");

            // El signo de pregunta despues del ReadLine hace la comparacion solo si no es nulo,
            // caso contrario el if daria false
            if (Console.ReadLine()?.ToUpper() == "S")
            {
                // === A realizar ===
                // Aquí deben ejecutar un DELETE sobre la tabla 'tarjetas' donde num_cuenta = numCuenta.
                // Como definimos ON DELETE CASCADE en la base de datos, las liquidaciones se borrarán solas.
                // Opcional: Evaluar si también eliminan al usuario de la tabla 'usuarios' o si lo mantienen.

                bool exito = DarDeBajaTarjeta(numCuenta);

                if (exito)
                    Console.WriteLine("\nTarjeta eliminada correctamente del sistema.");
                else
                    Console.WriteLine("\nError al intentar eliminar la tarjeta. Verifique el número de cuenta.");
            }
            else
            {
                Console.WriteLine("\nOperación cancelada.");
            }

            Console.WriteLine("\nPresione una tecla para volver al menú...");
            Console.ReadKey();
        }
        // -- MenuEmitirLiquidacion() NO ESTABA EN EL ARCHIVO BASE! --
        static void MenuEmitirLiquidacion()
        {
            Console.Clear();
            Console.WriteLine("--- EMITIR LIQUIDACION ---");
            Console.Write("Numero de Cuenta: ");
            string numCuenta = Console.ReadLine() ?? "";
            Console.Clear();
            Console.WriteLine("--- EMITIR LIQUIDACION ---");
            Console.Write("Periodo de Liquidacion: ");
            string periodoLiquidacion = Console.ReadLine() ?? "";
            Console.Clear();
            Console.WriteLine("--- EMITIR LIQUIDACION ---");
            Console.Write("Vencimiento: ");
            DateTime fechaVencimiento = DateTime.Parse(Console.ReadLine() ?? "");
            Console.Clear();
            Console.WriteLine("--- EMITIR LIQUIDACION ---");
            Console.Write("Total a pagar: ");
            decimal pagoTotal = Convert.ToDecimal(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("--- EMITIR LIQUIDACION ---");
            Console.Write("Pago minimo: ");
            decimal pagoMinimo = Convert.ToDecimal(Console.ReadLine());

            if (AgregarLiquidacion(numCuenta, periodoLiquidacion, fechaVencimiento, pagoTotal, pagoMinimo))
            {
                Console.Clear();
                Console.WriteLine("--- EMITIR LIQUIDACION ---");
                Console.WriteLine("Liquidacion agregada correctamente!");
            }
            else
            {
                Console.Clear();
                Console.WriteLine("--- EMITIR LIQUIDACION ---");
                Console.WriteLine("Error: La liquidacion no pudo ser agregada");
            }

            Console.WriteLine("\nPresione una tecla para volver al menú...");
            Console.ReadKey();
        }
        // -- MenuEmitirLiquidacion() NO ESTABA EN EL ARCHIVO BASE! --


        // =========================================================================
        // MÉTODOS BASE QUE DEBEN COMPLETAR CON LA LÓGICA 
        // =========================================================================

        static void ObtenerYMostrarTarjetas()
        {
            // Completar
            // Ejemplo de impresión dentro del bucle: 
            // Console.WriteLine("{0,-12} {1,-18} {2,-20} {3,-15}", reader["num_cuenta"], reader["numero_tarjeta"], ...);
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT * FROM tarjetas";
                using (MySqlCommand comando = new MySqlCommand(query, conn))
                {
                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        while (lector.Read())
                        {
                            Console.WriteLine("{0,-12} {1,-18} {2,-20} {3,-15}",
                                lector["num_cuenta"],
                                lector["numero_tarjeta"],
                                lector["banco_emisor"],
                                lector["dni_titular"]);
                        }
                    }
                }
            }
        }

        static void MostrarDetalleCompleto(int cuenta)
        {
            // Completar haciendo un SELECT con JOIN de usuarios y tarjetas WHERE num_cuenta = @cuenta
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT u.nombre, u.apellido, u.documento, u.email, t.num_cuenta, t.numero_tarjeta, t.banco_emisor, t.estado, t.saldo FROM tarjetas t JOIN usuarios u ON t.dni_titular = u.documento WHERE t.num_cuenta = @cuenta";
                using (MySqlCommand comando = new MySqlCommand(query, conn))
                {
                    comando.Parameters.AddWithValue("@cuenta", cuenta);

                    using (MySqlDataReader lector = comando.ExecuteReader())
                    {
                        Console.WriteLine(new string('=', 141));
                        Console.WriteLine("{0,-15}|{1,-15}|{2,-10}|{3,-35}|{4,-11}|{5,-17}|{6,-15}|{7,-9}|{8,-13}", "Nombre", "Apellido", "Documento", "Email", "NumCuenta", "NumTarjeta", "Banco", "Estado", "Saldo");
                        Console.WriteLine(new string('=', 141));
                        while (lector.Read())
                        {
                            Console.WriteLine("{0,-15}|{1,-15}|{2,-10}|{3,-35}|{4,-11}|{5,-17}|{6,-15}|{7,-9}|{8,-13}",
                                lector["nombre"],
                                lector["apellido"],
                                lector["documento"],
                                lector["email"],
                                lector["num_cuenta"],
                                lector["numero_tarjeta"],
                                lector["banco_emisor"],
                                lector["estado"],
                                lector["saldo"]);
                        }
                    }
                }
            }
        }

        static bool DarDeBajaTarjeta(int cuenta)
        {
            // Completar usando un DELETE FROM tarjetas WHERE num_cuenta = @cuenta
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string delete = "DELETE FROM tarjetas WHERE num_cuenta = @cuenta";
                using (MySqlCommand comando = new MySqlCommand(delete, conn))
                {
                    comando.Parameters.AddWithValue("@cuenta", cuenta);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }

        // -- InsertarTarjeta() NO ESTABA EN EL ARCHIVO BASE! --
        static bool InsertarTarjeta(string documento, string tipoDocumento, string nombre, string apellido, DateTime fechaNacimiento, string email, string numTarjeta, string banco, string estado, decimal saldo, string dni)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string insert = "INSERT INTO usuarios (documento, tipo_doc, nombre, apellido, fecha_nacimiento, email) VALUES (@documento, @tipoDocumento, @nombre, @apellido, @fechaNacimiento, @email); INSERT INTO tarjetas (numero_tarjeta, banco_emisor, estado, saldo, dni_titular) VALUES (@numTarjeta, @banco, @estado, @saldo, @dni)";

                using (MySqlCommand comando = new MySqlCommand(insert, conn))
                {
                    comando.Parameters.AddWithValue("@documento", documento);
                    comando.Parameters.AddWithValue("@tipoDocumento", tipoDocumento);
                    comando.Parameters.AddWithValue("@nombre", nombre);
                    comando.Parameters.AddWithValue("@apellido", apellido);
                    comando.Parameters.AddWithValue("@fechaNacimiento", fechaNacimiento);
                    comando.Parameters.AddWithValue("@email", email);
                    comando.Parameters.AddWithValue("@numTarjeta", numTarjeta);
                    comando.Parameters.AddWithValue("@banco", banco);
                    comando.Parameters.AddWithValue("@estado", estado);
                    comando.Parameters.AddWithValue("@saldo", saldo);
                    comando.Parameters.AddWithValue("@dni", dni);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }
        // -- InsertarTarjeta() NO ESTABA EN EL ARCHIVO BASE! --

        // -- AgregarLiquidacion() NO ESTABA EN EL ARCHIVO BASE! --
        static bool AgregarLiquidacion(string numCuenta, string periodoLiquidacion, DateTime fechaVencimiento, decimal pagoTotal, decimal pagoMinimo)
        {
            using (MySqlConnection conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string insert = "INSERT INTO liquidaciones (num_cuenta, periodo, fecha_vencimiento, total_a_pagar, pago_minimo) VALUES (@numCuenta, @periodoLiquidacion, @fechaVencimiento, @pagoTotal, @pagoMinimo)";

                using (MySqlCommand comando = new MySqlCommand(insert, conn))
                {
                    comando.Parameters.AddWithValue("@numCuenta", numCuenta);
                    comando.Parameters.AddWithValue("@periodoLiquidacion", periodoLiquidacion);
                    comando.Parameters.AddWithValue("@fechaVencimiento", fechaVencimiento);
                    comando.Parameters.AddWithValue("@pagoTotal", pagoTotal);
                    comando.Parameters.AddWithValue("@pagoMinimo", pagoMinimo);

                    return comando.ExecuteNonQuery() > 0;
                }
            }
        }
        // -- AgregarLiquidacion() NO ESTABA EN EL ARCHIVO BASE! --
    }
}