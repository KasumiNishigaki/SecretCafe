using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using SimpleJSON;

public class JSONTalkManager : MonoBehaviour {

	DataManager dataManager;

	public CharacterObject guest;
	public CharacterObject rui;

	bool isPlaySolioquy;

	// Use this for initialization
	void Awake () {
		dataManager = DataManager.Instance;

		rui.baloon.SetActive (false);
		guest.baloon.SetActive (false);

		//StartCoroutine (StartTalk ("マルコ"));
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public IEnumerator StartOpeningTalk (string characterName) {
		var json = CafeGameUtil.LoadJSON("selifs");

		int rdm = Random.Range (0, json [characterName].Count);
		var talkData = json [characterName] [rdm];

		for (int i = 0, len = talkData.Count; i < len; i++) {
			//print (talkData [i] ["name"].Value);
			if (talkData [i] ["name"].Value == "ルイ") {
				guest.baloon.SetActive (false);
				rui.baloon.SetActive (true);

				rui.SetSelif (talkData [i] ["selif"].Value);
			} else {
				rui.baloon.SetActive (false);
				guest.baloon.SetActive (true);

				guest.SetSelif (talkData [i] ["selif"].Value);
			}

			yield return new WaitWhile (() => Input.GetMouseButtonDown (0) == false);
			yield return new WaitForSeconds (0.1f);
		}
		rui.baloon.SetActive (false);
		guest.baloon.SetActive (false);

		dataManager.isEvent = false;
	}


	public IEnumerator StartAfterCoffeeTalk(string characterName) {
		var json = CafeGameUtil.LoadJSON("Impressions");
		JSONNode impressionsJson;

		string impression;

		if (dataManager.isLikeCoffee) {
			impressionsJson = json[dataManager.nowCustomer.Name][0];
		}else {
			impressionsJson = json[dataManager.nowCustomer.Name][1];
		}

		impression = impressionsJson[Random.Range(0, impressionsJson.Count - 1)]["selif"].Value;
		//print(impression);

		var tolker = characterName == "ルイ" ? rui : guest;

		yield return new WaitForSeconds(0.1f);

		tolker.baloon.SetActive(true);
		tolker.SetSelif(impression);

		yield return new WaitWhile(() => Input.GetMouseButtonDown(0) == false);

		tolker.baloon.SetActive(false);
	}

	public IEnumerator StartSolioquy (CharacterObject tolker) {
		if (isPlaySolioquy) yield break;

		var json = CafeGameUtil.LoadJSON ("soliloquy");
		var solioquyJson = json [tolker.data.Name];

		string solioquy = solioquyJson [Random.Range (0, solioquyJson.Count - 1)] ["selif"].Value;

		yield return new WaitForSeconds (0.1f);

		tolker.baloon.SetActive (true);
		tolker.SetSelif (solioquy);

		isPlaySolioquy = true;

		yield return new WaitWhile (() => Input.GetMouseButtonDown(0) == false);

		tolker.baloon.SetActive (false);

		yield return new WaitForSeconds (0.3f);
		isPlaySolioquy = false;
	}
}
