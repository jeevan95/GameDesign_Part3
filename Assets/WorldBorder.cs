using UnityEngine;
using System.Collections;

public class WorldBorder : MonoBehaviour {


		
		void OnTriggerExit(Collider other) {		
			Destroy(other.gameObject);
		}

//		void OnTriggerEnter(Collider other) {
//			Destroy(other.gameObject);
//		}
//
//


}
