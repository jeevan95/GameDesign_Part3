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

	int randomSpeed=5;



	public float ammoPerLevel=5;
	public int ammoPacksPerLevel=4;

	public bool respawn = false;

	public int healthPacksPerLevel=4;
	public float healthPerLevel=100;

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

			float hps = healthPerLevel / healthPacksPerLevel;
			Debug.Log ("HPS: " + hps);
//			healthPerLevel -= hps;
			e.GetComponent<HealthPack> ().HP = (int)hps;
			healthPacksNeeded -= 1;
		}


		if ( ammoPacksNeeded>0) {
			Debug.Log (ammoPacksNeeded);
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
			e.GetComponent<EnemyController>().moveSpeed = Random.Range(3, randomSpeed);
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
		ammoPacksPerLevel = ammoPacksPerLevel+ammoInc;
		ammoPacksNeeded = ammoPacksPerLevel;

		healthPacksPerLevel = healthPacksPerLevel + hpInc;
		healthPacksPerLevel = Mathf.Max (0,healthPacksPerLevel);
		healthPacksNeeded = healthPacksPerLevel;




//		Debug.Log ("SETTTTTTTT:" + ammoPacksPerLevel);
	}

	public void nextLevel(){


		level += 1;
		if(randomSpeed<15){
			randomSpeed += 2;
		}

		if (spawnXMax < 45f)
		{
			spawnXMax+=5;
			spawnXMin-= 5;
			spawnYMax+=5;
			spawnYMin-=5;

		}

		setEnemiesNumber (1,1);
		ammoPerLevel = (gunEnemiesNeeded+knifeEnemiesNeeded) / accuracyLast;

		healthPerLevel *= 0.6f;

		setPacks (-1,-1,0);
	}


	float accuracyLast=1f;

	public void playerDead(Unit unit){
		playerDeath += 1;

		PlayerController pc = unit.gameObject.GetComponent<PlayerController> ();
		Debug.Log (pc.accuracy + " " + accuracyLast);
		if (pc.accuracy == 0 || pc.accuracy != pc.accuracy) {
			pc.accuracy = 0.01f;
		}
		accuracyLast = (accuracyLast + pc.accuracy) / 2;
		Debug.Log ("Accuracy last: " + accuracyLast);
		float ammoPotential = accuracyLast * unit.ammo; //#kills

		int ammosLeftTotal = 0;
		if (GameObject.FindObjectsOfType<AmmoPack> ().Length > 0) {
			int ammosInPack = GameObject.FindObjectOfType<AmmoPack> ().ammo;
			ammosLeftTotal = ammosInPack * GameObject.FindObjectsOfType<AmmoPack> ().Length;
		}

		float ammoPotentialGlobal = accuracyLast * (unit.ammo + (ammosLeftTotal) );
		int enemyCount = GameObject.FindGameObjectsWithTag ("GunEnemy").Length + GameObject.FindGameObjectsWithTag ("KnifeEnemy").Length;
		if (ammoPotentialGlobal < enemyCount) {
			// need more ammo	
			ammoPerLevel = (knifeEnemiesLast+gunEnemiesLast) / accuracyLast;
			Debug.Log ("AMMOS NEXT LEVEL:" + ammoPerLevel);
//			setPacks (0,1,0);
			setPacks (0,0,0);	
		}

		if (ammoPotential < enemyCount  ){
			setPacks (0,1,0);			
		}

		int hpacksLeft = GameObject.FindObjectsOfType<HealthPack> ().Length;

		if (hpacksLeft>0){			
			setPacks (1,0,0);
			healthPerLevel *= 1.2f;
		}



		setEnemiesNumber (0,0);


		SceneManager.LoadScene ("Main");

	}
}


