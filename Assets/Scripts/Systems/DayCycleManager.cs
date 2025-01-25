using UnityEngine;
using Sirenix.OdinInspector;

public class DayCycleManager : MonoBehaviour
{
    public static DayCycleManager Instance;

    [PropertySpace, Title("Properties", TitleAlignment = TitleAlignments.Centered)]
    [ReadOnly, SerializeField] private int daysLeft; //Current Day For Visuals
    [ReadOnly, SerializeField] private int actionsLeft; //Current Actions taken
    [ReadOnly, SerializeField] public int currentDay;
    [ReadOnly, SerializeField] public int currentMonth;
    [SerializeField] private int maxActions; //Max actions per day
    [SerializeField] private int maxDays; //Max days (Upgradeable)
    [SerializeField] private GameObject DayAnimator;
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
        testActionButton = GameObject.Find("DayCycle/TestAction").gameObject;
        endDayButton = GameObject.Find("DayCycle/EndDay").gameObject;
    }

    void Start() {
        EnableButtons();
    }

    public void EndDay() {
        if(daysLeft > maxDays) {
            //EndGame Here
            Debug.Log("Game Over.");
        }
        else {
            DayAnimator.GetComponent<CycleTime>().TriggerDayChange();
            //Next Day
            daysLeft++;
            //Refresh Actions for next day
            actionsLeft = maxActions;

        }
    }

    public void UseAction() {
        if(actionsLeft <= 0) {
            Debug.Log("No more actions left.");
            EndDay();
        }
        else {
            actionsLeft--;
        }
    }

    void Update() {
        if(actionsLeft <= 0) {
            testActionButton.SetActive(false);
            endDayButton.SetActive(true);
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

    public void SetDay(int value) {
        currentDay = value;
    }

    public void SetMonth(int value) {
        currentMonth = value;
    }

    public void EnableButtons() {
        testActionButton.SetActive(true);
        endDayButton.SetActive(false);
    }
}
