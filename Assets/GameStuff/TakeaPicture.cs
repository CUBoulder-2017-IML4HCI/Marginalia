using UnityEngine;
using System.Collections;
using System.IO;
using Vuforia;

public class TakeaPicture : MonoBehaviour 
{
	private string savePath = "Assets/CamCaptures/";
	private int captureCounter = 0;
	WebCamTexture webCamTexture;

	void Start() 
	{
		webCamTexture = new WebCamTexture();
		webCamTexture.Play();
	}

	IEnumerator TakePhoto()
	{

		// NOTE - you almost certainly have to do this here:

		yield return new WaitForEndOfFrame (); 

		// it's a rare case where the Unity doco is pretty clear,
		// http://docs.unity3d.com/ScriptReference/WaitForEndOfFrame.html
		// be sure to scroll down to the SECOND long example on that doco page 

		if (captureCounter < 3) {
			Texture2D photo = new Texture2D (webCamTexture.width, webCamTexture.height);
			photo.SetPixels (webCamTexture.GetPixels ());
			photo.Apply ();
			System.IO.File.WriteAllBytes (savePath + captureCounter.ToString () + ".png", photo.EncodeToPNG ());
			++captureCounter;
			}
		}
	}