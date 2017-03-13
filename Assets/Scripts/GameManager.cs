using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public int eggsPilfered;
	public GameObject eggsPilferedUI;

	// Use this for initialization
	void Start () {
		eggsPilfered = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Reset")) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
	}

	public void EggPilfered(){
		eggsPilfered += 1;
		UpdateEggsPilferedUI ();
	}

	void UpdateEggsPilferedUI(){
		eggsPilferedUI.GetComponent<Text> ().text = "EGGS PILFERED: " + eggsPilfered;
	}
}
