using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using System.Collections;
using Microsoft.Unity.VisualStudio.Editor;

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
    [ReadOnly, SerializeField] private GameObject DayAnimator;
    [ReadOnly, SerializeField] private CanvasGroup FadeOut;
    [SerializeField, ReadOnly] private NewsFeedUpdater newsFeed;
    private GameObject testActionButton;
    private GameObject endDayButton;
    private bool returnTitle;

    void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else Destroy(this);
        daysLeft = 1;
        actionsLeft = maxActions;

        DayAnimator = GameObject.Find("DayCycle/Animator").gameObject;
        FadeOut = GameObject.Find("FadeOut").GetComponent<CanvasGroup>();
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
            GameWorldEventManager.Instance.onStartPend = false;
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
        else {
            switch(actionsLeft) {
                case 1: //Night
                    LightingManager.Instance.ChangeLighting(23);
                    break;
                case 2: //Midday
                    LightingManager.Instance.ChangeLighting(12);
                    break;
            }
        }
    }

    public void PurchaseAction() {
        NotificationManager.Instance.Notify(1.5f, "Successfully Purchased Selected Stocks!");
    }

    public void SellAction() {
        NotificationManager.Instance.Notify(1.5f, "Successfully Sold Selected Stocks!");
    }

    public void UpgradeAction() {
        NotificationManager.Instance.Notify(1.5f, "Successfully Purchased Selected Upgrades!");
    }

    public void FadeToTitleScreen() {
        returnTitle = true;
    }

    IEnumerator Fade() {
        FadeOut.alpha += 0.5f * Time.deltaTime;
        if(FadeOut.alpha >= 1) {
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("TitleMenu");
        }
    }


    void Update() {
        if(returnTitle) {
            StartCoroutine(Fade());
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
        return (maxDays - daysLeft) + 1;
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
