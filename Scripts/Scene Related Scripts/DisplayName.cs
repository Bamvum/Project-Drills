using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngineInternal;

public class DisplayName: MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject objectName;
    [SerializeField] private Transform playerCamera;

    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        Quaternion lookRotation = Quaternion.LookRotation(playerCamera.transform.forward, playerCamera.up);
        objectName.transform.rotation = lookRotation;
    }
    
}
