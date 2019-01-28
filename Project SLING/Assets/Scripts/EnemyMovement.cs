using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour {

    private Animator enemyAnim;
    private RaycastHit turnTest;
    private GameObject Player;
    private float nexTimeToFire;
    public float speed, playerDamage, damageRate, turnLimit, turnAngle;

    void Start () {
        enemyAnim = GetComponent<Animator>();
        Player = Master.instance.Player;
    }

    void FixedUpdate () {
        //turnTest = Physics.Raycast (transform.position, transform.forward, turnLimit);
        //if (Physics.Raycast (transform.position, transform.forward, out turnTest, turnLimit) && (turnTest.transform.tag == "Wall")) {
		//	StartCoroutine (Swerve ());
		//}
        //else{
            transform.LookAt (Player.transform);
            transform.position += transform.forward * speed * Time.deltaTime;
        //}
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
        /*else if(other.gameObject.tag == "Enemy"){
            Vector3 repelDir = Vector3.zero; 
            repelDir += (transform.position - other.gameObject.transform.position).normalized;
            other.gameObject.transform.position += repelDir * Time.fixedDeltaTime;
        }*/
    }

    void OnTriggerExit (Collider other) {
        if ((other.gameObject.tag == "Player")) {
            enemyAnim.SetBool("Attack",false);
        }
    }

    IEnumerator Swerve () {
		transform.rotation = Quaternion.AngleAxis (turnAngle, Vector3.up);
        //while(Time.deltaTime<=2f)
		transform.position += transform.forward * speed * 0.25f;
        Debug.Log(Time.deltaTime);
		yield return new WaitForSeconds(5f);
	}

}