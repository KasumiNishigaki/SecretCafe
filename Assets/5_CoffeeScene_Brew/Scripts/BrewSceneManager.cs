using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BrewSceneManager : MonoBehaviour {

	DataManager dataManager;

	public HitWaterManager hitWaterManager;

	public bool demerit_average;

	// Use this for initialization
	void Start () {
		dataManager = DataManager.Instance;

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void NextScene(){
		AudioPlayer.PlaySe("Sounds/SE/decision");

		//平均じゃなかったら
		if (!hitWaterManager.isAverageDropCnt ()) {
			dataManager.demeritPoints += 125;
			demerit_average = true;
		}

		//減点が50点以上であれば完成画面へ
		if (dataManager.demeritPoints >= 130)
		{
			dataManager.isSuccess = false;
			dataManager.resultCoffee = 3;

			if(!demerit_average){
				//フィルターからはみ出すぎ
				dataManager.resultCoffeeNum = 80;
			}else{
				//均等に注げていない
				dataManager.resultCoffeeNum = 81;
			}

			SceneManager.LoadScene("7_Finish");
		}
		else {
			dataManager.isSuccess = true;
			SceneManager.LoadScene("6_Milk");

		}
	}

}
