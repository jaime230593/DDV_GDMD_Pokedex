﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokemon : IEquatable<Pokemon>{

	int numero;
	string nombre;
	int generacion;
	bool legendario = false;
	string tipo1;
	string tipo2;

	public Pokemon(int numero,string nombre,int generacion,int legendario,string tipo1,string tipo2){
		this.numero = numero;
		this.nombre = nombre;
		this.generacion = generacion;
		if (legendario == 1){
			this.legendario = true;
		}
		this.tipo1 = tipo1;
		this.tipo2 = tipo2;
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