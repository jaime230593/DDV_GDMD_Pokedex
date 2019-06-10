using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonPokemon : MonoBehaviour {
	public Pokemon pokemon;
    Button boton;

	public GameObject vistaPokemon;
	public GameObject panel;
	public Image imagen,mega1,mega2;
	public Text numero,nombre,descripcion,tipo1,tipo2,japones;

	void Start () {
		boton = GetComponent<Button>();
		boton.onClick.AddListener(CargarInformacion);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CargarInformacion(){
		imagen.sprite = pokemon.imagen;
		numero.text = "Nº "+pokemon.numero.ToString();
		nombre.text = pokemon.nombre;
		descripcion.text = pokemon.descripcion;
		tipo1.text = pokemon.tipo1;
		tipo2.text = pokemon.tipo2;
		japones.text = pokemon.japones;
		if (mega1 != null && pokemon.mega.Count > 0){
			mega1.sprite = ConexionMongoDB.LoadTexture(pokemon.mega[0]);
		}else{
			mega1.sprite = null;
		}
		if (mega2 != null && pokemon.mega.Count > 1){
			mega2.sprite = ConexionMongoDB.LoadTexture(pokemon.mega[1]);
		}else{
			mega2.sprite = null;
		}
		

		vistaPokemon.SetActive(true);
		panel.SetActive(false);
	}
}
