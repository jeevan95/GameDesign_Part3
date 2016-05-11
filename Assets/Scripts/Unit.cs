using UnityEngine;

using System.Collections;
using UnityEditor;

public class Unit : MonoBehaviour {

	public float health;
	public float armor;
	public float ammo;

	public float maxAmmo=2000;
	public float maxArmor=100;
	public float maxHealth=100;

	public GameObject bloodDeath;
	public GameObject bloodDamage;
	private bool dead=false;


	void Start(){
		
	}

	void Update(){
			if (health <= 0) {

			if (gameObject.tag == "Player") {
				// game over
				Time.timeScale = 0;

				GetComponent<PlayerController> ().deathText.enabled=true;
				GetComponent<PlayerController> ().respawn.SetActive(true);
				GetComponent<PlayerController> ().exit.SetActive(true);
				if (!dead) {
					GameObject.FindObjectOfType<GameController> ().playerDeath += 1;
					dead = true;
				}
			} else {
				Quaternion rot = Random.rotation;
				rot.x = 0;
				rot.z = 0;
				Vector3 pos = transform.position;
				pos.y = 0.1f;
				GameObject go = Instantiate (bloodDeath, pos, rot) as GameObject;
				Destroy (this.gameObject);

			}
				
			}
		

			

	}


	void OnTriggerEnter (Collider other)
	{
		Bullet bullet = other.GetComponent<Bullet> ();

		if (bullet!=null) {
			Debug.Log ("hit regisrested");
			takeDamage (bullet.damage);
			if (health < 1) {
				if(bullet.owner.GetComponent<PlayerController>()!=null){
					bullet.owner.GetComponent<PlayerController> ().kills += 1;
				}
			}

		}			
	}


	public void takeDamage(float damage){

		health -= ((float)damage)*(1-((float)armor/100));
		health = Mathf.Max (0,health);
		armor -= damage;
		armor = Mathf.Max (0,armor);

		Quaternion rot = Random.rotation;
		rot.x = 0;
		rot.z = 0;
		Vector3 pos = transform.position;
		pos.y = 0.1f;
		GameObject go = Instantiate (bloodDamage, pos, rot) as GameObject;

	}

}
