using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject enemy;

	public int enemyCount;
	public int enemiesNeeded;

	public float spawnXMin;
	public float spawnXMax;
	public float spawnYMin;
	public float spawnYMax;



	void Start () {
	


	}

	void Update () {
	
		enemyCount = GameObject.FindGameObjectsWithTag ("Enemy").Length;

		if (enemiesNeeded != enemyCount) {
			float randomX = Random.Range (spawnXMin, spawnXMax);
			float randomY = Random.Range (spawnYMin, spawnYMax);

			Vector3 spawn = new Vector3 (randomX, 0.5f, randomY);

			GameObject e = Instantiate (enemy,spawn , transform.rotation) as GameObject;

		}


	}
}
