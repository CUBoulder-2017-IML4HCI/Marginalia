using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayEmotion : MonoBehaviour {
	Text txt;

	// Use this for initialization
	void Start () {
		txt = gameObject.GetComponent<Text> ();
		txt.text = "Are you " + PlayerPrefs.GetString ("Emotion") + "?";
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
