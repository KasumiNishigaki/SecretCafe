using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PotManager : MonoBehaviour {

	private AudioSource audioSource;
	private float nowVol;

	[HideInInspector]
	public bool canMove = true; // 自身のドラッグを許可するかどうか.

	bool isDrag;

	private int pourCnt = 0;

	private Vector3 emitPos;
	private Vector3 pEmitPos;

	public GameObject waterDropPrefab;

	public GameObject kona;

	//音をフェードアウト中か
	private bool _isFadeOut = false;
	//１回目か２回目か
	private int count;

	float brewTime;
	bool isEmptyWater;

	public GameObject nextBtn;

	public Slider slider; //Meterのslider
	public Image meterBg;

	float pourWaterPerTime;
	float audioDuration;

	//エミッタ保存フォルダー
	GameObject dropHolder;

	// Use this for initialization
	void Start () {
		print(Time.deltaTime);
		audioSource = gameObject.GetComponent<AudioSource>();

		dropHolder = new GameObject ();
		dropHolder.name =  "DropHolder";
	}
	
	// Update is called once per frame
	void Update () {

		if (canMove) {
			var touch = TouchManager.GetTouchInfo ();

			switch (touch) {
			case TouchInfo.Began:
				isDrag = IsTapMe ();
				break;
			case TouchInfo.Moved:
				if (isDrag) {
					OnDrag ();
				}
				break;
			case TouchInfo.Ended:
				if (isDrag) {
					isDrag = false;
					OnDragEnd ();
				}
				break;
			}
		}
			
		//フェードアウト処理
		if (_isFadeOut) {
			audioSource.volume -= Time.deltaTime * 0.5f;
			if(audioSource.volume <= 0){
				//ボリュームが0になったら停止、ボリュームを戻す
				_isFadeOut = false;
				audioSource.Stop ();

				slider.value = 0f;
				meterBg.fillAmount = slider.value;
			}
		}

	}

	/// <summary>
	/// 自身をタップ中かどうか調べる関数.
	/// </summary>
	/// <returns><c>true</c>, 自身をタップしている, <c>false</c> 自身をタップしていない.</returns>
	bool IsTapMe () {
		Vector2 tapPoint = TouchManager.GetTouchWorldPosition (Camera.main);
		var col2d = Physics2D.OverlapPoint (tapPoint);

		if (col2d) {
			var hit = Physics2D.Raycast (tapPoint, -Vector2.up);

			if (hit.collider.gameObject == gameObject) {
				this.transform.rotation = Quaternion.Euler(0, 0, 40);

				if (!isEmptyWater) {
					audioSource.Play();
				}

				//粉モクモクアニメーション
				kona.GetComponent<Animator>().SetBool("isPlayKona", true);
				return true;
			}
		}
		return false;
	}

	/// <summary>
	/// 自身をドラッグ中に呼ばれるイベント.
	/// </summary>
	void OnDrag () {
		Vector2 tapPoint = TouchManager.GetTouchWorldPosition (Camera.main);
		transform.position = tapPoint;

		brewTime += Time.deltaTime;
		/*
		if (brewTime >= 10.5f) {
			//音をフェードアウトさせる
			_isFadeOut = true;

			isEmptyWater = true;
			nextBtn.SetActive(true);
			kona.GetComponent<Animator>().SetBool("isPlayKona", false);
			return;
		}
		*/

		if (slider.value <= 0) {
			_isFadeOut = true;

			isEmptyWater = true;
			nextBtn.SetActive(true);
			kona.GetComponent<Animator>().SetBool("isPlayKona", false);
			return;
		}

		//水スライダー
		slider.value -= 0.0018f;

		//水
		Vector3 screenPos = Input.mousePosition;
		emitPos = Camera.main.ScreenToWorldPoint (screenPos);
		emitPos.z = 0;
		this.gameObject.transform.position = emitPos;

		//2で割り切れる時だけ生成(値を大きくするほど間隔があく)
		if(pourCnt++ % 2 == 0){
			GameObject dropGo = Instantiate (waterDropPrefab, new Vector3 (emitPos.x -1f, emitPos.y -0.4f, emitPos.z), Quaternion.identity) as GameObject;

			dropGo.transform.parent = dropHolder.transform;

			float offsetX = Mathf.Abs (emitPos.x - pEmitPos.x);
			offsetX *= 10;

			//方向に飛んでいく重力で落ちていく
			Vector2 vel = new Vector2 (-offsetX, 1f);
			dropGo.GetComponent<Rigidbody2D> ().velocity = vel;

		}

		//slider.value -= pourWaterPerTime;
		meterBg.fillAmount = slider.value;

		pEmitPos = emitPos;

	}

	/// <summary>
	/// 自身を離した時に呼ばれるイベント.
	/// </summary>
	void OnDragEnd () {
		// other...
		//animator.SetBool ("isPanic", false);

		this.transform.rotation = Quaternion.Euler(0, 0, 0);

		audioSource.Stop();
	}

}
