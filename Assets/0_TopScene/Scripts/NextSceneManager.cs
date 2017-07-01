using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NextSceneManager : MonoBehaviour {

	public GameObject nowLoading;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PushButton(){
		AudioPlayer.PlaySe("Sounds/SE/EnterShop");
		nowLoading.SetActive(true);

		SceneManager.LoadScene ("1_MainScene");

		AudioPlayer.StopBgm ();
	}
}
