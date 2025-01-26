using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;

[CreateAssetMenu(fileName = "New WorldEvent", menuName = "Scriptables/WorldEvents/UpgradeEvents/HackerEvent", order = 1)]
public class Hacker : GameWorldEventScriptable
{

    [SerializeField] private int reputationChange;
    [SerializeField] private float trendChange;
    [SerializeField] private int maxDaysChange;


    public override string GetEventText(){
        return EventText;
    }

    public override bool CheckAutoActivate(){
        return false;
    }

    public override void OnActivate(){
        ResourceManager.Instance.CurrentReputation -= reputationChange;

        StockManager.StockTracker mostStock = null;
        int maxStockCount = 0;

        foreach (var stock in StockManager.Instance.activeStocks)
        {
            if (stock.PlayerStockCount > maxStockCount)
            {
                maxStockCount = stock.PlayerStockCount;
                mostStock = stock;
            }
        }

        if (mostStock != null)
        {
            mostStock.CurrentTrendBase += trendChange;
        }
        /*  int maxDays = DayCycleManager.Instance.getDays();
          DayCycleManager.Instance.SetMaxDays(maxDays + maxDaysChange); */
    }
    
    public override void OnDeactivate(){

    }
    
    public override void ContinuousEffect(){

    }
    
}
