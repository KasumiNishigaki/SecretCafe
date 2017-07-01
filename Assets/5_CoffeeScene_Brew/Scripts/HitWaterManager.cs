using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWaterManager : MonoBehaviour {

	DataManager dataManager;

	private int drop_1Cnt;
	private int drop_2Cnt;
	private int drop_3Cnt;

	public GameObject moya;

	// Use this for initialization
	void Start() {
		dataManager = DataManager.Instance;
	}

	// Update is called once per frame
	void Update() {

	}

	public void HitWater (HitWaterCollider col) {
		if (col.id == 1) {
			drop_1Cnt++;
		}

		if (col.id == 2) {
			drop_2Cnt++;
		}

		if (col.id == 3) {
			drop_3Cnt++;
		}

		//BadColliderに当たったら減点
		if (col.id == 4) {
			Invoke("SetAnime", 1);
			dataManager.demeritPoints++;
			moya.GetComponent<Animator>().SetBool("isMoyaPlay", true);
		}
	}

	void SetAnime(){
		moya.GetComponent<Animator>().SetBool("isMoyaPlay", false);
	}

	public bool isAverageDropCnt() {
		int baseCnt = drop_1Cnt;
		int threshold = 150;

		// drop_2Cntとdrop_3Cntがdrop_1Cntの+- thereshold内に収まっているかを調べる.

		if (drop_2Cnt <= baseCnt + threshold && drop_2Cnt >= baseCnt - threshold) {
			// do nothing.
		} else {
			return false;
		}

		if (drop_3Cnt <= baseCnt + threshold && drop_3Cnt >= baseCnt - threshold) {
			// do nothing.
		} else {
			return false;
		}

		return true;
	}
}
