using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starController : MonoBehaviour {
    public Rigidbody2D myRigidBody;
    public float speed;
	// Use this for initialization
	void Start () {
        myRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per physics time isntance
	void FixedUpdate () {
        //update the speed
        myRigidBody.velocity = new Vector3(0, -speed , 0);
	}
    // Function used to set the speed of the falling stars
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
