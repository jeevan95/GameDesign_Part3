using UnityEngine;
using System.Collections;

public class WorldBorder : MonoBehaviour {



    void OnTriggerExit(Collider other) {


        if (other.gameObject.tag == "Player")
        {
            // game over
            Time.timeScale = 0;

            other.gameObject.GetComponent<PlayerController>().deathText.enabled = true;
            other.gameObject.GetComponent<PlayerController>().respawn.SetActive(true);
            other.gameObject.GetComponent<PlayerController>().exit.SetActive(true);
        }
        else
        {
            Destroy(other.gameObject);
        }

    }

//		void OnTriggerEnter(Collider other) {
//			Destroy(other.gameObject);
//		}
//
//


}
