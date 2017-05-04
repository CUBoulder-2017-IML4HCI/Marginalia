using UnityEngine;
using System.Collections;
using System.IO;

public class TakeaPicture2 : MonoBehaviour 
{
	private string savePath = "Assets/GameStuff/emotion_detector/";
	public int captureCounter = 0;
	WebCamTexture webCamTexture;

	IEnumerator Start() 
	{
		webCamTexture = new WebCamTexture(1280, 720, 60);
		webCamTexture.deviceName = "FaceTime HD Camera";
		webCamTexture.Play();

		// NOTE - you almost certainly have to do this here:

		yield return new WaitForEndOfFrame(); 

	}
	public void Update()
	{
		if(captureCounter < 3){

		
			// it's a rare case where the Unity doco is pretty clear,
			// http://docs.unity3d.com/ScriptReference/WaitForEndOfFrame.html
			// be sure to scroll down to the SECOND long example on that doco page 

			Texture2D photo = new Texture2D(webCamTexture.width, webCamTexture.height);
			photo.SetPixels(webCamTexture.GetPixels());
			photo.Apply();

			//Encode to a PNG
			byte[] bytes = photo.EncodeToJPG();
			//Write out the PNG. Of course you have to substitute your_path for something sensible
			File.WriteAllBytes(savePath + captureCounter.ToString () + ".jpg", bytes);
			++captureCounter;

			print ("captureCounter = " + captureCounter);
		}
	}
	public void retakePics()
	{
		captureCounter = 0;
	}
}