using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class RoastingSceneManager : MonoBehaviour {

	DataManager dataManager;

	public GameObject nextButton;

	// Use this for initialization
	void Start () {
		dataManager = DataManager.Instance;

		Invoke ("FinRoasting", 4);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FinRoasting(){
		nextButton.SetActive (true);
	}

	public void NextScene(){
		AudioPlayer.PlaySe("Sounds/SE/decision");
		SceneManager.LoadScene("4_Grind");

	}

}
