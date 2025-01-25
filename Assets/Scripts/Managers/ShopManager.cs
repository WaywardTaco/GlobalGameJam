using Enums;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    private UpgradeManager upgradeManager;

    void Start()
    {
        upgradeManager = FindAnyObjectByType<UpgradeManager>();
        if (upgradeManager == null)
        {
            Debug.LogError("UpgradeManager not found in the scene.");
        }
    }

    void Update()
    {
    }

    public void PurchaseUpgrade(UpgradeType upgradeType)
    {
        var upgradeTracker = upgradeManager.UpgradeTypeKeys[upgradeType];
        if (upgradeTracker != null)
        {
            if (upgradeTracker.ActiveCount < upgradeTracker.Upgrade.MaxPurchases)
            {
                if (upgradeManager.TryBuyUpgrade(upgradeType))
                {
                    //upgradeTracker.Upgrade.OnActivate(upgradeTracker.ActiveCount);
                    Debug.Log($"Purchased upgrade: {upgradeTracker.Upgrade.UpgradeName}");
                }
                else
                {
                    Debug.Log("Not enough currency to purchase this upgrade.");
                }
            }
            else
            {
                Debug.Log("Maximum purchases reached for this upgrade.");
            }
        }
    }
}
