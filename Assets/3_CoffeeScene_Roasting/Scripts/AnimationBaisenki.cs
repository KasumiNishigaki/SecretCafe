using UnityEngine;
using System.Collections;

public class AnimationBaisenki : MonoBehaviour {

	private Animator animator;
	//子の取得
	private Animator MaterAnimation;

	// Use this for initialization
	void Start () {
		animator = this.gameObject.GetComponent<Animator> ();
		MaterAnimation = transform.Find ("Mater").gameObject.GetComponent<Animator> ();

		StartRoast ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void StartRoast(){
		animator.SetBool ("isBraking", true);
		MaterAnimation.SetBool ("isBraking", true);

		StartCoroutine ("StopRoast");
	}

	IEnumerator StopRoast(){
		yield return new WaitForSeconds (3.0f);
		animator.SetBool ("isBraking", false);
		MaterAnimation.SetBool ("isBraking", false);
	}


}
