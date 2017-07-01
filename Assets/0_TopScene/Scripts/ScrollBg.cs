using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBg : MonoBehaviour {

	[SerializeField]
	GameObject myPrefab;

	[SerializeField]
	float scrollSpead;

	Vector3 myPos;
	float mySizeX;
	bool isGenerated;

	// Use this for initialization
	void Start () {
		myPos = transform.localPosition;
		mySizeX = GetComponent<RectTransform>().sizeDelta.x;
	}

	// Update is called once per frame
	void Update() {
		myPos.x -= scrollSpead;
		transform.localPosition = myPos;

		if (myPos.x <= mySizeX * -1) {
			Destroy(gameObject);
		} else if (myPos.x <= 0 && !isGenerated) {
			GenerateNextBg();
		}
	}

	void GenerateNextBg () {
		var nextBg = Instantiate(myPrefab);

		nextBg.transform.parent = transform.parent;

		nextBg.transform.localPosition = new Vector3(mySizeX, myPos.y, myPos.z);
		nextBg.transform.localScale = transform.localScale;

		isGenerated = true;
	}
}