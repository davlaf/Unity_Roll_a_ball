using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject player;
	public float sensitivity; 
	public LayerMask notPlayer;
	public bool button;
	public float distance;

	void Start () {
		Cursor.visible = false;
	}
	// Update is called once per frame 
	void Update () {
		if (Input.GetMouseButton(1) | button)
        {
			float mouse = Input.GetAxis ("Mouse Y") * sensitivity *-1; // 
			if (mouse > 2f)
			{
				mouse = 2f;
			}
			else if (mouse < -2f) // limiter la vitesse de la caméra verticale
			{
				mouse = -2f;
			}
			if (transform.eulerAngles.x < 5f && mouse > 0) // limiter le mouvement de la camera
			{
				transform.RotateAround (player.transform.position, transform.right, mouse);
			} 
			else if (transform.eulerAngles.x > 80f && mouse < 0) 
			{
				transform.RotateAround (player.transform.position, transform.right, mouse);
			}
			else if (transform.eulerAngles.x < 80f && transform.eulerAngles.x > 5f)
			{
				transform.RotateAround (player.transform.position, transform.right, mouse);
			}
        }
		distance += Input.mouseScrollDelta.y*-1;
		if (distance < 2f) {
			distance = 2f;
		} else if (distance > 20f) {
			distance = 20f;
		}
		// si la il y a qqc derriere la caméra, mettre la camera à ce point
		var heading = transform.position - player.transform.position;
		var direction = heading / heading.magnitude;
		RaycastHit hit;
		if (Physics.Raycast (player.transform.position, direction, out hit, distance, notPlayer, QueryTriggerInteraction.Collide)) {
			transform.position = hit.point;
		} else {
			transform.position = player.transform.position + direction * distance;
		}
	}
}
