using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Master : MonoBehaviour {

	public static Master instance;

	void Awake () {
		instance = this;
		PlayerPrefs.SetInt ("Score", 0);
	}
	public Text score,scoreOnDeath,soulsCollected,highScore,crossedOutMute;
	public GameObject Player
	;

}