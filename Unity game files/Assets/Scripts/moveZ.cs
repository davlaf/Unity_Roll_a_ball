using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class moveZ : MonoBehaviour {

	public float moveSpeed; // vitesse de mouvement
	public float minZ; // les bords 
    public float maxZ;
	
	// Update is called once per frame
	void Update () {
		if (transform.position.z > maxZ) // si la boite est trop loin
        {
			moveSpeed = Math.Abs(moveSpeed) * -1f; // change de direction
        }
		if (transform.position.z < minZ) // si la boite est trop loin
        {
			moveSpeed = Math.Abs(moveSpeed); // change de direction
        }
		transform.position += new Vector3(0,0,moveSpeed * Time.deltaTime); //bouge la boite
    }
}
