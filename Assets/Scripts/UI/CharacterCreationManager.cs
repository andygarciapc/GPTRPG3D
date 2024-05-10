using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Basic.Events;

public class CharacterCreationManager : MonoBehaviour
{
    public GameObject askName;
    public GameObject confirmName;
    public string inputName;
    public TMP_InputField inputFieldName;
    public ErrorMessage errorMessage;
    public TMP_Text displayName;
    public ConstantsManager constantsManager;
    private void OnEnable()
    {
        GameEventsManager.instance.inputEvents.onSubmitPressed += SubmitName;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.inputEvents.onSubmitPressed -= SubmitName;
    }
    public void SubmitName()
    {
        if (inputFieldName == null)
        {
            inputFieldName = GameObject.FindGameObjectWithTag("TextInput").GetComponent<TMP_InputField>();
            if (inputFieldName == null)
            {
                Debug.LogError("ERROR: inputFieldName is null");
                return;
            }
        }
        if (!CheckUsernameValid(inputFieldName.text))
        {
            errorMessage.ShowError("ERROR");
            return;
        }
        inputName = inputFieldName.text;
        inputFieldName.text = "";
        displayName.text = inputName;
        askName.SetActive(false);
        confirmName.SetActive(true);
    }
    private bool CheckUsernameValid(string Username)
    {
        if (Username == "") return false;
        if (Username.Length <= 2) return false;
        return true;
    }
    public void ConfirmName()
    {
        SetUsernameFromInput();
        // load the dialogue scene
        SceneManager.LoadScene("Dialogue");
    }
    public void DenyName()
    {
        confirmName.SetActive(false);
        askName.SetActive(true);
    }
    public void SetUsernameFromInput()
    {
        if (inputName == null || inputName == "") return;
        Debug.Log("SET USERNAME: " + inputName);
        constantsManager.username = inputName;
        PlayerPrefs.SetString("Username", inputName);
    }
}
