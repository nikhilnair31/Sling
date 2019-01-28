using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour {

	public int avgFrameRate;
	public float resolutionScale;
	public Text display_Text;

	void Start () {
		Screen.SetResolution ( 720, 1280 , true);
		Application.targetFrameRate = 60;
	}

	public void Update () {

		float current = 0;
		current = (int) (1f / Time.unscaledDeltaTime);
		avgFrameRate = (int) current;
		display_Text.text = avgFrameRate.ToString ();
	}

}