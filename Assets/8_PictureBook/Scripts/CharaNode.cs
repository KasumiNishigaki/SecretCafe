using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct CharaPBData {
	public Sprite lockImage;
	public Sprite unlockImage;
	public string name;
	public string age;
	public Sprite modalImage;
}

public class CharaNode : MonoBehaviour {

	public Image img;
	public Text nameTxt;
	public Text ageTxt;

	CharaPBData baseData;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	/* Member Methods */


	public void Init (CharaPBData _baseData, bool isUnlocked, ModalWindowManager modal) {
		baseData = _baseData;

		img.sprite = isUnlocked ? baseData.unlockImage : baseData.lockImage;
		nameTxt.text = baseData.name;
		ageTxt.text = baseData.age;

		if (isUnlocked) {
			GetComponent <Button> ().onClick.AddListener (() => modal.ShowModal (baseData.modalImage));
		}
	}
}
