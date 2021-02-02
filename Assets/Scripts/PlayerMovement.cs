using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] 
    private float speed = 10;

    [SerializeField] 
    private float rotationSpeed = 10;

    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        //rb.velocity = transform.forward * inputVertical * speed;
        rb.AddForce(transform.forward * inputVertical * speed);

        transform.RotateAround(transform.position, Vector3.up, inputHorizontal * rotationSpeed);
    }
}
