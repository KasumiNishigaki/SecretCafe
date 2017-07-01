using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PictureBookManager : MonoBehaviour {
	public GameObject beanTab;
	public GameObject charaTab;

	public List<CharaPBData> charaDatas;
	public List<BeanPBData> beanDatas;

	public CharaListManager charaListManager;
	public BeanListManager beanListManager;

	[Header("Debug")]
	public bool d_isDebug;
	PictureBookDebugManager debugManager;

	DataManager dataManageer;

	// Use this for initialization
	void Start () {
		dataManageer = DataManager.Instance;

		if (d_isDebug && GetComponent<PictureBookDebugManager>() && GetComponent<PictureBookDebugManager>().enabled) {
			debugManager = GetComponent<PictureBookDebugManager>();
			debugManager.Init();
		}

		int varistorRank = dataManageer.varistorRank;

		charaListManager.Init(charaDatas, varistorRank);
		beanListManager.Init(beanDatas, varistorRank);
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	/* onClickEvent */


	public void TapBeanTab () {
		beanTab.transform.SetAsLastSibling ();
	}

	public void TapCharaTab () {
		charaTab.transform.SetAsLastSibling ();
	}

	public void Backbtn() {
		AudioPlayer.PlaySe("Sounds/SE/cancel");
		SceneManager.LoadScene("1_MainScene");
	}
}
