using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;
using System.Linq;
using Sirenix.OdinInspector;

public class StockManager : MonoBehaviour
{
    [SerializeField] private int stockHistoryCount = 5;

    [Serializable] public class StockTracker {
        [SerializeReference] public StockScriptable Stock;

        [BoxGroup("Values", ShowLabel = false)]
        
        [VerticalGroup("Values/Row/Left"), HorizontalGroup("Values/Row", Width = 0.3f), LabelWidth(80), LabelText("Market Value")]
        [SerializeField] public int CurrentStockValue = 0;
        [VerticalGroup("Values/Row/Left"), HorizontalGroup("Values/Row"), LabelWidth(80), LabelText("Trend Base")]
        [SerializeField] public float CurrentTrendBase = 0;
        [VerticalGroup("Values/Row/Middle"), HorizontalGroup("Values/Row", Width = 0.3f), LabelWidth(80), LabelText("Player Count")]
        [SerializeField] public int PlayerStockCount = 0;
        [VerticalGroup("Values/Row/Middle"), HorizontalGroup("Values/Row"), LabelWidth(80), LabelText("Trend Var")]
        [SerializeField] public float CurrentTrendVariance = 0;
        [VerticalGroup("Values/Row/Right"), LabelText("Prev Vals")]
        [SerializeField] public List<int> PreviousValues = new();

        public bool TryAdjustStockCount(int amount){
            if(PlayerStockCount + amount < 0) return false;

            PlayerStockCount += amount;
            return true;
        }

        public int UpdateStockValue(){
            PreviousValues.Add(CurrentStockValue);
            while(PreviousValues.Count > Instance.stockHistoryCount){
                PreviousValues.RemoveAt(0);
            }

            CurrentStockValue = (int)((1.0f + RandomizeTrendEffect()) * CurrentStockValue);
            if(CurrentStockValue < 0) 
                CurrentStockValue = 0;
            if(CurrentStockValue > 500)
                CurrentStockValue = 500; 
            return CurrentStockValue;
        }

        private float RandomizeTrendEffect(){

            if(Math.Abs(CurrentTrendVariance) > 0.5)
                CurrentTrendVariance = 0.5f;
            if(Math.Abs(CurrentTrendVariance) < 0.01)
                CurrentTrendVariance = 0.01f;
            

            float randomVariance = ((UnityEngine.Random.Range(0, 200) - 100) / 100) * CurrentTrendVariance;

            if(CurrentTrendBase > 1.0f) CurrentTrendBase = 1.0f;
            if(CurrentTrendBase < -1.0f) CurrentTrendBase = -1.0f;
            if(Math.Abs(CurrentTrendBase) < 0.01){
                if(CurrentTrendBase < 0) CurrentTrendBase = -0.01f;
                if(CurrentTrendBase >= 0) CurrentTrendBase = 0.01f;
            }

            float trendEffect = 0.0f;
            trendEffect =
                CurrentTrendBase + Math.Abs(randomVariance);

            if(trendEffect > 1.5f) trendEffect = 1.5f;
            if(trendEffect < -1.5f) trendEffect = -1.5f;

            return trendEffect;
        }
    }

    public static StockManager Instance;

    [SerializeField] public List<StockTracker> activeStocks = new();
    public Dictionary<StockType, StockTracker> StockTypeKeys = new();

    public bool TryBuyStock(StockType stockType, int count = 1){
        if(stockType == StockType.None) return false;
        if(count < 1) return false;
        if(!StockTypeKeys.ContainsKey(stockType)) return false;

        StockTracker stock = StockTypeKeys[stockType];
        int buyTotalCost = stock.CurrentStockValue * count;

        if(ResourceManager.Instance.PlayerMoney - buyTotalCost < 0) return false;

        if(!stock.TryAdjustStockCount(count)) return false;

        ResourceManager.Instance.AdjustPlayerMoney(-buyTotalCost);
        return true;
    }

    public bool TrySellStock(StockType stockType, int count = 1){
        if(stockType == StockType.None) return false;
        if(count < 1) return false;
        if(!StockTypeKeys.ContainsKey(stockType)) return false;

        StockTracker stock = StockTypeKeys[stockType];

        if(stock.PlayerStockCount < count) return false;
        if(!stock.TryAdjustStockCount(-count)) return false;

        int sellTotalAmount = stock.CurrentStockValue * count;
        ResourceManager.Instance.AdjustPlayerMoney(sellTotalAmount);
        return true;
    }

    public void UpdateAllStockValues(){
        foreach(var stock in activeStocks)
            stock.UpdateStockValue();
    }

    public StockTracker getStock(StockType stockType){
        if(!StockTypeKeys.ContainsKey(stockType)) return null;

        return StockTypeKeys[stockType];
    }

    private void OnEnable() {
        StockTypeKeys.Clear();

        foreach(var stock in activeStocks){
            if(stock.Stock.StockType == StockType.None) continue; 
            if(StockTypeKeys.ContainsKey(stock.Stock.StockType)) continue;

            StockTypeKeys.Add(stock.Stock.StockType, stock);
        }
    }

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
}
