using System;
using System.IO;
using System.Collections;
namespace Hanoi
{
	public class Ranking
	{
		ArrayList[] ranking = new ArrayList[9];

		public Ranking ()
		{
			for (int i = 0; i < ranking.Length; i++)
			{
				ranking[ i] = new ArrayList();
			}
		}

		/** 
		* Carga las puntuaciones de las partidas finalizadas desde el fichero ranking.txt
		* y las almacena en el arraylist correspondiente (dependiendo del número de discos jugado)
		*
		*
		*/
		public void CargaRanking(){
			int numerodiscos;
			Console.Clear ();
			string nombrefichori = "ranking.txt";
			string linea;
			StreamReader fichorig;
			fichorig = File.OpenText (nombrefichori);
			do { 
				linea = fichorig.ReadLine ();
				if (linea!=null){
					string[] lineaArray=linea.Split(';');
					numerodiscos=Convert.ToInt32(lineaArray[1]);
					puestoRanking puesto=new puestoRanking();
					puesto.setNombre(lineaArray[0]);
					puesto.setNdiscos(lineaArray[1]);
					puesto.setMovimientos(Convert.ToInt32(lineaArray[2]));
					ranking[numerodiscos-2].Add(puesto);

				}
			} while (linea != null);
			fichorig.Close ();
		}


		/** 
		 * 
		 * Dibuja nuestro ranking.
		 * Automáticamente comprueba el ancho y alto de pantalla para no salirse de rango. también nos informa
		 * si no hay ningún record grabado en alguno de los niveles jugables.
		 * Previamente a dibujar los resultados, los ordena de menor a mayor número de movimientos. Para ello
		 * utilizamos la clase IComparer ComparadorRanking
		 * 
		 */
		public void DibujaRanking(){
			int posX=1;
			int posY=1;
			int posY2 = posY;
			int posX2 = posX;

			for (int i = 0; i < ranking.Length; i++) {
				if (Console.WindowHeight < (posY + ranking [i].Count+5)) {

					posY = 1;
					posX2 = posX2+40;
					posX = posX2;
					posY2 = posY;

				}
				if (Console.WindowWidth < (posX + 25)) {

					posX = 1;
					posY = Console.WindowHeight - 1;
					Console.SetCursorPosition (posX, posY);
					Console.WriteLine ("Pulse una tecla para continuar.");
					Console.ReadKey ();
					Console.Clear ();
					posX=1;
					posY=1;
					posY2 = posY;
					posX2 = posX;
				}
				if (ranking [i].Count > 0) {
					ranking [i].Sort ();

					Console.SetCursorPosition (posX, posY);
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine ("     Nº DE DISCOS: "+(i+2));
					Console.ForegroundColor = ConsoleColor.White;
					Console.SetCursorPosition (posX, posY+=1);
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine ("Nombre");
					Console.ForegroundColor = ConsoleColor.White;
					Console.SetCursorPosition (posX, posY++);
					foreach (puestoRanking puesto in ranking[i]) {
						Console.SetCursorPosition (posX, posY++);
						Console.WriteLine (puesto.getNombre());

					}
					posX = posX+15;
					posY = posY2+1;

					Console.SetCursorPosition (posX, posY++);
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine ("Movimientos");
					Console.ForegroundColor = ConsoleColor.White;
					posX = posX+4;
					Console.SetCursorPosition (posX, posY);
					foreach (puestoRanking puesto in ranking[i]) {
						Console.SetCursorPosition (posX, posY++);
						Console.WriteLine (puesto.getMovimientos());
					}
					Console.SetCursorPosition (posX, posY++);
					posX = posX2;
				
				} else {
					Console.SetCursorPosition (posX, posY);
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine ("     Nº DE DISCOS: "+(i+2));
					Console.SetCursorPosition (posX, posY+=2);
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine (" Ningún récord grabado.");
					Console.ForegroundColor = ConsoleColor.White;
					Console.WriteLine ();
					Console.SetCursorPosition (posX, posY+=2);
				}
					
				posY2 = posY;
			}
			posX = 1;
			posY = Console.WindowHeight - 1;
			Console.SetCursorPosition (posX, posY);
			Console.WriteLine ("Pulse una tecla para continuar.");
			Console.ReadKey ();

		}
	}
}

