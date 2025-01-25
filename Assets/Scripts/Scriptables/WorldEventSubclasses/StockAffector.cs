using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;

[CreateAssetMenu(fileName = "New WorldEvent", menuName = "Scriptables/WorldEvents/StockAffector", order = 1)]
public class StockAffector : GameWorldEventScriptable
{
    [Serializable] public class StockEffect {
        [SerializeField] public StockType AffectedStock = StockType.None;
        [SerializeField] public  float TrendEFfect = 0.0f;
        [SerializeField] public float TrendVarianceEffect = 0.0f;
    }
    
    [SerializeField] private int ReputationEffect = 0;
    [SerializeField] private List<StockEffect> StockEffects = new();

    public override void OnActivate(){
        ResourceManager.Instance.CurrentReputation += ReputationEffect;

        foreach(var stockEffect in StockEffects){
            StockManager.StockTracker stockTracker = StockManager.Instance.StockTypeKeys[stockEffect.AffectedStock];
            if(stockTracker != null){
                stockTracker.CurrentTrendBase += stockEffect.TrendEFfect;
                stockTracker.CurrentTrendVariance += stockEffect.TrendVarianceEffect;
                if(stockTracker.CurrentTrendVariance < 0.0f) 
                    stockTracker.CurrentTrendVariance = 0.0f;
            }
        }
    }    
}
