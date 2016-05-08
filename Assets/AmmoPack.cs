using UnityEngine;
using System.Collections;

public class AmmoPack : MonoBehaviour
{
	public int ammo;
	void OnTriggerEnter (Collider other)
	{

		Unit unit = other.GetComponent<Unit> ();

		if (unit != null) {
			unit.ammo += ammo;
			unit.ammo = Mathf.Min (unit.ammo,unit.maxAmmo);
			Destroy (this.gameObject);
		}

	}


}
