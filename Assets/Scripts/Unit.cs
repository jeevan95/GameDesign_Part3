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

	void Update(){
			if (health <= 0) {

			if (gameObject.tag == "Player") {
				// game over
				Time.timeScale = 0;

				GetComponent<PlayerController> ().deathText.enabled=true;
				GetComponent<PlayerController> ().respawn.SetActive(true);
				GetComponent<PlayerController> ().exit.SetActive(true);
			} else {
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


		}			
	}


	public void takeDamage(float damage){

		health -= ((float)damage)*(1-((float)armor/100));
		health = Mathf.Max (0,health);
		armor -= damage;
		armor = Mathf.Max (0,armor);

	}

}
