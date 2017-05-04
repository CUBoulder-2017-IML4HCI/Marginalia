using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;
using UnityEngine.UI;

public class addface : MonoBehaviour {
	int picTotal = 0;
	public GameObject fartiface;
	string emotion;
	Sprite sprite;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void saveNew(){
		File.Delete("Assets/GameStuff/emotion_detector/saved_" + picTotal+".jpg");
		File.Delete("Assets/GameStuff/emotion_detector/saved_" + picTotal+".jpg.meta");
		File.Move ("Assets/GameStuff/emotion_detector/1.jpg", "Assets/GameStuff/emotion_detector/saved_" + picTotal+".jpg");
		var path = "Assets/GameStuff/emotion_detector/saved_" + picTotal + ".jpg";

		System.DateTime dt = System.DateTime.Now + System.TimeSpan.FromSeconds(3);

		do { } while (System.DateTime.Now < dt);

		TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath (path);
		importer.textureType = TextureImporterType.Sprite;
		AssetDatabase.ImportAsset (path, ImportAssetOptions.ForceUpdate);

		GameObject newObject = Instantiate (fartiface);
		newObject.transform.parent = GameObject.Find("ImageTarget").transform;
		Sprite faceSprite = AssetDatabase.LoadAssetAtPath<Sprite> (path);
		newObject.GetComponent<SpriteRenderer>().sprite = faceSprite;

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

		//Vector3 tempPosition = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
		//newObject.transform.position = tempPosition;

		++picTotal;
	}
}

