using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
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
    [SerializeField] private List<StockDisplay> displayStocks;

    private void OnEnable() {
        InitializeStockAccronyms();
    }

    private void Update() {
        SetDateDisplay();
        SetStocksValues();
    }

    private void SetDateDisplay(){
        // dateText.text = DayCycleManager.Instance.getCurrentDay();
    }

    private void InitializeStockAccronyms(){
        foreach(var stock in displayStocks){
            StockManager.StockTracker stockItem = StockManager.Instance.getStock(stock.stockType);
            if(stockItem != null)
                stock.stockName.text = $"{stockItem.Stock.StockCode}" ;
        }
    }

    private void SetStocksValues(){
        foreach(var stock in displayStocks){
            StockManager.StockTracker stockItem = StockManager.Instance.getStock(stock.stockType);
            if(stockItem != null)
                stock.stockValue.text = $"{stockItem.CurrentStockValue} BT" ;
        }
    }

    // TODO : All event updating code
}
