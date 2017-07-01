using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BeanListManager : MonoBehaviour {

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

	public void Init (List <BeanPBData> beanDatas, int varistorRank) {
		for (int i = 0, len = beanDatas.Count; i < len; i++) {
			var item = Instantiate(node);
			item.SetParent(transform, false);

			var baseData = beanDatas [i];
			item.GetComponent<BeanNode> ().Init (baseData, i + 1, baseData.requireLevel <= varistorRank, modal);
		}
	}
}
