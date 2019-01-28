using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public static PowerUps instance;
    [Header ("Other")]
    public float xPos;
    public float yPos;
    public float zPos;
    public float timeValue;
    private bool isCORn;
    [Header ("Soul Currency")]
    public GameObject soul;
    public float soulSpawnInterval;
    public int soulProbabilityFactor;//It's to check if a random number from 0-100 >= this number
    [Header ("Health PowerUp")]
    public GameObject healthPowerup;
    public float healthPowerupSpawnInterval;
    public int healthProbabilityFactor;//It's to check if a random number from 0-100 >= this number
    [Header ("Rage PowerUp")]
    public GameObject ragePowerup;
    public float ragePowerupSpawnInterval;
    public int rageProbabilityFactor;//It's to check if a random number from 0-100 >= this number

    void Start()
    {
        instance = this;
        //Health PH = GetComponent<Health> ();
    }

    public void Update()
    {
        int rando = Random.Range (0, 100);
        //Debug.Log(rando);
        if((rando >= healthProbabilityFactor) && !isCORn){
            //Debug.Log("healthPowerup-"+isCORn);
            StartCoroutine(SpawnInterval(healthPowerup, healthPowerupSpawnInterval));
        }
        rando = Random.Range (0, 100);
        if((rando >= rageProbabilityFactor) && !isCORn){
            //Debug.Log("ragePowerup-"+isCORn);
            StartCoroutine(SpawnInterval(ragePowerup, ragePowerupSpawnInterval));
        }
        rando = Random.Range (0, 100);
        if((rando >= soulProbabilityFactor) && !isCORn){
            //Debug.Log("ragePowerup-"+isCORn);
            StartCoroutine(SpawnInterval(soul, soulSpawnInterval));
        }
    }

    IEnumerator SpawnInterval(GameObject powerup, float powerupSpawnInterval){
        isCORn = true;
        Vector3 spawnPosition = new Vector3 (Random.Range (-xPos, xPos), yPos, Random.Range (-zPos, zPos));
        Instantiate (powerup, spawnPosition, Quaternion.identity);
        float randomTimeValue = Random.Range (-timeValue, timeValue);
		yield return new WaitForSeconds(powerupSpawnInterval + randomTimeValue);
        isCORn = false;
	}
}
