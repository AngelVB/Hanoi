using System;
using System.IO;
using System.Collections;

namespace Hanoi
{
	[Serializable]
	public class Disco{
		int numdisco;
		string dibujo;
		bool seleccionado; //La utilizo para poder dibujar en verde cuando un disco es el que vamos a mover.

		public Disco(int numero, int discos){
			numdisco = numero;
			dibujo = DibujaDisco (numero,discos);
			seleccionado = false;
		}

		public int getDisco(){
			return numdisco;
		}

		public string getDibujo(){
			return dibujo;
		}

		public bool getSeleccionado(){
			return seleccionado;
		}

		public void setseleccionado(bool sel){
			seleccionado = sel;
			}

		/** 
		* 
		* Esta función nos rellena el string dibujo con los * necesarios, ya que el tamaño dependerá del nº de 
		* disco que queramos representar y del nº de discos
		* de nuestra partida, por eso se llama a la función desde el constructor.
		*
		*/
		public string DibujaDisco(int numdisco, int discos){

			string disco = "";
			Console.ForegroundColor = ConsoleColor.Red;

			for (int i = 0; i < (discos + 1) - numdisco; i++) {
				disco = disco + " ";
			}
			for (int i = 0; i < (numdisco*2) +1 ; i++) {
				disco = disco + "*";
			}
			for (int i = 0; i < (discos + 1) - numdisco; i++) {
				disco = disco + " ";
			}
			return disco;
		}

	}

}

