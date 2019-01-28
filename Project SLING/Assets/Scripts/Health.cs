using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

public class Health : MonoBehaviour {

	private const int maxHealth = 100;
	[HideInInspector] public int currentHealth = 100;
	private int numberOfAttemptsCopy;
	private float initRate, initRange, initDamage;
	private ColorGrading colorGradingLayer = null;
	private PlayerControllerv2 PCv2;
	private bool firstTime = true;

	[Header ("Health and Effects")]
	public MenuManager MM;
	public int numberOfAttempts = 3;
	public GameObject[] heart;
	public Slider healthBar;
	public GameObject switchEffect, Lights;
	public PostProcessVolume PPV;

	[Header ("Spawn Point")]
	public float xPos;
	public float yPos;
	public float zPos;

	[Header ("Rage")]
	public bool inTempRage;
	public int saturation;
	public int rageLimit;
	public int rageRateBoost;
	public int rageRangeBoost;
	public int rageDamageBoost;

	void Awake () {
		inTempRage = false;
		healthBar.value = currentHealth;
		numberOfAttemptsCopy = numberOfAttempts;
		PPV.profile.TryGetSettings (out colorGradingLayer);
		PCv2 = GetComponent<PlayerControllerv2> ();
	}

	public void TempRage (float ragePickupDuration) {
		StartCoroutine(TempRageTime(ragePickupDuration));
	}

	IEnumerator TempRageTime(float ragePickupDuration){
		inTempRage = true;
        initRate = PCv2.fireRate;
		initRange = PCv2.fireRange;
		initDamage = PCv2.fireDamage;
		colorGradingLayer.saturation.value = saturation;
		Lights.SetActive(true);
		Camera cam = Camera.main;
		GameObject instimpactEffect = Instantiate (switchEffect, cam.transform.position, Quaternion.identity);
		Destroy (instimpactEffect, 2f);
		PCv2.fireRate += rageRateBoost;
		PCv2.fireRange += rageRangeBoost;
		PCv2.fireDamage += rageDamageBoost;
		yield return new WaitForSeconds(ragePickupDuration);
		PCv2.fireRate = initRate;
		PCv2.fireRange = initRange;
		PCv2.fireDamage = initDamage;
		colorGradingLayer.saturation.value = -100;
		Lights.SetActive(false);
		inTempRage = false;
		
	}

	public void RageReset(){
		PCv2.fireRate = initRate;
		PCv2.fireRange = initRange;
		PCv2.fireDamage = initDamage;
		colorGradingLayer.saturation.value = -100;
		Lights.SetActive(false);	
	}

	public void TakeDamage (int amount) {

		currentHealth -= amount;
		healthBar.value = currentHealth;
		if ((healthBar.value <= rageLimit) && firstTime) {
			initRate = PCv2.fireRate;
		    initRange = PCv2.fireRange;
		    initDamage = PCv2.fireDamage;
			colorGradingLayer.saturation.value = saturation;
			Lights.SetActive(true);
			Camera cam = Camera.main;
			GameObject instimpactEffect = Instantiate (switchEffect, cam.transform.position, Quaternion.identity);
			Destroy (instimpactEffect, 2f);
			PCv2.fireRate += rageRateBoost;
			PCv2.fireRange += rageRangeBoost;
			PCv2.fireDamage += rageDamageBoost;
			firstTime = false;
		}
		if ((healthBar.value <= 0)) {

			if (numberOfAttemptsCopy == 1) {
				if( (PlayerPrefs.GetInt ("Score")) > (PlayerPrefs.GetInt ("HighScore")) ){
				    PlayerPrefs.SetInt("HighScore",(PlayerPrefs.GetInt ("Score")));
				}
				Master.instance.scoreOnDeath.text = PlayerPrefs.GetInt ("Score").ToString ();
				Master.instance.highScore.text = PlayerPrefs.GetInt ("HighScore").ToString ();
				Destroy (gameObject);
				MM.DeathScreen();
			} else {
				heart[numberOfAttemptsCopy-1].SetActive(false);
				numberOfAttemptsCopy--;
				Vector3 spawnPosition = new Vector3 (Random.Range (-xPos, xPos), yPos, Random.Range (-zPos, zPos));
				colorGradingLayer.saturation.value = -100;
				Lights.SetActive(false);
				firstTime = true;
				PCv2.fireRate = initRate;
				PCv2.fireRange = initRange;
				PCv2.fireDamage = initDamage;
				currentHealth = maxHealth;
				healthBar.value = currentHealth;
				transform.position = spawnPosition;
			}

		}

	}

}