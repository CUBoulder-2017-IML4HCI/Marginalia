using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour {

	public Canvas quitMenu;
	public Canvas startMenu;
	public Canvas aboutScreen;
	public Button newButton;
	public Button returningButton;
	public Button exitButton;
	public Button aboutButton;
	public Button backButton;

	// Use this for initialization
	void Start () {
		quitMenu = quitMenu.GetComponent<Canvas> ();
		startMenu = startMenu.GetComponent<Canvas> ();
		aboutScreen = aboutScreen.GetComponent<Canvas> ();
		newButton = newButton.GetComponent<Button> ();
		returningButton = returningButton.GetComponent<Button> ();
		exitButton = exitButton.GetComponent<Button> ();
		aboutButton = aboutButton.GetComponent<Button> ();
		backButton = backButton.GetComponent<Button> ();

		startMenu.enabled = true;
		quitMenu.enabled = false;
		aboutScreen.enabled = false;
	}
	
	public void ExitPress(){
		startMenu.enabled = false;
		quitMenu.enabled = true;
	}

	public void NoPress(){
		quitMenu.enabled = false;
		startMenu.enabled = true;
	}

	public void ExitGame(){
		Application.Quit ();
	}

	public void NewPress(){
		SceneManager.LoadScene ("TrainingDescription");
	}

	public void ReturningPress(){
		SceneManager.LoadScene ("imageCapture");
	}

	public void AboutPress(){
		startMenu.enabled = false;
		aboutScreen.enabled = true;
	}

	public void BackPress(){
		aboutScreen.enabled = false;
		startMenu.enabled = true;
	}
}
