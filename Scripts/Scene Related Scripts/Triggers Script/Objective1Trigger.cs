using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Objective1Trigger : MonoBehaviour
{
    [SerializeField] private GameObject objective;
    [SerializeField] private GameObject additionaInformation;
    [SerializeField] private GameObject invisibleWall;
    [SerializeField] private GameObject gasMask;
    [SerializeField] private bool playerTrigger;

    void Update()
    {
        if(gasMask == null)
        {
            
            if(invisibleWall != null)
            {
                Destroy(invisibleWall);
                additionaInformation.SetActive(true);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.LogWarning("Player Enter! Show Objective 1");
            objective.SetActive(true);
            //Destroy(this.gameObject);
        }
    }
}
