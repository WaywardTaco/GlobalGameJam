using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "Scriptables/Upgrades/BasicUpgrade", order = 1)]
public class UpgradeScriptable : ScriptableObject
{
    [SerializeField] public string UpgradeName = "Default Upgrade";
    [SerializeField] public UpgradeTypes UpgradeType = UpgradeTypes.None;
    [SerializeField] public bool IsOneshotUpgrade = true;
    [SerializeField][TextArea(15, 30)] public string UpgradeText = "Insert Upgrade Text";

    public virtual void OnActivate(){}
    public virtual void OnDeactivate(){}
    public virtual void ContinuousEffect(){}
}
