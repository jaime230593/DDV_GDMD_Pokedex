using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pokedex : MonoBehaviour {

	string pathXML = "Assets/users.xml";
	string pathMongoDB = "mongodb://localhost:27017";

	public Image imagen;

	void Start () {
		//MongoDB
		ConexionMongoDB.Conectar(pathMongoDB);

		string t = "";
		foreach (Pokemon p in ConexionMongoDB.BuscarPorGeneracion(new int[]{1,3})){
			t+="\n"+p.ToString();
		}
		//Debug.Log(t);

		t = "";
		foreach (Pokemon p in ConexionMongoDB.BuscarPorTipos(new string[]{"ice","grass"})){
			t+="\n"+p.ToString();
		}
		Debug.Log(t);

		t = "";
		foreach (Pokemon p in ConexionMongoDB.BuscarPorLegendario(true)){
			t+="\n"+p.ToString();
		}
		//Debug.Log(t);

		ConexionMongoDB.CargarImagenesPokemon();
		ConexionMongoDB.LoadTexture(imagen, "1.png");

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
}
