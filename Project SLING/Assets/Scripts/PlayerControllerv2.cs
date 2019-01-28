using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerv2 : MonoBehaviour {

	public Joystick firingJoystick;
	private RaycastHit hit, hitInfo,turnTest;
	private AudioClip gunSound;
	private ParticleSystem flash;
	private Rigidbody RGB;
	private GameObject impactEffect;
	private Vector3 moveVector;
	private float assignedMoveSpeed;
	public Follower followScript;
	public LayerMask layerMask;
	[HideInInspector] public float duration = 0.01f, magnitude = 0.005f;
	[HideInInspector] public float fireRange, fireDamage, fireRate, nexTimeToFire;

	[Header ("Player")]
	public GameObject turret;
	public GameObject currentGun;
	public GameObject hitEffect;
	public float time,sensitivity, moveSpeed, speedLimit, angularOffset, turnAngle, turnLimit;
	[Header ("Powerups")]
	public BoxCollider extraPowerupTrigger;
	public AudioClip healthPickupSound;
	public AudioClip ragePickupSound;
	public AudioClip soulPickupSound;
	public int healthPickupAmount;
	public float ragePickupDuration;
	[Header ("Rifle")]
	public AudioClip rifleSound;
	public ParticleSystem rifleEffect;
	public GameObject rifle, rifleImpactEffect;
	public float rifleRate, rifleDamage, rifleRange, rifleShake, rifleWeightFactor;
	[Header ("Shotgun")]
	public AudioClip shotgunSound;
	public ParticleSystem shotgunEffect;
	public GameObject shotgun, shotgunImpactEffect;
	public float shotgunRate, shotgunDamage, shotgunRange, shotgunShake, shotgunWeightFactor;
	[Header ("Rocket")]
	public AudioClip rocketSound;
	public ParticleSystem rocketEffect;
	public GameObject rocket, rocketImpactEffect;
	public float rocketRate, rocketDamage, rocketRange, rocketShake, rocketWeightFactor;

	void Start () {
		RGB = GetComponent<Rigidbody> ();
		Master.instance.soulsCollected.text = PlayerPrefs.GetInt ("Souls").ToString ();
		fireRate = rifleRate;
		fireRange = rifleRange;
		fireDamage = rifleDamage;
		flash = rifleEffect;
		impactEffect = rifleImpactEffect;
		assignedMoveSpeed = moveSpeed;
	}

	void FixedUpdate () {
		moveVector = (Vector3.right * firingJoystick.Horizontal + Vector3.forward * firingJoystick.Vertical);
		if (moveVector != Vector3.zero) {
			//Debug.Log(moveVector);
			turret.transform.rotation = Quaternion.AngleAxis (angularOffset, Vector3.up) * Quaternion.LookRotation (moveVector);
			if(RGB.velocity.magnitude < speedLimit){
				RGB.AddForce (turret.transform.forward * moveSpeed);
			}
			Shoot ();
		}
	}

	void OnTriggerEnter (Collider other) {
        extraPowerupTrigger.enabled = false;
		if (other.gameObject.tag == "Finish") {
			InstaKill ();
		}
		else if (other.gameObject.tag == "HealthPowerUp") {
			SoundManager.instance.PlaySingle(healthPickupSound);
			HealthDirect(other);
		}
		else if (other.gameObject.tag == "RagePowerUp") {
			SoundManager.instance.PlaySingle(ragePickupSound);
			TempRageDirect();
			Destroy(other.gameObject);
		}
		else if (other.gameObject.tag == "Soul") {
			SoundManager.instance.PlaySingle(soulPickupSound);
			PlayerPrefs.SetInt ("Souls", (PlayerPrefs.GetInt ("Souls") + 1));
			Master.instance.soulsCollected.text = PlayerPrefs.GetInt ("Souls").ToString ();
			Destroy(other.gameObject);
		}
	}

	void TempRageDirect(){
        Health PH = GetComponent<Health> ();
		if(!PH.inTempRage){
		    PH.TempRage(ragePickupDuration);
		}
	}

	void HealthDirect(Collider other){
		Health PH = GetComponent<Health> ();
		if( (PH.currentHealth + healthPickupAmount > PH.rageLimit) && (PH.currentHealth + healthPickupAmount < 100) ){
			PH.currentHealth += healthPickupAmount;
			PH.RageReset();
		}
		else{
			//PH.currentHealth += healthPickupAmount;
			PH.currentHealth = 100;
		}
		if(PH.currentHealth <= 100){
			Destroy(other.gameObject);
		}
		PH.healthBar.value = PH.currentHealth;
	}

	void InstaKill () {
		Health PH = GetComponent<Health> ();
		PH.TakeDamage (100);
	}

	public void ChangeWeapon (int weaponNo) {
		moveSpeed = assignedMoveSpeed;
		currentGun.SetActive (false);
		if ((weaponNo == 0) || (weaponNo == 3)) {
			fireRate = rifleRate;
			fireRange = rifleRange;
			fireDamage = rifleDamage;
			moveSpeed *= rifleWeightFactor;
			magnitude = rifleShake;
			flash = rifleEffect;
			impactEffect = rifleImpactEffect;
			currentGun = rifle;
			gunSound = rifleSound;
			rifle.SetActive (true);
		} else if (weaponNo == 1) {
			fireRate = shotgunRate;
			fireRange = shotgunRange;
			fireDamage = shotgunDamage;
			moveSpeed *= shotgunWeightFactor;
			magnitude = shotgunShake;
			flash = shotgunEffect;
			impactEffect = shotgunImpactEffect;
			currentGun = shotgun;
			gunSound = shotgunSound;
			shotgun.SetActive (true);
		} else if (weaponNo == 2) {
			fireRate = rocketRate;
			fireRange = rocketRange;
			fireDamage = rocketDamage;
			moveSpeed *= rocketWeightFactor;
			magnitude = rocketShake;
			flash = rocketEffect;
			impactEffect = rocketImpactEffect;
			currentGun = rocket;
			gunSound = rocketSound;
			rocket.SetActive (true);
		}
	}

	void Shoot () {
		if (Physics.Raycast (turret.transform.position, turret.transform.forward, out hit, fireRange, layerMask) && (hit.transform.tag != "Enemy") && (Time.time >= nexTimeToFire)) {
			//flash.Play ();
			//Debug.Log(fireRate+" | "+fireRange+" | "+fireDamage);
			nexTimeToFire = Time.time + 1f / fireRate;
			//SoundManager.instance.PlaySingle(gunSound);
			//GameObject instimpactEffect = Instantiate (impactEffect, hit.point, Quaternion.LookRotation (hit.normal));
			//Destroy (instimpactEffect, 1f);
		} else if (Physics.Raycast (turret.transform.position, turret.transform.forward, out hit, fireRange, layerMask) && (hit.transform.tag == "Enemy") && (Time.time >= nexTimeToFire)) {
			nexTimeToFire = Time.time + 1f / fireRate;
			//Debug.Log("Hit enemy");
			StartCoroutine (FireRate ());
		}
	}

	IEnumerator FireRate () {
		flash.Play ();
		EnemyHealth EH = hit.transform.GetComponent<EnemyHealth> ();
		EH.TakeDamage ((int) fireDamage);
		GameObject instimpactEffect = Instantiate (hitEffect, hit.point, Quaternion.LookRotation (hit.normal));
		Destroy (instimpactEffect, 1f);
		SoundManager.instance.PlaySingle(gunSound);
		yield return null;
	}

}