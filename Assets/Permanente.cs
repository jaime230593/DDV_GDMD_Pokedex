using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Permanente : MonoBehaviour {

	public User user;
	public int[] gen = {1};
	public string[] tipos = {"grass"};
	public bool legen = false;

	void Start () {
		if (GameObject.FindGameObjectsWithTag("permanente").Length > 1){
			Destroy(gameObject);
		}else{
			DontDestroyOnLoad(gameObject);
		}
	}
	
	public void CargarUsuario(User u){
		user = u;
	}
}