using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FireExtinguisherManager : MonoBehaviour
{

    [SerializeField] public int fireExtinguishMistakes;

    [Space(10)]
    [SerializeField] private AudioSource fireExtinguishSFX;
    [SerializeField] private AudioSource wrongSFX;
    [SerializeField] private float raycastRange = 6f;
    [Range(10, 50)]
    [SerializeField] private float fireDepletion;

    [Header("SFX")]
    [SerializeField] private float decreaseSpeed;

    private RaycastHit hit;

    void Start()
    {
        if(SceneManager.GetActiveScene().name != "2 - House")
        {
            fireExtinguishMistakes = 0;
        }

        if(fireExtinguishSFX == null)
        {
            return;
        }

        if(wrongSFX == null)
        {
            return;
        }
    }

#region PowderFireExtinguisher
    public void PowderFireExtinguisher()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out hit, raycastRange))
        {
            /* 
                Powder Fire Extinguisher is SAFE for class:
                A (Ordinary Combustibles)        8
                B (Flammable Liquids or Gas)     9
                C (Electrical)                   10
                D (Metal Fires)                  11
            */
            if (hit.collider.gameObject.layer == 8 || hit.collider.gameObject.layer == 9 ||
                hit.collider.gameObject.layer == 10 || hit.collider.gameObject.layer == 11)
            {
                ParticleSystem fireParticleSystem = hit.collider.GetComponent<ParticleSystem>();
                if(fireParticleSystem != null)
                {
                    var fireEmission = fireParticleSystem.emission;

                    float frameDependentValue = fireDepletion * Time.deltaTime;
                    fireEmission.rateOverTime = new ParticleSystem.MinMaxCurve(fireEmission.rateOverTime.constant - frameDependentValue);

                    BoxCollider fireBoxCollider = hit.collider.GetComponent<BoxCollider>();
                    if(fireEmission.rateOverTime.constant == 0)
                    {
                        fireBoxCollider.enabled = false;
                        fireExtinguishSFX.Play();
                    }

                    AudioSource fireAudioSource = hit.collider.GetComponent<AudioSource>();
                    if(fireAudioSource != null)
                    {
                        //Descrease audioSource volume as particle rate over time is deacreasing
                        float audioDependentValue = decreaseSpeed * Time.deltaTime;
                        fireAudioSource.volume = Mathf.Max(fireAudioSource.volume - audioDependentValue, 0f);
                    }
                }
            }
            /* 
                Powder Fire Extinguisher is NOT SAFE for class:
                K (Kitchen Fires)                12
            */
            else if (hit.collider.gameObject.layer == 12)
            {
                WrongFireExtinguisher();
            }
        }
    }
#endregion

#region Foam Fire Extinguisher
    public void FoamFireExtinguisher()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out hit, raycastRange))
        {
            /* 
                Foam Fire Extinguisher is SAFE for class:
                A (Ordinary Combustibles)        8
                B (Flammable Liquids or Gas)     9   
            */

            if(hit.collider.gameObject.layer == 8 || hit.collider.gameObject.layer == 9)
            {
                ParticleSystem fireParticleSystem = hit.collider.GetComponent<ParticleSystem>();
                if(fireParticleSystem != null)
                {
                    var fireEmission = fireParticleSystem.emission;

                    float frameDependentValue = fireDepletion * Time.deltaTime;
                    fireEmission.rateOverTime = new ParticleSystem.MinMaxCurve(fireEmission.rateOverTime.constant - frameDependentValue);

                    BoxCollider fireBoxCollider = hit.collider.GetComponent<BoxCollider>();
                    if(fireEmission.rateOverTime.constant == 0)
                    {
                        fireBoxCollider.enabled = false;
                        fireExtinguishSFX.Play();
                    }

                    AudioSource fireAudioSource = hit.collider.GetComponent<AudioSource>();
                    if(fireAudioSource != null)
                    {
                        //Descrease audioSource volume as particle rate over time is deacreasing
                        float audioDependentValue = decreaseSpeed * Time.deltaTime;
                        fireAudioSource.volume = Mathf.Max(fireAudioSource.volume - audioDependentValue, 0f);
                    }
                }
            }
            /*
                Foam Fire Extinguisher is NOT SAFE for class:
                C (Electrical)                   10
                D (Metal)                        11
                K (Kitchen)                      12
            */
            else if (hit.collider.gameObject.layer == 10 || hit.collider.gameObject.layer == 11 ||
                    hit.collider.gameObject.layer == 12)
            {
                WrongFireExtinguisher();
            }
        }
    }
#endregion

#region CO2 Fire Extinguisher
    public void CO2FireExtinguisher()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out hit, raycastRange))
        {
            /*
                CO2 Fire Extinguisher is SAFE for class:
                B (Flammable Liquids or Gas)     9
                C (Electrical)                   10
            */
            if(hit.collider.gameObject.layer == 9 || hit.collider.gameObject.layer == 10)
            {
                ParticleSystem fireParticleSystem = hit.collider.GetComponent<ParticleSystem>();
                if(fireParticleSystem != null)
                {
                    var fireEmission = fireParticleSystem.emission;

                    float frameDependentValue = fireDepletion * Time.deltaTime;
                    fireEmission.rateOverTime = new ParticleSystem.MinMaxCurve(fireEmission.rateOverTime.constant - frameDependentValue);

                    BoxCollider fireBoxCollider = hit.collider.GetComponent<BoxCollider>();
                    if(fireEmission.rateOverTime.constant == 0)
                    {
                        fireBoxCollider.enabled = false;
                        fireExtinguishSFX.Play();
                    }

                    AudioSource fireAudioSource = hit.collider.GetComponent<AudioSource>();
                    if(fireAudioSource != null)
                    {
                        //Descrease audioSource volume as particle rate over time is deacreasing
                        float audioDependentValue = decreaseSpeed * Time.deltaTime;
                        fireAudioSource.volume = Mathf.Max(fireAudioSource.volume - audioDependentValue, 0f);
                    }
                }
            }
            /*
                CO2 Fire Extinguisher is NOT SAFE for class:
                A (Ordinary Combustibles)        8
                D (Metal)                        11
                K (Kitchen)                      12
            */
            else if(hit.collider.gameObject.layer == 8  || hit.collider.gameObject.layer == 11 ||
                    hit.collider.gameObject.layer == 12 )
            {
                WrongFireExtinguisher();
            }
        }
    }
#endregion

#region Water Fire Extinguiser
    public void WaterFireExtinguisher()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if (Physics.Raycast(ray, out hit, raycastRange))
        {
            /*
                Water Fire Extinguisher is SAFE for class:
                A (Ordinary Combustibles)        8
            */
            if(hit.collider.gameObject.layer == 8)
            {
                ParticleSystem fireParticleSystem = hit.collider.GetComponent<ParticleSystem>();
                if(fireParticleSystem != null)
                {
                    var fireEmission = fireParticleSystem.emission;

                    float frameDependentValue = fireDepletion * Time.deltaTime;
                    fireEmission.rateOverTime = new ParticleSystem.MinMaxCurve(fireEmission.rateOverTime.constant - frameDependentValue);

                    BoxCollider fireBoxCollider = hit.collider.GetComponent<BoxCollider>();
                    if(fireEmission.rateOverTime.constant == 0)
                    {
                        fireBoxCollider.enabled = false;
                        fireExtinguishSFX.Play();
                    }

                    AudioSource fireAudioSource = hit.collider.GetComponent<AudioSource>();
                    if(fireAudioSource != null)
                    {
                        //Descrease audioSource volume as particle rate over time is deacreasing
                        float audioDependentValue = decreaseSpeed * Time.deltaTime;
                        fireAudioSource.volume = Mathf.Max(fireAudioSource.volume - audioDependentValue, 0f);
                    }
                }  
            }
            /*
                Water Fire Extinguisher is SAFE for class:
                B (Flammable Liquids or Gas)     9
                C (Electrical)                   10
                D (Metal)                        11
                K (Kitchen)                      12
            */
            else if(hit.collider.gameObject.layer == 9 || hit.collider.gameObject.layer == 10 ||
                    hit.collider.gameObject.layer == 11 || hit.collider.gameObject.layer == 12)
            {
                WrongFireExtinguisher();
            }
        }        
    }
#endregion
   
#region Wet Chemical Fire Extinguisher
    public void WetChemicalFireExtinguisher()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        if(Physics.Raycast(ray, out hit, raycastRange))
        {   
            /*
                Wet Chemical Fire Extinguisher is SAFE for class:
                A (Ordinary Combustibles)        8
                K (Kitchen)                      12
            */
            if(hit.collider.gameObject.layer == 8 || hit.collider.gameObject.layer == 12)
            {
                ParticleSystem fireParticleSystem = hit.collider.GetComponent<ParticleSystem>();
                if(fireParticleSystem != null)
                {
                    var fireEmission = fireParticleSystem.emission;

                    float frameDependentValue = fireDepletion * Time.deltaTime;
                    fireEmission.rateOverTime = new ParticleSystem.MinMaxCurve(fireEmission.rateOverTime.constant - frameDependentValue);

                    BoxCollider fireBoxCollider = hit.collider.GetComponent<BoxCollider>();
                    if(fireEmission.rateOverTime.constant == 0)
                    {
                        fireBoxCollider.enabled = false;
                        fireExtinguishSFX.Play();
                    }
                    
                    AudioSource fireAudioSource = hit.collider.GetComponent<AudioSource>();
                    if(fireAudioSource != null)
                    {
                        //Descrease audioSource volume as particle rate over time is deacreasing
                        float audioDependentValue = decreaseSpeed * Time.deltaTime;
                        fireAudioSource.volume = Mathf.Max(fireAudioSource.volume - audioDependentValue, 0f);
                    }
                }
            }
            /*
                Wet Chemical Fire Extinguisher is SAFE for class:
                B (Flammable Liquids or Gas)     9
                C (Electrical)                   10
                D (Metal)                        11
            */
            else if (hit.collider.gameObject.layer == 9 || hit.collider.gameObject.layer == 10 ||
                    hit.collider.gameObject.layer == 11)
            {
                WrongFireExtinguisher();
            }
        }
    }
#endregion
    
    // SFX Only played once.
    void WrongFireExtinguisher()
    {
        bool soundPlayed = false;

        if(Input.GetMouseButtonDown(0) && !soundPlayed)
        {
            fireExtinguishMistakes++;
            wrongSFX.Play();
            soundPlayed = true;
        }

        if(Input.GetMouseButtonUp(0))
        {
            soundPlayed = false;
        }
    }
}
