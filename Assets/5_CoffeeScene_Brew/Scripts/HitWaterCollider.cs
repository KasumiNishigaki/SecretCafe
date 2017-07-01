using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitWaterCollider : MonoBehaviour {

	public int id;
	public HitWaterManager hitWaterManager;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.tag == "WaterDrop") {
			if (id == 6) {
				Destroy(col.gameObject);
			} else {
				hitWaterManager.HitWater(this);
			}
		}
	}
}
