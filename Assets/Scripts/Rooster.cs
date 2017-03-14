using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rooster : MonoBehaviour {

	private Rigidbody2D rb;
	private SpriteRenderer sr;
	private Animator anim;
	public float accel;
	public float maxSpeed;
	public bool actionable;
	private Thwack thwack;
	public float dashVelocity;
	private List<int> bufferedInputs;
	public int inputBuffer;
	private Vector2 directionInput;
	private bool thwackInput;
	private bool dashInput;


	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		actionable = true;
		thwack = GetComponentInChildren<Thwack> ();
		bufferedInputs = new List<int> ();
	}
	
	// Update is called once per frame

	void Update(){
		LogInput ();
	}

	void FixedUpdate () {
		TickBufferedInputs ();
		ProcessInput ();
	}

	void LogInput(){
		float x = Input.GetAxis ("Horizontal_Rooster");
		float y = Input.GetAxis ("Vertical_Rooster");
		directionInput = new Vector2 (x, y);
		if (Input.GetButtonDown("Thwack")){
			thwackInput = true;
		} 
		if (Input.GetButtonDown ("Dash")) {
			dashInput = true;
		} 
	}

	void ProcessInput(){
		float x = directionInput.x;
		float y = directionInput.y;
		if (actionable) {
			Move (x, y);
			FaceProperDirection (x);
			if (ThwackBuffered () || thwackInput) {
				Thwack ();
				bufferedInputs.Clear ();
			}
			if (dashInput) {
				Dash ();
			}
		} else {
			if (thwackInput) {
				bufferedInputs.Add (0);
			}
		}
		thwackInput = false;
		dashInput = false;
	}

	void TickBufferedInputs(){
		if (bufferedInputs.Count > 0){
			for (int i = bufferedInputs.Count - 1; i >= 0; i--) {
				if (bufferedInputs [i] < inputBuffer) {
					bufferedInputs [i] += 1;
					Debug.Log (bufferedInputs [i]);
				}
				else {
					bufferedInputs.RemoveAt (i);
				}
			}
		}
	}

	bool ThwackBuffered(){
		return bufferedInputs.Count > 0;
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
				transform.localScale = new Vector3 (-1, 1, 1);
			} else {
				transform.localScale = new Vector3 (1, 1, 1);
			}
		}
	}

	void Thwack(){
		anim.SetTrigger ("thwack");
		actionable = false;
	}

	void Dash(){
		anim.SetTrigger ("dash");
		actionable = false;
		if (transform.localScale.x > 0) {
			rb.velocity = dashVelocity * Vector3.right;
		} else {
			rb.velocity = dashVelocity * Vector3.left;
		}

		rb.drag = 5;
	}

	void EndAnimation(){
		actionable = true;
		rb.drag = 10;
	}

	void EnableThwackHitbox(){
		thwack.EnableHitbox ();
	}

	void DisableThwackHitbox(){
		thwack.DisableHitbox ();
	}
}
