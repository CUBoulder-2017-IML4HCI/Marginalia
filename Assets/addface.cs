using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

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
		Texture2D tex = (Texture2D)Resources.Load ("Assets/savedPics/saved_" + picTotal+".jpg");
		//Material mat = new Material (tex);
		GameObject newObject = Instantiate (fartiface);
		newObject.GetComponent<SpriteRenderer>().sharedMaterial.mainTexture = tex;


		//newObject.GetComponent<SpriteRenderer>().material.mainTexture = tex;
		++picTotal;
		//sprite = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);


	}
			}

