using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class NewsFeedUpdater : MonoBehaviour
{
    [Serializable] public class StockDisplay {
        [SerializeField] public TMP_Text stockName;
        [SerializeField] public TMP_Text stockValue;
        [SerializeField] public StockType stockType;
    }

    [SerializeField] private TMP_Text dateText;
    [SerializeField] private List<StockDisplay> displayStocks = new();

    void Start() {
        InputManager.Instance.SetNews(gameObject);
    }

    private void OnEnable() {
    }

    private void Update() {
        SetDateDisplay();
        SetStocksValues();
        UpdateStockAccronyms();
    }

    private void SetDateDisplay(){
        // dateText.text = DayCycleManager.Instance.getCurrentDay();
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

    // TODO : All event updating code
}
