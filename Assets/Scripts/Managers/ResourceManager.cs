using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;
using System.Linq;
using Sirenix.OdinInspector;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    [HorizontalGroup("Row")]
    [VerticalGroup("Row/Left"), LabelWidth(120)]
    public int PlayerMoney = 0;
    [VerticalGroup("Row/Right"), LabelWidth(120)]
    public int CurrentReputation = 100;

    public void AdjustPlayerMoney(int amount){
        PlayerMoney += amount;
    }
    public void AdjustPlayerReputation(int amount){
        CurrentReputation += amount;
    }

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
