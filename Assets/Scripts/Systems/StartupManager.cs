
using Sirenix.OdinInspector;
using UnityEngine;

public class StartupManager : MonoBehaviour
{
    public static StartupManager Instance;
    [ReadOnly, SerializeField] private GameObject LoginGUI;
    private GameObject PFP;
    private GameObject loadingUI;
    private GameObject notifsUI;
    [ReadOnly, SerializeField] private GameObject windowGUI;
    [ReadOnly, SerializeField] private GameObject newsGUI;
    [ReadOnly, SerializeField] private GameObject stocksGUI;
    [ReadOnly, SerializeField] private GameObject upgradeGUI;
    [ReadOnly, SerializeField] private GameObject moneyUI;
    [ReadOnly, SerializeField] private GameObject stocksUI;
    [ReadOnly, SerializeField] private GameObject purchaseUI;
    [ReadOnly, SerializeField] private GameObject sellUI;
    [ReadOnly, SerializeField] private GameObject upgradeUI;
    
    private float time = 1.5f;
    private float currentTime;
    public bool loadScreen;
    public bool hasLoaded = false;
    void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else Destroy(this);

        //Login Screen
        LoginGUI = GameObject.Find("LoginGUI").gameObject;
        PFP = GameObject.Find("LoginGUI/PFP").gameObject;
        loadingUI = GameObject.Find("LoginGUI/Loading").gameObject;

        //Window Tabs
        windowGUI = GameObject.Find("WindowGUI").gameObject;
        newsGUI = GameObject.Find("NewsfeedUI").gameObject;
        upgradeGUI = GameObject.Find("Upgrade Menu").gameObject;
        moneyUI = GameObject.Find("MoneyCounterUI").gameObject;
        stocksUI = GameObject.Find("StockPage").gameObject;
        notifsUI = GameObject.Find("NotifUI").gameObject;
        purchaseUI = GameObject.Find("PurchaseUI").gameObject;
        sellUI = GameObject.Find("SellUI").gameObject;
        upgradeUI = GameObject.Find("UpgradeUI").gameObject;

        currentTime = time;
    }

    void Start() {
        LoginGUI.SetActive(true);
        windowGUI.SetActive(false);
        newsGUI.SetActive(false);
        upgradeGUI.SetActive(false);
        moneyUI.SetActive(false);
        stocksUI.SetActive(false);
        loadingUI.SetActive(false);
        notifsUI.SetActive(false);
        purchaseUI.SetActive(false);
        sellUI.SetActive(false);
        upgradeUI.SetActive(false);

        SFXManager.Instance.Play("Ambience");
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

    public void ResetDay() {
        PFP.SetActive(true);
        loadingUI.SetActive(false);
        newsGUI.SetActive(false);
        upgradeGUI.SetActive(false);
        stocksUI.SetActive(false);
        notifsUI.SetActive(false);
        purchaseUI.SetActive(false);
        sellUI.SetActive(false);
        upgradeUI.SetActive(false);
        hasLoaded = false;
        currentTime = time;
    }

    public void SetLoadScreen(bool value) {
        loadScreen = value;
    }
    
    public void SetHasLoaded(bool value) {
        hasLoaded = value;
    }

}
