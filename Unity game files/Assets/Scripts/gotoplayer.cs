using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gotoplayer : MonoBehaviour {

	// Use this for initialization

	public GameObject player;
	// Update is called once per frame
	void Update () {
		transform.position = player.transform.position;
		transform.eulerAngles += new Vector3 (0,Input.GetAxis("Mouse X")*3,0);
	}
}
