using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using System;

public class callpythontest : MonoBehaviour {

	void run_cmd(string cmd, string args)
	{
		ProcessStartInfo start = new ProcessStartInfo();
		start.FileName = cmd;
		start.UseShellExecute = false;
		start.RedirectStandardOutput = true;
		using(Process process = Process.Start(start))
		{
			using(StreamReader reader = process.StandardOutput)
			{
				string result = reader.ReadToEnd();
				Console.Write(result);
			}
		}
	}
	// Use this for initialization
	void Start () {
		run_cmd ("/Users/mhallwie/pytest.py", "");
	}
	
	// Update is called once per frame
	//void Update () {
		
//	}
}
