using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
#region Data Types

    [Header("Animation")]
    [SerializeField] private Animator animator;

#endregion

#region Pause

    public void Paused()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

#endregion

#region Unpause

    public void Unpaused()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1f;
    }

#endregion
    
#region To Lobby  

    public void GoToLobby()
    {
        SceneManager.LoadScene("0 - Lobby");
        Time.timeScale = 1f;
    }

#endregion

#region To House

    public void GoToHouse()
    {
        Debug.Log("Loading House");
        SceneManager.LoadScene("2 - House");
        Time.timeScale = 1f;
    }

#endregion

#region To Office

    public void GoToOffice()
    {
        SceneManager.LoadScene("1 - Office");
        Time.timeScale = 1f;
    }

#endregion

#region ButtonStart

    public void ButtonStart()
    {
        animator.SetTrigger("Start Game");
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

#endregion
}
