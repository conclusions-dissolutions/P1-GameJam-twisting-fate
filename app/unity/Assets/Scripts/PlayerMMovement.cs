using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMMovement : MonoBehaviour
{
    //Changable speed for the player var.
    public float speed = 0.1f;

    //Vector3 of the start pos var.
    private Vector3 startDest;
    //Vector3 of the end pos var.
    private Vector3 endDest;
    //Start time var.
    private float startTime;
    //Length from start to end vars var.
    private float journeyLength;
    //Boolean to check if the left click is held down.
    private bool leftClickedDown;

    // Start is called before the first frame update
    void Start()
    {
        //Grabs the time before the first frame and stores it into startTime.
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //If the left mouse button (lmb, Fire1) is pressed down, leftClickedDown is true, else false.
        if(Input.GetButtonDown("Fire1"))
        {
            leftClickedDown = true;
        }
        else if(Input.GetButtonUp("Fire1"))
        {
            leftClickedDown = false;
        }

        //Triggers event when Left mouse click
        if(leftClickedDown)
        {
            //Grab player current position and stores it into startDest var.
            startDest = transform.position;

            //Gets mouse cursor position.
            Vector3 mousePos = Input.mousePosition;
            //Creates new position from converting screen space point from camera plane.
            endDest = Camera.main.ScreenToWorldPoint(new Vector3 (mousePos.x, mousePos.y, Camera.main.nearClipPlane));
            //Locks the player y and z axis, only changing the x axis.
            endDest = new Vector3(endDest.x, startDest.y, startDest.z);

            //Finds the distance from startDest to endDest and uses speed to create a fraction of that journey.
            journeyLength = Vector3.Distance(startDest, endDest);
            float fractionOfJourney = (speed / journeyLength);
            
            //Sets new position for player using Lerp.
            transform.position = Vector3.Lerp(startDest, endDest, fractionOfJourney);
        }
    }
}
