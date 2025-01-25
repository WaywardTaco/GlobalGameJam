using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class Blink : MonoBehaviour
{
    private TextMeshProUGUI text;
    [SerializeField] private float speed;
    [ReadOnly, SerializeReference] private bool blink;
    public bool hasStarted;

    void Awake() {
        text = GetComponent<TextMeshProUGUI>();
        text.alpha = 0;
        blink = true;
    }
    void Update() {
        if(hasStarted == false) {
            if(blink) BlinkIn();
            else BlinkOut();
        }
    }

    void BlinkIn() {
        text.alpha += speed * Time.deltaTime;
        if(text.alpha >= 1) blink = false;
    }

    public void BlinkOut() {
        text.alpha -= speed * Time.deltaTime;
        if(text.alpha <= 0) blink = true;
    }   


}
