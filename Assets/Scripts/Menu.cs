using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public GameObject container;
    public GameObject startButton, returnButton;
    public GameObject info;
    public Toggle ruLanguage, enLanguage;

    void Start() {
        if (SceneManager.GetActiveScene().name == "MainScene"){
            startButton.gameObject.SetActive(false);
            returnButton.gameObject.SetActive(true);
            SetEnabled(false);
        } else {
            startButton.gameObject.SetActive(true);
            returnButton.gameObject.SetActive(false);
            SetEnabled(true);
        }

        ruLanguage.onValueChanged.AddListener((value) => OnLanguageButtonClick( value ? "en" : "ru"));
        enLanguage.onValueChanged.AddListener((value) => OnLanguageButtonClick(!value ? "en" : "ru"));
        OnLanguageButtonClick(PlayerPrefs.GetString("language", "en"));
    }

    public void Toggle(){
        SetEnabled(!container.gameObject.activeSelf);
    }

    public void SetEnabled(bool value){
        container.gameObject.SetActive(value);
        Cursor.visible = value;
        Cursor.lockState = value ? CursorLockMode.None : CursorLockMode.Locked;
    }

    public void OnLanguageButtonClick(string name){
        PlayerPrefs.SetString("language", name);
        if (name == "ru"){
            ruLanguage.SetIsOnWithoutNotify(false);
            ruLanguage.interactable = false;
            enLanguage.SetIsOnWithoutNotify(true);
            enLanguage.interactable = true;
        } else if (name == "en"){
            ruLanguage.SetIsOnWithoutNotify(true);
            ruLanguage.interactable = true;
            enLanguage.SetIsOnWithoutNotify(false);
            enLanguage.interactable = false;
        }
        Debug.Log(name);
    }

    public void StartGame() {
        SceneManager.LoadScene("MainScene");
    }

    public void ReturnToMenu(){
        SceneManager.LoadScene("MenuScene");
    }

    public void ExitGame() {
        Application.Quit();
    }

    public void Info(bool value) {
        info.SetActive(value);
    }

}
