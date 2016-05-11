using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {



	public GameObject gunEnemy;
	public GameObject knifeEnemy;
	public GameObject healthPack;
	public GameObject ammoPack;
	public GameObject armorPack;

	public int gunEnemyCount;
	public int gunEnemiesNeeded;

	public int knifeEnemyCount;
	public int knifeEnemiesNeeded;

	public int healthPackCount;
	public int ammoPackCount;
	public int armorPackCount;

	public int healthPacksNeeded;
	public int ammoPacksNeeded;
	public int armorPacksNeeded;


	public float spawnXMin;
	public float spawnXMax;
	public float spawnYMin;
	public float spawnYMax;

	public int playerDeath = 0;
	public float playerAccuracy;


	void Start () {

		GameController gc = GameObject.FindObjectOfType<GameController> ();
		if (gc != null && gc!=this) {			
			Destroy (this);			
		} else {
			DontDestroyOnLoad (this.gameObject);
		}




	}

	void Update () {
	
		Debug.Log (playerDeath);
		SpawnEnemies ();
		SpawnPacks ();


	}

	void SpawnPacks(){
		int healthPackCount = GameObject.FindObjectsOfType<HealthPack>().Length;
		int ammoPackCount = GameObject.FindObjectsOfType<AmmoPack>().Length;
		int armorPackCount = GameObject.FindObjectsOfType<ArmorPack>().Length;

		if (healthPackCount < healthPacksNeeded) {
			GameObject e = Instantiate (healthPack,getRandomSpawn() , transform.rotation) as GameObject;
			e.GetComponent<HealthPack> ();
		}


		if (ammoPackCount < ammoPacksNeeded) {
			GameObject e = Instantiate (ammoPack,getRandomSpawn() , ammoPack.transform.rotation) as GameObject;
			e.GetComponent<AmmoPack> ();
		}


		if (armorPackCount< armorPacksNeeded){
			GameObject e = Instantiate (armorPack,getRandomSpawn() , armorPack.transform.rotation) as GameObject;
			e.GetComponent<ArmorPack> ();
		}

	}


	void SpawnEnemies(){
		gunEnemyCount = GameObject.FindGameObjectsWithTag ("GunEnemy").Length;
		if (gunEnemiesNeeded != gunEnemyCount) {			
			GameObject e = Instantiate (gunEnemy,getRandomSpawn(), transform.rotation) as GameObject;
		}


		knifeEnemyCount = GameObject.FindGameObjectsWithTag ("KnifeEnemy").Length;
		if (knifeEnemiesNeeded != knifeEnemyCount) {			
			GameObject e = Instantiate (knifeEnemy,getRandomSpawn(), transform.rotation) as GameObject;
		}


	}

	Vector3 getRandomSpawn(){
		float randomX = Random.Range (spawnXMin, spawnXMax);
		float randomY = Random.Range (spawnYMin, spawnYMax);
		return new Vector3 (randomX, 0.5f, randomY);
	}

}


