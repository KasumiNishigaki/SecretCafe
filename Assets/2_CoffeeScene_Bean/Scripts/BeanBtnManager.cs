using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BeanBtnManager : MonoBehaviour {

	public int id;
	bool isSelect;

	Image myImg;

	public SelectBeanManager selectBeanManager;

	void Start () {
		myImg = GetComponent<Image> ();
	}

	public void OnClick () {
		if (isSelect) {
			myImg.sprite = selectBeanManager.defaultBeanImgs [id];
			selectBeanManager.RemoveBeanId (id);
		} else {
			myImg.sprite = selectBeanManager.selectedBeanImgs [id];
			selectBeanManager.AddBeanId (id);
		}
		// boolを反転させる.
		isSelect = !isSelect;
	}
}
