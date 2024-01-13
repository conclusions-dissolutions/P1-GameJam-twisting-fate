using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player movement script.
/// 
/// Authors: Danieyll Wilson
/// Created: Jan 07, 2024
/// Last Modified: Jan 12, 2024
/// </summary>
public class PlayerMMovement : MonoBehaviour
{
    /// <summary>
    /// Changable speed for the player var.
    /// </summary>
    public float speed = 0.1f;
    /// <summary>
    ///  Changable jump speed for the player var.
    /// </summary>
    public float jumpSpeed = 10f;
    /// <summary>
    /// Changable default mass for rigidbody component var.
    /// </summary>
    public float defaultRigidbodyMass = 1f;
    
    /// <summary>
    /// Rigidbody2D var.
    /// </summary>
    private Rigidbody2D rb;
    /// <summary>
    /// Vector3 of the start pos var.
    /// </summary>
    private Vector3 startDest;
    /// <summary>
    /// Vector3 of the end pos var.
    /// </summary>
    private Vector3 endDest;
    /// <summary>
    /// Start time var.
    /// </summary>
    private float startTime;
    /// <summary>
    /// Length from start to end vars var.
    /// </summary>
    private float journeyLength;
    /// <summary>
    /// Boolean to check if the left click is held down.
    /// </summary>
    private bool leftClickedDown;
    /// <summary>
    /// Boolean to check if player is on the ground/platform
    /// </summary>
    private bool isOnGround;

    /// <summary>
    /// Start is called before the first frame update.
    /// </summary>
    void Start()
    {
        //Grabs the time before the first frame and stores it into startTime.
        startTime = Time.time;
        //Player should start on the ground/platform.
        isOnGround = true;

        //Sets up rigidbody for the player, such as lock their rotation and sets their default mass.
        rb = GetComponent<Rigidbody2D>();
        rb.mass = defaultRigidbodyMass;
        rb.freezeRotation = true;

    }

    /// <summary>
    /// Update is called once per frame.
    /// TODO: Add lockout when dialog screen is up.
    /// </summary>
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

        //If the right button (rmb, fire2) is pressed down, enable jumping.
        if(Input.GetButtonDown("Fire2") && isOnGround)
        {
           rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
           isOnGround = false;
        }
    }
    /// <summary>
    /// On 2D collision enter, If the collision is with an gameobject with the tag "LandScape", set isOnGround to true.
    /// </summary>
    /// <param name="collision"> Gameobject that the player collided with </param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("LandScape"))
        {
            isOnGround = true;
        }
    }
}
