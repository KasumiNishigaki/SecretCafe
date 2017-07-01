using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CharacterPreference : MonoBehaviour {

	public List<CharaStatus> charaStatuses; //CharaStatus型のリスト「Chara」を宣言

	DataManager dataManager;

	// Use this for initialization
	void Start () {
		
	}


	// Update is called once per frame
	void Update () {
		
	}

	public void Init () {
		dataManager = DataManager.Instance;

		dataManager.characterList = new List <Character> ();

		for (int i = 0, len = charaStatuses.Count; i < len; i++) {
			var chara = new Character (charaStatuses [i].Name,
			                           charaStatuses [i].Bitter,
			                           charaStatuses [i].BlinkImages,
			                           charaStatuses [i].RequiredLevel,
			                           charaStatuses [i].StartComingHour,
			                           charaStatuses [i].EndComingHour);

			//リストにキャラクター情報を追加
			dataManager.characterList.Add(chara);
		}
	}
}