using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void FixedUpdate()
    {
        var x = Input.GetAxis("Horizontal") * turnspeed / 60.0f;
        var z = Input.GetAxis("Vertical") * speed / 60.0f;


        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, -z);
    }
}