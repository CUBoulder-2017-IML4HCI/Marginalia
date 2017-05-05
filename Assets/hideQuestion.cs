using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hideQuestion : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void hide(){
		GameObject.Find ("AcceptButton").GetComponent<UnityEngine.UI.Image> ().enabled = false;
		GameObject.Find ("DeclineButton").GetComponent<UnityEngine.UI.Image> ().enabled = false;
		GameObject.Find ("TextPanel").GetComponent<UnityEngine.UI.Image> ().enabled = false;
		GameObject.Find ("DisplayEmotionText").GetComponent<UnityEngine.UI.Text> ().enabled = false;
	}

	public void show(){

		GameObject.Find ("AcceptButton").GetComponent<UnityEngine.UI.Image> ().enabled = true;
		GameObject.Find ("DeclineButton").GetComponent<UnityEngine.UI.Image> ().enabled = true;
		GameObject.Find ("TextPanel").GetComponent<UnityEngine.UI.Image> ().enabled = true;
		GameObject.Find ("DisplayEmotionText").GetComponent<UnityEngine.UI.Text> ().enabled = true;

	}
}
