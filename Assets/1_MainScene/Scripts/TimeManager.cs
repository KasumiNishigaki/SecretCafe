using UnityEngine;
using System.Collections;

public class TimeManager : MonoBehaviour {

	private string hourStr;
	private string minuteStr;

	//時間の経過保存
	private float duration;


	// Use this for initialization
	void Start () {
		//最初にdurationを０にする。
		duration = 0;

		// 取得する値: 時
		hourStr = System.DateTime.Now.Hour.ToString();
		// 取得する値: 分
		minuteStr = System.DateTime.Now.Minute.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		//フレーム書き換えごとに経過時間を累積
		//duration += Time.deltaTime;
	}
}
