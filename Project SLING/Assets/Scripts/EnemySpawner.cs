using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour {

    private Vector3 spawnPosition;
    private int i, j;

    [Header ("Spawn")]
	public GameObject spawnEffect;
	public GameObject[] enemyPrefab;
	public int xPos,yPos,zPos;
	public int numberOfEnemies;
	[Header ("Wave")]
	public int waveLimit;
	public float waveRate;
	public float waveRateReduction;
	public int waveLimitAddition;
	[Header ("Enemy")]
	public int enemyLimit;
	public float spawnRate;
	public float spawnRateReduction;
	public int enemyLimitAddition;

	void Start () {
		StartCoroutine (SpawnWaves ());
	}

	IEnumerator SpawnWaves ()
    {
		while(true){
			for (int j = 0; j < waveLimit; j++)
			{
				GameObject selectedPrefab = enemyPrefab[(Random.Range (0, enemyPrefab.Length))];
				for (int i = 0; i < enemyLimit; i++)
				{
					spawnPosition = new Vector3 (Random.Range (-xPos, xPos), transform.position.y, Random.Range (-zPos, zPos));
					GameObject effect = Instantiate (spawnEffect, spawnPosition, Quaternion.identity);
					Destroy (effect, 2f);
					Instantiate (selectedPrefab, spawnPosition, Quaternion.identity);
					yield return new WaitForSeconds (spawnRate);
				}
				yield return new WaitForSeconds (waveRate);
			}
			waveLimit += waveLimitAddition;
			enemyLimit += enemyLimitAddition;
			spawnRate -= spawnRateReduction;
			waveRate -= waveRateReduction;
		}
    }
	
}