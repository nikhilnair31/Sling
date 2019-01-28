using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLayout : MonoBehaviour {

    public PlayerControllerv2 PC;
    private Animator moveAnim;
	private int faceNumber,currentFaceNumber;
	private bool firstTime = true;
	public GameObject[] flames;
	public GameObject[] Face;

	void Start()
	{
		//moveAnim = GetComponent<Animator>();
		currentFaceNumber = faceNumber = 0;
		Face[currentFaceNumber].SetActive(true);
	}

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			if(currentFaceNumber>=3){
				Face[currentFaceNumber].SetActive (false);
				currentFaceNumber = 0;
				firstTime = false;
				StartCoroutine(Break());
				Face[currentFaceNumber].SetActive (true);
				PC.ChangeWeapon(currentFaceNumber);
			}
			else{
				Face[currentFaceNumber].SetActive (false);
				currentFaceNumber++;
				StartCoroutine(Break());
				Face[currentFaceNumber].SetActive (true);
				PC.ChangeWeapon(currentFaceNumber);
			}
			if(currentFaceNumber==0 && firstTime){
				Face[currentFaceNumber].SetActive (false);
				faceNumber++;
				currentFaceNumber = faceNumber;
			}
		}
	}

	IEnumerator Break () {
		for(int i=0;i<4;i++){
			flames[i].SetActive (true);
		}
		yield return new WaitForSeconds (4f);
		for(int i=0;i<4;i++){
			flames[i].SetActive (false);
		}
		yield return null;
	}

	/*IEnumerator Delay() 
    {  
		moveAnim.SetInteger("FaceUp", 0);//Face0Down
		yield return new WaitForSeconds (1f);
		Face[currentFaceNumber].SetActive (false);
		faceNumber++;
		currentFaceNumber = faceNumber;
		//currentFace = Face[faceNumber];
		Face[currentFaceNumber].SetActive (true);
		moveAnim.SetInteger("FaceUp", 1);//Face1Up
	}*/

    //Box.transform.Rotate(Box.transform.rotation.eulerAngles.x + finalRotation,0,0);
    //StartCoroutine(RotateMe());
	/*IEnumerator RotateMe() 
     {  
		Quaternion fromAngle = Box.transform.localRotation;  
		float temp1 = fromAngle.eulerAngles.x;
		float temp2 = finalRotation;
		float temp3 = temp1 + temp2;
		Vector3 temp4 = Vector3.right * temp3;
		Quaternion toAngle = Quaternion.Euler(temp4);
        Debug.Log(fromAngle.eulerAngles+" - "+temp4+" - "+Box.transform.eulerAngles);
		float t = 0f;
		while(temp1 != temp2){
			float angle = Mathf.LerpAngle(temp1, temp2, t);
            Box.transform.eulerAngles = new Vector3(angle, 0, 0);
			t += Time.deltaTime/inTime;
			yield return null;
		}
     }*/
}