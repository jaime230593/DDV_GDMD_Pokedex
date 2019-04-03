using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcultarObjeto : MonoBehaviour {
	public GameObject objeto;
	public GameObject mostrar;

	public void Ocultar(){
		objeto.SetActive(false);
		mostrar.SetActive(true);
	}
}
