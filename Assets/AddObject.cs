using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddObject : MonoBehaviour {

	public GameObject o, grid;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Add(){
		Instantiate(o,o.transform).transform.SetParent(grid.transform);
	}
}
