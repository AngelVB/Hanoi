using System;
using System.IO;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
namespace Hanoi
{
	/** 
		* 
		* 
		* Clase encargada de guardar y cargar una partida para poder continuar con ella más adelante.
		*
		*/
		public class Serializador
		{
			string nombre;
			public Serializador(string nombreFich)
			{
				nombre = nombreFich;
			}
			public void Guardar(partida objeto)
			{
				IFormatter formatter = new SoapFormatter();
				Stream stream = new FileStream(nombre,
					FileMode.Create, FileAccess.Write, FileShare.None);
				formatter.Serialize(stream, objeto);
				stream.Close();
			}
			public partida Cargar()
			{
				partida objeto;
				IFormatter formatter = new SoapFormatter();
				Stream stream = new FileStream(nombre,
					FileMode.Open, FileAccess.Read, FileShare.Read);
				objeto = (partida)formatter.Deserialize(stream);
				stream.Close();
				return objeto;
			}
		}
	}


