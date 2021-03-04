using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }


    [Header("Movement config")]
    [SerializeField] private float moveSpeed = 10f;

    [Header("Rotation config")]
    [SerializeField] private float rotationSpeed = 800f;

    [Header("Gravity")]
    [SerializeField] private float jumpHeight = 10f;
    [SerializeField] private float gravityScale = 2;

    [Header("Visual Effects")]
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private GameObject spawnEffect;

    [Header("References")]
    [SerializeField] private CharacterController controller;
    [SerializeField] private Animator anim;
    [SerializeField] private Transform cameraFollow;

    private float gravity;
    private Vector3 startPosition;
    private bool isResetting;

    private Camera mainCamera;

    public void DoDamage()
    {
        if(isResetting)
        {
            return;
        }


        //TODO отнимать и проверять жизни
        Instantiate(deathEffect, cameraFollow.position, Quaternion.identity);
        anim.SetTrigger("Death");
        ResetPosition();
    }
    
    private void ResetPosition()
    {
        isResetting = true;

        Vector3 spawnEffectPosition = startPosition;
        RaycastHit hit;
        if (Physics.Raycast(startPosition, Vector3.down, out hit))
        {
            spawnEffectPosition = hit.point;
            spawnEffectPosition.y += 0.1f;
        }

        Instantiate(spawnEffect, spawnEffectPosition, Quaternion.identity);
        transform.DOMove(startPosition, 1f).SetDelay(1f).OnComplete(
            () =>
            {
                isResetting = false;
            }
        );

    }

    IEnumerator ResetPositionCoroutine()
    {
        isResetting = true;
        transform.position = startPosition;
        yield return new WaitForSeconds(0.1f);
        isResetting = false;
    }

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        startPosition = transform.position;
        Cursor.lockState = CursorLockMode.Locked;

        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (isResetting)
        {
            return;
        }

        Move();
    }

    void Move()
    {
        float inputH = Input.GetAxis("Horizontal");
        float inputV = Input.GetAxis("Vertical");

        Vector3 forward = mainCamera.transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = mainCamera.transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 moveDirection = forward * inputV + right * inputH;

        if (moveDirection.sqrMagnitude > 1)
        {
            moveDirection.Normalize();
        }

        //rotate
        if (Mathf.Abs(inputH) > 0 || Mathf.Abs(inputV) > 0)
        {
            anim.SetBool("Running", true);
            //transform.rotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(moveDirection), Time.deltaTime * rotationSpeed);
        }
        else
        {
            anim.SetBool("Running", false);
        }

        //apply gravity
        if (controller.isGrounded)
        {
            gravity = -0.1f;
            if (Input.GetButtonDown("Jump"))
            {
                gravity = jumpHeight;
            }
        }
        else
        {
            gravity += gravityScale * Physics.gravity.y * Time.deltaTime;
        }

        ////TODO fix
        //bool isJumping = (gravity > 0 || gravity < -0.2f) && !controller.isGrounded;
        //anim.SetBool("Jumping", isJumping);

        if (gravity > 0)
        {
            anim.SetInteger("Gravity", 1);
        }
        else if (gravity < -0.3f) //TODO move to config params
        {
            anim.SetInteger("Gravity", -1);
        }
        else
        {
            anim.SetInteger("Gravity", 0);
        }





        moveDirection.y = gravity;

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
