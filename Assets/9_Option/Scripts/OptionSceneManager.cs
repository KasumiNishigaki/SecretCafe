using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class OptionSceneManager : MonoBehaviour {

	public int id;

	public Image help;
	public List<Sprite> helpImgs = new List<Sprite>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//画像差し替えて表示

	//BGM<とかも
	public void BtnDown(){

		help.sprite = helpImgs[id];
	}
}
