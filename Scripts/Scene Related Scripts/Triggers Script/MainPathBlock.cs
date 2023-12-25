using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MainPathBlock : MonoBehaviour
{
    [SerializeField] private GameObject collapseObject;
    [SerializeField] private GameObject preCollapseObject;
    [SerializeField] private GameObject additionalInformation;
    [SerializeField] private bool stopLoop;

    [Header("Look At Block Path")]
    [SerializeField] private AudioSource audioCue; 

    void Update()
    {
        if(preCollapseObject == null)
        {
            Invoke("AdditinalInformationMethod", .5f);
            
            if(!stopLoop)
            {
                stopLoop = true;
                audioCue.Play();
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Debug.LogWarning("Player Enter! Main Path Block ");
            collapseObject.SetActive(true);
            Destroy(preCollapseObject);
        }
    }

    void AdditinalInformationMethod()
    {
        additionalInformation.SetActive(true);
        
    }
}
