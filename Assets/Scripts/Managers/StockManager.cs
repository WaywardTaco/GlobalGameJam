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
        
        [VerticalGroup("Values/Row/Left"), HorizontalGroup("Values/Row", Width = 0.7f), LabelWidth(180)]
        [SerializeField] public int CurrentStockValue = 0;
        [VerticalGroup("Values/Row/Left"), HorizontalGroup("Values/Row"), LabelWidth(180)]
        [SerializeField] public float CurrentTrendBase = 0;
        [VerticalGroup("Values/Row/Left"), HorizontalGroup("Values/Row"), LabelWidth(180)]
        [SerializeField] public float CurrentTrendVariance = 0;
        [VerticalGroup("Values/Row/Left"), HorizontalGroup("Values/Row"), LabelWidth(180)]
        [SerializeField] public int PlayerStockCount = 0;
        [VerticalGroup("Values/Row/Right"), LabelText("Prev Vals")]
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

    public static StockManager Instance;

    [SerializeField] private List<StockTracker> ActiveStocks = new();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
}
