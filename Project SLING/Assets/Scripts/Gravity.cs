using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravity : MonoBehaviour {

	public GameObject Cube;
    public float gravityForce = 10;

	void Start () {
        Physics.gravity = Vector3.zero;
	}

	void FixedUpdate()
	{
		Vector3 pos = transform.position;
        Vector3 gravityDirection = -(this.transform.position - Cube.transform.position).normalized;
		this.GetComponent<Rigidbody>().AddForce(gravityDirection * gravityForce);
	}
}