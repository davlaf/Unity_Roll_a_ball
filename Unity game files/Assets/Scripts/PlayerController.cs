using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    
	public float speed; // vitesse de la balle
	public float normalspeed; // vitesse normale de la balle (utilisé avec les speed trigger)
	public float fastspeed; // vitesse vite de la balle
    public Text countText; // texte qui apparait en haut a la gauche, dit le nombre de points
    public Text winText; // texte qui apparait par dessus la balle
    public float jumpForce; // force du saut de la balle
	public float normaljump; // force du saut normale de la balle (utilisé avec les jump trigger)
	public float bigjump; // force du grand saut de la balle
    public LayerMask groundLayers; // indique toutes les choses que si la balle touche elle peut sauter
    public SphereCollider col; // le collider de la balle, utilisé pour voir si le joueur peut sauter
	public GameObject cameraContainer; // définit le container de la caméra, utilisé pour connaitre la rotation Y de la caméra
    public int level; // le niveau du joueur
	public Vector3 checkpointPos; // position du checkpoint
    public Text timetext; // texte pour le temps
	public int count; // nombre de points
	public Text deathtext; // texte pour le nombre de fois que l'utilisateur est mort
	public AudioClip JumpClip; // son pour saut
	public AudioClip CoinClip; // son pour coin
	public AudioClip DeathClip; // son pour mort
	public AudioSource deathsource;

	private int deathcount; // nombre de fois que l'utilisateur est mort
	private string finaltime; // temps final
    private Rigidbody rb;
    private Transform camTransform;

    void Start () // initialise les variables
    {
		deathsource.clip = DeathClip;
		level = 1;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<SphereCollider>();
        count = 0;
		deathcount = 0;
        SetCountText();
        winText.text = "";
		deathtext.text = "Essais: 0";
        camTransform = cameraContainer.transform;
		checkpointPos = new Vector3 (-115f, 5f, -10f);
		resetPosition ();
    }

    private void Update()
    {
		if (Input.GetButtonDown("Jump") && IsGrounded()) // seulement laisser l'utilisateur sauter si il est sur le plancher
		{ 
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
		if (level == 4) {
			timetext.text = finaltime;
		} else {
			timetext.text = "Temps: " + String.Format("{0:F2}", System.Math.Round(Time.time,2)); //arrondis à deux places décimales et ajoute des 0 p.ex. 10 devient 10.00
		}
    }

    void FixedUpdate ()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");
		Vector3 movement = new Vector3 (moveHorizontal,0.0f,moveVertical); // les axis du joueur selon les x et z globaux
        Vector3 camAdjusted = camTransform.TransformDirection(movement); // ajuster les axis selon l'axe de rotation Y de la camera
        Debug.Log(movement);
		rb.AddForce(camAdjusted * speed); // bouger l'utilisateur 
		if (transform.position.y < -5) // si l'utilisateur a tombé
		{
			resetPosition();
			deathsource.Play();
			deathcount ++;
			deathtext.text = "Essais: " + deathcount.ToString();
		}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            other.gameObject.SetActive(false);
            count += 1;
            SetCountText();
        }
		if (other.gameObject.CompareTag("speed")) // si l'utilsateur rentre dans un speed trigger
		{
			speed = fastspeed; // mettre la vitesse à la vitesse rapide
		}
		if (other.gameObject.CompareTag("jumpBoost")) // si l'utilsateur rentre dans un jump boost trigger
		{
			jumpForce = bigjump; // mettre la force de saut à un grand saut
		}
        if (other.gameObject.CompareTag("kill")) // si l'utilisateur touche une chose rouge,
        {
			resetPosition(); // retourner au début du niveau
			deathsource.Play();
			deathcount ++;
			deathtext.text = "Essais: " + deathcount.ToString();
        }
		if (other.gameObject.CompareTag("checkpoint"))
		{
			checkpointPos = other.transform.position;
			other.gameObject.SetActive(false);
		}
        if (other.gameObject.CompareTag("teleport")) // teleport c'est le gros bloc mauve
        {
            if (level == 1 & count == 4) // si l'utilisateur a assez de points au niveau 1,
            {
                SetCountText(); // update le compteur
				checkpointPos = new Vector3(0f, 5f, 0f); // changer la position d'ou le joueur va apparaitre
				resetPosition(); // teleporter au prochain niveau
				count = 0; // reset le nombre de points
				level = 2; // aller au prochain niveau
            }
            if (level == 2 & count == 7) // même que l'autre niveau
            {
                SetCountText();
				checkpointPos = new Vector3(140f, 5f, -5f);
				resetPosition();
				level = 3;
				count = 0;
            }
			if (level == 3 & count == 13) // même que l'autre niveau
			{ 
				checkpointPos = new Vector3 (300f, 8f, 0f);
				resetPosition ();
				level = 4;
				finaltime = "Temps final: " + String.Format("{0:F2}", System.Math.Round(Time.time,2)); // calcule le temps final
				count = 0;
			} else { // si l'utilisateur n'a pas assez de points
				//filtre les fauses activations due au multiples evenements de collision
				//sans le if ca dit pas assez de points 1 frame après que tu as été téléporté
				if (transform.position != new Vector3 (0f, 5f, 0f) && transform.position != new Vector3 (140f, 5f, -5f) && transform.position != new Vector3 (300f, 8f, 0f)) 
				{
					winText.text = "Pas assez de points!";
				}
				SetCountText ();
			}
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("speed")) // si la sort du speed trigger
        {
            speed = normalspeed; // remettre la vitesse à la vitesse normale
        }
		if (other.gameObject.CompareTag("jumpBoost")) // si la sort du jump boost trigger
        {
            jumpForce = normaljump; // remettre la force du saut au saut normal
        }
        if (other.gameObject.CompareTag("teleport")) // reset le message de pas assez de points
        {
            winText.text = "";
			SetCountText ();
        }
    }

    void SetCountText ()
    {
        countText.text = "Points: " + count.ToString();
		if (level == 4) // si l'utilisateur a terminé le jeu
        {
			countText.text = "Points: INFINI!!!";
            winText.text = "Tu as gagné!";
        }
    }

    private void resetPosition() // retourner au checkpoint après d'avoir mourut
    {
		transform.position = checkpointPos;
    }

    private bool IsGrounded()
    {
		if (level != 4) {
			return Physics.CheckCapsule (col.bounds.center, new Vector3 (col.bounds.center.x, 
				col.bounds.min.y, col.bounds.center.z), col.radius * 0.9f, groundLayers); // vérifie si une capsule qui sort seulement du bas est en collision avec le sol
		} else {
			return true;
		}
			
	}
}

