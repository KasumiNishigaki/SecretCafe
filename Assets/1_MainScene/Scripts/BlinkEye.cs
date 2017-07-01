using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkEye : MonoBehaviour {

	DataManager dataManager;

	Image img;
	public Sprite[] blinkSprites;

	bool canBlink = true;

	// Use this for initialization
	void Start () {
		dataManager = DataManager.Instance;

		img = GetComponent<CharacterObject>().img;
		StartCoroutine(Blink());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator Blink () {
		while (canBlink) {
			yield return new WaitForSeconds(3);

			img.sprite = blinkSprites[1];
			yield return new WaitForSeconds(0.05f);
			img.sprite = blinkSprites[2];
			yield return new WaitForSeconds(0.05f);
			img.sprite = blinkSprites[0];

			yield return new WaitForSeconds(1.5f);

			img.sprite = blinkSprites[1];
			yield return new WaitForSeconds(0.05f);
			img.sprite = blinkSprites[2];
			yield return new WaitForSeconds(0.05f);
			img.sprite = blinkSprites[0];

			yield return new WaitForSeconds(0.3f);

			img.sprite = blinkSprites[1];
			yield return new WaitForSeconds(0.05f);
			img.sprite = blinkSprites[2];
			yield return new WaitForSeconds(0.05f);
			img.sprite = blinkSprites[0];

			yield return null;
		}
		yield return null;
	}
}
