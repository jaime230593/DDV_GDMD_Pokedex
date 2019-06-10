using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CargadorPokedex : MonoBehaviour {
	public Text texto, place;
	public string escena;

	//1
	//public int filtro = 0;
	//2
	public Toggle[] gen;
	//3
	public Toggle[] tipos;
	//4
	public Toggle legenPorTipos;

	//1
	public void CargarEscenaFiltroTodos(){
		GameObject.FindGameObjectWithTag("permanente").GetComponent<Permanente>().filtro = 1;
		SceneManager.LoadScene(escena, LoadSceneMode.Single);
	}
	//2
	public void CargarEscenaFiltroPorGeneracion(){
		GameObject.FindGameObjectWithTag("permanente").GetComponent<Permanente>().filtro = 2;
		List<int> temp = new List<int>();
		foreach (Toggle tmp in gen){
			if (tmp.isOn){
				temp.Add(int.Parse(tmp.transform.GetChild(1).GetComponent<Text>().text));
			}
		}
		GameObject.FindGameObjectWithTag("permanente").GetComponent<Permanente>().gen = temp.ToArray();

		SceneManager.LoadScene(escena, LoadSceneMode.Single);
	}
	//3
	public void CargarEscenaFiltroPorTipos(){
		GameObject.FindGameObjectWithTag("permanente").GetComponent<Permanente>().filtro = 3;
		List<string> temp = new List<string>();
		Debug.Log(tipos.Length);
		foreach (Toggle tmp in tipos){
			Debug.Log(tmp.transform.GetChild(1).GetComponent<Text>().text.ToLower());
			if (tmp.isOn){
				temp.Add(tmp.transform.GetChild(1).GetComponent<Text>().text.ToLower());
			}
		}
		GameObject.FindGameObjectWithTag("permanente").GetComponent<Permanente>().tipos = temp.ToArray();

		SceneManager.LoadScene(escena, LoadSceneMode.Single);
	}
	//4 legendarios
	public void CargarEscenaFiltroPorLegendarios(){
		GameObject.FindGameObjectWithTag("permanente").GetComponent<Permanente>().filtro = 4;
		GameObject.FindGameObjectWithTag("permanente").GetComponent<Permanente>().legenPorTipos = legenPorTipos.isOn;
		if (legenPorTipos.isOn){
			List<string> temp = new List<string>();
			foreach (Toggle tmp in tipos){
				if (tmp.isOn){
					temp.Add(tmp.transform.GetChild(1).GetComponent<Text>().text.ToLower());
				}
			}
			GameObject.FindGameObjectWithTag("permanente").GetComponent<Permanente>().tipos = temp.ToArray();
		}
		
		SceneManager.LoadScene(escena, LoadSceneMode.Single);
	}

	public void CargarEscena(){
		SceneManager.LoadScene(escena, LoadSceneMode.Single);
	}

	public void Cargar(){
		if (texto.text != string.Empty){
			XMLPokedexDatos datosXML = XML.CargarXML();
			List<User> datosGuardar = new List<User>();
			User nuevo = null;

			if (datosXML != null){
				foreach (User u in datosXML.users){
					datosGuardar.Add(u);
					if (u.nombre.Equals(texto.text.ToLower())){
						Debug.Log("Existe");
						nuevo = u;
					}
				}
			}

			if (nuevo == null){
				nuevo = new User();
				nuevo.nombre = texto.text.ToLower();
				datosGuardar.Add(nuevo);
			}

			XMLPokedexDatos temp = new XMLPokedexDatos();
			temp.users = datosGuardar.ToArray();
			XML.GuardarXML(temp, XML.pathXML);

			GameObject.FindGameObjectWithTag("permanente").GetComponent<Permanente>().CargarUsuario(nuevo);
			SceneManager.LoadScene(escena, LoadSceneMode.Single);
		}else{
			place.text = "Debes usar un nombre de usuario...";
		}
	}

	public static void Cargar(string usuarioForzado){
		if (usuarioForzado != string.Empty){
			XMLPokedexDatos datosXML = XML.CargarXML();
			List<User> datosGuardar = new List<User>();
			User nuevo = null;

			if (datosXML != null){
				foreach (User u in datosXML.users){
					datosGuardar.Add(u);
					if (u.nombre.Equals(usuarioForzado.ToLower())){
						Debug.Log("Existe");
						nuevo = u;
					}
				}
			}

			if (nuevo == null){
				nuevo = new User();
				nuevo.nombre = usuarioForzado.ToLower();
				datosGuardar.Add(nuevo);
			}

			XMLPokedexDatos temp = new XMLPokedexDatos();
			temp.users = datosGuardar.ToArray();
			XML.GuardarXML(temp, XML.pathXML);

			GameObject.FindGameObjectWithTag("permanente").GetComponent<Permanente>().CargarUsuario(nuevo);
		}
	}


}
