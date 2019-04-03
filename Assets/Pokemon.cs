using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : IEquatable<Pokemon>{

	public int numero;
	public string nombre;
	public int generacion;
	public bool legendario = false;
	public string tipo1;
	public string tipo2;
	public List<string> mega;
	public Sprite imagen;
	public string descripcion;

	public Pokemon(int numero,string nombre,int generacion,int legendario,string tipo1,string tipo2,List<string> mega,string descripcion){
		//Meter las megaevoluciones que tenga en una variable
		this.numero = numero;
		this.nombre = nombre;
		this.generacion = generacion;
		if (legendario == 1){
			this.legendario = true;
		}
		this.tipo1 = tipo1;
		this.tipo2 = tipo2;
		this.mega = mega;
		try{
			this.imagen = ConexionMongoDB.LoadTexture(numero.ToString()+".png");
		}catch (Exception e){
			this.imagen = null;
			Debug.Log("Excepcion cargando imagen de "+numero.ToString()+": "+e.Message);
		}
		this.descripcion = descripcion;
	}

	override
	public string ToString(){
		string texto = "";
		texto+="Numero: "+numero;
		texto+="\n\tNombre: "+nombre;
		texto+="\n\tTipo primario: "+tipo1;
		texto+="\n\tTipo secundario: "+tipo2;
		texto+="\n\tGeneracion: "+generacion;
		texto+="\n\tLegendario: "+legendario;

		return texto;
	}

	public bool Equals(Pokemon p){
		if (nombre.Equals(p.nombre)){
			return true;
		}
		return false;
	}
}
