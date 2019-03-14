using UnityEngine;
using System.Collections;

public class RepeatingBackground : MonoBehaviour
{
    public Camera cam;
    public Renderer myRenderer;
    float bgHeight;
    Vector3 targetPosition;
    private void Start()
    {
        myRenderer = GetComponent<Renderer>();
        if (cam == null) //set the camera up if there is none set
            cam = Camera.main;
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f); //Find the upper corner on the screen
        targetPosition = cam.ScreenToWorldPoint(upperCorner); //Store upper corner as a position
        bgHeight = myRenderer.bounds.extents.y; //Find y length of the background image
        //Note bgHeight is half the length of the sprite (length from center to edge)
        InvokeRepeating("Run", 0.000011f, 0.000011f);//Call run for 0.000011 seconds every 0.000011 seconds
    }
    private void Run()
    {
        if (transform.position.y < -(2*targetPosition.y))//When background goes outside camera view reposition
        {
            RepositionBackground();
        }
    }

    private void RepositionBackground()
    {
        //Reposition background to outside the camera frame
        transform.position = new Vector3 (0.0f, targetPosition.y + bgHeight, 0.0f);
    }
}