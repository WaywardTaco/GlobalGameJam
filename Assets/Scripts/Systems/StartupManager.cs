using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartupManager : MonoBehaviour
{
    private GameObject LoginGUI;
    private GameObject windowGUI;
    private GameObject newsGUI;
    private GameObject stocksGUI;
    private GameObject upgradeGUI;
    private float time = 1.5f;
    private float currentTime;
    public bool loadScreen;
    public bool hasLoaded;
    void Awake() {
        //Login Screen
        LoginGUI = GameObject.Find("LoginGUI").gameObject;

        //Window Tabs
        windowGUI = GameObject.Find("WindowGUI").gameObject;
        newsGUI = GameObject.Find("NewsfeedUI").gameObject;
        upgradeGUI = GameObject.Find("Upgrade Menu").gameObject;

        currentTime = time;
    }

    void Start() {
        LoginGUI.SetActive(true);
        windowGUI.SetActive(false);
        newsGUI.SetActive(false);
        upgradeGUI.SetActive(false);
    }

    void Update() {
        if(loadScreen) {
            currentTime -= Time.deltaTime;
            if(currentTime <= 0) {
                loadScreen = false;
                hasLoaded = true;
            }
        }
        if(hasLoaded) {
            LoginGUI.SetActive(false);
            windowGUI.SetActive(true);
        }
    }

    public void SetLoadScreen(bool value) {
        loadScreen = value;
    }
    
    public void SetHasLoaded(bool value) {
        hasLoaded = value;
    }

}
