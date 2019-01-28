using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {

    public int maxHealth;
    public int currentHealth, scoreValue;
    public Slider healthBar;
    public GameObject spawnEffect;
    public bool destroyOnDeath;
    private Vector3 initPos;

    void Awake () {
        healthBar.value = maxHealth;
        initPos = transform.position;
    }

    public void TakeDamage (int amount) {

        currentHealth -= amount;
        healthBar.value = currentHealth;
        if (destroyOnDeath) {
            if (healthBar.value <= 0) {
                PlayerPrefs.SetInt ("Score", (PlayerPrefs.GetInt ("Score") + scoreValue));
                Master.instance.score.text = PlayerPrefs.GetInt ("Score").ToString ();
                Destroy (gameObject);
            }
        } else {
            if (healthBar.value <= 0) {
                PlayerPrefs.SetInt ("Score", (PlayerPrefs.GetInt ("Score") + scoreValue));
                Master.instance.score.text = PlayerPrefs.GetInt ("Score").ToString ();
                PowerUps.instance.Update();
                Vector3 spawnPosition = new Vector3 (Random.Range (-45, 45), 0, Random.Range (-45, 45));
                GameObject effect = Instantiate (spawnEffect, spawnPosition, Quaternion.identity);
                Destroy (effect, 2f);
                transform.position = spawnPosition;
                currentHealth = maxHealth;
                healthBar.value = maxHealth;
            }
        }

    }

    void OnTriggerStay (Collider other) {
        if (other.gameObject.tag == "Finish") {
            TakeDamage (100);
        }
    }

}