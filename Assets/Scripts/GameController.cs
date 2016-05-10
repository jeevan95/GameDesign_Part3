using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject enemy;
	public GameObject healthPack;
	public GameObject ammoPack;
	public GameObject armorPack;

	public int enemyCount;
	public int enemiesNeeded;

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



	void Start () {
	


	}

	void Update () {
	

		SpawnEnemies ();
		SpawnPacks ();


	}

	void SpawnPacks(){
		int healthPackCount = GameObject.FindObjectsOfType<HealthPack>().Length;
		int ammoPackCount = GameObject.FindObjectsOfType<AmmoPack>().Length;
		int armorPackCount = GameObject.FindObjectsOfType<ArmorPack>().Length;

		if (healthPackCount < healthPacksNeeded) {
			GameObject e = Instantiate (healthPack,getRandomSpawn() , transform.rotation) as GameObject;
		}


		if (ammoPackCount < ammoPacksNeeded) {
			GameObject e = Instantiate (ammoPack,getRandomSpawn() , transform.rotation) as GameObject;
		}


		if (armorPackCount< armorPacksNeeded){
			GameObject e = Instantiate (armorPack,getRandomSpawn() , transform.rotation) as GameObject;
			
		}

	}


	void SpawnEnemies(){
		enemyCount = GameObject.FindGameObjectsWithTag ("Enemy").Length;

		if (enemiesNeeded != enemyCount) {
			float randomX = Random.Range (spawnXMin, spawnXMax);
			float randomY = Random.Range (spawnYMin, spawnYMax);

			Vector3 spawn = new Vector3 (randomX, 0.5f, randomY);

			GameObject e = Instantiate (enemy,spawn , transform.rotation) as GameObject;

		}


	}

	Vector3 getRandomSpawn(){
		float randomX = Random.Range (spawnXMin, spawnXMax);
		float randomY = Random.Range (spawnYMin, spawnYMax);
		return new Vector3 (randomX, 0.5f, randomY);
	}

}


