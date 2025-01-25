using System.Collections;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CycleTime : MonoBehaviour
{
    public Transform black;
    public Transform Main;
    public Transform Day;
    public Transform Month;
    public TextMeshProUGUI month;
    public TextMeshProUGUI day;
    public TextMeshProUGUI monthAlt;
    public TextMeshProUGUI dayAlt;
    private int tempDayValue;
    private int tempMonthValue;
    private bool hasPlayedDay;
    private bool hasPlayedMonth;
    private bool changeMonth;
    private bool changeDay;

    void Awake() {
        black = GameObject.Find("NightBG").GetComponent<Transform>();
        Main = GameObject.Find("Main").GetComponent<Transform>();
        Day = GameObject.Find("Day").GetComponent<Transform>();
        Month = GameObject.Find("Month").GetComponent<Transform>();
        Main.GetComponent<CanvasGroup>().alpha = 0f;
        Day.GetComponent<CanvasGroup>().alpha = 0f;
        Month.GetComponent<CanvasGroup>().alpha = 0f;
        month.text = "1";
        day.text = " 26";
        tempDayValue = 26;
        tempMonthValue = 1;

        black.localPosition = new Vector2(0, Screen.height);
    }

    void Start() {
        DayCycleManager.Instance.SetDay(tempDayValue);
        DayCycleManager.Instance.SetMonth(tempMonthValue);
    }

    // void OnEnable() {
    //     tempMonthValue++;
    //     StartCoroutine(StartMonthAnimation(tempMonthValue));
    // }

    [PropertySpace, Button("Change Day", ButtonSizes.Large)]
    public void TriggerDayChange() {
        ResetDayValues();
        tempDayValue++;
        if(tempDayValue >= 31) {
            tempDayValue = 1;
            tempMonthValue++;
            changeMonth = true;
            if(tempMonthValue >= 13) {
                tempMonthValue = 1;
                tempDayValue = 1;
            }
            StartCoroutine(StartCycleAnimation(tempDayValue, tempMonthValue));
        }
        else {
            StartCoroutine(StartDayAnimation(tempDayValue));
        }
        
        hasPlayedDay = true;
    }

    IEnumerator StartDayAnimation(int day) {
        dayAlt.text = $" {day}";

        //Transition to night
        black.localPosition = new Vector2(0, Screen.height);
        black.LeanMoveLocalY(0, 2f).setEaseOutExpo().delay = 0.1f;

        yield return new WaitForSeconds(1);

        //Show text
        Main.GetComponent<CanvasGroup>().alpha = 0;
        Main.GetComponent<CanvasGroup>().LeanAlpha(1, 0.5f);
        Main.localPosition = new Vector2(-150, 0);
        Main.LeanMoveLocalX(0, 0.5f).setEaseOutExpo();

        yield return new WaitForSeconds(1);

        //Show Count
        Day.GetComponent<CanvasGroup>().LeanAlpha(1, 0.5f);
        LeanTween.scale(dayAlt.rectTransform, Vector3.one, 0.5f);
        dayAlt.transform.LeanMoveLocalY(dayAlt.transform.localPosition.y + 125, 1f).setEaseInQuart().setOnComplete(OnCompleteDay);

        yield return new WaitForSeconds(1);

        //Transition Back
        Main.LeanMoveLocalX(-150, 0.5f).setEaseInExpo();
        Main.GetComponent<CanvasGroup>().LeanAlpha(0, 0.5f);

        yield return new WaitForSeconds(1);

        black.LeanMoveLocalY(Screen.height, 1f).setEaseInExpo().delay = 0.1f;

        hasPlayedDay = true;

        DayCycleManager.Instance.EnableButtons();
        DayCycleManager.Instance.SetDay(day);

        yield return 0;
    }

    IEnumerator StartCycleAnimation(int day, int month) {
        dayAlt.text = $" {day}";
        monthAlt.text = $"{month}";

        //Transition to night
        black.localPosition = new Vector2(0, Screen.height);
        black.LeanMoveLocalY(0, 2f).setEaseOutExpo().delay = 0.1f;

        yield return new WaitForSeconds(1);

        //Show text
        Main.GetComponent<CanvasGroup>().alpha = 0;
        Main.GetComponent<CanvasGroup>().LeanAlpha(1, 0.5f);
        Main.localPosition = new Vector2(-150, 0);
        Main.LeanMoveLocalX(0, 0.5f).setEaseOutExpo();

        yield return new WaitForSeconds(1);

        //Show Count
        Day.GetComponent<CanvasGroup>().LeanAlpha(1, 0.5f);
        LeanTween.scale(dayAlt.rectTransform, Vector3.one, 0.5f);
        dayAlt.transform.LeanMoveLocalY(dayAlt.transform.localPosition.y + 125, 1f).setEaseInQuart().setOnComplete(OnCompleteDay);

        Month.GetComponent<CanvasGroup>().LeanAlpha(1, 0.5f);
        LeanTween.scale(monthAlt.rectTransform, Vector3.one, 0.5f);
        monthAlt.transform.LeanMoveLocalY(monthAlt.transform.localPosition.y + 125, 1f).setEaseInQuart().setOnComplete(OnCompleteMonth);

        yield return new WaitForSeconds(1);

        //Transition Back
        Main.LeanMoveLocalX(-150, 0.5f).setEaseInExpo();
        Main.GetComponent<CanvasGroup>().LeanAlpha(0, 0.5f);

        yield return new WaitForSeconds(1);

        black.LeanMoveLocalY(Screen.height, 1f).setEaseInExpo().delay = 0.1f;

        hasPlayedDay = true;

        DayCycleManager.Instance.EnableButtons();
        DayCycleManager.Instance.SetDay(day);
        DayCycleManager.Instance.SetMonth(month);

        yield return 0;
    }

    void ResetDayValues() {
        Main.GetComponent<CanvasGroup>().alpha = 0f;
        Month.GetComponent<CanvasGroup>().alpha = 0f;
        Day.GetComponent<CanvasGroup>().alpha = 0f;

        if(hasPlayedDay) 
            dayAlt.transform.localPosition = new Vector2(dayAlt.transform.localPosition.x, dayAlt.transform.localPosition.y - 125f);
    }

    void ResetMonthValues() {
        Main.GetComponent<CanvasGroup>().alpha = 0f;
        Month.GetComponent<CanvasGroup>().alpha = 0f;
        Day.GetComponent<CanvasGroup>().alpha = 0f;

        if(hasPlayedMonth)
            monthAlt.transform.localPosition = new Vector2(monthAlt.transform.localPosition.x, monthAlt.transform.localPosition.y - 125f);
    }

    void OnCompleteDay() {
        dayAlt.text = "";
        day.text = $" {tempDayValue}";
        if(changeMonth) {
            month.text = $"{tempMonthValue}";
            changeMonth = false;
        }
    }

    void OnCompleteMonth() {
        monthAlt.text = "";
        month.text = $"{tempMonthValue}";
        if(changeDay) {
            day.text = $" {tempDayValue}";
            changeDay = false;
        }
    }
}
