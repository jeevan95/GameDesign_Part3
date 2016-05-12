using UnityEngine;
using System.Collections;

public class HealthPack : MonoBehaviour
{
	
	public int HP;
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player") {
			
			Unit unit = other.GetComponent<Unit> ();

			if (unit != null) {
				unit.health += HP;
				unit.health = Mathf.Min (unit.health, unit.maxHealth);
				Destroy (this.gameObject);
			}

		}

	}
}
