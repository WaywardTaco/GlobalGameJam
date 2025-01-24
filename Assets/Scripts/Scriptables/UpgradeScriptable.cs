using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Scriptables/Upgrades/BasicUpgrade", order = 1)]
public class UpgradeScriptable : ScriptableObject
{
    [SerializeField] public string UpgradeName = "Default Upgrade";
    [SerializeField] public UpgradeType UpgradeType = UpgradeType.None;
    [SerializeField] public int Cost = 0;
    [SerializeField] public bool IsOneshotUpgrade = true;
    [SerializeField] public List<string> UpgradeBulletedText = new();

    public virtual void OnActivate(int count){}
    public virtual void OnDeactivate(int count){}
    public virtual void ContinuousEffect(int count){}
}
