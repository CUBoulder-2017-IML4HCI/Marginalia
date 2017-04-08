using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Diagnostics;
using System.IO;

public class DisplayEmotion : MonoBehaviour {
	Text txt;

	// Use this for initialization
	void Start () {
		Process proc = new Process();
		proc.StartInfo.FileName = "python";
		proc.StartInfo.WorkingDirectory = "/Users/jen/Development/Marginalia/Assets";
		proc.StartInfo.Arguments = "test3.py";
		proc.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
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

		txt = gameObject.GetComponent<Text> ();

		UnityEngine.Debug.Log( "Successfully launched app" );
	}

	void DataReceived( object sender, DataReceivedEventArgs eventArgs )
	{
		txt.text = "Are you " + eventArgs.Data + "?";
	}

	void ErrorReceived( object sender, DataReceivedEventArgs eventArgs )
	{
		UnityEngine.Debug.LogError( eventArgs.Data );
	}
	
	// Update is called once per frame
	void Update () {

	}
}
