using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkEye2 : MonoBehaviour
{

	DataManager dataManager;

	Image img;
	public Sprite[] blinkSprites;

	bool canBlink = true;

	// Use this for initialization
	void Start()
	{
		dataManager = DataManager.Instance;


		if(dataManager.nowCustomer.Name != "ルイ")
		{
			blinkSprites = dataManager.nowCustomer.BlinkImages;
		}

		img = GetComponent<Image>();
		StartCoroutine(Blink());
	}

	// Update is called once per frame
	void Update()
	{

	}

	IEnumerator Blink()
	{
		while (canBlink)
		{
			yield return new WaitForSeconds(4);

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

