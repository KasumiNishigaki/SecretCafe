using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine.SceneManagement;


/*
[System.Serializable]
public struct CoffeeData {
	public string name;
}*/

public class FinishSceneManager : MonoBehaviour {

	DataManager dataManager;

	private int _bitterJudge;

	public GameObject success;
	public GameObject failure;

	public List<Image> bitterStarImgs = new List<Image>();
	public Sprite changeStarImg;

	public List<Sprite> coffeeImgs = new List<Sprite>();
	public Image changeCoffeeImg;

	public Text coffeeNameText;
	public Text descriptionText;

	public List<string> resultCoffeeName;

	//public List<CoffeeData> coffeeDatas = new List<CoffeeData>();

	// Use this for initialization
	void Start () {

		dataManager = DataManager.Instance;

		//成功失敗のアニメーションを流す
		if(dataManager.isSuccess){
			success.SetActive(true);
			StartCoroutine (FadeOut(success));
		}else{
			failure.SetActive(true);
			StartCoroutine (FadeOut(failure));
		}

		//完成珈琲差し替え
		DisplayResult();

		//最終の苦さレベルを変数に入れる
		_bitterJudge = dataManager.bitterJudge;

		//苦さレベルの数だけ星の画像を変える   苦さが５以上やマイナスの時はどうする？
		for (int i = 0 , len = _bitterJudge; i < len; i++){
			bitterStarImgs[i].GetComponent<Image>().sprite = changeStarImg;
		}


		//苦さがお客の好みに合っていればtrueに
		if(_bitterJudge == dataManager.nowCustomer.Bitter){
			dataManager.isLikeCoffee = true;
		}

		//珈琲作りしました
		dataManager.isAfterCoffee = true;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void DisplayResult(){
		changeCoffeeImg.GetComponent<Image>().sprite = coffeeImgs[dataManager.resultCoffee];

		coffeeNameText.text = resultCoffeeName[dataManager.resultCoffee];
		descriptionText.text = CoffeeGameDatabase2.resultCoffeeDic[dataManager.resultCoffeeNum];
	}

	IEnumerator FadeOut(GameObject target){
		var img = target.GetComponent<Image>();

		yield return new WaitForSeconds(1);

		DOTween.ToAlpha(
			() => img.color,
			color => img.color = color,
			0f,                             // 最終的なalpha値
			1.5f);
	}

	public void NextScene(){
		AudioPlayer.PlaySe("Sounds/SE/decision");
		SceneManager.LoadScene("1_MainScene");

	}

}
