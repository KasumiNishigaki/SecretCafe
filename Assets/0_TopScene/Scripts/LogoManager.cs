using UnityEngine;
using System.Collections;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class LogoManager : MonoBehaviour {

	public Image logo;
	public GameObject nowLoading;
	public GameObject dot;

	// Use this for initialization
	void Start () {
		Invoke("FadeOut",1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FadeOut(){
		DOTween.ToAlpha(
			() => logo.color,
			color => logo.color = color,
			0f,                             // 最終的なalpha値
			1f);
		Invoke("NextScene", 1);
	}

	void NextScene(){
		nowLoading.SetActive(true);
		dot.SetActive(true);
		SceneManager.LoadScene ("0_Title");

	}


}
