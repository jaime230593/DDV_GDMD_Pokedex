using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pokedex : MonoBehaviour {
	Permanente permanente;

	public bool recargarImagenes = false;
	//public Image imagen,imagen_mega,imagen_mega2;
	//public Text texto,texto_mega1,texto_mega2;
	public Text texto_usuario;
	//public Pokemon pokemonCargado = null;
	public GameObject prefabPokemon;
	public GameObject gridPokemons;
	public GameObject tamanioPokemon;

	void Start () {
		permanente = GameObject.FindGameObjectWithTag("permanente").GetComponent<Permanente>();
		texto_usuario.text = "Usuario: "+permanente.user.nombre;

		//MongoDB
		ConexionMongoDB.Conectar();

		if (recargarImagenes){
			ConexionMongoDB.CargarImagenesPokemon();
		}

		CargarPokemonsEnLista();

		//int pok = 1;

		/*if (permanente.user.pokemonACargar != null){
			pok = int.Parse(permanente.user.pokemonACargar);
		}*/

		//Pokemon temp = ConexionMongoDB.BuscarPokemon(pok);
		//CargarPokemon(temp);
		//Debug.Log(temp);

		/*string t = "";
		foreach (Pokemon p in ConexionMongoDB.BuscarPorGeneracion(new int[]{1,3})){
			t+="\n"+p.ToString();
		}
		//Debug.Log(t);

		t = "";
		foreach (Pokemon p in ConexionMongoDB.BuscarPorTipos(new string[]{"ice","grass"})){
			t+="\n"+p.ToString();
		}
		//Debug.Log(t);

		t = "";
		foreach (Pokemon p in ConexionMongoDB.BuscarPorLegendario(true)){
			t+="\n"+p.ToString();
		}*/
		//Debug.Log(t);

		//XML
		/*User jaime = new User();
        jaime.nombre = "Jaime";
		jaime.opciones = new string[]{"gen1","gen3"};
		User tom = new User();
        tom.nombre = "Tom";
		tom.opciones = new string[]{"gen2"};

		XMLPokedexDatos datos = new XMLPokedexDatos();
		datos.users = new User[]{jaime,tom};

		XML.GuardarXML(datos,pathXML);

		XMLPokedexDatos datosXML = XML.CargarXML<XMLPokedexDatos>(pathXML);
        Debug.Log(datosXML.ToString());*/
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/*public void CargarSiguientePokemon(){
		int n = pokemonCargado.numero+1;
		if (n>=721){
			n=721;
		}
		Pokemon temp = ConexionMongoDB.BuscarPokemon(n);
		CargarPokemon(temp);
	}

	public void CargarAnteriorPokemon(){
		int n = pokemonCargado.numero-1;
		if (n<=0){
			n=0;
		}
		Pokemon temp = ConexionMongoDB.BuscarPokemon(n);
		CargarPokemon(temp);
	}*/

	/*void CargarPokemon(Pokemon p){
		pokemonCargado = p;
		permanente.ActualizarPokemonUsuario(p);
		texto.text = p.nombre;
		imagen.sprite = ConexionMongoDB.LoadTexture(p.numero.ToString()+".png");
		List<string> megas = new List<string>();
		int n = 0;
		foreach (string s in p.mega){
			if (s.Contains("-mega.png") || s.Contains("-mega-x.png") || s.Contains("-mega-y.png") || s.Contains("-primal.png")){
				//Debug.Log(s);
				megas.Add(s);
				n++;
			}
		}

		switch (n){
			case 0:{
				Color c = imagen_mega.color;
				c.a = 0f;
				imagen_mega.color = c;
				imagen_mega2.color = c;
				texto_mega1.text = "";
				texto_mega2.text = "";
				break;
			}
			case 1:{
				imagen_mega.sprite = ConexionMongoDB.LoadTexture(megas[0]);
				Color c = imagen_mega2.material.color;
				c.a = 1f;
				imagen_mega.color = c;
				texto_mega1.text = "Mega "+p.nombre;

				c = imagen_mega2.material.color;
				c.a = 0f;
				imagen_mega2.color = c;
				texto_mega2.text = "";
				break;
			}
			case 2:{
				imagen_mega.sprite = ConexionMongoDB.LoadTexture(megas[0]);
				Color c = imagen_mega2.material.color;
				c.a = 1f;
				imagen_mega.color = c;
				texto_mega1.text = megas[0];

				imagen_mega2.sprite = ConexionMongoDB.LoadTexture(megas[1]);
				c = imagen_mega2.material.color;
				c.a = 1f;
				imagen_mega2.color = c;
				texto_mega2.text = megas[1];
				break;
			}
		}
	}*/

	void CargarPokemonsEnLista(){
		int n=151;
		foreach (Pokemon p in ConexionMongoDB.CogerPokemons()){
			//Debug.Log(p.ToString());
			if (n != 0){
				GameObject o = Instantiate(prefabPokemon,tamanioPokemon.transform);
				Image imagenBoton = o.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<Image>();
				Text nombre = o.transform.GetChild(1).gameObject.GetComponent<Text>();
				Text descripcion = o.transform.GetChild(2).gameObject.GetComponent<Text>();
				imagenBoton.sprite = p.imagen;
				nombre.text = p.nombre;
				descripcion.text = p.descripcion;
				
				o.transform.SetParent(gridPokemons.transform);
				n--;
			}
		}
		

		//MEGAS
		/*
		List<string> megas = new List<string>();
		int n = 0;
		foreach (string s in p.mega){
			if (s.Contains("-mega.png") || s.Contains("-mega-x.png") || s.Contains("-mega-y.png") || s.Contains("-primal.png")){
				//Debug.Log(s);
				megas.Add(s);
				n++;
			}
		}
		
		switch (n){
			case 0:{
				Color c = imagen_mega.color;
				c.a = 0f;
				imagen_mega.color = c;
				imagen_mega2.color = c;
				texto_mega1.text = "";
				texto_mega2.text = "";
				break;
			}
			case 1:{
				imagen_mega.sprite = ConexionMongoDB.LoadTexture(megas[0]);
				Color c = imagen_mega2.material.color;
				c.a = 1f;
				imagen_mega.color = c;
				texto_mega1.text = "Mega "+p.nombre;

				c = imagen_mega2.material.color;
				c.a = 0f;
				imagen_mega2.color = c;
				texto_mega2.text = "";
				break;
			}
			case 2:{
				imagen_mega.sprite = ConexionMongoDB.LoadTexture(megas[0]);
				Color c = imagen_mega2.material.color;
				c.a = 1f;
				imagen_mega.color = c;
				texto_mega1.text = megas[0];

				imagen_mega2.sprite = ConexionMongoDB.LoadTexture(megas[1]);
				c = imagen_mega2.material.color;
				c.a = 1f;
				imagen_mega2.color = c;
				texto_mega2.text = megas[1];
				break;
			}
		}*/
	}
}
