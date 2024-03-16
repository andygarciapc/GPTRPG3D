using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public CanvasGroup OptionPanel;
    public CanvasGroup InstructionPanel;
    public AudioSource audio;
    public AudioClip buttonSound;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Option()
    {
        OptionPanel.alpha = 1;
        OptionPanel.blocksRaycasts = true;
    }

    public void Instructions()
    {
        InstructionPanel.alpha = 1;
        InstructionPanel.blocksRaycasts = true;
    }
    public void Back()
    {
        OptionPanel.alpha = 0;
        InstructionPanel.alpha = 0;
        InstructionPanel.blocksRaycasts = false;
        OptionPanel.blocksRaycasts = false;
    }


    public void SaveCurrentScene()
    {
        PlayerPrefs.SetString("SavedScene", SceneManager.GetActiveScene().name);
    }

    public void LoadSavedScene()
    {
        SceneManager.LoadScene(PlayerPrefs.GetString("SavedScene"));
    }


    public void QuitGame()
    {
        Application.Quit();
    }

    public void PlayButtonSound()
    {
        audio.clip = buttonSound;
        audio.Play();
    }
}
