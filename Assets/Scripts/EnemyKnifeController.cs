﻿using UnityEngine;
using System.Collections;
using System;

public class EnemyKnifeController : MonoBehaviour {

    public GameObject target; //the enemy's target
    public float moveSpeed = 5; //move speed
    public float rotationSpeed = 5; //speed of turning
	public float knifeDamage = 10;
	public float cooldown = 1f;
	private float actionLast=0;
    public int moveradius;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
		target = GameObject.FindGameObjectWithTag ("Player");
    }


    void Update()
    {
        LookAtAndMoveTowardsTarget();
    }

    void LookAtAndMoveTowardsTarget()
    {
        float deltaY = target.transform.position.z - transform.position.z;
        float deltaX = target.transform.position.x - transform.position.x;
        float angleInDegrees = Mathf.Atan2(deltaY, deltaX) * 180 / Mathf.PI;
        transform.eulerAngles = new Vector3(90, -angleInDegrees, 0);

        if (Mathf.Abs((target.transform.position - transform.position).x) < moveradius && Mathf.Abs((target.transform.position - transform.position).z) < moveradius)
        {
            //     



            //  transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(target.transform.position - transform.position), rotationSpeed * Time.deltaTime);
            //Vector3 nn = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);

            //move towards the player
            //        transform.position = Vector3.MoveTowards(transform.position, nn, moveSpeed * Time.deltaTime);
            rb.velocity = (target.transform.position - transform.position).normalized * moveSpeed;
        }
        else
        {
            rb.velocity = Vector3.zero;

        }

        transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);

    }
    void OnCollisionStay(Collision collision) {

		if(collision.gameObject == target && cooldownReady()){
			target.GetComponent<Unit> ().takeDamage (knifeDamage);
			actionLast = Time.time;
		}

	}

	bool cooldownReady(){
		return ((Time.time-actionLast)>cooldown);
	}

}
