using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    [ReadOnly, SerializeField] public GameObject news;
    [ReadOnly, SerializeField] public bool newsToggle = true;

    void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else Destroy(this);
    }

    void Update() {
        CheckScroll();
    }

    void CheckScroll() {

        if(newsToggle) {
            //Debug.Log($"Scroll: {-Input.mouseScrollDelta.y}");
            news.GetComponent<WebsiteScroller>().Scroll(-Input.mouseScrollDelta.y);
        }
    }

    public void SetNewsToggle(bool value) {
        newsToggle = value;
    }

    public void SetNews(GameObject gameObject) {
        news = gameObject;
    }
}
