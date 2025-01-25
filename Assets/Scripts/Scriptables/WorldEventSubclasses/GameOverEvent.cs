using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

[CreateAssetMenu(fileName = "New WorldEvent", menuName = "Scriptables/WorldEvents/GameOverEvent", order = 1)]
public class GameOverEvent : ReputationDetector
{
    public override void OnActivate()
    {
        // TODO : Call game over text
    }

}
