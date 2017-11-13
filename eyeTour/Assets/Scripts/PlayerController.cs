using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public float turnspeed = 150.0f;
    private float curTurnSpeed = 0.0f;
    public float moveSpeed = 0.25f;

    private Vector3 targetPosition;

    public float smoothFactor = 5.0f;
    public float acclerationForward = 0.001f;



    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    /*void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * turnspeed;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * speed;


        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, -z);
    }*/

    Vector2 smoothedView;
    Vector2 userGaze;
    //private float desiredRotation = 90.0f;
    //public float damping = 10.0f;


    void FixedUpdate()
    {
        var x = Input.GetAxis("Horizontal") * turnspeed / 60.0f;
        var z = Input.GetAxis("Vertical") * speed / 60.0f;


        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if (TobiiAPI.GetUserPresence() == UserPresence.NotPresent)
            userGaze = new Vector2(Screen.width / 2.0f, Screen.height / 2.0f);

        else if (TobiiAPI.GetUserPresence() == UserPresence.Present)
            userGaze = TobiiAPI.GetGazePoint().Screen;
        else if (Input.GetMouseButton(2)) // if the middle mouse button is held down, use the mouse to simulate the eye tracker
            userGaze = Input.mousePosition;
        
        smoothedView = Vector2.Lerp(smoothedView, userGaze, 0.5f); //Smooths gaze points 

        curTurnSpeed = 0.0f;
        Debug.Log(userGaze); //Outputs where the user is looking in x,y px to console

        if (userGaze.x <= Screen.width / 8.0f || userGaze.x >= Screen.width - 200) //Check for left most and right most boundary of screen
        {
            curTurnSpeed += turnspeed / 7.0f;

            if (curTurnSpeed > turnspeed)
                curTurnSpeed = turnspeed;

            float angleFactor = (userGaze.x <= Screen.width / 8) ? -1.0f : 1.0f;

            Quaternion r1 = Quaternion.AngleAxis(angleFactor * curTurnSpeed * Time.deltaTime, Vector3.up);
            //transform.Rotate(0, -curTurnSpeed * Time.deltaTime, 0); // Using the transform.rotate instead of quaternions

            transform.rotation = r1 * transform.rotation;
            //var desiredRotationQ = Quaternion.Euler(0, transform.rotation.y + desiredRotation, 0);
            //var desiredRotationQ = Quaternion.AngleAxis(, Vector3.up Time.deltaTime * speed);

            //transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotationQ, Time.deltaTime * damping);

            // transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotationQ , Time.time * damping);
        }
        else
            curTurnSpeed = 0.0f;

        if (userGaze.y <= Screen.height / 4.0f)
        {   // Comments are attempts to smooth translation
            //targetPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.25f);

            //Vector3 lerpMovement = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothFactor);

            //transform.Translate(lerpMovement);
                
            transform.Translate(Vector3.forward * Time.deltaTime, Space.Self); // Best implementation So far

            //transform.Translate((Vector3.SmoothDamp(transform.position, targetPosition, )))
        }
        

        //Debug.Log("test");
        //Debug.Log(smoothedView);


    }
}