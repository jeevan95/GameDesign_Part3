using UnityEngine;
using System.Collections;

public class ArmorPack : MonoBehaviour
{
	public int armor;
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.tag == "Player") {
			Unit unit = other.GetComponent<Unit> ();

			if (unit != null) {
				unit.armor += armor;
				unit.armor = Mathf.Min (unit.armor, unit.maxArmor);

				Destroy (this.gameObject);
			}

		}
	}

}
