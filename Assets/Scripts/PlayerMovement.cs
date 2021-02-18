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

    [Header("References")]
    [SerializeField] private CharacterController controller;

    private float gravity;
    private Vector3 startPosition;
    private bool isResetting;

    private Camera mainCamera;

    public void DoDamage()
    {
        //TODO отнимать и проверять жизни

        ResetPosition();
    }
    
    private void ResetPosition()
    {
        isResetting = true;
        transform.DOMove(startPosition, 1f)/*.SetDelay(0.5f)*/.OnComplete(
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
        //Cursor.lockState = CursorLockMode.Locked;

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
        if(Mathf.Abs(inputH) > 0 || Mathf.Abs(inputV) > 0)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
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

        moveDirection.y = gravity;

        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
    }
}
