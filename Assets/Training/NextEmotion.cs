using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextEmotion : MonoBehaviour {

	public string nextScene;
	public Button continueButton;

	// Use this for initialization
	void Start () {
		continueButton = continueButton.GetComponent<Button> ();
	}
	
	public void ContinuePress(){
		SceneManager.LoadScene (nextScene);
	}
}
