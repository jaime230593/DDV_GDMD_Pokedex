using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pokedex : MonoBehaviour {
	Permanente permanente;

	//public int pokemonsACargar = 151;

	public bool recargarImagenes = false;
	//public Image imagen,imagen_mega,imagen_mega2;
	//public Text texto,texto_mega1,texto_mega2;
	public Text texto_usuario;
	//public Pokemon pokemonCargado = null;
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
		switch (permanente.filtro){
			case 1:{
				foreach (Pokemon p in ConexionMongoDB.CogerPokemons()){
					CargarVistaPokemon(p);
				}
				break;
			}

			case 2:{
				foreach (Pokemon p in ConexionMongoDB.BuscarPorGeneracion(permanente.gen)){
					CargarVistaPokemon(p);
				}
				break;
			}

			case 3:{
				foreach (Pokemon p in ConexionMongoDB.BuscarPorTipos(permanente.tipos)){
					CargarVistaPokemon(p);
				}
				break;
			}

			case 4:{
				if (permanente.legenPorTipos){
					foreach (Pokemon p in ConexionMongoDB.BuscarPorLegendarioTipos(permanente.tipos)){
						CargarVistaPokemon(p);
					}
				}else{
					foreach (Pokemon p in ConexionMongoDB.BuscarPorLegendario()){
						CargarVistaPokemon(p);
					}
				}
				
				break;
			}
		}
	}
}
