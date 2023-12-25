using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLook : MonoBehaviour
{
#region Data Types

    [Header("Camera Look")]
    [SerializeField] private Transform playerBody;
    [Range(50, 200)]
    [SerializeField] private float mouseSensitivity = 100f;
    [SerializeField] private float xRotation = 100f;
#endregion

#region Start
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }
#endregion
    
#region Update
    // Update is called once per frame
    void Update()
    {
        // Get the horizontal movement (left and right) and vertocal movement of the mouse (up and down)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        
        xRotation -= mouseY;
        // Clamp the vertical rotation to a range between -90 degrees and 90 degrees
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // Set the local rotation of the GameObject (the camera) around the X and Y axes
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotate the player's body (parent object) around the Y-axis based on the horizontal mouse movement
        playerBody.Rotate(Vector3.up * mouseX);

    }
#endregion
}
