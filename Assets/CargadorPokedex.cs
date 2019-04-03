﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CargadorPokedex : MonoBehaviour {
	public Text texto, place;
	public string escena;

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
