using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NewsFeedUpdater : MonoBehaviour
{
    [Serializable] public class StockDisplay {
        [SerializeField] public TMP_Text stockName;
        [SerializeField] public TMP_Text stockValue;
        [SerializeField] public StockType stockType;
    }
    [Serializable] public class NewsArticleTracker {
        [SerializeField] public Image DisplayImage;
        [SerializeField] public TMP_Text ArticleTitle;
        [SerializeField] public TMP_Text ArticleText;
    }

    [SerializeField] private TMP_Text dateText;
    [SerializeField] private List<StockDisplay> displayStocks = new();
    [SerializeField] private List<NewsArticleTracker> newsArticles = new();

    private bool onStartUpdate = true;
    private bool isDayOne = true;

    void Start() {
        InputManager.Instance.SetNews(gameObject);
        DayCycleManager.Instance.SetNewsFeed(this);
    }

    private void OnEnable() {
    }

    private void LateUpdate() {
        if(onStartUpdate){
            SetStocksValues();
            UpdateStockAccronyms();
            SetActiveEventInfo();
            SetDateDisplay();
            NotificationManager.Instance.PopNotifs();
            onStartUpdate = false;
        }
    }

    public void RefreshDayInfo(){
        SetStocksValues();
        UpdateStockAccronyms();
        SetActiveEventInfo();
        SetDateDisplay();
    }

    private void SetActiveEventInfo(){
        List<GameWorldEventManager.WorldEventTracker> activeEvents = GameWorldEventManager.Instance.GetToActivateEvents();
        
        int newsArticleCount = newsArticles.Count;
        int activeEventsCount = activeEvents.Count;
        for(int i = 0; i < newsArticleCount && i < activeEventsCount; i++){
            newsArticles[i].DisplayImage.sprite = activeEvents[i].WorldEvent.EventImage;
            newsArticles[i].ArticleTitle.text = activeEvents[i].WorldEvent.EventName;
            newsArticles[i].ArticleText.text = activeEvents[i].WorldEvent.EventText;
        }
    }

    private void SetDateDisplay(){
        Debug.Log($"{DayCycleManager.Instance.currentMonth}/{DayCycleManager.Instance.currentDay}");

        int currDay = DayCycleManager.Instance.currentDay + 1;
        if(isDayOne){
            currDay -= 1;
            isDayOne = false;
            GameWorldEventManager.Instance.ProcessPendingEvents();
        }
        dateText.text = $"{DayCycleManager.Instance.currentMonth}/{currDay}";
    }

    private void UpdateStockAccronyms(){
        foreach(var stock in displayStocks){
            StockManager.StockTracker stockItem = StockManager.Instance.getStock(stock.stockType);
            if(stockItem != null)
                stock.stockName.text = $"{stockItem.Stock.StockCode}" ;
            else
                stock.stockName.text = "UKN";
        }
    }

    private void SetStocksValues(){
        foreach(var stock in displayStocks){
            StockManager.StockTracker stockItem = StockManager.Instance.getStock(stock.stockType);
            if(stockItem != null)
                stock.stockValue.text = $"{stockItem.CurrentStockValue} BT" ;
            else
                stock.stockValue.text = "0 BT"; 
        }
    }

    public void SetToggle(bool value) {
        InputManager.Instance.SetNewsToggle(value);
    }

}
