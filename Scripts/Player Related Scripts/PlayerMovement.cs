using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class PlayerMovement : MonoBehaviour
{
#region Data Types

    [SerializeField] public CharacterController characterController;
    [SerializeField] public Transform playerCamera;
    private float speed;
    
    [SerializeField] private float defaultSpeed;
    [SerializeField] private float gravity = -9.81f;

    #region Sprint

    [Header("Sprint")]
    [SerializeField] private float sprintSpeed;
    [SerializeField] public bool isSprinting;

    #endregion

    #region Crouch

    [Header("Crouch")]
    [SerializeField] private float crouchSpeed;
    [SerializeField] public float standingHeight;
    [SerializeField] public float crouchHeight = 0.5f;
    [SerializeField] public bool isCrouching;

    #endregion

    #region Ground Check

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundDistance = 0.4f;
    [SerializeField] private LayerMask groundMask;
    private bool isGrounded;
    
    #endregion

    [Header("Scripts")]
    [SerializeField] private CrouchZone crouchZone;
    private Vector3 velocity;

#endregion

#region Start

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        speed = defaultSpeed;
        standingHeight = characterController.height;

        if(crouchZone == null) { return; }
        
    }
    
#endregion

#region Movement & Gravity
    // Update is called once per frame
    void Update()
    {
        //Movement
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);

        //Prevent sliding
        if (move.sqrMagnitude > 1f)
        {
            //Stop exciding to the speed when doing horizontal movement
            move.Normalize();
        }

        //Ground Check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        //Gravity
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);

        Crouch();
        Sprint();
    }
#endregion     

#region Sprint
    void Sprint()
    {
        if(Input.GetKey(KeyCode.LeftShift) && isCrouching == false)
        {
            speed = sprintSpeed;
            isSprinting = true;
        }
        else
        {
            speed = defaultSpeed;
            isSprinting = false;
        }

        if(isCrouching)
        {
            speed = crouchSpeed;
        }
    }
#endregion

#region Crouch
     void Crouch()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            characterController.height = crouchHeight;
            isCrouching = true;
            UpdateCameraPosition();
        }

        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            characterController.height = standingHeight;
            isCrouching = false;
            UpdateCameraPosition();
        }
    }
#endregion
    
#region Camera Update (Crouching & Standing) 
    void UpdateCameraPosition()
    {
        // Adjust the camera position based on crouch state
        Vector3 newCameraPosition = isCrouching ? new Vector3(playerCamera.position.x, playerCamera.position.y - 0.5f, playerCamera.position.z) : new Vector3(playerCamera.position.x, playerCamera.position.y + 0.5f, playerCamera.position.z);
        playerCamera.position = newCameraPosition;
    }
    
#endregion   
}
