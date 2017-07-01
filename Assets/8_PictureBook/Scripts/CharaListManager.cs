using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharaListManager : MonoBehaviour {

	[SerializeField]
	RectTransform node;

	[SerializeField]
	ModalWindowManager modal;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Init (List<CharaPBData> charaDatas, int varistorRank) {
		for (int i = 0, len = charaDatas.Count; i < len; i++) {
			var item = Instantiate(node);
			item.SetParent(transform, false);

			var baseData = charaDatas [i];
			item.GetComponent<CharaNode> ().Init (baseData, CafeGameUtil.IsVisited (baseData.name), modal);
		}
	}
}
