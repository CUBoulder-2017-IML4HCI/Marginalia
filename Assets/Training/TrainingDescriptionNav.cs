using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TrainingDescriptionNav : MonoBehaviour {

	public Button backButton;
	public Button continueButton;

	// Use this for initialization
	void Start () {
		backButton = backButton.GetComponent<Button> ();
		continueButton = continueButton.GetComponent<Button> ();
	}

	public void BackPress(){
		SceneManager.LoadScene ("Splash Screen");
	}

	public void ContinuePress(){
		SceneManager.LoadScene ("CameraCheck");
	}

}
