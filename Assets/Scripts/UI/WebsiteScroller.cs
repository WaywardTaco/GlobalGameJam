
using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class WebsiteScroller : MonoBehaviour
{
    [Serializable] public class ScrollTracker {
        [HorizontalGroup("Row")]
        [VerticalGroup("Row/Left")]
        [SerializeField] public GameObject Scrollable;
        [VerticalGroup("Row/Right"), HorizontalGroup("Row", Width = 0.3f), LabelText("Max Offset")]
        [SerializeField] public float maxVertOffset;
    }

    [SerializeField] private float scrollSpeed;
    [SerializeField] private GameObject scrollBarBacking;
    [SerializeField] private GameObject scrollRect;
    [SerializeField] private float scrollOffset = 0.0f;
    [SerializeField, ReadOnly] private float maxScroll = 0.0f;
    [SerializeField, ReadOnly] private float scrollBarHeight = 0.0f;
    [SerializeField, ReadOnly] private float scrollHeight = 0.0f;
    [SerializeField, ReadOnly] private float scrollMaxDisplace = 0.0f;
    [SerializeField] private List<ScrollTracker> scrollObjects;


    void Start()
    {
        scrollOffset = 0.0f;

        foreach(var scrollable in scrollObjects){
            if(scrollable.maxVertOffset > maxScroll) 
                maxScroll = scrollable.maxVertOffset;
        }

        if(scrollBarBacking != null){
            RectTransform rect = scrollBarBacking.GetComponent<RectTransform>();
            if(rect != null){
                scrollBarHeight = rect.rect.height;
            }
        }

        if(scrollRect != null){
            RectTransform rect = scrollRect.GetComponent<RectTransform>();
            if(rect != null){
                scrollHeight = rect.rect.height;
            }
        }

        scrollMaxDisplace = scrollBarHeight - scrollHeight;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var scrollable in scrollObjects){
            
            float actualOffset = (scrollOffset - 0.2f) * maxScroll * 2;
            if(actualOffset >= scrollable.maxVertOffset)
                actualOffset = scrollable.maxVertOffset;

            RectTransform rect = scrollable.Scrollable.GetComponent<RectTransform>();
            if(rect != null){
                Debug.Log(scrollable.Scrollable.name + actualOffset);
                Vector2 newPosition = rect.anchoredPosition;
                newPosition.y = actualOffset / 2;
                rect.anchoredPosition = newPosition;
            }
        }

        if(scrollRect != null){
            RectTransform rect = scrollRect.GetComponent<RectTransform>();
            if(rect != null){
                Vector2 newPosition = rect.anchoredPosition;
                newPosition.y = -((scrollOffset - 0.5f) * scrollMaxDisplace);
                rect.anchoredPosition = newPosition;
            }
        }
    }

    public void Scroll(float rateMod){
        scrollOffset += rateMod * scrollSpeed;

        if(scrollOffset > 1.0f)
            scrollOffset = 1.0f;
        if(scrollOffset < 0.0f)
            scrollOffset = 0.0f;
    }
}
