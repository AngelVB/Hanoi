using System;
using System.IO;
using System.Collections;

namespace Hanoi
{
public class puestoRanking : IComparable
{ 
	 string nombre; 
	 string nDiscos; 
	 int movimientos; 
	
		public string getNombre(){
			return nombre;
		}

		public string getNdiscos(){
			return nDiscos;
		}

		public int getMovimientos(){
			return movimientos;
		}

		public void setNombre(string nom){
			nombre = nom;
		}

		public void setNdiscos(string ndis){
			nDiscos = ndis;
		}

		public void setMovimientos(int mov){
			movimientos = mov;
		}

	public int CompareTo(object obj) //función utilizada para ordenar nuestros ránkings.
	{
		puestoRanking otro = obj as puestoRanking;
		return this.movimientos.CompareTo(otro.movimientos);
	}
}
}