using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Permanente : MonoBehaviour {

	public User user;

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CargarUsuario(User u){
		user = u;
	}

	public void ActualizarPokemonUsuario(Pokemon p){
		user.pokemonACargar = p.numero.ToString();
	}
}
