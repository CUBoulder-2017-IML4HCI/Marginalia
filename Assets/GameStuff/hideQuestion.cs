using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class hideQuestion : MonoBehaviour {

	public Canvas emotionCanvas;
	public Canvas menuCanvas;
	public Canvas artifactCanvas;
	public Canvas messageCanvas;
	public Canvas photoCanvas;

	// Use this for initialization
	void Start () {
		emotionCanvas.GetComponent<Canvas> ();
		emotionCanvas.enabled = true;
		menuCanvas.enabled = false;
		artifactCanvas.enabled = false;
		messageCanvas.enabled = false;
		photoCanvas.enabled = false;
	}

	public void hide(){
		emotionCanvas.enabled = false;
		menuCanvas.enabled = true;
		artifactCanvas.enabled = true;
	}

	public void onMenuClick(){
		SceneManager.LoadScene ("Splash Screen");	
	}

	public void onMessageClick(){
		menuCanvas.enabled = false;
		artifactCanvas.enabled = false;
		messageCanvas.enabled = true;
	}

	public void onPhotoClick(){
		menuCanvas.enabled = false;
		artifactCanvas.enabled = false;
		photoCanvas.enabled = true;
	}

	public void artifactCancel(){
		menuCanvas.enabled = true;
		artifactCanvas.enabled = true;
		photoCanvas.enabled = false;
		messageCanvas.enabled = false;
	}
}
