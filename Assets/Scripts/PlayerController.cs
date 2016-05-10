using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class PlayerController : MonoBehaviour
{

	public Slider healthBarSlider;
	public Slider armorBarSlider;
	public Text ammoText;

	public float moveSpeed;
	Rigidbody rb;

	public GameObject projectile;
	public float projectileSpeed;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update ()
	{		

		LookAt ();
		Move ();
		Shoot ();

//		Debug.Log (rb.velocity);


		healthBarSlider.value = GetComponent<Unit> ().health;
		armorBarSlider.value = GetComponent<Unit> ().armor;
		ammoText.text = "AMMO: " + GetComponent<Unit> ().ammo;
	}

	void FixedUpdate(){
		
		rb.angularVelocity *= 0.9f;
	}

	void LookAt ()
	{

		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		mousePos.y = transform.position.y;

		float deltaY = mousePos.z - transform.position.z;
		float deltaX = mousePos.x - transform.position.x;
		float angleInDegrees = Mathf.Atan2 (deltaY, deltaX) * 180 / Mathf.PI;
		transform.eulerAngles = new Vector3 (90, -angleInDegrees, 0);
	}


	void Move ()
	{		
		
		float inputHorizontal = Input.GetAxis ("Horizontal");
		float inputVertical = Input.GetAxis ("Vertical");
		Vector3 newVelocity = new Vector3 (inputVertical * moveSpeed, 0f, -inputHorizontal * moveSpeed);
		rb.velocity = newVelocity;

		transform.position = new Vector3 (transform.position.x,0.5f,transform.position.z);

	}


	void Shoot ()
	{
		
		if (Input.GetButtonUp ("Fire1") && GetComponent<Unit>().ammo>0) {
			GetComponent<Unit> ().ammo -= 1;
			GameObject go = Instantiate (projectile, transform.GetChild (0).position, transform.rotation) as GameObject;
			go.GetComponent<Rigidbody> ().AddForce (transform.right * projectileSpeed);
		}
			
	}





}
