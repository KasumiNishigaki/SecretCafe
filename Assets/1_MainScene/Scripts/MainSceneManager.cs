using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using SimpleJSON;
using DG.Tweening;

public class MainSceneManager : MonoBehaviour {

	DataManager dataManager;

	public CharacterObject rui;
	public CharacterObject customer;

	bool isCutomerOnlyRui;

	public List<Sprite> reactionImages; //お客の反応画像リスト
	Sprite reactionSprite; //入れ替え用お客の反応Sprite
	public GameObject reactionIconObject; //吹き出し

	public GameObject balloon;

	public Image happyGaugeImage;

	public JSONTalkManager jsonTalkManager;
	public Animator happyAnim;

	public float blinkInvokeTime;

	public GameObject RankNotice;
	public List<MedalObject> varistorMedals;

	[Header("Debug")]
	public bool d_isDebug;
	MainSceneDebugManager debugManager;

	float gaugeVal;
	public float GaugeVal { // これでfillAmount関連管理してるので、更新するときはこれを更新すること。
		set {
			gaugeVal = value;
			happyGaugeImage.fillAmount = gaugeVal;
		} get {
			return gaugeVal;
		}
	}

	// Use this for initialization
	void Start () {
		dataManager = DataManager.Instance;
		dataManager.isEvent = true;

		//幸せゲージのロード
		GaugeVal = dataManager.nowHappyGauge;

		// debug.
		if (d_isDebug && GetComponent<MainSceneDebugManager> () && GetComponent<MainSceneDebugManager>().enabled) {
			Debug.LogWarning("---- Debug Mode ----");

			dataManager.LoadSaveData ();

			debugManager = GetComponent<MainSceneDebugManager>();
			debugManager.Init();

			SetDebugStatuses();
		} else {
			d_isDebug = false;
		}

		AudioPlayer.PlayBgm("Sounds/Common/HomeBGM");

		//バリスタメダル情報初期化.
		InitVaristorMedals();

		rui.data = dataManager.characterList [0];

		if(dataManager.isAfterCoffee) {
			customer.data = dataManager.nowCustomer;
			customer.img.sprite = dataManager.nowCustomer.BlinkImages[0];

			// ボスにコーヒー出し終えたらボスイベント終了.
			if (customer.data.Name == "ボス") {
				dataManager.BossEventFlg = false;
				dataManager.UpdateSaveData ();
			}

			if (customer.name == "ルイ") {
				isCutomerOnlyRui = true;

				customer.gameObject.SetActive (false);
			} else {
				Invoke("SetBlinkToCustomer", blinkInvokeTime);
			}

			StartCoroutine(CustomerAction());
		} else {
			CallRandomCustomer ();
		}

		dataManager.isAfterCoffee = false;

	}
	
	// Update is called once per frame
	void Update () {

	}

	void SetBlinkToCustomer() {
		var blinkEye = customer.gameObject.AddComponent<BlinkEye>();
		blinkEye.blinkSprites = dataManager.nowCustomer.BlinkImages;
	}


	void CallRandomCustomer() {

		//AudioPlayer.PlaySe("Sounds/SE/EnterShop");

		if (dataManager.nowCustomer == null) {
			dataManager.SetNowCustomer ();
		}

		// debug.
		if (d_isDebug && !debugManager.d_isRandomCustomer) {
			dataManager.nowCustomer = dataManager.characterList[debugManager.d_characterId];
		}
		CafeGameUtil.SaveIsVisited (dataManager.nowCustomer.Name);

		if (!dataManager.isOpenDictionary) {
			StartCoroutine (jsonTalkManager.StartOpeningTalk (dataManager.nowCustomer.Name));
		} else {
			dataManager.isEvent = false;
		}

		customer.data = dataManager.nowCustomer;
		if (dataManager.nowCustomer.Name != "ルイ") {
			dataManager.isComingCustomer = true;
			//お客の画像を当てはめる
			customer.img.sprite = dataManager.nowCustomer.BlinkImages[0];

			Invoke("SetBlinkToCustomer", blinkInvokeTime);
		} else {
			isCutomerOnlyRui = true;
			//ルイくんしか居ない時
			dataManager.isComingCustomer = false;
			balloon.SetActive(false);

			customer.gameObject.SetActive (false);
		}

	}

	void InitVaristorMedals () {
		for (int i = 0, len = varistorMedals.Count; i < len; i++) {
			varistorMedals[i].Init();
		}
		SetVaristorMedals();
	}

	void SetVaristorMedals(){
		for (int i = 0, len = dataManager.varistorRank; i < len; i++)
		{
			Debug.LogFormat("lank {0}", i);
			varistorMedals[i].ToActive();
		}
	}


	#region AfterCoffee

	IEnumerator CustomerAction () {
		// 吹き出し.
		yield return DisplayReaction();
		// リアクションのコメントする.
		yield return jsonTalkManager.StartAfterCoffeeTalk (dataManager.nowCustomer.Name);

		// アニメーション.
		if (dataManager.isSuccess){
			if (dataManager.isLikeCoffee){
				PlayHappyGaugeAnime();
			}else {
				PlayKiraKiraAnime();
			}
		}else {
			PlayGuruGuruAnime();
		}
	}

	IEnumerator DisplayReaction() {
		if (dataManager.isSuccess) {
			if (dataManager.isLikeCoffee) {
				reactionSprite = reactionImages[1];
			} else {
				reactionSprite = reactionImages[0];
			}
		} else {
			reactionSprite = reactionImages[2];
		}
			
		//吹き出しの子オブジェクトにreactionSpriteをつける
		reactionIconObject.transform.Find("ReactionImage").gameObject.GetComponent<Image>().sprite = reactionSprite;

		yield return new WaitForSeconds(0.5f);

		reactionIconObject.SetActive(true);

		yield return new WaitForSeconds(1.5f);

		reactionIconObject.SetActive(false);
	}

	IEnumerator SayImpression () {

		yield return new WaitForSeconds(1);
	}

	//成功
	void PlayKiraKiraAnime()
	{
		//幸せのカケラアニメーション
		happyAnim.SetBool("isPlayKiraKira", true);
		StartCoroutine(KiraKiraAnime());
	}

	IEnumerator KiraKiraAnime()
	{
		yield return new WaitForSeconds(2);

		//happyGaugeImage.fillAmount += 0.2f;
		ChangeGaugeVal(0.2f, true);
		happyAnim.SetBool("isPlayKiraKira", false);
	}


	//成功＆好み
	void PlayHappyGaugeAnime () {
		//幸せのカケラアニメーション
		happyAnim.SetBool("isPlayHeart", true);
		StartCoroutine(HappyGaugeAnime());
	}

	IEnumerator HappyGaugeAnime() {
		yield return new WaitForSeconds(2);

		//happyGaugeImage.fillAmount += 0.4f;
		ChangeGaugeVal(0.4f, true);
		happyAnim.SetBool("isPlayHeart", false);
	}


	//失敗
	void PlayGuruGuruAnime () {
		//幸せのカケラアニメーション
		happyAnim.SetBool("isPlayGuruGuru", true);
		StartCoroutine(GuruGuruAnime());
	}

	IEnumerator GuruGuruAnime()
	{
		yield return new WaitForSeconds(2);

		//happyGaugeImage.fillAmount -= 0.2f;
		ChangeGaugeVal(-0.2f, true);
		happyAnim.SetBool("isPlayGuruGuru", false);
	}

	// fill amountが変わる
	void ChangeGaugeVal (float difference, bool isAnim) {
		float changeTo = GaugeVal + difference;

		if (changeTo > 1) {
			changeTo = 1;
		} else if (changeTo < 0) {
			changeTo = 0;
		}

		//データの保存
		dataManager.nowHappyGauge = changeTo;
		dataManager.UpdateSaveData();

		if (isAnim) {
			DOTween.To(() => GaugeVal, x => GaugeVal = x, changeTo, 1).OnComplete(OnCompleChangeGaugeVal);
		} else {
			GaugeVal = changeTo;
			happyGaugeImage.fillAmount = GaugeVal;
		}


	} 

	void OnCompleChangeGaugeVal () {
		//print("Oncomplete: " + GaugeVal);
		if (GaugeVal == 1.0f)
		{
			//バリスタランクを上げる
			dataManager.varistorRank += 1;

			switch (dataManager.varistorRank) {
				case 3:
				case 6:
				case 9:
					dataManager.BossEventFlg = true;
					break;
			}

			dataManager.UpdateSaveData ();

			StartCoroutine(ErasePanel());

			//バリスタメダルのロード
			SetVaristorMedals();

			ChangeGaugeVal(-1, false);
		}
		dataManager.isEvent = false;
	}

	IEnumerator ErasePanel(){
		dataManager.isEvent = true;

		RankNotice.SetActive(true);
		yield return new WaitWhile(() => Input.GetMouseButtonDown(0) == false);
		RankNotice.SetActive(false);

		dataManager.isEvent = false;
	}

	#endregion


	#region Debug

	void SetDebugStatuses () {
		if (debugManager.d_isDebugHappyGauge) {
			GaugeVal = debugManager.d_gaugeVal;
		}
	}

	#endregion
}
