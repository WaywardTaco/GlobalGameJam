using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

[CreateAssetMenu(fileName = "New WorldEvent", menuName = "Scriptables/WorldEvents/ReputationDetector", order = 1)]
public class ReputationDetector : GameWorldEventScriptable
{
    [SerializeField] private int lowestActivationRep = 0;
    [SerializeField] private int higherActivationRep = 0;

    public override bool CheckAutoActivate(){
        bool result = 
            ResourceManager.Instance.CurrentReputation <= higherActivationRep &&
            ResourceManager.Instance.CurrentReputation >= lowestActivationRep;
        return result;
    }
}
