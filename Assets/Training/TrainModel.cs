using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Diagnostics;
using System.IO;
using UnityEngine.SceneManagement;

public class TrainModel : MonoBehaviour {
	String img_path;
	String img_name;
	String python_args;
	String emotion;
	bool done;

	// Use this for initialization
	void Start () {
		done = false;

		Process proc = new Process();
		img_path = "/GameStuff/emotion_detector/";
		python_args = "EmoTrain.py";
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
	}
	
	void DataReceived( object sender, DataReceivedEventArgs eventArgs )
	{
		if (eventArgs.Data != null){
			done = true;
		}
	}

	void ErrorReceived( object sender, DataReceivedEventArgs eventArgs )
	{
		UnityEngine.Debug.LogError( eventArgs.Data );
	}

	void Update(){
		if (done) {
			SceneManager.LoadScene ("TrainingComplete");
		}
	}
}
