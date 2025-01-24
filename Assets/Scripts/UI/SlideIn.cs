using System.Collections;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlideIn : MonoBehaviour
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

    void Awake() {
        black = GameObject.Find("NightBG").GetComponent<Transform>();
        Main = GameObject.Find("Main").GetComponent<Transform>();
        Day = GameObject.Find("Day").GetComponent<Transform>();
        Month = GameObject.Find("Month").GetComponent<Transform>();
        Main.GetComponent<CanvasGroup>().alpha = 0f;
        Day.GetComponent<CanvasGroup>().alpha = 0f;
        Month.GetComponent<CanvasGroup>().alpha = 0f;
        tempDayValue = 0;
        tempMonthValue = 0;
    }

    void OnEnable() {
        StartCoroutine(StartMonthAnimation(2));
    }

    [PropertySpace, Button("Change Day", ButtonSizes.Large)]
    public void TriggerDayChange() {
        ResetDayValues();
        StartCoroutine(StartDayAnimation(tempDayValue++));
    }

    [Button("Change Month", ButtonSizes.Large)]
    public void TriggerMonthChange() {
        ResetMonthValues();
        StartCoroutine(StartDayAnimation(tempMonthValue++));
    }

    IEnumerator StartDayAnimation(int value) {
        if(value >= 31) tempDayValue = 1;
        else tempDayValue = value;

        dayAlt.text = $" {tempDayValue}";

        //Transition to night
        black.localPosition = new Vector2(0, Screen.height);
        black.LeanMoveLocalY(0, 1f).setEaseOutExpo().delay = 0.1f;

        yield return new WaitForSeconds(1f);

        //Show text
        Main.GetComponent<CanvasGroup>().alpha = 0;
        Main.GetComponent<CanvasGroup>().LeanAlpha(1, 0.5f);
        Main.localPosition = new Vector2(-150, 0);
        Main.LeanMoveLocalX(0, 0.5f).setEaseOutExpo();

        yield return new WaitForSeconds(0.7f);

        //Show Count
        Day.GetComponent<CanvasGroup>().LeanAlpha(1, 0.5f);
        LeanTween.scale(dayAlt.rectTransform, Vector3.one, 0.5f);
        dayAlt.transform.LeanMoveLocalY(dayAlt.transform.localPosition.y + 125, 1f).setEaseInQuart().setOnComplete(OnCompleteDay);

        yield return 0;
    }

    IEnumerator StartMonthAnimation(int value) {
        if(value >= 13) tempMonthValue = 1;
        else tempMonthValue = value;

        monthAlt.text = $"{tempMonthValue}";

        //Transition to night
        black.localPosition = new Vector2(0, Screen.height);
        black.LeanMoveLocalY(0, 1f).setEaseOutExpo().delay = 0.1f;

        yield return new WaitForSeconds(1f);

        //Show text
        Main.GetComponent<CanvasGroup>().alpha = 0;
        Main.GetComponent<CanvasGroup>().LeanAlpha(1, 0.5f);
        Main.localPosition = new Vector2(-150, 0);
        Main.LeanMoveLocalX(0, 0.5f).setEaseOutExpo();

        yield return new WaitForSeconds(0.7f);

        //Show Count
        Month.GetComponent<CanvasGroup>().LeanAlpha(1, 0.5f);
        LeanTween.scale(monthAlt.rectTransform, Vector3.one, 0.5f);
        monthAlt.transform.LeanMoveLocalY(monthAlt.transform.localPosition.y + 125, 1f).setEaseInQuart().setOnComplete(OnCompleteMonth);

        yield return 0;
    }

    void ResetDayValues() {
        Main.GetComponent<CanvasGroup>().alpha = 0f;
        Month.GetComponent<CanvasGroup>().alpha = 0f;
        Day.GetComponent<CanvasGroup>().alpha = 0f;
        dayAlt.transform.localPosition = new Vector2(dayAlt.transform.localPosition.x, dayAlt.transform.localPosition.y - 125f);
    }

    void ResetMonthValues() {
        Main.GetComponent<CanvasGroup>().alpha = 0f;
        Month.GetComponent<CanvasGroup>().alpha = 0f;
        Day.GetComponent<CanvasGroup>().alpha = 0f;
        monthAlt.transform.localPosition = new Vector2(monthAlt.transform.localPosition.x, monthAlt.transform.localPosition.y - 125f);
    }

    void OnCompleteDay() {
        dayAlt.color = Color.white;
        day.text = $" {tempDayValue}";
    }

    void OnCompleteMonth() {
        monthAlt.color = Color.white;
        month.text = $" {tempMonthValue}";
    }

    public void Close() {

    }

}
