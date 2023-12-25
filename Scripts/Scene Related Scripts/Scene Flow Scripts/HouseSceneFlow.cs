using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HouseSceneFlow : MonoBehaviour
{
    [Header("Prompt Finish")]
    [SerializeField] private GameObject doneHUD;
    [SerializeField] private TextMeshProUGUI bestTimeTMP;
    [SerializeField] private TextMeshProUGUI finishElapsedTime;
    [SerializeField] private TextMeshProUGUI mistakes;
    [SerializeField] private FireExtinguisherManager fireExtinguisherManager;

    [Header("Random Fire Extinguisher")]
    [SerializeField] private Transform[] fireExtinguishers; // 3 Fire Extinguishers
    [SerializeField] private Transform[] fireExtinguishersLocations;

    [Header("Timer")]
    [SerializeField] private TextMeshProUGUI timerText;
    float elapsedTime;
    private float bestTime = 0f; // Initialize with a large value or load from PlayerPrefs

    [Header("Wave")]
    [SerializeField] private int currentWave = 1;
    [SerializeField] private int maxWave = 3;
    
    [Space(5)]
    [SerializeField] private GameObject fireWave1No1;

    [Space(5)]
    [SerializeField] private GameObject fireWave2No1;
    [SerializeField] private GameObject fireWave2No2;
    [SerializeField] private GameObject fireWave2No3;

    [Space(5)]
    [SerializeField] private GameObject fireWave3No1;
    [SerializeField] private GameObject fireWave3No2;
    [SerializeField] private GameObject fireWave3No3;
    [SerializeField] private GameObject fireWave3No4;
    [SerializeField] private GameObject fireWave3No5;

    [Header("Animation")]
    [SerializeField] private Animator animator;


    [Header("Achievement")]
    [SerializeField] private AchievementDisplay achievementDisplay;

    [SerializeField] private GameObject weStartOffSomewhere;
    [SerializeField] private bool weStartOffSomewhereIsAchieve;

    [SerializeField] private GameObject bravo;
    [SerializeField] private bool bravoIsAchieve;
    [SerializeField] private AudioSource achievementSFX;

    // Start is called before the first frame update
    void Start()
    {

        TeleportFireExtinguishersToRandomLocations();
        
        currentWave = 1;

        // Load the best time from PlayerPrefs
        bestTime = PlayerPrefs.GetFloat("Best Time", 0f);
        
        // Display the best time on the UI
        bestTimeTMP.text = "Best Time: \n" + FormatTime(bestTime);
        
        timerText.gameObject.SetActive(true);
        
        Time.timeScale = 0f; //<- Time stop when start because of player need to read instruction then animation start when button click

        PlayerPrefs.GetInt("We Start Off Somewhere Achievement", 0);
        PlayerPrefs.GetInt("Bravo Achievement", 0);

    }

    void Update()
    {
        elapsedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(elapsedTime / 60);
        int seconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        

        WaveOfFire();
    }
    void TeleportFireExtinguishersToRandomLocations()
    {
        // Create a list of available indices for fire extinguisher locations
        List<int> availableIndices = new List<int>(fireExtinguishersLocations.Length);
        for (int i = 0; i < fireExtinguishersLocations.Length; i++)
        {
            availableIndices.Add(i);
        }

        // Teleport each fire extinguisher to a different random location
        for (int i = 0; i < fireExtinguishers.Length; i++)
        {
            if (availableIndices.Count == 0)
            {
                // Handle the case where there are no more available indices
                Debug.LogError("Not enough available fire extinguisher locations.");
                return;
            }

            // Choose a random index from the availableIndices list
            int randomIndex = UnityEngine.Random.Range(0, availableIndices.Count);
            int locationIndex = availableIndices[randomIndex];

            // Remove the chosen index to ensure it's not used again
            availableIndices.RemoveAt(randomIndex);

            // Teleport the fire extinguisher to the chosen location
            fireExtinguishers[i].position = fireExtinguishersLocations[locationIndex].position;
            fireExtinguishers[i].rotation = fireExtinguishersLocations[locationIndex].rotation;
        }
    }

#region Wave of Fire!
    void WaveOfFire()
    {
        if (currentWave != maxWave)
        {
            Debug.Log("The Current Wave: " + currentWave);

            FirstWave();
            SecondWave();
            ThirdWave();
            FinishWave();

        }
    }

    void FirstWave()
    {
        if (currentWave == 1)
        {   
            if(fireWave1No1 != null)
                {
                    ParticleSystem fireWave1No1PS= fireWave1No1.GetComponent<ParticleSystem>();
                    if (fireWave1No1PS != null)
                    {
                        var fireWave1No1PSEmission = fireWave1No1PS.emission;

                        if (fireWave1No1PSEmission.rateOverTime.constant == 0)
                        {
                            Debug.Log("Fire Wave No. 1 is Extinguish");
                            // stopLoop = true;
                            Destroy(fireWave1No1);
                        }
                    }
                }
                else
                {
                    fireWave2No1.SetActive(true);
                    fireWave2No2.SetActive(true);
                    fireWave2No3.SetActive(true);
                    currentWave++;
                }
        }
    }

    void SecondWave()
    {
        if(currentWave == 2)
        {
            Debug.Log("Fire Wave 2!");
            bool stopLoop = false;
            if (!stopLoop)
            {
                //Fire Wave 2 No 1
                if(fireWave2No1 != null)
                {
                    ParticleSystem firePS1 = fireWave2No1.GetComponent<ParticleSystem>();
                    if (firePS1 != null)
                    {
                        var firePSEmission = firePS1.emission;

                        if (firePSEmission.rateOverTime.constant == 0)
                        {
                            Destroy(fireWave2No1);    
                        }
                    }
                }

                //Fire Wave 2 No 2
                if (fireWave2No2 != null)
                {
                    ParticleSystem firePS2 = fireWave2No2.GetComponent<ParticleSystem>();
                    if (firePS2 != null)
                    {
                        var firePSEmission = firePS2.emission;

                        if (firePSEmission.rateOverTime.constant == 0)
                        {
                            Destroy(fireWave2No2);
                        }
                    }
                }

                //Fire Wave 2 No 3
                if (fireWave2No3 != null)
                {
                    ParticleSystem firePS3 = fireWave2No3.GetComponent<ParticleSystem>();
                    if (firePS3 != null)
                    {
                        var firePSEmission = firePS3.emission;

                        if (firePSEmission.rateOverTime.constant == 0)
                        {
                            Destroy(fireWave2No3);
                        }
                    }
                }

                if(fireWave2No1 == null && fireWave2No2 == null && fireWave2No3 == null)
                {
                    fireWave3No1.SetActive(true);
                    fireWave3No2.SetActive(true);
                    fireWave3No3.SetActive(true);
                    fireWave3No4.SetActive(true);
                    fireWave3No5.SetActive(true);
                    currentWave++;
                }
            }
        }
    }

    void ThirdWave()
    {
        if (currentWave == 3)
        {
            bool stopLoop = false;
            if (!stopLoop)
            {
                //Fire Wave 3 No 1
                if(fireWave3No1 != null)
                {
                    ParticleSystem firePS1 = fireWave3No1.GetComponent<ParticleSystem>();
                    if (firePS1 != null)
                    {
                        var firePSEmission = firePS1.emission;

                        if (firePSEmission.rateOverTime.constant == 0)
                        {
                            Destroy(fireWave3No1);
                        }
                    }
                }

                //Fire Wave 3 No 2
                if (fireWave3No2 != null)
                {
                    ParticleSystem firePS2 = fireWave3No2.GetComponent<ParticleSystem>();
                    if (firePS2 != null)
                    {
                        var firePSEmission = firePS2.emission;

                        if (firePSEmission.rateOverTime.constant == 0)
                        {
                            Destroy(fireWave3No2);
                        }
                    }
                }

                //Fire Wave 3 No 3
                if (fireWave3No3 != null)
                {
                    ParticleSystem firePS3 = fireWave3No3.GetComponent<ParticleSystem>();
                    if (firePS3 != null)
                    {
                        var firePSEmission = firePS3.emission;

                        if (firePSEmission.rateOverTime.constant == 0)
                        {
                            Destroy(fireWave3No3);
                        }
                    }
                }

                //Fire Wave 3 No 4
                if (fireWave3No4 != null)
                {
                    ParticleSystem firePS4 = fireWave3No4.GetComponent<ParticleSystem>();
                    if (firePS4 != null)
                    {
                        var firePSEmission = firePS4.emission;

                        if (firePSEmission.rateOverTime.constant == 0)
                        {
                            Destroy(fireWave3No4);
                        }
                    }
                }
                
                //Fire Wave 3 No 5
                if (fireWave3No5 != null)
                {
                    ParticleSystem firePS5 = fireWave3No5.GetComponent<ParticleSystem>();
                    if (firePS5 != null)
                    {
                        var firePSEmission = firePS5.emission;

                        if (firePSEmission.rateOverTime.constant == 0)
                        {
                            Destroy(fireWave3No5);
                        }
                    }
                }

                if(fireWave3No1 == null && fireWave3No2 == null && fireWave3No3 == null &&
                    fireWave3No4 == null && fireWave3No5 == null)
                {
                    currentWave++;
                }
            }
        }
    }

    void FinishWave()
    {
        if(currentWave == 4)
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;

            doneHUD.SetActive(true);
            timerText.gameObject.SetActive(false);

            finishElapsedTime.text = "Your Time: \n" + FormatTime(elapsedTime);
            mistakes.text = "Mistakes: \n"+ fireExtinguisherManager.fireExtinguishMistakes;

            Debug.LogWarning("Mistakes: " + fireExtinguisherManager.fireExtinguishMistakes);

            // Check if the current time is faster than the best time
            if (elapsedTime < bestTime || bestTime == 0f)
            {
                // Save the new best time
                bestTime = elapsedTime;

                PlayerPrefs.SetFloat("Best Time", bestTime);
                PlayerPrefs.Save();

                // Update the UI text for best time
                bestTimeTMP.text = "Best Time: \n" + FormatTime(bestTime);

                // Display a message or perform any other actions for a new best time
                Debug.LogWarning("New Best Time: " + FormatTime(bestTime));
            }
            else
            {
                Debug.LogWarning("Not a new best time. Best Time: " + FormatTime(bestTime));
            }
            
            // Trigger PlayerPrefs
            if(fireExtinguisherManager.fireExtinguishMistakes == 0)
            {   
                BravoAchievement();
            }
 
            if(fireExtinguisherManager.fireExtinguishMistakes >= 4)
            {
                WeStartOffSomewhereAchievement();
            }
        }
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

#endregion

    public void ButtonStart()
    {
        animator.SetTrigger("Start Game");
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

#region Achievements
    //Achievement Triger "We Start Off Somewheres"
    void WeStartOffSomewhereAchievement()
    {
        if(!achievementDisplay.isAID1Unlocked)
        {
            achievementSFX.Play();
            weStartOffSomewhere.SetActive(true);
            weStartOffSomewhereIsAchieve = true;
            PlayerPrefs.SetInt("We Start Off Somewhere Achievement", weStartOffSomewhereIsAchieve ? 1 : 0);
            PlayerPrefs.Save();
        }
    }

    //Achievement Triger "Bravo"
    void BravoAchievement()
    {
        if(PlayerPrefs.GetInt("Bravo Achievement", 0) != 1)
        {
            achievementSFX.Play();
            bravo.SetActive(true);
            bravoIsAchieve = true;
            PlayerPrefs.SetInt("Bravo Achievement", bravoIsAchieve ? 1 : 0);
            PlayerPrefs.Save();
        }           
    }
#endregion
}   
