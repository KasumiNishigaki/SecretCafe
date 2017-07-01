using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneDebugManager : MonoBehaviour {

	[Header("Customer")]
	[Space(5)]
	public bool d_isRandomCustomer;
	public int d_characterId;
	public GameObject characterPreferencePrefab;

	[Header("AfterCofee")]
	[Space(5)]
	public bool d_isAfterCoffee;
	public bool d_isSuccess;
	public bool d_isLikeCoffee;

	[Header("HappyGauge")]
	[Space(5)]
	public bool d_isDebugHappyGauge;
	[Range(0.0f, 1.0f)]
	public float d_gaugeVal;

	DataManager dataManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Init () {
		dataManager = DataManager.Instance;

		dataManager.isAfterCoffee = d_isAfterCoffee;
		var cp = Instantiate(characterPreferencePrefab).GetComponent<CharacterPreference>();

		dataManager.nowCustomer = new Character(cp.charaStatuses[d_characterId].Name,
												cp.charaStatuses[d_characterId].Bitter,
												cp.charaStatuses[d_characterId].BlinkImages,
		                                        cp.charaStatuses[d_characterId].RequiredLevel,
		                                        cp.charaStatuses[d_characterId].StartComingHour,
		                                        cp.charaStatuses[d_characterId].EndComingHour);
		dataManager.isLikeCoffee = d_isLikeCoffee;
		dataManager.isSuccess = d_isSuccess;

		cp.Init();
	}
}
