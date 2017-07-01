using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class NextBtnManager : MonoBehaviour {

	DataManager dataManager;

	public string scene;

	// Use this for initialization
	void Start () {
		dataManager = DataManager.Instance;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Backbtn(){
		AudioPlayer.PlaySe("Sounds/SE/cancel");
		SceneManager.LoadScene(scene);
	}

	public void Test(){
		AudioPlayer.PlaySe("Sounds/SE/decision");
		SceneManager.LoadScene (scene);

	}

}
