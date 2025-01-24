using UnityEngine;
using Sirenix.OdinInspector;

public class DayCycleManager : MonoBehaviour
{
    public static DayCycleManager Instance;

    [PropertySpace, Title("Properties", TitleAlignment = TitleAlignments.Centered)]
    [ReadOnly, SerializeField] private int currentDay; //Current Day For Visuals
    [ReadOnly, SerializeField] private int currentActions; //Current Actions taken
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
        currentDay = 1;
        currentActions = maxActions;

        DayAnimator = GameObject.Find("DayCycle/Animator").gameObject;
        testActionButton = GameObject.Find("DayCycle/TestAction").gameObject;
        endDayButton = GameObject.Find("DayCycle/EndDay").gameObject;
    }

    void Start() {
        EnableButtons();
    }

    public void EndDay() {
        if(currentDay > maxDays) {
            //EndGame Here
            Debug.Log("Game Over.");
        }
        else {
            DayAnimator.GetComponent<CycleTime>().TriggerDayChange();
            //Next Day
            currentDay++;
            //Refresh Actions for next day
            currentActions = maxActions;

        }
    }

    public void UseAction() {
        if(currentActions <= 0) {
            Debug.Log("No more actions left.");
            EndDay();
        }
        else {
            currentActions--;
        }
    }

    void Update() {
        if(currentActions <= 0) {
            testActionButton.SetActive(false);
            endDayButton.SetActive(true);
        }
    }

    public int getCurrentDay() {
        return currentDay;
    }

    public int getActions() {
        return maxActions;
    }

    public int getDays() {
        return maxDays;
    }

    public void EnableButtons() {
        testActionButton.SetActive(true);
        endDayButton.SetActive(false);
    }
}
