using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	GameObject player;

	public int offsetX;
	public int offsetY;
	public int offsetZ;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = player.transform.position;
		newPos.x += offsetX;
		newPos.y += offsetY;
		newPos.z += offsetZ;
		transform.position = newPos;

	}
}
