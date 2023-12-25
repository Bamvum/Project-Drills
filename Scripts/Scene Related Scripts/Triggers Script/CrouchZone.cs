using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrouchZone : MonoBehaviour
{
    [SerializeField] private GameObject keepCrouching;

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player"))
        {   
            keepCrouching.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            keepCrouching.SetActive(false);
        }
    }
}
