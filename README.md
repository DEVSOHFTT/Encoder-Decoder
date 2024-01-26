# Encoder-Decoder
Este código en C# proporciona una aplicación de consola para encriptar y desencriptar archivos utilizando el algoritmo AES. La aplicación presenta un menú de opciones que incluye la capacidad de encriptar un archivo, desencriptar un archivo y salir del programa.

Uso
Al ejecutar la aplicación, se mostrará un menú con las siguientes opciones:

1 - Encriptar un archivo
2 - Desencriptar un archivo
3 - Salir
Para seleccionar una opción, ingresa el número correspondiente y presiona Enter.
si no se ingresa un número correctamente o si se ingresa algún otro caracter que no sea un número se volvera a desplegar el menu.

Opción 1: Encriptar un archivo
Ingresa la ruta del archivo que deseas encriptar; Esta ruta puede quedar en blanco, de esta forma el programa interpretara que tiene que buscar en la carpeta en donde se encuentra el ejecutable. Pero si no le colocamos al menos un nombre de un archivo de texto que se encuentre en esa carpeta volvera a preguntar por la ruta. ejemplo "lorem decoded.txt"

Ingresa la clave de encriptación; Esta debe tener al menos 16 digitos ej: zxcvbnmasdfghjkl

Se creará un archivo encriptado y se te preguntará si deseas guardarlo.

1 - SI: Ingresa el nombre del archivo y la ubicación donde deseas guardarlo; tambien puedes optar por colocar solo un nombre como por ejemplo "lorem encoded.txt" que se guardara en la carperta del ejecutable.

2 - NO: La aplicación volverá al menú principal.


Opción 2: Desencriptar un archivo
Ingresa la ruta del archivo que deseas desencriptar; Esta ruta puede quedar en blanco, de esta forma el programa interpretara que tiene que buscar en la carpeta en donde se encuentra el ejecutable. Pero si no le colocamos al menos un nombre de un archivo de texto que se encuentre en esa carpeta volvera a preguntar por la ruta. ejemplo "lorem encoded.txt"

Ingresa la clave de encriptación; Esta debe tener al menos 16 digitos ej: zxcvbnmasdfghjkl

Se mostrará el contenido desencriptado y se te preguntará si deseas guardarlo.

1 - SI: Ingresa el nombre del archivo y la ubicación donde deseas guardarlo; tambien puedes optar por colocar solo un nombre como por ejemplo "lorem decoded.txt" que se guardara en la carperta del ejecutable.

2 - NO: La aplicación volverá al menú principal.


Opción 3: Salir
La aplicación se cerrará.

Requisitos
.NET Framework: Asegúrate de tener instalado el entorno de ejecución .NET Framework en tu sistema.
Consideraciones
Si encuentras algún problema al ejecutar la aplicación, asegúrate de ingresar las rutas y claves correctamente.
Este código utiliza el algoritmo AES para encriptar y desencriptar archivos.
Contribuciones
Las contribuciones son bienvenidas. Si encuentras errores o mejoras potenciales, no dudes en abrir un problema o enviar una solicitud de extracción.