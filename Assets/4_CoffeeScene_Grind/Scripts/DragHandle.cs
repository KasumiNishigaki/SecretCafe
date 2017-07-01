using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;



public class DragHandle : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler  {

	DataManager dataManager;

	private Vector2 targetPosition; //指の位置
	private Vector3 joystickPosition; //丸の位置
	private Vector2 basePosition; //基準点

	public GameObject baseObj;

	private float originalZ;

	public float handleRange = 340f; //ハンドルの長さ
	public GameObject handle;

	float prevQutZ = 0;
	float totalRotationZ = 0;

	int[] hitPoints = { 360, 315, 270, 225, 180, 135, 90, 45 };
	int prevHitPointsIdx = 0;

	bool startRotation;

	int oneCount;

	bool isReverseTurn;

	public Slider slider; //Meterのslider
	public Image meter;

	public GameObject nextBtn;
	
	// Use this for initialization
	void Start () {
		dataManager = DataManager.Instance;

		//基準の位置をbasePositionに入れる
		basePosition = baseObj.GetComponent<RectTransform> ().transform.position;
		originalZ = transform.position.z;

		handleRange = GetComponent <RectTransform> ().transform.position.y - basePosition.y;

		targetPosition = Vector2.up;
		targetPosition = targetPosition.normalized * handleRange; //ベクトルの長さを１にしてhandleRange分だけ伸ばす

		joystickPosition = basePosition + targetPosition;
		joystickPosition.z = originalZ;
	}
	
	// Update is called once per frame
	void Update () {

		//Vector3 pos = joystickPosition;//this.gameObject.transform.position;

	}
		
	public void OnBeginDrag(PointerEventData eventData){
		//Debug.Log ("OnBeginDrag eventData = " + eventData);
	}
		
	public void OnDrag(PointerEventData eventData){
		//Debug.Log ("OnDrag eventData = " + eventData);

		targetPosition = eventData.position;
		targetPosition = Vector2.ClampMagnitude (targetPosition - basePosition, handleRange); //handleRangeを超えないようにする(それ以上は切ってくれる)
		targetPosition = targetPosition.normalized * handleRange;

		joystickPosition = basePosition + targetPosition;
		joystickPosition.z = originalZ;

		this.gameObject.transform.position = joystickPosition;

		float angle = Mathf.Atan2 (joystickPosition.y-basePosition.y, joystickPosition.x-basePosition.x);

		//Debug.Log ("joystickPosition = " + joystickPosition);
		//Debug.Log ("angle = " + angle );
		//Debug.Log ("qut = " + angle * Mathf.Rad2Deg );

		Quaternion qut =  Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg-90);
		handle.transform.localRotation = qut;

		float qutZ = handle.transform.eulerAngles.z;
		float prevTotalRotationZ = totalRotationZ;

		totalRotationZ += (qutZ - prevQutZ);

		if (prevQutZ > 300 && totalRotationZ < 50) {
			isReverseTurn = true;
		} else if (totalRotationZ > prevQutZ) {
			isReverseTurn = true;
		} else {
			isReverseTurn = false;
		}

		//Debug.LogFormat ("逆回転: {0}", isReverseTurn);

		if (!isReverseTurn) {
			if (prevHitPointsIdx == hitPoints.Length - 1) {
				if (totalRotationZ < hitPoints [0] && totalRotationZ > hitPoints [7]) {
					On45DegreeRotation ();
				}
			} else if (totalRotationZ < hitPoints [prevHitPointsIdx + 1]) {
				On45DegreeRotation ();
			}
		}

		if (totalRotationZ > prevTotalRotationZ && startRotation) {
			OnOneRevolution ();
		}

		//Debug.Log ("qut = " + qutZ );
		//Debug.Log ("prevQutZ = " + prevQutZ );

		//print ("total = " + totalRotationZ);

		prevQutZ = qutZ;
	}

	public void OnEndDrag(PointerEventData eventData) {
		//Debug.Log ("OnEndDrag eventData = " + eventData);
	}

	void OnTriggerEnter(Collider col){
		//Debug.Log (col.name);
	}

	// 45°回転.
	void On45DegreeRotation () {
		if (!startRotation) {
			startRotation = true;
		}

		prevHitPointsIdx++;
		if (prevHitPointsIdx == hitPoints.Length) {
			prevHitPointsIdx = 0;
		}

		slider.value += 0.005f;
		meter.fillAmount = slider.value;

		AudioPlayer.PlaySe("Sounds/SE/garigari_2");
	}

	// 1回転.
	void OnOneRevolution () {
		oneCount++;
		dataManager.millCount = oneCount;

		nextBtn.SetActive(true);

		//print ("１回転しました");
		//AudioPlayer.PlaySe("Sounds/SE/cancel");
	}

	//逆走した時は音鳴らない、回数は数えない
}