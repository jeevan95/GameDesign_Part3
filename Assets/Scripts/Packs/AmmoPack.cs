using UnityEngine;
using System.Collections;

public class AmmoPack : MonoBehaviour
{
	public int ammo;
	void OnTriggerEnter (Collider other)
	{
		if(other.gameObject.tag == "Player"){
			Unit unit = other.GetComponent<Unit> ();

			if (unit != null) {
				unit.ammo += ammo;
				unit.ammo = Mathf.Min (unit.ammo,unit.maxAmmo);
				Destroy (this.gameObject);
			}
		}


	}


}
