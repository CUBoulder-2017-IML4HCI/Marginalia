using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEditor;

public class ShowWebCam : MonoBehaviour {
	private WebCamTexture webCamTexture;

	// Use this for initialization
	void Start () {
		webCamTexture = new WebCamTexture(1280, 720, 60);
		webCamTexture.deviceName = "FaceTime HD Camera";
		webCamTexture.Play();

		GetComponent<RawImage> ().texture = webCamTexture as WebCamTexture;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
