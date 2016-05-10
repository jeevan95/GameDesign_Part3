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
		if (tag == "Enemy") {
			if (health <= 0) {
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
			Destroy (other.gameObject);
		}			
	}


	public void takeDamage(float damage){

		health -= ((float)damage)*(1-((float)armor/100));
		health = Mathf.Max (0,health);
		armor -= damage;
		armor = Mathf.Max (0,armor);

	}

}
