using System.Collections;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    [ReadOnly, SerializeReference] private CanvasGroup image;
    [ReadOnly, SerializeReference] private Blink start;
    [ReadOnly, SerializeReference] private TextMeshProUGUI text;
    [ReadOnly, SerializeReference] private bool hasStarted;

    void Awake() {
        image = GameObject.Find("Canvas/Image").GetComponent<CanvasGroup>();
        start = GameObject.Find("Canvas/Start").GetComponent<Blink>();
        text = GameObject.Find("Canvas/Start").GetComponent<TextMeshProUGUI>();

    }

    void Start() {
        SFXManager.Instance.Play("Ambience");
    }

    void Update() {
        if(Input.GetMouseButtonDown(0) && IsMouseOverGameWindow == true) {
            Debug.Log("Started!");
            SFXManager.Instance.fadeThreshold = 1.25f;
            SFXManager.Instance.FadeOut("Ambience");
            SFXManager.Instance.Play("Start");
            start.hasStarted = true;
            hasStarted = true;
        }
        if(hasStarted) StartCoroutine(Fade());
    }

    IEnumerator Fade() {
        text.alpha -= 0.5f * Time.deltaTime;
        if(text.alpha <= 0) {
            image.alpha += 0.5f * Time.deltaTime;
            if(image.alpha >= 1) {
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene("Bedroom");
            }
        }
        
    }

    bool IsMouseOverGameWindow
    {
        get
        {
            Vector3 mp = Input.mousePosition;
            return !( 0>mp.x || 0>mp.y || Screen.width<mp.x || Screen.height<mp.y );
        }
    }
}
