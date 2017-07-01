using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct BeanPBData {
	public Sprite lockImage;
	public Sprite unlockImage;
	public string name;
	public string locality;
	public Sprite modalImage;

	public int requireLevel;
}

public class BeanNode : MonoBehaviour {

	public Image img;
	public Text numTxt;
	public Text nameTxt;
	public Text localityTxt;

	BeanPBData baseData;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	/* Member Methods */


	public void Init (BeanPBData _baseData, int index, bool isUnlocked, ModalWindowManager modal) {
		baseData = _baseData;

		img.sprite = isUnlocked ? baseData.unlockImage : baseData.lockImage;
		numTxt.text = "0" + index;
		nameTxt.text = baseData.name;
		localityTxt.text = baseData.locality;

		if (isUnlocked) {
			GetComponent <Button> ().onClick.AddListener (() => modal.ShowModal (baseData.modalImage));
		}
	}
}
