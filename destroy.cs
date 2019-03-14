using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroy : MonoBehaviour
{
    //When sprite collides with an other sprite that isn't the background destroy the other sprite
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag != "background")
             Destroy(other.gameObject);
    }
}