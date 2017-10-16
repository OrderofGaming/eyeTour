using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public float turnspeed = 150.0f;

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
    //private float desiredRotation = 90.0f;
    //public float damping = 10.0f;


    void FixedUpdate()
    {
        var x = Input.GetAxis("Horizontal") * turnspeed / 60.0f;
        var z = -Input.GetAxis("Vertical") * speed / 60.0f;


        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, -z);

        Vector2 userGaze = TobiiAPI.GetGazePoint().Screen;
        smoothedView = Vector2.Lerp(smoothedView, userGaze, 0.5f); //Smooths gaze points 

        Debug.Log(userGaze); //Outputs where the user is looking in x,y px to console

        if (userGaze.x <= Screen.width / 8)
        {

            transform.Rotate(0, -.5f * turnspeed / 60.0f, 0);
            //var desiredRotationQ = Quaternion.Euler(0, transform.rotation.y + desiredRotation, 0);

            //transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotationQ, Time.deltaTime * damping);

            // transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotationQ , Time.time * damping);
        }

        else if (userGaze.x >= Screen.width)
        {
            transform.Rotate(0, .5f * turnspeed / 60.0f, 0);
        }

        //else if (userGaze.x <= 100)
        // {
        //     transform.Rotate(0, userGaze.x, 0);
        // }

        // else if (userGaze.x <= 100)
        // {
        //     transform.Rotate(0, userGaze.x, 0);
        // }

        //Debug.Log("test");
        //Debug.Log(smoothedView);


    }
}