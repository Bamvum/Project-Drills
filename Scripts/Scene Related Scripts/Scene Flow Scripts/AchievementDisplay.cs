using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AchievementDisplay : MonoBehaviour
{ 
#region Data Types

    #region Achievements
    [Header("Achievements")]
    [SerializeField] private GameObject weStartOffSomewhereAchievement;
    [SerializeField] private GameObject bravoAchievement;
    #endregion  
    
    #region Achievements Checker

    [Header("Achievements Checker")]
    [SerializeField] public bool isAID1Unlocked;
    [SerializeField] public bool isAID2Unlocked;
    #endregion
    
#endregion

#region Start

    // Start is called before the first frame update
    void Start()
    {
#region Checking of Null
        if(SceneManager.GetActiveScene().name != "0 - Lobby")
        {
            if(weStartOffSomewhereAchievement == null)
            {
                return;
            }

            if(bravoAchievement == null)
            {
                return;
            }
        }
#endregion

#region PlayerPrefs
        // We Start Off Somewhere Achievement
        if(PlayerPrefs.GetInt("We Start Off Somewhere Achievement", 0) == 1)    
        {
            weStartOffSomewhereAchievement.SetActive(true);
            isAID1Unlocked = true;
        }
        
        // Bravo
        if (PlayerPrefs.GetInt("Bravo Achievement", 0) == 1)
        {
            bravoAchievement.SetActive(true);
            isAID2Unlocked = true;
        }
#endregion
    
    } 
#endregion  

}
