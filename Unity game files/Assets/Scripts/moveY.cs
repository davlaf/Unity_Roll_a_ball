using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class moveY : MonoBehaviour {

	public float moveSpeed; // vitesse de mouvement
	public float minY; // les bords 
	public float maxY;

	// Update is called once per frame
	void Update () {
		if (transform.position.y > maxY) // si la boite est trop loin
		{
			moveSpeed = Math.Abs(moveSpeed) * -1f; // change de direction
		}
		if (transform.position.y < minY) // si la boite est trop loin
		{
			moveSpeed = Math.Abs(moveSpeed); // change de direction
		}
		transform.position += new Vector3(0, moveSpeed * Time.deltaTime, 0); //bouge la boite
	}
}

