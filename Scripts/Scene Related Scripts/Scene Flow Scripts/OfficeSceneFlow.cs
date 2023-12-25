using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OfficeSceneFlow : MonoBehaviour
{
    [Header("Start of Game")]
    [SerializeField] private AudioSource fireAnnouncement;
    [SerializeField] private AudioSource[] fireSFX;
    [SerializeField] private GameObject startButton;
    [SerializeField] private Animator animator;



    // Start is called before the first frame update
    void Start()
    {
        Invoke("AudioIsFinish", 1f);  
        Time.timeScale = 0f;  
        StartCoroutine(AudioIsFinish());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator AudioIsFinish()
    {
        yield return new WaitForSecondsRealtime(fireAnnouncement.clip.length);
        startButton.SetActive(true);
    }

    public void ButtonStart()
    {
        //Animation
        animator.SetTrigger("Start Game");
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
