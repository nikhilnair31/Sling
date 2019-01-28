using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour {

	//public Camera cam;

	void Update () {
		transform.LookAt (Camera.main.transform);
	}
}