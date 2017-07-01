using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GrindSceneManager : MonoBehaviour {

	DataManager dataManager;

	// Use this for initialization
	void Start () {
		dataManager = DataManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//次へボタンをおした時
	public void NextScene()
	{
		//ミルを回した回数によって苦さレベルを変える
		if (dataManager.millCount >= 30)
		{
			dataManager.bitterJudge = dataManager.bitterJudge + 1;
		}

		AudioPlayer.PlaySe("Sounds/SE/decision");
		SceneManager.LoadScene("5_Brew");
	}
}
