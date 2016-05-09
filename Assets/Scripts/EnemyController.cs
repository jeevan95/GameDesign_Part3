using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public GameObject target; //the enemy's target
    public float moveSpeed = 5; //move speed
    public float rotationSpeed = 5; //speed of turning
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
		target = GameObject.FindGameObjectWithTag ("Player");
    }


    void Update()
    {
        //rotate to look at the player

        float deltaY = target.transform.position.z - transform.position.z;
        float deltaX = target.transform.position.x - transform.position.x;
        float angleInDegrees = Mathf.Atan2(deltaY, deltaX) * 180 / Mathf.PI;
        transform.eulerAngles = new Vector3(90, -angleInDegrees, 0);



        //  transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), rotationSpeed * Time.deltaTime);
        Vector3 nn = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);

        //move towards the player
        transform.position = Vector3.MoveTowards(transform.position, nn, moveSpeed * Time.deltaTime);


		transform.position = new Vector3 (transform.position.x,0.5f,transform.position.z);

    }
}
