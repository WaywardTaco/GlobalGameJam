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


    void Start() {
        InputManager.Instance.SetNews(gameObject);
        DayCycleManager.Instance.SetNewsFeed(this);
    }

    private void OnEnable() {

    }

    private void Update() {
        // SetStocksValues();
        // UpdateStockAccronyms();
        // SetDateDisplay();
        // SetActiveEventInfo();
    }

    public void RefreshDayInfo(){
        SetStocksValues();
        UpdateStockAccronyms();
        SetDateDisplay();
        SetActiveEventInfo();
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
        dateText.text = $"{DayCycleManager.Instance.currentMonth}/{DayCycleManager.Instance.currentDay}";
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
