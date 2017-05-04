using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Diagnostics;
using System.IO;

public class DisplayEmotion : MonoBehaviour {
	Text txt;
	String img_path;
	String img_name;
	String python_args;
	public string emotion;



	// Use this for initialization
	void Start () {
		Process proc = new Process();
		img_path = "/GameStuff/emotion_detector/";
		img_name = "1.jpg";
		python_args = "EmoDetect.py -i " + img_name;
		proc.StartInfo.FileName = "/usr/local/bin/python2";
		proc.StartInfo.WorkingDirectory = Application.dataPath + "/GameStuff/emotion_detector/";
		proc.StartInfo.Arguments = python_args;
		//proc.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
		proc.StartInfo.CreateNoWindow = false;

		proc.StartInfo.RedirectStandardOutput = true;
		proc.StartInfo.UseShellExecute = false;
		proc.StartInfo.RedirectStandardInput = true;
		proc.StartInfo.RedirectStandardError = true;
		proc.OutputDataReceived += new DataReceivedEventHandler( DataReceived );
		proc.ErrorDataReceived += new DataReceivedEventHandler( ErrorReceived );
		proc.Start();
		proc.BeginOutputReadLine();
		//messageStream = process.StandardInput;
		emotion = null;

		txt = gameObject.GetComponent<Text> ();
		txt.text = "Loading...";

		UnityEngine.Debug.Log( "Successfully launched app" );
	}

	void DataReceived( object sender, DataReceivedEventArgs eventArgs )
	{
		if (eventArgs.Data != null) {
			emotion = eventArgs.Data;
		}
	}

	void ErrorReceived( object sender, DataReceivedEventArgs eventArgs )
	{
		UnityEngine.Debug.LogError( eventArgs.Data );
	}

	// Update is called once per frame
	void Update () {
		if (emotion != null) {
			txt.text = "Do you feel " + emotion + "?";
			PlayerPrefs.SetString ("Emotion", emotion);
		}
	}

	public void TryAgain(){
		Process proc = new Process();
		img_path = "/GameStuff/emotion_detector/";
		img_name = "1.jpg";
		python_args = "EmoDetect.py -i " + img_name;
		proc.StartInfo.FileName = "/usr/local/bin/python2";
		proc.StartInfo.WorkingDirectory = Application.dataPath + "/GameStuff/emotion_detector/";
		proc.StartInfo.Arguments = python_args;
		//proc.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
		proc.StartInfo.CreateNoWindow = false;

		proc.StartInfo.RedirectStandardOutput = true;
		proc.StartInfo.UseShellExecute = false;
		proc.StartInfo.RedirectStandardInput = true;
		proc.StartInfo.RedirectStandardError = true;
		proc.OutputDataReceived += new DataReceivedEventHandler( DataReceived );
		proc.ErrorDataReceived += new DataReceivedEventHandler( ErrorReceived );
		proc.Start();
		proc.BeginOutputReadLine();
		//messageStream = process.StandardInput;
		emotion = null;

		txt = gameObject.GetComponent<Text> ();
		txt.text = "Loading...";

	}
}
