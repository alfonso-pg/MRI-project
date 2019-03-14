using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class meteoriteController : MonoBehaviour {

    public Rigidbody2D myRigidBody;
    public float speed;
    // Use this for initialization
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();

    }
    //Called once per physics time instance
    void FixedUpdate()
    {
        myRigidBody.velocity = new Vector3(0, -speed, 0); // Set the meteorite velocity
    }
    //Used to set the speed variable from another script
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
}
