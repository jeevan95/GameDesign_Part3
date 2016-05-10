using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

    public GameObject target; //the enemy's target
    public float moveSpeed = 5; //move speed
    public float rotationSpeed = 5; //speed of turning
    private Rigidbody rb;


    public GameObject projectile;
    public float projectileSpeed;
    public float shootradius;
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
//        transform.position = Vector3.MoveTowards(transform.position, nn, moveSpeed * Time.deltaTime);
		rb.velocity = (target.transform.position - transform.position).normalized * moveSpeed;


        transform.position = new Vector3 (transform.position.x,0.5f,transform.position.z);

        if (Mathf.Abs((target.transform.position - transform.position).x) < shootradius && Mathf.Abs((target.transform.position - transform.position).z) < shootradius)
        {
            int e = Random.Range(1, 40);
            if (e < 5)
            {
                Vector3 childpos = new Vector3(transform.GetChild(0).position.x, 0.5f, transform.GetChild(0).position.z);
                GameObject go = Instantiate(projectile, childpos, transform.rotation) as GameObject;
                go.GetComponent<Rigidbody>().AddForce(transform.right * projectileSpeed);

            }
        }

       
    }
}
