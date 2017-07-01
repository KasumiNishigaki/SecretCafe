using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MedalObject : MonoBehaviour {

	public Text myTxt;
	public Sprite activeImg;

	Image myImg;
	string rankName;

	// Use this for initialization
	void Start () {
		
	}

	public void Init () {
		myImg = GetComponent<Image>();
		rankName = myTxt.text;

		myTxt.text = "???";
	}
	
	public void ToActive () {
		myImg.sprite = activeImg;
		myTxt.text = rankName;
	}
}
