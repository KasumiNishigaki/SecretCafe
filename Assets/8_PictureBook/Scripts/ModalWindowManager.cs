using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModalWindowManager : MonoBehaviour {
	[SerializeField]
	Image image;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ShowModal (Sprite showImage) {
		image.sprite = showImage;
		gameObject.SetActive (true);
	}

	public void HideModal () {
		gameObject.SetActive (false);
	}
}
