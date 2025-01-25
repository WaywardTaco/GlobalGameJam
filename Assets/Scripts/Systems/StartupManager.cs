
using Sirenix.OdinInspector;
using UnityEngine;

public class StartupManager : MonoBehaviour
{
    [ReadOnly, SerializeField] private GameObject LoginGUI;
    [ReadOnly, SerializeField] private GameObject windowGUI;
    [ReadOnly, SerializeField] private GameObject newsGUI;
    [ReadOnly, SerializeField] private GameObject stocksGUI;
    [ReadOnly, SerializeField] private GameObject upgradeGUI;
    [ReadOnly, SerializeField] private GameObject moneyUI;
    private float time = 1.5f;
    private float currentTime;
    public bool loadScreen;
    public bool hasLoaded = false;
    void Awake() {
        //Login Screen
        LoginGUI = GameObject.Find("LoginGUI").gameObject;

        //Window Tabs
        windowGUI = GameObject.Find("WindowGUI").gameObject;
        newsGUI = GameObject.Find("NewsfeedUI").gameObject;
        upgradeGUI = GameObject.Find("Upgrade Menu").gameObject;
        moneyUI = GameObject.Find("MoneyCounterUI").gameObject;

        currentTime = time;
    }

    void Start() {
        LoginGUI.SetActive(true);
        windowGUI.SetActive(false);
        newsGUI.SetActive(false);
        upgradeGUI.SetActive(false);
        moneyUI.SetActive(false);
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
            moneyUI.SetActive(true);
        }
        else {
            LoginGUI.SetActive(true);
            windowGUI.SetActive(false);
            moneyUI.SetActive(false);
        }
    }

    public void SetLoadScreen(bool value) {
        loadScreen = value;
    }
    
    public void SetHasLoaded(bool value) {
        hasLoaded = value;
    }

}
