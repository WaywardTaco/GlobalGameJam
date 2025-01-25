using Enums;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuySellStocks : MonoBehaviour
{
    [SerializeField] GameObject buyInfo;
    [SerializeField] GameObject sellInfo;
    [SerializeField] GameObject shares;

    private StockType stockType = (StockType) 0;
    private int buy = 0;
    private int sell = 0;

    // Start is called before the first frame update
    void Awake()
    {
        buy = int.Parse(buyInfo.GetComponent<TMP_Text>().text);
        sell = int.Parse(sellInfo.GetComponent<TMP_Text>().text);
    }

    public void ChangeStocks(int i)
    {
        stockType = (StockType) i;
        UpdateStocks();
    }

    public void ChangeAmountBuy(bool up)
    {
        if (up && buy < 99) buy++;
        else if (!up && buy > 1) buy--;
        UpdateStocks();
    }

    public void ChangeAmountSells(bool up)
    {
        if (up && sell < 99) sell++;
        else if (!up && sell > 1) sell--;
        UpdateStocks();
    }

    public void BuyStocks()
    {
        StockManager.Instance.TryBuyStock(stockType, buy);
        UpdateStocks();
    }

    public void SellStocks()
    {
        StockManager.Instance.TrySellStock(stockType, sell);
        UpdateStocks();
    }

    // Update is called once per frame
    void UpdateStocks()
    {
        buyInfo.GetComponent<TMP_Text>().text = buy.ToString();
        sellInfo.GetComponent<TMP_Text>().text = sell.ToString();

        string stringAmount = "Total Shares: ";
        stringAmount += StockManager.Instance.getStock(stockType).PlayerStockCount.ToString();
        stringAmount += " Stock Value: ";
        stringAmount += StockManager.Instance.getStock(stockType).CurrentStockValue.ToString();
        shares.GetComponent<TMP_Text>().text = stringAmount;
    }
}
