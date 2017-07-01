using UnityEngine;
using System.Collections;

public class TitleSceneManager : MonoBehaviour {

	DataManager dataManager;

	public CharacterPreference characterPreference;

	// Use this for initialization
	void Start () {

		dataManager = DataManager.Instance;
		dataManager.isOpenDictionary = false;

		AudioPlayer.PlayBgm("Sounds/Common/titlebgm");
		characterPreference.Init();

		dataManager.LoadSaveData();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
