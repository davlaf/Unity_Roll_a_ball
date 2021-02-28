using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class moveX : MonoBehaviour {

    public float moveSpeed; // vitesse de mouvement
    public float minX; // les bords 
    public float maxX;
	
	// Update is called once per frame
	void Update () {
        if (transform.position.x > maxX) // si la boite est trop loin
        {
            moveSpeed = Math.Abs(moveSpeed) * -1f; // change de direction
        }
		if (transform.position.x < minX) // si la boite est trop loin
        {
			moveSpeed = Math.Abs(moveSpeed); // change de direction
        }
        transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0); //bouge la boite
    }
}
