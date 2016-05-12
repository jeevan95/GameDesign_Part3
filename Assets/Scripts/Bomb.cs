using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class Bomb : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(Collider other) {
		Debug.Log ("sdds");
		if(other.gameObject.tag=="Player"){
			GameObject.FindObjectOfType<GameController> ().nextLevel ();
			SceneManager.LoadScene ("Main");
		}

	}

}
