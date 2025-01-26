using System;
using System.Collections;
using System.Collections.Generic;
using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [Serializable] public class UpgradeTracker {
        [HorizontalGroup("Row")]
            [VerticalGroup("Row/Left")]
                [SerializeReference] public UpgradeScriptable Upgrade;
            [VerticalGroup("Row/Right"), HorizontalGroup("Row", Width = 0.2f), LabelText("Count")]
                [SerializeField] public int ActiveCount;
        
        public bool TryAdjustUpgradeCount(int amount){
            if(ActiveCount + amount < 0) return false;

            ActiveCount += amount;
            return true;
        }

    }

    public static UpgradeManager Instance { get; private set;}

    [SerializeField] private List<UpgradeTracker> UpgradeReferences = new();
    public Dictionary<UpgradeType, UpgradeTracker> UpgradeTypeKeys = new();

    public bool TryBuyUpgrade(UpgradeType upgradeType, int count = 1){
        if(upgradeType == UpgradeType.None) return false;
        if(count < 1) return false;
        if(!UpgradeTypeKeys.ContainsKey(upgradeType)) return false;

        UpgradeTracker upgrade = UpgradeTypeKeys[upgradeType];
        int buyTotalCost = upgrade.Upgrade.Cost * count;

        if(ResourceManager.Instance.PlayerMoney - buyTotalCost < 0) return false;

        if(!upgrade.TryAdjustUpgradeCount(count)) return false;

        ResourceManager.Instance.AdjustPlayerMoney(-buyTotalCost);
        return true;
    }

    private void Awake() {
        if(Instance == null){
            Instance = this;
        } else {
            Destroy(this);
        }
    }
    
    private void OnEnable() {
        UpgradeTypeKeys.Clear();

        foreach(var upgrade in UpgradeReferences){
            if(upgrade.Upgrade.UpgradeType == UpgradeType.None) continue;
            UpgradeTypeKeys.Add(upgrade.Upgrade.UpgradeType, upgrade);
        }
    }

    private void Update() {
        foreach(var upgrade in UpgradeReferences){
            if(upgrade.ActiveCount > 0)
                upgrade.Upgrade.ContinuousEffect(upgrade.ActiveCount);
        }
    }

    private void ActivateUpgrade(UpgradeType upgradeType){
        if(!UpgradeTypeKeys.ContainsKey(upgradeType))
            return;

        UpgradeTracker upgrade = UpgradeTypeKeys[upgradeType];
        upgrade.ActiveCount ++;
        upgrade.Upgrade.OnActivate(upgrade.ActiveCount);
    }

    private void DeactivateUpgrade(UpgradeType upgradeType){
        if(!UpgradeTypeKeys.ContainsKey(upgradeType))
            return;

        UpgradeTracker upgrade = UpgradeTypeKeys[upgradeType];
        upgrade.ActiveCount --;
        upgrade.Upgrade.OnDeactivate(upgrade.ActiveCount);
    }
}
