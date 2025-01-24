using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;
using System.Linq;

public class ResourceManager : MonoBehaviour
{
    [SerializeField] private int stockHistoryCount = 5;

    public class Resource {
        [SerializeField] public ResourceType Type = ResourceType.None;
        [SerializeField] public int CurrentValue = 0;
    }
    [Serializable] public class StockTracker {
        [SerializeReference] public StockScriptable Stock;
        [SerializeField] public int CurrentStockValue = 0;
        [SerializeField] public float CurrentTrendBase = 0;
        [SerializeField] public float CurrentTrendVariance = 0;
        [SerializeField] public int PlayerStockCount = 0;
        [SerializeField] public List<int> PreviousValues = new();

        public int UpdateStockValue(){
            PreviousValues.Add(CurrentStockValue);
            while(PreviousValues.Count > Instance.stockHistoryCount){
                PreviousValues.RemoveAt(0);
            }

            CurrentStockValue = (int)((1.0f + RandomizeTrendEffect()) * CurrentStockValue);
            return CurrentStockValue;
        }

        private float RandomizeTrendEffect(){
            float randomVariance = ((UnityEngine.Random.Range(0, 200) - 100) / 100) * CurrentTrendVariance;

            float trendEffect = 0.0f;

            trendEffect =
                CurrentTrendBase + randomVariance;


            return trendEffect;
        }
    }


    public static ResourceManager Instance;
    public int PlayerMoney = 0;
    public int CurrentReputation = 100;

    [SerializeField] private List<StockTracker> ActiveStocks = new();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void OnEnable() {
        ResetMetrics();
    }

    private void ResetMetrics(){
        PlayerMoney = 0;
        CurrentReputation = 100;
    }


}
