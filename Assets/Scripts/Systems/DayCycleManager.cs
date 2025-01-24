using UnityEngine;
using Sirenix.OdinInspector;

public class DayCycleManager : MonoBehaviour
{
    public static DayCycleManager Instance;

    [PropertySpace, Title("Properties", TitleAlignment = TitleAlignments.Centered)]
    [SerializeField] private int currentDay; //Current Day For Visuals
    [SerializeField] private int currentActions; //Current Actions taken
    [SerializeField] private int maxActions; //Max actions per day
    [SerializeField] private int maxDays; //Max days (Upgradeable)

    void Awake() {
        if(Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    public void EndDay() {
        if(currentDay >= maxDays) {
            //EndGame Here
            Debug.Log("Game Over.");
        }
        else {
            //Next Day
            currentDay++;
            //Refresh Actions for next day
            currentActions = maxActions;
        }
    }

    public void UseAction() {
        if(currentActions <= 0) {
            Debug.Log("No more actions left.");
        }
        else {
            currentActions--;
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
}
