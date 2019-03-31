using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CargadorPokedex : MonoBehaviour {

	public Text texto, place;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
			SceneManager.LoadScene("Pokedex", LoadSceneMode.Single);
		}else{
			place.text = "Debes usar un nombre de usuario...";
		}
    }
}
