using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using UnityEngine.UI;

public class TakeAPhoto : MonoBehaviour {
	private WebCamTexture webCamTexture;
	public GameObject photoArtifact;
	int captureCounter;
	string emotion;

	// Use this for initialization
	void Start () {
		webCamTexture = new WebCamTexture(1280, 720, 60);
		webCamTexture.deviceName = "FaceTime HD Camera";
		webCamTexture.Play();

		GetComponent<RawImage> ().texture = webCamTexture as WebCamTexture;
	}
	
	public void onAcceptClick(){
		Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
		photo.SetPixels(webCamTexture.GetPixels());
		photo.Apply();

		//Encode to a PNG
		byte[] bytes = photo.EncodeToJPG();
		var path = "Assets/GameStuff/emotion_detector/Saved_" + captureCounter + ".jpg";
		File.WriteAllBytes(path, bytes);
		++captureCounter;

		System.DateTime dt = System.DateTime.Now + System.TimeSpan.FromSeconds(2);

		do { } while (System.DateTime.Now < dt);

		TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath (path);
		importer.textureType = TextureImporterType.Sprite;
		AssetDatabase.ImportAsset (path, ImportAssetOptions.ForceUpdate);

		GameObject newObject = Instantiate (photoArtifact);
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
	}
}
