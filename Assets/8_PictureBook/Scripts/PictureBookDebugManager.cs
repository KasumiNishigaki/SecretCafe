using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureBookDebugManager : MonoBehaviour {

	public int d_varistorRank;

	[Header("Character")]
	[Space(5)]
	public bool d_isDebugCharacter;
	public GameObject characterPreferencePrefab;

	[Header("Bean")]
	[Space(5)]
	public bool d_isDebugBean;

	DataManager dataManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void Init () {
		dataManager = DataManager.Instance;
		dataManager.varistorRank = d_varistorRank;

		var cp = Instantiate(characterPreferencePrefab).GetComponent<CharacterPreference>();
		cp.Init ();
	}
}
