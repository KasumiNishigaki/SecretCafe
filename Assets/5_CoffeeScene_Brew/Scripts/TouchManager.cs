using UnityEngine;
using System.Collections;

public enum TouchInfo {
	Began = 0,
	Moved,
	Stationary,
	Ended,
	Canceled,
	None = 99
}

public static class TouchManager {
	static Vector3 TouchPosition = Vector3.zero;

	/// <summary>
	/// タッチ情報を取得.
	/// </summary>
	/// <returns>タッチ情報.</returns>
	public static TouchInfo GetTouchInfo () {
		if (Application.isEditor) {
			if (Input.GetMouseButtonDown (0)) {
				return TouchInfo.Began;
			}

			if (Input.GetMouseButton (0)) {
				return TouchInfo.Moved;
			}

			if (Input.GetMouseButtonUp (0)) {
				return TouchInfo.Ended;
			}
		} else if (Application.isMobilePlatform) {
			if (Input.touchCount > 0) {
				return (TouchInfo)((int)Input.GetTouch (0).phase);
			}
		}
		return TouchInfo.None;
	}

	/// <summary>
	/// タッチポジションを取得.
	/// </summary>
	/// <returns>タッチポジション。タッチされていない場合は (0, 0, 0).</returns>
	public static Vector3 GetTouchPosition () {
		if (Application.isEditor) {
			TouchInfo touch = GetTouchInfo ();

			if (touch != TouchInfo.None) {
				return Input.mousePosition;
			}
		} else {
			if (Input.touchCount > 0) {
				Touch touch = Input.GetTouch (0);

				TouchPosition.x = touch.position.x;
				TouchPosition.y = touch.position.y;

				return TouchPosition;
			}
		}
		return Vector3.zero;
	}

	/// <summary>
	/// タッチワールドポジションを取得.
	/// </summary>
	/// <param name='camera'>カメラ.</param>
	/// <returns>タッチワールドポジション。タッチされていない場合は (0, 0, 0).</returns>
	public static Vector3 GetTouchWorldPosition (Camera camera) {
		return camera.ScreenToWorldPoint (GetTouchPosition ());
	}
}
