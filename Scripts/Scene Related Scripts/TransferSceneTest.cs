using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerResetFire : MonoBehaviour
{
    [Header("Fire Outside")]
    [SerializeField] private GameObject classAFire;
    [SerializeField] private GameObject classBFire;
    [SerializeField] private GameObject classCFire;
    [SerializeField] private GameObject classDFire;
    [SerializeField] private GameObject classKFire;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered trigger zone! Resetting Fires");
            ResetFire();
        }
    }

    void ResetFire()
    {
        ClassA();
        ClassB();
        ClassC();
        ClassD();
        ClassK();
    }

    void ClassA()
    {
        ParticleSystem APS = classAFire.GetComponent<ParticleSystem>();
        BoxCollider ABC = classAFire.GetComponent<BoxCollider>();
        AudioSource AAS = classAFire.GetComponent<AudioSource>();

        if(APS != null)
        {
            var emission = APS.emission;

            if(emission.rateOverTime.constant <= 49.9f)
            {
                emission.rateOverTime = new ParticleSystem.MinMaxCurve(50.0f);
            }

            if(ABC != null)
            {
                if(ABC.enabled == false)
                {
                    ABC.enabled = true;
                }
            }

            if(AAS.volume <= 1f)
            {
                AAS.volume = 1f;
            }
        }
    }

    void ClassB()
    {
        ParticleSystem BPS = classBFire.GetComponent<ParticleSystem>();
        BoxCollider BBC = classBFire.GetComponent<BoxCollider>();
        AudioSource BAS = classBFire.GetComponent<AudioSource>();

        if(BPS != null)
        {
            var emission = BPS.emission;

            if(emission.rateOverTime.constant <= 49.9f)
            {
                emission.rateOverTime = new ParticleSystem.MinMaxCurve(50.0f);
            }

            if(BBC != null)
            {
                if(BBC.enabled == false)
                {
                    BBC.enabled = true;
                }
            }

            if(BAS.volume <= 1f)
            {
                BAS.volume = 1f;
            }
        }
    }

    void ClassC()
    {
        ParticleSystem CPS = classCFire.GetComponent<ParticleSystem>();
        BoxCollider CBC = classCFire.GetComponent<BoxCollider>();
        AudioSource CAS = classCFire.GetComponent<AudioSource>();        

        if(CPS != null)
        {
            var emission = CPS.emission;

            if(emission.rateOverTime.constant <= 49.9f)
            {
                emission.rateOverTime = new ParticleSystem.MinMaxCurve(50.0f);
            }

            if(CBC != null)
            {
                if(CBC.enabled == false)
                {
                    CBC.enabled = true;
                }
            }

            if(CAS.volume <= 1f)
            {
                CAS.volume = 1f;
            }
        }
    }

    void ClassD()
    {
        ParticleSystem DPS = classDFire.GetComponent<ParticleSystem>();
        BoxCollider DBC = classDFire.GetComponent<BoxCollider>();
        AudioSource DAS = classDFire.GetComponent<AudioSource>();

        if(DPS != null)
        {
            var emission = DPS.emission;

            if(emission.rateOverTime.constant <= 49.9f)
            {
                emission.rateOverTime = new ParticleSystem.MinMaxCurve(50.0f);
            }

            if(DBC != null)
            {
                if(DBC.enabled == false)
                {
                    DBC.enabled = true;
                }
            }

            if(DAS.volume <= 1f)
            {
                DAS.volume = 1f;
            }
        }
    }

        void ClassK()
    {
        ParticleSystem KPS = classKFire.GetComponent<ParticleSystem>();
        BoxCollider KBC = classKFire.GetComponent<BoxCollider>();
        AudioSource KAS = classKFire.GetComponent<AudioSource>();

        if(KPS != null)
        {
            var emission = KPS.emission;

            if(emission.rateOverTime.constant <= 49.9f)
            {
                emission.rateOverTime = new ParticleSystem.MinMaxCurve(50.0f);
            }

            if(KBC != null)
            {
                if(KBC.enabled == false)
                {
                    KBC.enabled = true;
                }
            }

            if(KAS.volume <= 1f)
            {
                KAS.volume = 1f;
            }
        }
    }
}
