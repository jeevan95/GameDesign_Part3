using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float moveSpeed;
	Rigidbody rb;

	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {		

		LookAt ();
		Move ();
	}


	void LookAt(){

		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos.y = transform.position.y;

		float deltaY = mousePos.z - transform.position.z;
		float deltaX = mousePos.x - transform.position.x;
		float angleInDegrees = Mathf.Atan2 (deltaY, deltaX) * 180 / Mathf.PI;
		transform.eulerAngles = new Vector3 (90, -angleInDegrees, 0);
	}


	void Move(){		
		float inputHorizontal = Input.GetAxis ("Horizontal");
		float inputVertical = Input.GetAxis ("Vertical");
		//	float speedY = inputVertical > 0.1 ? Mathf.Clamp ((inputVertical * moveSpeed), moveSpeed / 2.0f, moveSpeed) : 0.0f;
		//float speedX = inputHorizontal > 0.1 ? Mathf.Clamp ((inputHorizontal * moveSpeed), moveSpeed / 2.0f, moveSpeed) : 0.0f;
		Vector3 newVelocity=new Vector3(inputVertical*moveSpeed, 0.0f, inputHorizontal*-moveSpeed);
		rb.velocity = newVelocity;

	}
}
