using UnityEngine;
using System.Collections;
using Vuforia;

public class saveCapture : MonoBehaviour {
	private bool mAccessCameraImage = true;
	private string savePath = "Assets/CamCaptures/";
	private int captureCounter = 0;

	// The desired camera image pixel format
	private Image.PIXEL_FORMAT mPixelFormat = Image.PIXEL_FORMAT.RGBA8888;// or RGBA8888, RGB888, RGB565, YUV
	// Boolean flag telling whether the pixel format has been registered
	private bool mFormatRegistered = false;
	void Start ()
	{
		// Register Vuforia life-cycle callbacks:
		VuforiaARController.Instance.RegisterVuforiaStartedCallback(OnVuforiaStarted);
		//VuforiaARController.Instance.RegisterOnPauseCallback(OnPaused);
		VuforiaARController.Instance.RegisterTrackablesUpdatedCallback(OnTrackablesUpdated);
	}
	/// <summary>
	/// Called when Vuforia is started
	/// </summary>
	private void OnVuforiaStarted()
	{
		// Try register camera image format
		if (CameraDevice.Instance.SetFrameFormat(mPixelFormat, true))
		{
			Debug.Log("Successfully registered pixel format " + mPixelFormat.ToString());
			mFormatRegistered = true;
		}
		else
		{
			Debug.LogError("Failed to register pixel format " + mPixelFormat.ToString() +
				"\n the format may be unsupported by your device;" +
				"\n consider using a different pixel format.");
			mFormatRegistered = false;
		}
	}
	/// <summary>
	/// Called when app is paused / resumed
	/// </summary>
	private void OnPause(bool paused)
	{
		if (paused)
		{
			Debug.Log("App was paused");
			UnregisterFormat();
		}
		else
		{
			Debug.Log("App was resumed");
			RegisterFormat();
		}
	}
	/// <summary>
	/// Called each time the Vuforia state is updated
	/// </summary>
	private void OnTrackablesUpdated()
	{
		if (mFormatRegistered)
		{
			if (mAccessCameraImage)
			{
				Vuforia.Image image = CameraDevice.Instance.GetCameraImage(mPixelFormat);
				if ((image != null) && (captureCounter < 3))
				{
					Texture2D snap = new Texture2D(image.Width,image.Height);
					image.CopyToTexture(snap);
					snap.Apply();

					Texture2D flipped = new Texture2D(snap.width,snap.height);

					int xN = snap.width;
					int yN = snap.height;

					for(int i=0;i<xN;i++){
						for(int j=0;j<yN;j++){
							flipped.SetPixel(i, yN-j-1, snap.GetPixel(i,j));
						}
					}

					flipped.Apply();


					System.IO.File.WriteAllBytes (savePath + captureCounter.ToString () + ".png", flipped.EncodeToPNG ());
					++captureCounter;
				}
			}
		}
	}
	/// <summary>
	/// Unregister the camera pixel format (e.g. call this when app is paused)
	/// </summary>
	private void UnregisterFormat()
	{
		Debug.Log("Unregistering camera pixel format " + mPixelFormat.ToString());
		CameraDevice.Instance.SetFrameFormat(mPixelFormat, false);
		mFormatRegistered = false;
	}
	/// <summary>
	/// Register the camera pixel format
	/// </summary>
	private void RegisterFormat()
	{
		if (CameraDevice.Instance.SetFrameFormat(mPixelFormat, true))
		{
			CameraDevice.Instance.SetFrameFormat (mPixelFormat, true);
			Debug.Log("Successfully registered camera pixel format " + mPixelFormat.ToString());
			mFormatRegistered = true;
		}
		else
		{
			Debug.LogError("Failed to register camera pixel format " + mPixelFormat.ToString());
			mFormatRegistered = false;
		}
	}
}
