using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;
    [SerializeField] private LayerMask layerMask;
    [ReadOnly, SerializeField] public GameObject news;
    [ReadOnly, SerializeField] public bool newsToggle = true;

    void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else Destroy(this);

        news = GameObject.Find("NewsfeedUI").gameObject;
    }

    void Update() {
        CheckScroll();
        CheckLeftClick();
    }

    void CheckLeftClick() {
        if(IsMouseOverGameWindow) {
            if(Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), Mathf.Infinity, layerMask)) {
                SFXManager.Instance.Play("MouseClick");
            }
        }
        
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

    bool IsMouseOverGameWindow
    {
        get
        {
            Vector3 mp = Input.mousePosition;
            return !( 0>mp.x || 0>mp.y || Screen.width<mp.x || Screen.height<mp.y );
        }
    }
}
