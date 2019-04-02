using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarPokemon : MonoBehaviour {

	public bool derecha;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Cargar(){
		Pokedex pokedex = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Pokedex>();
		if (derecha){
			//pokedex.CargarSiguientePokemon();
		}else{
			//pokedex.CargarAnteriorPokemon();
		}
    }
}
