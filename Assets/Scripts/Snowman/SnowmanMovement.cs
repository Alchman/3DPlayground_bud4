using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowmanMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 10;
    [SerializeField] private float rotationSpeed = 10;
    [SerializeField] private int jumpForce = 10;

    [Header("Pushing Objects")]
    [SerializeField] private GameObject objectsCenter;
    [SerializeField] private float objectsRadius = 1f;
    [SerializeField] private LayerMask objectsMask;
    [SerializeField] private float maxPushForce = 1f;
    [SerializeField] private float pushHeight = 5f;
    [SerializeField] private float forceUpSpeed = 10;


    Rigidbody rb;

    float pushForce;
    bool isGoingUp;

    float time;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public float GetForcePercentage()
    {
        return pushForce / maxPushForce;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce);
        }

        if (Input.GetMouseButtonDown(0))
        {
            pushForce = 0;
            isGoingUp = true;

            time = (3 * Mathf.PI) / 2;
        }

        if (Input.GetMouseButton(0))
        {
            //if (isGoingUp)
            //{
            //    pushForce += maxPushForce * Time.deltaTime;
            //    if (pushForce >= maxPushForce)
            //    {
            //        isGoingUp = false;
            //    }
            //}
            //else
            //{
            //    pushForce -= maxPushForce * Time.deltaTime;
            //    if (pushForce <= 0)
            //    {
            //        isGoingUp = true;
            //    }
            //}
            time += Time.deltaTime;
            pushForce = ((1 + Mathf.Sin((1 / forceUpSpeed) * time)) / 2) * maxPushForce;
        }

        if (Input.GetMouseButtonUp(0))
        {
            Collider[] colliders = Physics.OverlapSphere(objectsCenter.transform.position, objectsRadius, objectsMask);
            foreach(Collider col in colliders)
            {
                Rigidbody colRb = col.GetComponent<Rigidbody>();

                Vector3 forceDirection = transform.forward;
                forceDirection.y = pushHeight;
                colRb.AddForce(forceDirection.normalized * pushForce, ForceMode.Impulse);
            }
            pushForce = 0;
        }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(objectsCenter.transform.position, objectsRadius);
    }
}
