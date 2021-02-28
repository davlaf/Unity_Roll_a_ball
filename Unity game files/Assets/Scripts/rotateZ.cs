using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateZ : MonoBehaviour {

	public float rotateSpeed; // vitesse de rotation
	public GameObject rotateAround; // l'objet que cet objet tourne autour
	// Update is called once per frame
	void Update () 
	{
		transform.RotateAround(rotateAround.transform.position, new Vector3 (0,0,1), rotateSpeed * Time.deltaTime); // tourne autour de l'objet
	}
}
