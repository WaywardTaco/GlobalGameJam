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
    void Awake() {
        //Login Screen
        LoginGUI = GameObject.Find("LoginGUI").gameObject;

        //Window Tabs
        windowGUI = GameObject.Find("WindowGUI").gameObject;
        newsGUI = GameObject.Find("NewsfeedUI").gameObject;
    }

    void Start() {
        LoginGUI.SetActive(true);
        windowGUI.SetActive(false);
        newsGUI.SetActive(false);
    }
}
