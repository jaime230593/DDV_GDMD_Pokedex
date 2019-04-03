﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Permanente : MonoBehaviour {

	public User user;
	//Para indicar si es la escena main, para trampear el usuario a jaime siempre si no se ejecuta desde la main
	public bool main = false;

	void Awake () {
		if (GameObject.FindGameObjectWithTag("permanente") == null){
			Destroy(gameObject);
		}else{
			if (!main){
				CargadorPokedex.Cargar("jaime");
			}
			DontDestroyOnLoad(gameObject);
		}
	}
	
	public void CargarUsuario(User u){
		user = u;
	}

	public void ActualizarPokemonUsuario(Pokemon p){
		user.pokemonACargar = p.numero.ToString();
	}
}
