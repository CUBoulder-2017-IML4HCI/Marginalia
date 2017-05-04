using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class createMessage : MonoBehaviour {
	public GameObject messageArtifact;
	public Text inputText;
	string emotion;

	public void createArtifact(){
		GameObject newObject = Instantiate (messageArtifact);
		newObject.transform.parent = GameObject.Find("ImageTarget").transform;
		newObject.GetComponent<Text>().text = inputText.text;

		emotion = PlayerPrefs.GetString("Emotion");
		if (emotion == "Happy")
			newObject.tag = "Happy";
		else if (emotion == "Sad")
			newObject.tag = "Sad";
		else if (emotion == "Angry")
			newObject.tag = "Angry";
		else if (emotion == "Afraid")
			newObject.tag = "Afraid";
		else if (emotion == "Disgust")
			newObject.tag = "Disgust";
		else if (emotion == "Contempt")
			newObject.tag = "Contempt";
		else if (emotion == "Surprise")
			newObject.tag = "Surprise";
	}
}
