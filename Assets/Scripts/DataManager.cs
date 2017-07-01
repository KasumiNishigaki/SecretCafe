using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class DataManager : MonoBehaviour {

	private static DataManager _instance;
	public static DataManager Instance{
		get{
			if(_instance == null){
				var go = new GameObject ("DataManager");
				_instance = go.AddComponent<DataManager> ();
			}
			//初期化しない/シングルトンだけでつかってあげると良い
			DontDestroyOnLoad (_instance);
			return _instance;
		}
	}

	//珈琲を作ったあとにホームに来たかどうか
	public bool isAfterCoffee;

	//来たお客記録用変数
	public Character nowCustomer;

	//お客がいるかいないか
	public bool isComingCustomer;

	//選択された豆の記録用変数
	public List<int> beanList = new List<int>();

	//作った(現在の)珈琲の苦さ保存用変数
	public int bitterJudge;

	//ミルを何回回したか記録用変数
	public int millCount;

	//減点記録用変数
	public int demeritPoints;

	//失敗か成功か
	public bool isSuccess;

	//お客の好みの珈琲か記録用変数
	public bool isLikeCoffee;

	//最終的にできた珈琲の説明番号
	public int resultCoffeeNum;

	//何の珈琲が出来たか記録用変数 0_ブレンド　1_カフェオレ 2_ウインナー 3_失敗 
	public int resultCoffee;

	//キャラクター情報リスト
	public List<Character> characterList = new List<Character>();

	//幸せゲージデータ保存変数
	public float nowHappyGauge;

	//バリスタランク
	public int varistorRank;

	public bool isEvent;

	public bool BossEventFlg;

	public bool isOpenDictionary;

	// Use this for initialization
	void Start () {
		
	}
	// Update is called once per frame
	void Update () {
	
	}

	public void SetNowCustomer () {
		int nowHour = System.DateTime.Now.Hour;

		if (BossEventFlg) {
			nowCustomer = GetCharacter ("ボス");
			return;
		}

		// 必要レベルを満たしていて、かつ来店時間内のキャラクターを、ボスを除いて一人ピックアップ.
		var rdmCustomer = characterList
			.Where (c => c.Name != "ボス" && c.Name != "ルイ")
			.OrderBy (i => System.Guid.NewGuid ())
			.First ();

		nowCustomer = rdmCustomer;
	}

	//キャラの設定を呼び出す
	public Character GetCharacter (string name) {
		for(int i = 0, len = characterList.Count; i < len; i++){
			if(characterList[i].Name == name){
				return characterList [i];
			}
		}
		return null;
	}

	//データのロード
	public void LoadSaveData() {
		/* ロードするデータがあるか確認 */
		if (ES2.Exists("nowGauge")) {
			//幸せゲージのロード.
			nowHappyGauge = ES2.Load<float>("nowGauge");
		}

		if (ES2.Exists("myRank")) {
			//バリスタランクのロード.
			varistorRank = ES2.Load<int>("myRank");
		}

		if (ES2.Exists ("bossEventFlg")) {
			BossEventFlg = ES2.Load <bool> ("bossEventFlg");
		}
	}


	//データのセーブ
	public void UpdateSaveData() {
		ES2.Save (nowHappyGauge, "nowGauge");
		ES2.Save (varistorRank, "myRank");
		ES2.Save (BossEventFlg, "bossEventFlg");
	}
}
