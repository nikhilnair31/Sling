using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

	private Scene scene;
	private string storeID = "3009316";
	private Scene m_Scene;
	public PostProcessVolume PPV;
	public GameObject controlUI, startCanvasUI, pauseCanvasUI;
	public Animator startCanvasAnim, pauseCanvasAnim;

	void Awake () {
		Time.timeScale = 0f;
		controlUI.SetActive (false);
		pauseCanvasUI.SetActive (false);
	}

	void Start () {
		Advertisement.Initialize(storeID,true);
		PPV.weight = 0;
	}

	public void toEnter () {
		StartCoroutine (WaittoEnter ());
	}

	public void toAlt () {
		Time.timeScale = 1f;
		SceneManager.LoadScene("Funky_Skybox_Scene");
		m_Scene = SceneManager.GetActiveScene();
	}

	public void DirectPause () {
		StartCoroutine (WaittoDirectPause ());
	}

	public void Pause () {
		controlUI.SetActive (false);
		pauseCanvasUI.SetActive (true);
		pauseCanvasAnim.Play ("Pausing");
		Time.timeScale = 0.05f;
	}

	public void Mute () {
		if(SoundManager.instance.efxSource.volume == 1){
			SoundManager.instance.efxSource.volume = 0;
			Master.instance.crossedOutMute.text = "-----".ToString ();
		}
		else if(SoundManager.instance.efxSource.volume == 0){
			SoundManager.instance.efxSource.volume = 1;
		Master.instance.crossedOutMute.text = "".ToString ();
		}
	}

	public void UnPause () {
		StartCoroutine (WaittoUnPause ());
	}

	public void DeathScreen () {
		controlUI.SetActive (false);
		pauseCanvasUI.SetActive (true);
		pauseCanvasAnim.Play ("Death");
		Time.timeScale = 1f;
	}

	public void Settings () {
		pauseCanvasAnim.Play ("SettingsOn");
	}

	public void Help () {
		pauseCanvasAnim.Play ("HelpOn");
	}

	public void HelpOff () {
		pauseCanvasAnim.Play ("HelpOff");
	}

	public void Back () {
		StartCoroutine (WaittoBack ());
	}

	public void Reset () {
		ShowAd ();
		SceneManager.LoadScene ("1");
	}

	public void ShowAd () {
		if (Advertisement.IsReady ("video")) {
			Advertisement.Show ("video");
		}
	}

	IEnumerator WaittoEnter () {
		Time.timeScale = 1f;
		PPV.weight = 1;
		startCanvasAnim.Play ("Starting");
		yield return new WaitForSeconds (.5f);
		controlUI.SetActive (true);
		startCanvasUI.SetActive (false);
		pauseCanvasUI.SetActive (false);
	}

	IEnumerator WaittoUnPause () {
		Time.timeScale = 1f;
		pauseCanvasAnim.Play ("UnPausing");
		yield return new WaitForSeconds (0.625f);
		controlUI.SetActive (true);
		startCanvasUI.SetActive (false);
		pauseCanvasUI.SetActive (false);
	}

	IEnumerator WaittoBack () {
		Time.timeScale = 1f;
		pauseCanvasAnim.Play ("UnPausing");
		yield return new WaitForSeconds (1.25f);
		controlUI.SetActive (true);
		startCanvasUI.SetActive (false);
	}

	IEnumerator WaittoDirectPause () {
		Time.timeScale = 1f;
		PPV.weight = 1;
		startCanvasAnim.Play ("Starting");
		yield return new WaitForSeconds (1.25f);
		controlUI.SetActive (false);
		pauseCanvasUI.SetActive (true);
		pauseCanvasAnim.Play ("Pausing");
		Time.timeScale = 0.05f;
	}

}