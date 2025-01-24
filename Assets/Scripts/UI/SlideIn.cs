using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlideIn : MonoBehaviour
{
    public Transform black;
    public TextMeshProUGUI month;
    public TextMeshProUGUI day;
    private Vector3 monthPos;
    private Vector3 dayPos;

    void Awake() {
        black = GameObject.Find("NightBG").GetComponent<Transform>();
        monthPos = month.transform.localPosition;
        dayPos = day.transform.localPosition;
    }

    void OnEnable() {
        StartCoroutine(StartAnimation());
    }

    IEnumerator StartAnimation() {
        black.localPosition = new Vector2(0, Screen.height);
        black.LeanMoveLocalY(0, 1f).setEaseOutExpo().delay = 0.1f;

        yield return new WaitForSeconds(0.6f);

        month.transform.localPosition = monthPos;
        day.transform.localPosition = dayPos;

        month.transform.LeanMoveLocalY(0, 0.35f).setEaseOutQuart().delay = 0.1f;

        month.transform.LeanMoveLocalY(0, 0.35f).setEaseInQuart().delay = 0.1f;

        // white.localPosition = new Vector2(0, Screen.height);
        // white.LeanMoveLocalY(0, 0.5f).setEaseOutExpo().delay = 0.1f;

        yield return 0;
    }

    public void Close() {

    }
}
