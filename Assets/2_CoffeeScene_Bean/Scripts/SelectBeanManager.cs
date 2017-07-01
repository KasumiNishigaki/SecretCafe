using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;


public class SelectBeanManager : MonoBehaviour {
	DataManager dataManager;

	//次へボタン
	public GameObject nextBtn;

	[HideInInspector]
	public List<int> selectBean = new List<int> ();

	public List<Button> beanBtns = new List<Button> ();

	public List<Sprite> defaultBeanImgs = new List<Sprite> ();
	public List<Sprite> selectedBeanImgs = new List<Sprite> ();


	public GameObject startPanel;

	void Start (){
		dataManager = DataManager.Instance;

		StartCoroutine(FadeOut(startPanel));

		beanBtns [4].interactable = dataManager.varistorRank >= 3;
		beanBtns [5].interactable = dataManager.varistorRank >= 6;
		beanBtns [6].interactable = dataManager.varistorRank >= 9;
		
	}

	public void AddBeanId (int beanId) {
		selectBean.Add (beanId);

		if (selectBean.Count == 2) {
			DisableTapUnSelectBeans ();
		}
	}

	public void RemoveBeanId (int beanId) {
		if (selectBean.Count == 2) {
			EnableTapAllBeans ();
		}

		selectBean.Remove (beanId);
	}

	void DisableTapUnSelectBeans () {
		for (int i = 0, len = beanBtns.Count; i < len; i++) {
			if (!selectBean.Contains (i)) {
				beanBtns [i].interactable = false;
			}
		}
		nextBtn.SetActive (true);
	}

	void EnableTapAllBeans () {
		for (int i = 0, len = beanBtns.Count; i < len; i++) {
			beanBtns [i].interactable = true;
		}
		nextBtn.SetActive (false);
	}

	//次へボタンを押したら
	public void NextScene()
	{
		dataManager.beanList = selectBean.Select(n => n + 1).ToList();
		//beanList内を小さい数字順に並び替え
		dataManager.beanList.Sort();

		int bitterLevel = ConcatBeanListVals();
		dataManager.resultCoffeeNum = bitterLevel;

		//現在の苦さ記録
		dataManager.bitterJudge = CoffeeGameDatabase.beansBitterDic[bitterLevel];

		AudioPlayer.PlaySe("Sounds/SE/decision");
		SceneManager.LoadScene("3_Roasting");
	}

	//beanListの中身を[0][1]でくっつけてintにする.
	int ConcatBeanListVals()
	{
		string blendNum = "";
		for (int i = 0, len = dataManager.beanList.Count; i < len; i++)
		{
			blendNum = blendNum + dataManager.beanList[i];
			Debug.Log(blendNum);
		}
		return int.Parse(blendNum);
	}


	IEnumerator FadeOut(GameObject target)
	{
		var img = target.GetComponent<Image>();

		yield return new WaitForSeconds(1);

		DOTween.ToAlpha(
			() => img.color,
			color => img.color = color,
			0f,                             // 最終的なalpha値
			1.0f);

		yield return new WaitForSeconds(1);

		Destroy(target);
	}

}
