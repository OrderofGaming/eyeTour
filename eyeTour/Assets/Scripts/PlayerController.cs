using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Gaming;

public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public float turnspeed = 150.0f;
    private float curTurnSpeed = 0.0f;

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

        curTurnSpeed = 0.0f;

        Debug.Log(userGaze); //Outputs where the user is looking in x,y px to console

        if (userGaze.x <= Screen.width / 8 || userGaze.x >= Screen.width)
        {
            curTurnSpeed += turnspeed / 5.0f;

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

        //Debug.Log("test");
        //Debug.Log(smoothedView);


    }
}