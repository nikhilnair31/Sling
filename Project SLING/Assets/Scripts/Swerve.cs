using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swerve : MonoBehaviour {
    
    // Fix a range how early u want your enemy detect the obstacle.
    private int range;
    private bool isThereAnyThing = false;
    private Animator enemyAnim;
    private GameObject Player;
    private float nexTimeToFire;
    public float  speed, playerDamage, damageRate;
    public GameObject target;
    private float rotationSpeed;
    private RaycastHit hit;


    void Start() {
        enemyAnim = GetComponent<Animator>();
        Player = Master.instance.Player;
        range = 80;
        rotationSpeed = 15f;
    }

    void OnTriggerEnter (Collider other) {
        if ((other.gameObject.tag == "Player")) {
            enemyAnim.SetBool("Attack",true);
        }
    }

    void OnTriggerStay (Collider other) {
        if ((other.gameObject.tag == "Player") && (Time.time >= nexTimeToFire)) {
            nexTimeToFire = Time.time + 1f / damageRate;
            Health PH = other.transform.GetComponent<Health> ();
            enemyAnim.SetBool("Attack",true);
            PH.TakeDamage ((int) playerDamage);
        }
    }

    void OnTriggerExit (Collider other) {
        if ((other.gameObject.tag == "Player")) {
            enemyAnim.SetBool("Attack",false);
        }
    }

    void Update() {
        //Look At Somthly Towards the Target if there is nothing in front.
        if (!isThereAnyThing) {
            Vector3 relativePos = target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime);
        }
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        Transform leftRay = transform;
        Transform rightRay = transform;
        if (Physics.Raycast(leftRay.position + (transform.right * 2), transform.forward, out hit, range) || Physics.Raycast(rightRay.position - (transform.right * 2), transform.forward, out hit, range)) {
            if (hit.collider.gameObject.CompareTag("Wall")) {
                isThereAnyThing = true;
                transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
            }
        }
        if (Physics.Raycast(transform.position - (transform.forward * 4), transform.right, out hit, 6) || Physics.Raycast(transform.position - (transform.forward * 4), -transform.right, out hit, 6)) {
            if (hit.collider.gameObject.CompareTag("Wall")) {
                isThereAnyThing = false;
            }
        }
        Debug.DrawRay(transform.position + (transform.right * 7), transform.forward * 20, Color.red);
        Debug.DrawRay(transform.position - (transform.right * 7), transform.forward * 20, Color.red);
        Debug.DrawRay(transform.position - (transform.forward * 4), -transform.right * 20, Color.yellow);
        Debug.DrawRay(transform.position - (transform.forward * 4), transform.right * 20, Color.yellow);
    }
}