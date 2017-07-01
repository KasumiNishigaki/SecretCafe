using UnityEngine;
using System.Collections;

public class RankPanelManager : MonoBehaviour {

	public GameObject rankPanel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RankButton(){
		AudioPlayer.PlaySe("Sounds/SE/decision");

		//一時停止
		Time.timeScale = 0;

		//ランクパネル表示させる
		rankPanel.SetActive (true);
	}

	public void BackRankButton(){
		AudioPlayer.PlaySe("Sounds/SE/cancel");

		//パネルを消す
		rankPanel.SetActive (false);

		//再開
		Time.timeScale = 1;
	}
}
