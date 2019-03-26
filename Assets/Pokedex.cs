using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pokedex : MonoBehaviour {

	public int pokemonACargar = 1;

	string pathXML = "Assets/users.xml";
	string pathMongoDB = "mongodb://localhost:27017";

	public bool recargarImagenes = false;
	public Image imagen,imagen_mega,imagen_mega2;
	public Text texto,texto_mega1,texto_mega2;

	void Start () {
		//MongoDB
		ConexionMongoDB.Conectar(pathMongoDB);

		if (recargarImagenes){
			ConexionMongoDB.CargarImagenesPokemon();
		}

		Pokemon temp = ConexionMongoDB.BuscarPokemon(pokemonACargar);

		Debug.Log(temp);

		CargarPokemon(temp);
		

		string t = "";
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
		}
		//Debug.Log(t);

		//XML
		User jaime = new User();
        jaime.nombre = "Jaime";
		jaime.opciones = new string[]{"gen1","gen3"};
		User tom = new User();
        tom.nombre = "Tom";
		tom.opciones = new string[]{"gen2"};

		XMLPokedexDatos datos = new XMLPokedexDatos();
		datos.users = new User[]{jaime,tom};

		XML.GuardarXML(datos,pathXML);

		XMLPokedexDatos datosXML = XML.CargarXML<XMLPokedexDatos>(pathXML);
        Debug.Log(datosXML.ToString());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void CargarPokemon(Pokemon p){
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
	}
}
