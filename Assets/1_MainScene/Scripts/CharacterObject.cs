using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CharacterObject : MonoBehaviour {

	public Character data;

	public Image img;

	public GameObject baloon;
	public Text myText;

	public JSONTalkManager jsonTalkManager;

	[HideInInspector]
	public bool isSolioquy;

	DataManager dataManager;

	void Start () {
		dataManager = DataManager.Instance;
	}

	public void SetSelif (string selif) {
		myText.text = selif;
	}

	public void Tap () {
		if (!dataManager.isEvent) {
			StartCoroutine (jsonTalkManager.StartSolioquy (this));
		}
	}
}
