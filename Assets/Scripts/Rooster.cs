using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooster : MonoBehaviour {

	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private Animator anim;
	public float accel;
	public float maxSpeed;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		ProcessInput ();
	}

	void ProcessInput(){
		float x = Input.GetAxis ("Horizontal_Rooster");
		float y = Input.GetAxis ("Vertical_Rooster");
		Move (x, y);
		FaceProperDirection (x);
	}

	void Move(float x, float y){
		Vector2 inputVector = new Vector2 (x, y);
		rb.AddForce (inputVector * accel);

		if (inputVector.magnitude > 0.05f) {
			anim.SetBool ("moving", true);
		} else {
			anim.SetBool ("moving", false);
		}

		if (rb.velocity.magnitude > maxSpeed) {
			rb.velocity = maxSpeed * inputVector.normalized;
		}
	}

	void FaceProperDirection(float x){
		if (Mathf.Abs(x) > 0.01f) {
			if (x < 0) {
				sr.flipX = true;
			} else {
				sr.flipX = false;
			}
		}
	}
}
