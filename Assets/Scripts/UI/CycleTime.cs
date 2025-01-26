using System.Collections;
using System.Threading;
using Sirenix.OdinInspector;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CycleTime : MonoBehaviour
{
    public Transform black;
    public Transform daysLeftMain;
    public Transform Main;
    public Transform Day;
    public Transform Month;
    public TextMeshProUGUI daysLeft;
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
        daysLeftMain = GameObject.Find("DaysLeftCounter").GetComponent<Transform>();
        daysLeft = daysLeftMain.transform.Find("DaysLeft").GetComponent<TextMeshProUGUI>();
        Main = GameObject.Find("Main").GetComponent<Transform>();
        Day = GameObject.Find("Day").GetComponent<Transform>();
        Month = GameObject.Find("Month").GetComponent<Transform>();
        daysLeftMain.GetComponent<CanvasGroup>().alpha = 0f;
        Main.GetComponent<CanvasGroup>().alpha = 0f;
        Day.GetComponent<CanvasGroup>().alpha = 0f;
        Month.GetComponent<CanvasGroup>().alpha = 0f;
        daysLeft.text = "";
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
        MonitorController.Instance.lockCursor();
        dayAlt.text = $" {day}";

        //Transition to night
        black.localPosition = new Vector2(0, Screen.height);
        black.LeanMoveLocalY(0, 1.5f).setEaseOutQuart().delay = 0.1f;

        yield return new WaitForSeconds(1f);
        SFXManager.Instance.Play("Cricket");

        //Show text
        daysLeft.text = $"{DayCycleManager.Instance.getDaysLeft()} Days Left";

        Main.localPosition = new Vector2(-150, 0);
        daysLeftMain.localPosition = new Vector2(-120, 100);

        Main.GetComponent<CanvasGroup>().alpha = 0;
        daysLeftMain.GetComponent<CanvasGroup>().alpha = 0;

        Main.GetComponent<CanvasGroup>().LeanAlpha(1, 0.5f);
        daysLeftMain.GetComponent<CanvasGroup>().LeanAlpha(1, 0.5f);

        Main.LeanMoveLocalX(0, 1f).setEaseOutExpo();
        daysLeftMain.LeanMoveLocalX(0, 1f).setEaseOutExpo();

        yield return new WaitForSeconds(1.5f);

        //Show Count
        Day.GetComponent<CanvasGroup>().LeanAlpha(1, 0.75f);
        LeanTween.scale(dayAlt.rectTransform, Vector3.one, 0.5f);
        dayAlt.transform.LeanMoveLocalY(dayAlt.transform.localPosition.y + 125, 1f).setEaseInQuart().setOnComplete(OnCompleteDay);
        SFXManager.Instance.fadeThreshold = 1;

        yield return new WaitForSeconds(2f);

        SFXManager.Instance.FadeOut("Cricket");
        SFXManager.Instance.FadeIn("Clock");

        //Transition Back
        Main.LeanMoveLocalX(-150, 1f).setEaseInExpo().setOnComplete(FadeOutClock);
        daysLeftMain.LeanMoveLocalX(-150, 1f).setEaseInExpo().setOnComplete(FadeOutClock);

        Main.GetComponent<CanvasGroup>().LeanAlpha(0, 0.5f);
        daysLeftMain.GetComponent<CanvasGroup>().LeanAlpha(0, 0.5f);
        
        black.LeanMoveLocalY(Screen.height, 1.5f).setEaseInExpo().delay = 0.1f;
        MonitorController.Instance.UnlockCursor();
        hasPlayedDay = true;
        DayCycleManager.Instance.SetDay(day);
        yield return 0;
    }

    void FadeOutClock() {
        SFXManager.Instance.FadeOut("Clock");
    }

    IEnumerator StartCycleAnimation(int day, int month) {
        MonitorController.Instance.lockCursor();
        dayAlt.text = $" {day}";
        monthAlt.text = $"{month}";

        //Transition to night
        black.localPosition = new Vector2(0, Screen.height);
        black.LeanMoveLocalY(0, 1.5f).setEaseOutExpo().delay = 0.1f;

        yield return new WaitForSeconds(1);
        SFXManager.Instance.Play("Cricket");

        //Show text
        daysLeft.text = $"{DayCycleManager.Instance.getDaysLeft()} Days Left";

        Main.localPosition = new Vector2(-150, 0);
        daysLeftMain.localPosition = new Vector2(-120, 100);

        Main.GetComponent<CanvasGroup>().alpha = 0;
        daysLeftMain.GetComponent<CanvasGroup>().alpha = 0;

        Main.GetComponent<CanvasGroup>().LeanAlpha(1, 0.5f);
        daysLeftMain.GetComponent<CanvasGroup>().LeanAlpha(1, 0.5f);

        Main.LeanMoveLocalX(0, 1f).setEaseOutExpo();
        daysLeftMain.LeanMoveLocalX(0, 1f).setEaseOutExpo();

        yield return new WaitForSeconds(1.5f);

        //Show Count
        Day.GetComponent<CanvasGroup>().LeanAlpha(1, 0.75f);
        LeanTween.scale(dayAlt.rectTransform, Vector3.one, 0.5f);
        dayAlt.transform.LeanMoveLocalY(dayAlt.transform.localPosition.y + 125, 1f).setEaseInQuart().setOnComplete(OnCompleteDay);

        Month.GetComponent<CanvasGroup>().LeanAlpha(1, 0.5f);
        LeanTween.scale(monthAlt.rectTransform, Vector3.one, 0.5f);
        monthAlt.transform.LeanMoveLocalY(monthAlt.transform.localPosition.y + 125, 1f).setEaseInQuart().setOnComplete(OnCompleteMonth);
        SFXManager.Instance.fadeThreshold = 1;

        yield return new WaitForSeconds(2);

        SFXManager.Instance.FadeOut("Cricket");
        SFXManager.Instance.FadeIn("Clock");

        //Transition Back
        Main.LeanMoveLocalX(-150, 1f).setEaseInExpo().setOnComplete(FadeOutClock);
        daysLeftMain.LeanMoveLocalX(-150, 1f).setEaseInExpo().setOnComplete(FadeOutClock);

        Main.GetComponent<CanvasGroup>().LeanAlpha(0, 0.5f);
        daysLeftMain.GetComponent<CanvasGroup>().LeanAlpha(0, 0.5f);

        black.LeanMoveLocalY(Screen.height, 1.5f).setEaseInExpo().delay = 0.1f;
        MonitorController.Instance.UnlockCursor();
        hasPlayedDay = true;
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
