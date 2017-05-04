using UnityEngine;
using System;
using System.Collections;
using System.Diagnostics;
using System.IO;

//StreamWriter messageStream;

public class terminalcommandrunscript : MonoBehaviour {

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

		UnityEngine.Debug.Log( "Successfully launched app" );
	}

	void DataReceived( object sender, DataReceivedEventArgs eventArgs )
	{
		PlayerPrefs.SetString ("Emotion", (string)eventArgs.Data);
		print(PlayerPrefs.GetString("Emotion"));
	}


	void ErrorReceived( object sender, DataReceivedEventArgs eventArgs )
	{
		UnityEngine.Debug.LogError( eventArgs.Data );
	}

	void Update () {

	}
}