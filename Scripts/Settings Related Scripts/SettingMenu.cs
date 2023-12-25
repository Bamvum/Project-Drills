using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Resolution[] resolutions;
    [SerializeField] private TMP_Dropdown resolutionDropdown;

    [Header("ForPlayerPrefs")]
    [SerializeField] public Slider volumeSlider;
    [SerializeField] public Toggle fullScreenToggle;
    [SerializeField] public TMP_Dropdown qualityDropdown;

    void Start()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("Volume");
        fullScreenToggle.isOn = PlayerPrefs.GetInt("IsFullscreen") != 0;
        qualityDropdown.value = PlayerPrefs.GetInt("QualityIndex");
        //resolutionDropdown.value = PlayerPrefs.GetInt("ResolutionIndex");

        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width +  " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        int savedResolutionIndex = PlayerPrefs.GetInt("ResolutionIndex", currentResolutionIndex);
        resolutionDropdown.value = savedResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        PlayerPrefs.SetFloat("Volume", volume);
        Debug.Log("Volume: " + volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt("QualityIndex", qualityIndex);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
        PlayerPrefs.SetInt("IsFullscreen", isFullscreen ? 1 : 0);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", resolutionIndex);
    }

    public void ExitGame()
    {
        Debug.Log("Game Exit");
        Application.Quit();
    }
}
