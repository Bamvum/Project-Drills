using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HeadBob : MonoBehaviour
{
#region Data Types
    [HideInInspector] private float bobbingSpeed = 0.25f;
    [HideInInspector] private float bobbingAmount = 0.05f;
    
    #region HeadBob
    [Header("Head Bob")]
    [SerializeField] private float walkingBobbingSpeed = 7.5f;
    [SerializeField] private float walkingBobbingAmount = 0.09f;
    #endregion

    #region Sprint HeadBob
    [SerializeField] private float sprintBobbingSpeed = 15f;
    [SerializeField] private float sprintBobbingAmount = 0.09f;
    #endregion

    #region Mid Point
    [Space(10)]
    [SerializeField] private float standingMidpoint = 0.6f;
    [SerializeField] private float crouchMidpoint = 0.6f;
    #endregion
    [HideInInspector] private float timer = 0.0f;

    public PlayerMovement playerMovement;
#endregion

#region Start
    void Start()
    {
        standingMidpoint = transform.localPosition.y;
        crouchMidpoint= transform.localPosition.y / 2;
    }
#endregion

#region Oscillation <- Complicated Science stuff (Formula)
    void Update()
    {
        float waveslice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (Mathf.Abs(horizontal) == 0f && Mathf.Abs(vertical) == 0f)
        {
            timer = 0.0f;
        }
        else
        {
            waveslice = Mathf.Sin(timer);
            timer += bobbingSpeed * Time.deltaTime;
        
            if (timer > Mathf.PI * 2f)
            {
                timer = timer - (Mathf.PI * 2f);
            }
        }

        float translateChange = 0f;

        if (waveslice != 0f)
        {
            translateChange = waveslice * bobbingAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;
        }

        Vector3 localPos = transform.localPosition;

#endregion
 
#region Oscilation sprint multiplier
        if (playerMovement.isCrouching)
        {
            localPos.y = crouchMidpoint + translateChange * Time.timeScale;
        }
        else
        {
            localPos.y = standingMidpoint + translateChange * Time.timeScale;
        }

        transform.localPosition = localPos;

        if (playerMovement.isSprinting == true)
        {
            bobbingSpeed = sprintBobbingSpeed;
            bobbingAmount = sprintBobbingAmount;
        }
        else
        {
            bobbingSpeed = walkingBobbingSpeed;
            bobbingAmount = walkingBobbingAmount;
        }
    }
#endregion
}
