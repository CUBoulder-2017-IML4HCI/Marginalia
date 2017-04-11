using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEditor;

public class addface : MonoBehaviour {
	int picTotal = 0;
	public GameObject fartiface;
	Sprite sprite;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void saveNew(){
		File.Copy ("Assets/emotion_detector/1.jpg", "Assets/savedPics/saved_" + picTotal+".jpg", true);
		var path = "Assets/savedPics/saved_" + picTotal + ".jpg";

		TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath (path);
		importer.textureType = TextureImporterType.Sprite;
		AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);


		GameObject newObject = Instantiate (fartiface);
		newObject.transform.parent = GameObject.Find("ImageTarget").transform;
		Sprite faceSprite = AssetDatabase.LoadAssetAtPath<Sprite> (path);
		newObject.GetComponent<SpriteRenderer>().sprite = faceSprite;

		Vector3 tempPosition = new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(-10.0f, 10.0f), 0);
		newObject.transform.position = tempPosition;

		++picTotal;
	}
}

