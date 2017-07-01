using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MilkSceneManager : MonoBehaviour {


	DataManager dataManager;

	public GameObject milkBtn;
	public GameObject creamBtn;
	public GameObject sugerBtn;

	// Use this for initialization
	void Start () {
		dataManager = DataManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void MilkBtn(){
		dataManager.bitterJudge = 1;
		dataManager.resultCoffee = 1;
		dataManager.resultCoffeeNum = 77;

		creamBtn.SetActive(false);
		sugerBtn.SetActive(false);
	}

	public void CreamBtn(){
		dataManager.bitterJudge = 1;
		dataManager.resultCoffee = 2;
		dataManager.resultCoffeeNum = 78;

		milkBtn.SetActive(false);
		sugerBtn.SetActive(false);

	}

	public void SugerBtn(){
		dataManager.bitterJudge = dataManager.bitterJudge - 1;
		dataManager.resultCoffee = 0;

		creamBtn.SetActive(false);
		milkBtn.SetActive(false);
	}

	public void NextScene(){
		AudioPlayer.PlaySe("Sounds/SE/decision");
		SceneManager.LoadScene("7_Finish");

	}
}
