using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Permanente : MonoBehaviour {

	public User user;
	//Para indicar si es la escena main, para trampear el usuario a jaime siempre si no se ejecuta desde la main
	public bool main = false;
	//1
	public int filtro = 0;
	//2
	public int[] gen = {1};
	//3
	public string[] tipos = {"grass"};
	//4
	public bool legen = false;

	void Start () {
		if (GameObject.FindGameObjectsWithTag("permanente").Length > 1){
			Destroy(gameObject);
		}else{
			/*if (!main){
				CargadorPokedex.Cargar("jaime");
			}*/
			DontDestroyOnLoad(gameObject);
		}
	}
	
	public void CargarUsuario(User u){
		user = u;
	}
}