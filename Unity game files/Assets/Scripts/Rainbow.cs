using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rainbow : MonoBehaviour {

	private Renderer rend;
	private float color;

	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		color = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		color += 0.005f;
		if (color > 1) 
		{
			color = 0f;
		}
		rend.material.SetColor ("_Color", Color.HSVToRGB (color, 1, 0.8f));
	}
}
