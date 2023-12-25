using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerRaycast : MonoBehaviour
{   
#region Data Type
    [SerializeField] private GameObject pauseHUD;
    [SerializeField] private LayerMask pickableLayerMask;
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private GameObject pickUpUI;
    [Min(1)]
    [SerializeField] private float hitRange = 3f;
    [SerializeField] private Transform pickUp;
    [SerializeField] private Transform pickUpFPE;
    [SerializeField] private GameObject inHandItem;
    private RaycastHit hit;

    [Header("Instructions")]
    [SerializeField] private GameObject instructions;

    [Header("Other Scripts")]
    [SerializeField] private Objective1Trigger objective1Trigger;
#endregion

#region Update
    // Update is called once per frame
    void Update()
    {
        // If Raycast hit is not null (or Raycast not colliding with any gameObject)
        if(hit.collider != null)
        {
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
            pickUpUI.SetActive(false);
        }

        // Raycast only run with Layer of pickableLayerMask (The Root of everything)
        Ray ray = new Ray(playerCameraTransform.position, playerCameraTransform.forward);
        if(Physics.Raycast(ray, out hit, hitRange, pickableLayerMask))
        {
            // if not holding anything...
            if(inHandItem == null)
            {
                hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
                pickUpUI.SetActive(true);
            }
        }
        
        // If paused, no input can be perform
        if(!IsPaused())
        {
            InputHandler();
        }

        // Hand checker (If player is holding something)
        if(inHandItem != null)
        {
            return;
        }

    }
#endregion

#region Input Handler
    void InputHandler()
    {
        // Interact Object
        if(Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

        // Drop Object
        if(Input.GetKeyDown(KeyCode.G))
        {
            Drop();
        }

        // Use object that is holding
        if(Input.GetMouseButton(0))
        {
            Use();
        }

        // Can only press "Escape" when not in Lobby Scene!
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
#endregion

#region Interact
    void Interact()
    {
        // Prevent pick up another object if player is holding something...
        if(hit.collider != null && inHandItem == null)
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

            // If-else because of 2 different hand placememt (Item -> Right Hand, FPE -> Two Hands (Middle))
            if(hit.collider.GetComponent<FirePreventionEquipment>())
            {
                //Player is now holding a GameObject
                inHandItem = hit.collider.gameObject;
                //Position & Rotation Reset!
                inHandItem.transform.position = Vector3.zero;
                inHandItem.transform.rotation = Quaternion.identity;
                inHandItem.transform.SetParent(pickUpFPE.transform, false);
                
                if(rb != null)
                {
                    // Physics
                    rb.isKinematic = true;
                }

                return;
            }
            else if(hit.collider.GetComponent<Item>())
            {
                //Player is now holding a GameObject
                inHandItem = hit.collider.gameObject;
                //Position & Rotation Reset!
                inHandItem.transform.position = Vector3.zero;
                inHandItem.transform.rotation = Quaternion.identity;
                inHandItem.transform.SetParent(pickUp.transform, false);

                if(rb != null)
                {
                    // Physics
                    rb.isKinematic = true;
                }

                return;
            }
            // Display Panel for (notes and posters)
            else if (hit.collider.GetComponent<DisplayPanel>())
            {
                IUsable usable = hit.collider.GetComponent<IUsable>();
                if(usable != null)
                {
                    usable.Use(this.gameObject);
                }
            }
            // For Television (0 - Lobby)
            else if (hit.collider.GetComponent<Television>())
            {
                Debug.Log("Interact Television!");
                IUsable usable = hit.collider.GetComponent<IUsable>();
                if(usable != null)
                {
                    usable.Use(this.gameObject);
                }
            }
            // Gas Mask (1 - Office)
            else if (hit.collider.GetComponent<GasMask>())
            {
                Debug.LogWarning("Gas Mask Destroy!");
                Destroy(hit.collider.gameObject);
                pickUpUI.SetActive(false);
            }



        }
    }
#endregion

#region Drop
    void Drop()
    {
        // If the player is holding something, perform the code below.
        if(inHandItem != null)
        {
            Rigidbody rb = inHandItem.GetComponent<Rigidbody>();
            
            if(rb != null)
            {
                //Physics
                rb.isKinematic = false;
            }

            inHandItem.transform.SetParent(null);
            inHandItem = null;
        }
    }
#endregion

#region Use
    void Use()
    {
        // if the player is holding something, perform the code below
        if(inHandItem != null)
        {
            IUsable usable = inHandItem.GetComponent<IUsable>();
            if(usable != null)
            {
                usable.Use(this.gameObject);
            }
        }
    }
#endregion

#region Pause
    void Pause()
    {   
        Debug.Log("Pause Method!");
        if(pauseHUD != null)
        {
            Cursor.lockState = CursorLockMode.None; 
            pauseHUD.SetActive(true);
            Time.timeScale = 0;
        }

    }

    public bool IsPaused()
    {
        return Time.timeScale == 0f;
    }
#endregion
}
