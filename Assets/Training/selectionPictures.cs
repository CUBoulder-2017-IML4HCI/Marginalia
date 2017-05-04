using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEditor;
using System.IO;

public class selectionPictures : MonoBehaviour {
	public string emotion;
	public Image image1;
	public Image image2;
	public Image image3;
	public Image image4;
	public Image image5;

	private string path;
	private string imageName;

	// Use this for initialization
	void Start () {
		path = null;
		image1.GetComponent<Image> ().sprite = Resources.Load<Sprite>(emotion+"/"+emotion+"_"+3);
		image2.GetComponent<Image> ().sprite = Resources.Load<Sprite>(emotion+"/"+emotion+"_"+4);
		image3.GetComponent<Image> ().sprite = Resources.Load<Sprite>(emotion+"/"+emotion+"_"+5);
		image4.GetComponent<Image> ().sprite = Resources.Load<Sprite>(emotion+"/"+emotion+"_"+6);
		image5.GetComponent<Image> ().sprite = Resources.Load<Sprite>(emotion+"/"+emotion+"_"+7);
	}
	
	public void onPicClick(){
		//string imageName = Selectable.is;
		//File.Move (imageName, "./Assets/Training/BuildModel/" + emotion + ".jpg");
	}

	void Update(){
		if(EventSystem.current.currentSelectedGameObject.name != null){
			imageName = EventSystem.current.currentSelectedGameObject.GetComponent<Image> ().sprite.name;
			if(path == null){
				path = "Assets/Resources/" + emotion + "/" + imageName+".jpg";
				File.Delete ("Assets/GameStuff/emotion_detector/" + emotion + ".jpg");
				File.Delete ("Assets/GameStuff/emotion_detector/" + emotion + ".jpg.meta");
				File.Copy (path, "Assets/GameStuff/emotion_detector/" + emotion + ".jpg");
			}
		}
	}
}
