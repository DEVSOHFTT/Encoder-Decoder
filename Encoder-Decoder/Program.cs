// Importar las bibliotecas necesarias
using System;
using System.IO;
using System.Security.Cryptography;
using System.Linq;
using System.Text;

namespace Encoder_Decoder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Iniciar la aplicación llamando a la función Menu
            Menu();
        }

        #region Menú de opciones
        public static void Menu()
        {
            bool opcionOK = false;
            bool salir = false;

            // Bucle principal del menú
            while (!salir)
            {
                Console.Beep();
                Console.Clear();
                int op = 0;
                Console.WriteLine("Elige una opción para continuar.");
                Console.WriteLine("1 - Encriptar un archivo.");
                Console.WriteLine("2 - Desencriptar un archivo.");
                Console.WriteLine("3 - Salir.");
                string opcion = Console.ReadLine();

                try
                {
                    op = Convert.ToInt32(opcion);
                    opcionOK = true;
                }
                catch (Exception e)
                {
                    opcionOK = false;
                    op = 0;
                    Console.WriteLine($"La opción ingresada en inválida.");
                }

                if (opcionOK)
                {
                    Console.Clear();
                    Console.Beep();
                    switch (op)
                    {
                        case 1:
                            Encrypt();
                            break;
                        case 2:
                            Decrypt();
                            break;
                        case 3:
                            salir = true;
                            break;
                    }
                }
            }
        }
        #endregion

        #region Desencriptar
        public static void Decrypt()
        {
            bool esperarRuta = true;
            bool esperarClave = true;

            // Bucle para obtener la ruta del archivo
            while (esperarRuta)
            {
                Console.WriteLine("Ingresa la ruta del archivo que quieres desencriptar.");
                string rutaArchivo = Console.ReadLine();

                // Verificar si la ruta del archivo es válida
                if (rutaArchivo != null)
                {
                    esperarRuta = false;
                    try
                    {
                        // Abrir el archivo para lectura
                        using (StreamReader reader = new StreamReader(rutaArchivo))
                        {
                            // Bucle para obtener la clave de encriptación
                            while (esperarClave)
                            {
                                Console.Clear();
                                Console.Beep();
                                Console.WriteLine("Ingresa la clave de encriptación.");
                                string clave = Console.ReadLine();
                                StringBuilder contenido = new StringBuilder();

                                // Verificar si la clave del archivo es válida
                                if (clave != null)
                                {
                                    esperarClave = false;
                                    try
                                    {
                                        // Leer líneas del archivo hasta el final
                                        while (!reader.EndOfStream)
                                        {
                                            // Crear una instancia de Aes para desencriptar
                                            using (Aes aesAlg = Aes.Create())
                                            {
                                                aesAlg.Key = Encoding.UTF8.GetBytes(clave);
                                                aesAlg.IV = aesAlg.Key.Take(16).ToArray(); // Tomar los primeros 16 bytes como IV

                                                // Crear transformador para desencriptar
                                                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                                                // Usar CryptoStream para desencriptar el contenido del archivo
                                                using (MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(reader.ReadLine())))
                                                {
                                                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                                                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                                                    {
                                                        string read = srDecrypt.ReadToEnd();
                                                        Console.WriteLine(read);
                                                        contenido.AppendLine(read);
                                                    }
                                                }
                                            }
                                        }

                                        Console.WriteLine("Presiona una tecla para continuar.");
                                        Console.ReadLine();

                                        // Preguntar si se desea guardar el archivo desencriptado
                                        AskToSaveFile(contenido.ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.Beep();
                                        esperarClave = true;
                                        Console.WriteLine($"Error al desencriptar: " + ex.Message);
                                    }
                                }
                                else
                                {
                                    Console.Beep();
                                    Console.WriteLine("Primero debes ingresar una clave.");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Beep();
                        esperarRuta = true;
                        Console.WriteLine($"Error al encontrar el archivo: " + ex.Message);
                    }
                }
                else
                {
                    Console.Beep();
                    Console.WriteLine("Primero debes ingresar una ruta.");
                }
            }
        }
        #endregion

        #region Encriptar
        public static void Encrypt()
        {
            bool esperarRuta = true;
            bool esperarClave = true;

            // Bucle para obtener la ruta del archivo
            while (esperarRuta)
            {
                Console.WriteLine("Ingresa la ruta del archivo que quieres encriptar.");
                string rutaArchivo = Console.ReadLine();

                // Verificar si la ruta del archivo es válida
                if (rutaArchivo != null)
                {
                    esperarRuta = false;
                    try
                    {
                        // Abrir el archivo para lectura
                        using (StreamReader reader = new StreamReader(rutaArchivo))
                        {
                            // Bucle para obtener la clave de encriptación
                            while (esperarClave)
                            {
                                Console.Clear();
                                Console.Beep();
                                Console.WriteLine("Ingresa la clave de encriptación.");
                                string clave = Console.ReadLine();
                                StringBuilder contenido = new StringBuilder();

                                // Verificar si la clave del archivo es válida
                                if (clave != null)
                                {
                                    esperarClave = false;
                                    try
                                    {
                                        // Leer líneas del archivo hasta el final
                                        while (!reader.EndOfStream)
                                        {
                                            // Crear una instancia de Aes para encriptar
                                            using (Aes aesAlg = Aes.Create())
                                            {
                                                aesAlg.Key = Encoding.UTF8.GetBytes(clave);
                                                aesAlg.IV = aesAlg.Key.Take(16).ToArray(); // Tomar los primeros 16 bytes como IV

                                                // Crear transformador para encriptar
                                                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                                                // Usar CryptoStream para encriptar el contenido del archivo
                                                using (MemoryStream msEncrypt = new MemoryStream())
                                                {
                                                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                                                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                                                    {
                                                        swEncrypt.Write(reader.ReadLine());
                                                    }

                                                    string encoded = Convert.ToBase64String(msEncrypt.ToArray());

                                                    contenido.AppendLine(encoded);
                                                    Console.WriteLine(encoded);
                                                }
                                            }
                                        }

                                        Console.WriteLine("Presiona una tecla para continuar.");
                                        Console.ReadLine();

                                        // Preguntar si se desea guardar el archivo encriptado
                                        AskToSaveFile(contenido.ToString());
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.Beep();
                                        esperarClave = true;
                                        Console.WriteLine($"Error al encriptar: " + ex.Message);
                                    }
                                }
                                else
                                {
                                    Console.Beep();
                                    Console.WriteLine("Primero debes ingresar una clave.");
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.Beep();
                        esperarRuta = true;
                        Console.WriteLine($"Error al encontrar el archivo: " + ex.Message);
                    }
                }
                else
                {
                    Console.Beep();
                    Console.WriteLine("Primero debes ingresar una ruta.");
                }
            }
        }
        #endregion

        #region Guardar
        public static void SaveFile(string ubicacion, string nombreArchivo, string contenido)
        {
            try
            {
                string rutaCompleta = Path.Combine(ubicacion, nombreArchivo);

                // Guardar el contenido en el archivo
                File.WriteAllText(rutaCompleta, contenido);
                Console.Beep();
                Console.WriteLine("El archivo se guardó correctamente.");
            }
            catch (Exception e)
            {
                Console.Beep();
                Console.WriteLine($"Error al guardar el archivo: " + e.Message);
            }

            Console.WriteLine("Presione una tecla para continuar.");
            Console.ReadLine();
        }
        #endregion

        #region Preguntar si guardar archivo
        public static void AskToSaveFile(string contenido)
        {
            bool guardar = false;

            // Bucle para preguntar si se desea guardar el archivo
            while (!guardar)
            {
                Console.Clear();
                Console.Beep();
                Console.WriteLine("¿Deseas guardar el archivo?");
                Console.WriteLine("1 - SI");
                Console.WriteLine("2 - NO");

                string rGuardar = Console.ReadLine();
                int iGuardar = 0;

                try
                {
                    iGuardar = Convert.ToInt32(rGuardar);
                }
                catch (Exception)
                {
                    guardar = false;
                    Console.WriteLine("La opción ingresada es inválida.");
                }

                switch (iGuardar)
                {
                    case 1:
                        Console.WriteLine("Ingrese el nombre del archivo con su extensión.");
                        string nombre = Console.ReadLine();
                        Console.WriteLine("Ingrese el destino de guardado.");
                        string ubicacion = Console.ReadLine();
                        guardar = true;
                        SaveFile(ubicacion, nombre, contenido);
                        break;
                    case 2:
                        guardar = true;
                        break;
                }
            }
        }
        #endregion
    }
}

