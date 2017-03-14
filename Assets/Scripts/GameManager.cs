using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public int eggCount;
	public GameObject winText;

	// Use this for initialization
	void Start () {
		eggCount = 5;
		winText.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Reset")) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex);
		}
	}

	public void EggPilfered(){
		eggCount -= 1;
		if (eggCount == 0) {
			GameWin ("HEDGEHOG");
		}
	}

	public void GameWin(string winner){
		winText.GetComponent<Text> ().text = winner + " WINS";
		winText.SetActive (true);
	}
}
