using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thwack : MonoBehaviour {

	public float thwackPower;
	public float stun;

	void Start(){
		DisableHitbox ();
	}

	void OnTriggerEnter2D(Collider2D col){
		GameObject go = col.gameObject;
		if (go.tag == "Hedgehog") {
			Vector3 knockback = (go.transform.position - transform.position).normalized * thwackPower;
			go.GetComponent<Hedgehog> ().GetThwacked (knockback, stun);
		}
	}

	public void EnableHitbox(){
		GetComponent<Collider2D> ().enabled = true;
	}

	public void DisableHitbox(){
		GetComponent<Collider2D> ().enabled = false;
	}
}
