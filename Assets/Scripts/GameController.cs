using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class GameController : MonoBehaviour {



	public GameObject gunEnemy;
	public GameObject knifeEnemy;
	public GameObject healthPack;
	public GameObject ammoPack;
	public GameObject armorPack;

	public GameObject bomb;

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

	public int gunEnemiesLast=0;
	public int knifeEnemiesLast=0;

	public float spawnXMin;
	public float spawnXMax;
	public float spawnYMin;
	public float spawnYMax;

	public int playerDeath = 0;
	public float playerAccuracy;
	public int level=0;

	float ammoPerLevel=5;
	float healthPerLevel=100;

	int ammoPacksPerLevel=4;
	int healthPacksPerLevel=4;

	public bool respawn = false;

	void Start () {

		GameController gc = GameObject.FindObjectOfType<GameController> ();
		if (gc != null && gc!=this) {			
			Destroy (this.gameObject);			
		} else {
			DontDestroyOnLoad (this.gameObject);
		}

		GameObject e = Instantiate (bomb,getRandomSpawn() , bomb.transform.rotation) as GameObject;


	}

	void Update () {

		SpawnEnemies ();
		SpawnPacks ();

		respawnPlayer ();


	}


	public void respawnPlayer(){
		if (respawn) {
			playerDead (GameObject.FindGameObjectWithTag("Player").GetComponent<Unit>());


			respawn = false;
		}
	}

	void SpawnPacks(){
		int healthPackCount = GameObject.FindObjectsOfType<HealthPack>().Length;
		int ammoPackCount = GameObject.FindObjectsOfType<AmmoPack>().Length;
		int armorPackCount = GameObject.FindObjectsOfType<ArmorPack>().Length;

		if ( healthPacksNeeded>0) {
			GameObject e = Instantiate (healthPack,getRandomSpawn() , transform.rotation) as GameObject;
			e.GetComponent<HealthPack> ();
			healthPacksNeeded -= 1;
		}


		if ( ammoPacksNeeded>0) {
			GameObject e = Instantiate (ammoPack,getRandomSpawn() , ammoPack.transform.rotation) as GameObject;
			e.GetComponent<AmmoPack> ();
			float ammos = ammoPerLevel / ammoPacksNeeded;
			ammoPerLevel -= ammos;
			e.GetComponent<AmmoPack> ().ammo = (int)ammos;
			ammoPacksNeeded -= 1;
		}


		if (armorPackCount< armorPacksNeeded){
			GameObject e = Instantiate (armorPack,getRandomSpawn() , armorPack.transform.rotation) as GameObject;
			e.GetComponent<ArmorPack> ();
			armorPacksNeeded -= 1;
		}

	}


	void SpawnEnemies(){
		gunEnemyCount = GameObject.FindGameObjectsWithTag ("GunEnemy").Length;
		if (gunEnemiesNeeded >0) {			
			GameObject e = Instantiate (gunEnemy,getRandomSpawn(), transform.rotation) as GameObject;
			gunEnemiesNeeded -= 1;
			gunEnemiesLast += 1;
		}


		knifeEnemyCount = GameObject.FindGameObjectsWithTag ("KnifeEnemy").Length;
		if (knifeEnemiesNeeded>0) {			
			GameObject e = Instantiate (knifeEnemy,getRandomSpawn(), transform.rotation) as GameObject;
			knifeEnemiesNeeded -= 1;
			knifeEnemiesLast += 1;
		}


	}

	Vector3 getRandomSpawn(){
		float randomX = Random.Range (spawnXMin, spawnXMax);
		float randomY = Random.Range (spawnYMin, spawnYMax);
		return new Vector3 (randomX, 0.5f, randomY);
	}



	public void setEnemiesNumber(int gunInc, int knifeInc){
		
		gunEnemiesNeeded = gunEnemiesLast+gunInc;
		knifeEnemiesNeeded = knifeEnemiesLast+knifeInc;

		gunEnemiesLast = 0;
		knifeEnemiesLast = 0;

	}

	void setPacks(int hpInc, int ammoInc, int armorInc){
		ammoPacksNeeded = ammoPacksPerLevel+ammoInc;
		healthPacksNeeded = healthPacksPerLevel+hpInc;

	}

	public void nextLevel(){
		level += 1;
		setEnemiesNumber (1,1);
		setPacks (0,0,0);
	}

	public void playerDead(Unit unit){
		playerDeath += 1;

		PlayerController pc = unit.gameObject.GetComponent<PlayerController> ();

		float ammoPotential = pc.accuracy * unit.ammo; //#kills

		int enemyCount = gunEnemyCount + knifeEnemyCount;
		if (ammoPotential<enemyCount){
			// need more ammo
			ammoPerLevel = enemyCount/pc.accuracy;
		}

		setPacks (0,0,0);
		setEnemiesNumber (0,0);

		SceneManager.LoadScene ("Main");
//		SceneManager.LoadScene ("Main");

	}
}


