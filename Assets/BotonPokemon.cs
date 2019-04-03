using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BotonPokemon : MonoBehaviour {
	public Pokemon pokemon;
    Button boton;

	public GameObject vistaPokemon;
	public GameObject panel;
	public Image imagen;
	public Text numero,nombre,descripcion;

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

		vistaPokemon.SetActive(true);
		panel.SetActive(false);
	}
}
