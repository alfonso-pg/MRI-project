using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionControl : MonoBehaviour
{
    public GameObject explosion;

    void OnTriggerEnter2D(Collider2D collision)
    {
        //Trigger explosion animation everytime there is a collision with the rocket
        if (collision.gameObject.tag == "rocket")
        {
            Instantiate(explosion, transform.position, transform.rotation);//Trigger explosion
            Destroy(gameObject);//Destroy the explosion
        }
    }
}
