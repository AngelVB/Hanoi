using System;
using System.IO;
using System.Collections;

namespace Hanoi
{
	class MainClass
	{

		/** 
		*
		* Función que comprueba si la altura de ventana es suficiente como para poder mostrar menús...
		*
		*/
		public static void CompruebaAltura(){
			while (Console.WindowHeight < 24) {
				if (Console.WindowHeight < 20) {
					Console.WriteLine ("Es necesario que la ventana sea más alta para continuar con el programa.");
					Console.ReadKey ();
					Console.Clear ();
				}
			}
		}

		/** 
		*
		* Menú Principal
		*
		*/
		public static void Menu(){
			int posY = 1;
			int posX = (Console.WindowWidth / 2) - 17;  //Calculamos la posición de X para centrar el menú.
			Console.Clear();
			CompruebaAltura();
			Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**********************************");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**                              **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**      LA TORRE DE HANOI       **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**                              **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**                              **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**  1. Jugar                    **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**                              **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**  2. Instrucciones            **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**                              **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**  3. Puntuaciones             **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**                              **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**  0. Salir                    **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**                              **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**********************************");Console.SetCursorPosition (posX, posY++);

			Console.Write ("Seleccione una opción: ");
		}

		/** 
		*
		* Primera opción del menú desde la que empezaremos una nueva partida o cargaremos una existente.
		* ATENCIÓN: Solo será visible en el caso de que exista una partida guardada con anterioridad.
		*
		*/
		public static void Jugar()
		{
			partida p;
			string opc;
			int posX, posY;

			do
			{
				Menu2();
				opc = Console.ReadLine();
				switch (opc)
				{
				case "1": 
					p = new partida ();
					p.Nueva ();
					break;
				case "2": 
					Serializador s = new Serializador("partida.dat");
					p = new partida ();
					p = s.Cargar ();
					p.Jugar();
					break;
				case "0":
					break;
				default:
					posX = (Console.WindowWidth / 2) - 17;  //Calculamos la posición de X para centrar el menú.
					posY= 15;
					Console.SetCursorPosition (posX, posY++);
					Console.Write("Seleccione una opción correcta.");
					Console.ReadLine(); break;
				}
			} while (opc!="0");
			Menu2 ();
		}

		/** 
		*
		* Menú De la opción Jugar
		*
		*/
		public static void Menu2(){
			int posY = 1;
			int posX = (Console.WindowWidth / 2) - 17; //Calculamos la posición de X para centrar el menú.
			Console.Clear();
			CompruebaAltura();
			Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**********************************");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**                              **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**            JUGAR             **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**                              **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**                              **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**  1. Nueva Partida            **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**                              **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**  2. Cargar Partida           **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**                              **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**                              **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**  0. Volver                   **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**                              **");Console.SetCursorPosition (posX, posY++);
			Console.WriteLine("**********************************");Console.SetCursorPosition (posX, posY++);

			Console.Write ("Seleccione una opción: ");
		}


		/** 
		*
		* Visualizador del fichero Instrucciones.txt, en el que se explica el funcionamiento del programa.
		*
		*/
		public static void Instrucciones(){
			Console.Clear ();
			string nombrefichori = "instrucciones.txt";
			string linea="";
			StreamReader fichorig;
			fichorig = File.OpenText (nombrefichori);

			do {
				if(Console.CursorTop<=Console.WindowHeight-4){ //Detecta por qué linea va el cursor y hace un Clear() para seguir con el bucle.
					linea = fichorig.ReadLine ();
				if(linea!=null){
						Console.WriteLine(linea);
					}
				} else {	
					Console.WriteLine(" pulse una tecla para continuar...");
					Console.ReadKey();
					Console.Clear();
				}
			} while (linea != null);
			Console.WriteLine();
			Console.WriteLine(" pulse una tecla para continuar...");
			Console.ReadKey();
		}

		/** 
		*
		* Llama a las funciones de la clase Ranking para mostrar las puntuaciones en pantalla
		*
		*/
		public static void Puntuaciones(){
			Ranking r = new Ranking ();
			r.CargaRanking ();
			r.DibujaRanking ();
		}

		/** 
		*
		* Main. Desde el llamamos al menú principal.
		*
		*/
		public static void Main (string[] args){
			partida p;
			Console.BackgroundColor = ConsoleColor.Black;
			Console.ForegroundColor = ConsoleColor.White;
			Console.Clear ();
			string opc;
			int posX, posY;
				do
				{
					Menu();
					opc = Console.ReadLine();
					switch (opc)
					{
				case "1":
					if (File.Exists("partida.dat")){//Comprobamos si hay una partida guardada, si no, nos saltamos el menu2 y creamos nueva partida.
						Jugar(); 
					} else {
						p = new partida ();
						p.Nueva ();
					}
						break;
					case "2": Instrucciones(); 
						break;
					case "3": Puntuaciones(); 
						break;


					case "0":
					posX = (Console.WindowWidth / 2) - 17;  //Calculamos la posición de X para centrar el menú.
						posY= 16;
						Console.SetCursorPosition (posX, posY++);
						Console.Write("¡Hasta pronto!"); 
						break;
					default:
					posX = (Console.WindowWidth / 2) - 17;  //Calculamos la posición de X para centrar el menú.
						posY= 16;
						Console.SetCursorPosition (posX, posY++);
						Console.Write("Seleccione una opción correcta.");
						Console.ReadLine(); 
					break;
					}
				} while (opc!="0");
			}
	}
}
