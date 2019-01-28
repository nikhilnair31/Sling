using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour {

	public Transform target;
	public float smoothSpeed, rotationSpeed, swing;
	public Vector3 offset;

	void FixedUpdate () {
		Vector3 desiredPosition = target.position + offset;
		transform.position = Vector3.Lerp (transform.position, desiredPosition, smoothSpeed);;
		transform.rotation =  Quaternion.Slerp(transform.rotation, target.rotation, rotationSpeed *  Time.deltaTime);
		transform.LookAt (target);
	}

	public IEnumerator Shake (float duration, float magnitude) {
		Vector3 orignalPosition = transform.position;
		float elapsed = 0f;
		while (elapsed < duration) {
			float x = Random.Range (1.25f, 1.75f);
			float y = Random.Range (16.25f, 16.75f);
			float z = Random.Range (-10.25f, -10.75f);
			transform.position = new Vector3 (x, y, z);
			elapsed += Time.deltaTime;
			yield return 0;
		}
		transform.position = orignalPosition;
	}

}