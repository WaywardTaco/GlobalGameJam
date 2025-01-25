using Enums;
using Sirenix.OdinInspector;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;
    private UpgradeManager upgradeManager;

    [ReadOnly, SerializeField] private UpgradeType upgradeType;
    public void SetUpgradeType(UpgradeType value) {
        upgradeType = value;
    }

    void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else Destroy(this);
    }


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

    public void PurchaseUpgradeALT() {
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
