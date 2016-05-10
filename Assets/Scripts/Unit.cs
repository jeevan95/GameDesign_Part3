using UnityEngine;

using System.Collections;
using UnityEditor;

public class Unit : MonoBehaviour {

	public int health;
	public int armor;
	public int ammo;

	public int maxAmmo=2000;
	public int maxArmor=100;
	public int maxHealth=100;

	void Update(){
			if (health <= 0) {
				Destroy (this.gameObject);
			}
		

			

	}


	void OnTriggerEnter (Collider other)
	{
		Bullet bullet = other.GetComponent<Bullet> ();

		if (bullet!=null) {
			Debug.Log ("hit regisrested");
			health -= bullet.damage*(1-armor/100);
			health = Mathf.Max (0,health);
			armor -= bullet.damage;
			armor = Mathf.Max (0,armor);
			Destroy (other.gameObject);
		}



	}

}
