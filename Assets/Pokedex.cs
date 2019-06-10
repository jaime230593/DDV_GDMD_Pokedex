using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pokedex : MonoBehaviour {
	Permanente permanente;

	public bool recargarImagenes = false;
	public Text texto_usuario;
	public GameObject prefabPokemon;
	public GameObject gridPokemons;
	public GameObject tamanioPokemon;

	public GameObject vistaPokemon;
	public Image vistaImagen,vistaMega1,vistaMega2;
	public Text vistaNumero,vistaNombre,vistaDescripcion,vistaJapones,vistaTipo1,vistaTipo2;

	void Start () {
		permanente = GameObject.FindGameObjectWithTag("permanente").GetComponent<Permanente>();
		texto_usuario.text = "Usuario: "+permanente.user.nombre;

		//MongoDB
		ConexionMongoDB.Conectar();

		if (recargarImagenes){
			ConexionMongoDB.CargarImagenesPokemon();
		}

		CargarPokemonsEnLista();
	}

	void CargarVistaPokemon(Pokemon p){
		GameObject o = Instantiate(prefabPokemon,tamanioPokemon.transform);
		Image imagenBoton = o.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>();
		Text nombre = o.transform.GetChild(1).gameObject.GetComponent<Text>();
		Text descripcion = o.transform.GetChild(2).gameObject.GetComponent<Text>();
		imagenBoton.sprite = p.imagen;
		nombre.text = p.nombre;
		descripcion.text = p.descripcion;

		BotonPokemon botonPokemon = o.transform.GetChild(0).gameObject.GetComponent<BotonPokemon>();
		botonPokemon.pokemon = p;
		botonPokemon.vistaPokemon = vistaPokemon;
		botonPokemon.panel = gridPokemons.transform.parent.gameObject;
		
		botonPokemon.imagen = vistaImagen;
		botonPokemon.numero = vistaNumero;
		botonPokemon.nombre = vistaNombre;
		botonPokemon.descripcion = vistaDescripcion;
		botonPokemon.tipo1 = vistaTipo1;
		botonPokemon.tipo2 = vistaTipo2;
		botonPokemon.japones = vistaJapones;

		botonPokemon.mega1 = vistaMega1;
		botonPokemon.mega2 = vistaMega2;

		o.transform.SetParent(gridPokemons.transform);
	}

	void CargarPokemonsEnLista(){
		foreach (Pokemon p in ConexionMongoDB.CogerPokemons(permanente.gen, permanente.tipos, permanente.legen)){
			CargarVistaPokemon(p);
		}
	}
}
