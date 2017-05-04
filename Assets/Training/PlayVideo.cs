using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;

[RequireComponent (typeof(AudioSource))]

public class PlayVideo : MonoBehaviour {

	public MovieTexture movie;
	public string nextScene;
	public int videoLengthSecs;
	public string emotion;

	private AudioSource audio;
	private WebCamTexture webCamTexture;
	private int timeInterval;
	private float timeCount;
	private int captureCounter;
	private string savePath; 

	// Use this for initialization
	void Start () {
		webCamTexture = new WebCamTexture(1280, 720, 60);
		webCamTexture.deviceName = "FaceTime HD Camera";
		webCamTexture.Play();
		timeInterval = (int)videoLengthSecs / 7;
		timeCount = 0;
		captureCounter = 0;
		savePath = "Assets/Resources/"+emotion+"/";

		GetComponent<RawImage> ().texture = movie as MovieTexture;
		audio = GetComponent<AudioSource> ();
		audio.clip = movie.audioClip;
		movie.Play ();
		audio.Play ();

		StartCoroutine ("waitForMovieEnd");
	}
	
	public void Update(){
		timeCount += Time.deltaTime;
	}

	IEnumerator waitForMovieEnd(){
		while (movie.isPlaying) {
			if (timeCount % timeInterval <= 0.062) {
				Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
				photo.SetPixels(webCamTexture.GetPixels());
				photo.Apply();

				//Encode to a PNG
				byte[] bytes = photo.EncodeToJPG();
				//Write out the PNG. Of course you have to substitute your_path for something sensible
				File.WriteAllBytes(savePath + emotion +"_"+ captureCounter.ToString () + ".jpg", bytes);

				++captureCounter;
				print ("captureCounter = " + captureCounter);
			}
			yield return new WaitForEndOfFrame ();
		}
		onMovieEnded ();
	}

	void onMovieEnded(){
		string path;
		for (int i = 3; i < 8; i++) {
			path = savePath + emotion + "_" + i.ToString () + ".jpg";
			TextureImporter importer = (TextureImporter)TextureImporter.GetAtPath (path);
			importer.textureType = TextureImporterType.Sprite;
			AssetDatabase.ImportAsset (path, ImportAssetOptions.ForceUpdate);
		}

		SceneManager.LoadScene (nextScene);
	}
}
