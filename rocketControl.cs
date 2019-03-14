using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class rocketControl : MonoBehaviour {
    public Camera cam;
    public Rigidbody2D myRigidBody;
    private float maxWidth;
    public Renderer myRenderer;
    private bool playing;
    private Vector3 lastTargetPosition;
    private Vector2 filteredPoint;

    // Use this for initialization
    void Start () {
        lastTargetPosition = new Vector3(0, 0, 0);
        playing = false;
        myRenderer = GetComponent<Renderer>();
        myRigidBody = GetComponent<Rigidbody2D>();
        if (cam == null)
            cam = Camera.main;
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        float shipWidth = myRenderer.bounds.extents.x;
        maxWidth = targetWidth.x - shipWidth;
	}
	
	// Update is called once per physics timestep
	void FixedUpdate () {
        if (playing)
        {
            Vector2 gazePoint = TobiiAPI.GetGazePoint().Screen;
            //filteredPoint = Vector2.Lerp(filteredPoint, gazePoint, 0.5f);
            Vector3 rawPosition = cam.ScreenToWorldPoint(gazePoint);//change gazePoint to Input.mousePosition for testing
            Vector3 targetPosition = new Vector3(rawPosition.x, -3.0f, 0.0f);
            float targetWidth = Mathf.Clamp(targetPosition.x, -maxWidth, maxWidth);
            targetPosition = new Vector3(targetWidth, targetPosition.y, targetPosition.z);


            myRigidBody.MovePosition(targetPosition);

            //transform.Rotate(Vector3.forward * 2);
            //Debug.Log((targetPosition.x - lastTargetPosition.x));
            transform.eulerAngles = new Vector3(0, 0, (targetPosition.x - lastTargetPosition.x) * -50);
            lastTargetPosition = targetPosition;
        }

    }
    public void CanControl(bool control)
    {
        if (control)
            playing = true;
        else
            playing = false;
    }
}
