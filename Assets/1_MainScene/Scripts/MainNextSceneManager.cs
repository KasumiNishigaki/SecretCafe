using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class MainNextSceneManager : MonoBehaviour {

	public GameObject nowLowding;

	DataManager dataManager;

	// Use this for initialization
	void Start () {
		dataManager = DataManager.Instance;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void CoffeeButton(){
		nowLowding.SetActive(true);
		AudioPlayer.PlaySe("Sounds/SE/decision");
		AudioPlayer.PlayBgm("Sounds/Common/CoffeeMakeBGM");
		SceneManager.LoadScene ("2_Bean");
	}

	public void OptionButton() {
		dataManager.nowCustomer = null;

		AudioPlayer.PlaySe("Sounds/SE/OptionBtn");
		SceneManager.LoadScene ("0_Title");
	}

	public void PictureBookButton() {
		dataManager.isOpenDictionary = true;

		AudioPlayer.PlaySe("Sounds/SE/decision");
		SceneManager.LoadScene ("8_PictureBook");
	}
		
}
