using Enums;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonNamer : MonoBehaviour
{
    //List<TMP_Text> Button = new List<TMP_Text>();

    public void Open()
    {
        string stockString = null;
        StockType stockType = StockType.None;
        for (int i = 0; i < transform.childCount; i++)
        {
            stockType = (StockType) (i + 1);
            stockString = StockManager.Instance.getStock(stockType).Stock.StockCode;

            if (stockString != null) transform.GetChild(i).GetComponentInChildren<TMP_Text>().text = stockString;
        }
    }
}
