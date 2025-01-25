using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyCounterUpdater : MonoBehaviour
{
    [SerializeField] private TMP_Text moneyText;

    void Update()
    {
        if(moneyText != null){
            moneyText.text = $"{ResourceManager.Instance.PlayerMoney}";
        }
    }
}
