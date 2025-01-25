using Enums;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeButton : MonoBehaviour
{
    public UpgradeType upgradeType;
    private Button button;
    private ShopManager shopManager;

    void Start()
    {
        button = GetComponent<Button>();
        shopManager = FindAnyObjectByType<ShopManager>();

        if (shopManager == null)
        {
            Debug.LogError("ShopManager not found in the scene.");
        }

        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    public void OnButtonClick()
    {
        if (shopManager != null)
        {
            shopManager.PurchaseUpgrade(upgradeType);
            Debug.Log("Upgrade button clicked.");
        }
    }
}
