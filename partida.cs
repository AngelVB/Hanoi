using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace Hanoi
{	
	[Serializable]
	public class partida
	{

		int discos=-1;
		Stack v1;
		Stack v2;
		Stack v3;
		int contador=0;

		public partida(){
			v1 = new Stack ();
			v2 = new Stack ();
			v3 = new Stack ();
		
		}

		/** 
		* 
		* Función para crear una nueva partida, en la que se selecciona 
		* el número de discos de la misma (de 2 a 10).
		* 
		* Tras definirse, se llama a la función jugar().
		*/
		public void Nueva()
		{
			Console.Clear ();
			v1.Clear ();
			v2.Clear ();
			v3.Clear ();

			do {
				int posY = 1;
				int posX = (Console.WindowWidth / 2) - 30;  //Calculamos la posición de X para centrar el menú.
				Console.SetCursorPosition (posX, posY++);

				try{

					Console.Write ("Introduzca número de discos (Entre 2 y 10) o 0 para volver: ");
					discos = Convert.ToInt32 (Console.ReadLine ());
					if (((discos<2)||(discos>10))&&(discos!=0)){

						Console.SetCursorPosition (posX, posY++);
						Console.WriteLine("Introduzca número correcto de discos");
						Console.ReadKey();
						Console.Clear();
					}		
				}catch{
					Console.SetCursorPosition (posX, posY++);
					Console.WriteLine("Introduzca número válido de discos");
					Console.ReadKey();
					Console.Clear();
				}
					
			}while (((discos < 2) || (discos > 10))&& (discos!=0));

			if (discos!=0){
				for (int i = discos - 1; i >= 0; i--) {
					Disco d = new Disco (i, discos);
					v1.Push (d);
				}
				Jugar ();
			}
		}


		/** 
		* 
		* Función que que controla las acciones que el usuario puede hacer durante el juego: 
		*
		* Seleccionar Varilla Origen [1] [2] [3]
		* Seleccionar Varilla Destino [1] [2] [3]
		* Salir
		*
		* Las funciones que se llaman desde esta función son:
		* 
		* Dibujar()
		* SeleccionaDisco()
		* MoverDisco();
		* 
		*/
		public void Jugar()
		{
			ConsoleKeyInfo varillaori;
			ConsoleKeyInfo varillades;

			Stack varorigen;
			Stack vardestino;

			Dibujar ();
			do{
				Console.Clear();
				Dibujar();

				Console.Write ("Introduzca varilla origen o 0 para salir:");
				varillaori=Console.ReadKey(true);
				if (varillaori.KeyChar!='0' && varillaori.KeyChar!='1' && varillaori.KeyChar!='2' && varillaori.KeyChar!='3'){
					centrar();
					Console.WriteLine("Introduzca una opción correcta.");
					Console.ReadKey();
					Dibujar();
				} else {
					if (varillaori.KeyChar!='0'){
						if (((varillaori.KeyChar == '1') && (v1.Count==0)) || ((varillaori.KeyChar == '2') && (v2.Count==0))  || ((varillaori.KeyChar == '3') && (v3.Count==0))){
							centrar();
							Console.WriteLine("Varilla Origen vacía. Pulse una tecla para continuar ");
							Console.ReadKey();
							Dibujar();
						} else {
							do{
								if (varillaori.KeyChar == '1') 
									varorigen=v1;
								else if (varillaori.KeyChar == '2') 
									varorigen=v2;
								else 
									varorigen=v3;
								SeleccionaDisco(varorigen);
								Dibujar();
								Console.Write ("Introduzca varilla destino:");
								varillades=Console.ReadKey(true);
								if (varillades.KeyChar!='1' && varillades.KeyChar!='2' && varillades.KeyChar!='3'){
									centrar();
									Console.Write("Introduzca opción correcta. ");
									Console.ReadKey();
									Dibujar();
								} else {
									if (varillades.KeyChar == '1') 
										vardestino=v1;
									else if (varillades.KeyChar == '2') 
										vardestino=v2;
									else 
										vardestino=v3;
									MoverDisco(varorigen,vardestino);
									Dibujar ();	
								}
							}while (varillades.KeyChar!='1' && varillades.KeyChar!='2' && varillades.KeyChar!='3');
						}
					} else {

						salir();	
					}
				}
			}while ((varillaori.KeyChar!='0')&&(!finpartida()));
		}

		/** 
		* 
		* Función que centra bajo las varillas los distintos mensajes de opción o mensajes de error.
		*
		*/
		public void centrar(){
			int posY =0;
			posY = posY + discos + 11;
			int posX = ((Console.WindowWidth-(50+((discos*2)+1)))/2);
			Console.SetCursorPosition (posX, posY+=1);
		
		}
	/** 
		* 
		* Función que gestiona la salida del programa, a la que le podemos decir si guardamos o no el estado actual
		* de la partida en juego.
		*
		*/
		public void salir(){
			string guardar = " ";
			do{
				Console.Clear();
				Dibujar();
				int posY =0;
				int posX = ((Console.WindowWidth-(50+((discos*2)+1)))/2);
				posY = posY + discos + 11;
				Console.SetCursorPosition (posX, posY);
				Console.Write("¿Desea guardar la partida (s/n)? ");
				guardar=Console.ReadLine();
				if ((guardar!="s")&&(guardar!="S")&&(guardar!="n")&&(guardar!="N")){
					centrar();
					Console.Write("Introduzca opción correcta. ");
					Console.ReadKey();

				} else {
					if((guardar=="s")||(guardar=="S")){
						Serializador s = new Serializador("partida.dat");
						s.Guardar(this);
					}
				}
			}while ((guardar!="s")&&(guardar!="S")&&(guardar!="n")&&(guardar!="N"));
		}

		/** 
		* 
		* Función encargada de marcar el disco movido como seleccionado para ser pintado en verde.
		*
		*/
		public void SeleccionaDisco(Stack vari){
			Disco t;
			t=(Disco) vari.Pop();
			t.setseleccionado(true);
			vari.Push(t);
		}

		/** 
		* 
		* Función que añade movimientos a nuestro contador.
		*
		*/
		public void Contar (){
			contador=contador+1;
		}

		/** 
		* 
		* Función a la que le pasamos la varilla de origen y la de destino y determina si el movimiento se lleva a cabo
		* o no. También es el encargado de marcar como deseleccionado el disco movido (para que se pinte en rojo). 
		*
		*/
		public  void MoverDisco(Stack origen, Stack destino){
			Disco t;
			Disco t1;

			if (origen == destino) {
				DeseleccionaDisco (origen);
				Dibujar ();
			} else {
				t = (Disco)origen.Pop ();
				if (destino.Count > 0) {
					t1 = (Disco)destino.Pop ();
					if (t.getDisco () < t1.getDisco ()) {
						destino.Push (t1);
						destino.Push (t);
						Contar ();
						DeseleccionaDisco (destino);
						Dibujar ();
					} else {
						centrar();
						Console.WriteLine ("No se puede colocar un disco sobre otro más pequeño que él");
						Console.ReadKey ();
						origen.Push (t);
						destino.Push (t1);								
						DeseleccionaDisco (origen);
						Dibujar ();
					}
				} else {
					destino.Push (t);
					Contar ();
					DeseleccionaDisco (destino);
					Dibujar ();
				}
			}
		}

		/** 
		* 
		* Función encargada de marcar el disco movido como deseleccionado (rojo)
		*
		*/
		public void DeseleccionaDisco(Stack vari){
			Disco t;
			t=(Disco) vari.Pop();
			t.setseleccionado(false);
			vari.Push(t);
		}

		/** 
		* 
		* Función que detecta si hemos acabado una partida con éxito. Solicita al usuario un nombre para nuestro
		* ranking limitado a 14 caracteres. En caso de no introducirlo, lo llamaremos "Anónimo"
		*
		*
		*/
		public  bool finpartida(){

			bool fin = false;
			if (v2.Count == discos || v3.Count == discos) {
				string nombreuser = "";
				Console.Write ("Partida acabada.");
				centrar ();
				Console.Write ("Introduzca nombre para guardar su puntuación: ");
				nombreuser = Console.ReadLine ();
				if (nombreuser.Length > 14)
					nombreuser=nombreuser.Substring (0, 14);
				if (nombreuser == "") {
					nombreuser = "Anónimo";
				}
				grabaRanking (nombreuser);
				fin = true;
			}

			return fin;
		}

		/** 
		* 
		*Función que añade a nuestro ranking.txt el nombre de jugador, el nº de discos y el nº de movimientos. 
		*Esta función es llamada desde finpartida().
		*
		*/	
		public void grabaRanking(string nombre){
			string nombrefich = "ranking.txt";
			StreamWriter fichero;
			fichero = File.AppendText (nombrefich);
			fichero.WriteLine(nombre+";"+discos+";"+contador);
			fichero.Close ();
		}
			
		/** 
		* 
		*Función encargada de ir redibujando cada vez nuestra pantalla de juego, se dibujan los movimientos
		*y se va llamando a la función DibujaVarilla() para ir pintando cada una de las varillas.
		*Bajo estas, se dibuja los nº que representan a cada una de las varillas.
		*
		*/
		public void Dibujar(){
			Console.Clear ();
			while (Console.WindowHeight < 24) {   //Con este bucle compruebo si la altura de pantalla es suficientemente alta como para que quepan las varillas.
				if (Console.WindowHeight < 20) {
					Console.WriteLine ("Es necesario que la ventana sea más alta para continuar con el programa.");
					Console.ReadKey ();
					Console.Clear ();
				}
			}
			int posX = ((Console.WindowWidth-(50+((discos*2)+1)))/2)+50;
			int posY = 3;
			Console.SetCursorPosition (posX, posY);
			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine ("Nº Movimientos: "+ contador);
			posX = posX - 50;
			posY = 8;
			DibujaVarilla (posX, posY, v1);
			Console.Write ("1");
			posY = 8;
			posX = posX + 25;
			DibujaVarilla (posX, posY, v2);
			Console.Write ("2");
			posY = 8;
			posX = posX + 25;
			DibujaVarilla (posX, posY, v3);
			Console.Write ("3");
			//posY = Console.WindowHeight - 5;
			posX = ((Console.WindowWidth-(50+((discos*2)+1)))/2);
			posY = posY + discos + 3;
			Console.SetCursorPosition (posX, posY);
			Console.ForegroundColor = ConsoleColor.White;
		}


		/** 
		* 
		*Función encargada de dibujar cada una de las tres varillas de nuestro juego. Se llama desde la función Dibujar()
		*Lo primero que hace es dibujar los espacios libres de nuestras Varillas y luego recorre
		*la pila a dibujar y va pintando la cadena dibujo de cada uno de nuestros discos.
		*
		*/
		public void DibujaVarilla(int x, int y, Stack vari){
			string space = Espacio ();

			Console.SetCursorPosition (x, y);
			Console.ForegroundColor = ConsoleColor.White;

			for (int i = vari.Count; i < discos; i++) {
				Console.WriteLine (space);
				y++;
				Console.SetCursorPosition (x, y);
			}
			Console.ForegroundColor = ConsoleColor.Red;
			foreach (Disco d in vari) {
				if (d.getSeleccionado())
					Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine(d.getDibujo());
				Console.ForegroundColor = ConsoleColor.Red;
				y++;
				Console.SetCursorPosition (x, y);
			}
			Console.SetCursorPosition (x + discos+1, Console.CursorTop+1);
			Console.ForegroundColor = ConsoleColor.Green;
		}

		/** 
		* 
		*Función encargada de rellenarnos la cadena que dibujaremos para representar las posiciones libres de
		*nuestras varillas. Se llama desde la función DibujaVarilla().
		*
		*/
		public  string Espacio(){
			string espacio = "";

			for (int i = 0; i < (discos +1); i++) {
				espacio = espacio + " ";
			}
			espacio=espacio+"*";
			for (int i = 0; i <= (discos+1); i++) {
				espacio = espacio + " ";
			}
			return espacio;
		}
	}
}

