using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonhandler : MonoBehaviour {

	public GameObject instructions;
	public GameObject screen1;
	public GameObject examples;

    public void ChangeScene(string scenename)
    {
        SceneManager.LoadScene(scenename);
    }

    public void loadinstructions()
    {
		instructions.SetActive (true);
		examples.SetActive (true);
		screen1.SetActive (false);
    }

	public void gobacktotitle()
	{
		instructions.SetActive (false);
		examples.SetActive (false);
		screen1.SetActive (true);
	}
}
