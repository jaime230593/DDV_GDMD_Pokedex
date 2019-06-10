using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;

public class CargadorPokedex : MonoBehaviour {
	public Text texto, place;
	public string escena;

	public Toggle[] gen;
	public Toggle[] tipos;
	public Toggle legen;

	public void CargarEscenaFiltroTodos(){
		Permanente p = GameObject.FindGameObjectWithTag("permanente").GetComponent<Permanente>();

		//generaciones
		List<int> gens = new List<int>();
		foreach (Toggle tmp in gen){
			if (tmp.isOn){
				gens.Add(int.Parse(tmp.transform.GetChild(1).GetComponent<Text>().text));
			}
		}
		p.gen = gens.ToArray();

		//tipos
		List<string> tips = new List<string>();
		Debug.Log(tipos.Length);
		foreach (Toggle tmp in tipos){
			Debug.Log(tmp.transform.GetChild(1).GetComponent<Text>().text.ToLower());
			if (tmp.isOn){
				tips.Add(tmp.transform.GetChild(1).GetComponent<Text>().text.ToLower());
			}
		}
		p.tipos = tips.ToArray();

		//legendarios
		p.legen = legen.isOn;

		SceneManager.LoadScene(escena, LoadSceneMode.Single);
	}

	public void CargarImagenes(){
		Text tmp = transform.GetChild(0).gameObject.GetComponent<Text>();
		ConexionMongoDB.Conectar();
		tmp.text = "Cargando imagenes";
		ConexionMongoDB.CargarImagenesPokemon();
		tmp.text = "Imagenes cargadas";
	}

	public void CargarEscena(){
		SceneManager.LoadScene(escena, LoadSceneMode.Single);
	}

	public void Cargar(){
		if (File.Exists("user.xml")){
			XMLPokedexDatos datosXML = XML.CargarXML();
			List<User> datosGuardar = new List<User>();
			User nuevo = null;

			if (datosXML != null){
				nuevo = datosXML.user;
				Debug.Log("Existe");

				GameObject.FindGameObjectWithTag("permanente").GetComponent<Permanente>().CargarUsuario(nuevo);
				SceneManager.LoadScene(escena, LoadSceneMode.Single);
			}else{
				place.text = "No hay un nombre de usuario correcto o no hay archivo xml, mirar xml de ejemplo para ver su estructura y crearlo en la ubicacion del ejecutable";
			}
		}else{
			place.text = "No hay un nombre de usuario correcto o no hay archivo xml, mirar xml de ejemplo para ver su estructura y crearlo en la ubicacion del ejecutable";
		}
	}
}