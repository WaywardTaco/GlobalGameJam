using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class DayCycleManager : MonoBehaviour
{
    public static DayCycleManager Instance;

    [PropertySpace, Title("Properties", TitleAlignment = TitleAlignments.Centered)]
    [ReadOnly, SerializeField] public int daysLeft; //Current Day For Visuals
    [ReadOnly, SerializeField] private int actionsLeft; //Current Actions taken
    [ReadOnly, SerializeField] public int currentDay;
    [ReadOnly, SerializeField] public int currentMonth;
    [SerializeField] private int maxActions; //Max actions per day
    [SerializeField] private int maxDays; //Max days (Upgradeable)
    [SerializeField] private GameObject DayAnimator;
    [SerializeField, ReadOnly] private NewsFeedUpdater newsFeed;
    private GameObject testActionButton;
    private GameObject endDayButton;

    void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else Destroy(this);
        daysLeft = 1;
        actionsLeft = maxActions;

        DayAnimator = GameObject.Find("DayCycle/Animator").gameObject;
    }

    void Start() {

    }

    public void SetNewsFeed(NewsFeedUpdater newsFeedUpdater){
        this.newsFeed = newsFeedUpdater;
    }

    public void EndDay() {
        if(daysLeft >= maxDays) {
            //EndGame Here
            SceneManager.LoadScene("Game Over");
            Debug.Log("Game Over.");
        }
        else {
            //Next Day
            daysLeft++;
            //Refresh Actions for next day
            actionsLeft = maxActions;
            GameWorldEventManager.Instance.PendAutomaticEvents();
            StockManager.Instance.UpdateAllStockValues();
            newsFeed.RefreshDayInfo();
            GameWorldEventManager.Instance.ProcessPendingEvents();
            DayAnimator.GetComponent<CycleTime>().TriggerDayChange();
            StartupManager.Instance.ResetDay();
            MonitorController.Instance.ResetDay();
            NotificationManager.Instance.PopNotifs();
        }
    }
    
    public void UseAction() {
        actionsLeft--;
        if(actionsLeft <= 0) {
            Debug.Log("No more actions left.");
            EndDay();
        }
    }

    public void PurchaseAction() {
        NotificationManager.Instance.Notify(2.5f, "Successfully Purchased Selected Stocks!");
    }

    public void SellAction() {
        NotificationManager.Instance.Notify(2.5f, "Successfully Sold Selected Stocks!");
    }

    public void UpgradeAction() {
        NotificationManager.Instance.Notify(2.5f, "Successfully Purchased Selected Upgrades!");
    }


    void Update() {
        if(actionsLeft <= 0) {
        }
    }

    public int getCurrentDay() {
        return daysLeft;
    }

    public int getActions() {
        return maxActions;
    }

    public int getDays() {
        return maxDays;
    }

    public int getDaysLeft() {
        return maxDays - (daysLeft - 1);
    }

    public void SetMaxDays(int value)
    {
        maxDays = value;
    }

    public void SetDay(int value) {
        currentDay = value;
    }

    public void SetMonth(int value) {
        currentMonth = value;
    }

    // public void EnableButtons() {
    //     testActionButton.SetActive(true);
    //     endDayButton.SetActive(false);
    // }
}
