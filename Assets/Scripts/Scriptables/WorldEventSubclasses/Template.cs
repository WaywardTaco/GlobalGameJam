using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

[CreateAssetMenu(fileName = "New WorldEvent", menuName = "Scriptables/WorldEvents/BasicEvent", order = 1)]
public class Template : GameWorldEventScriptable
{
    public override string GetEventText(){
        return EventText;
    }

    public override bool CheckAutoActivate(){
        return false;
    }

    public override void OnActivate(){

    }
    
    public override void OnDeactivate(){

    }
    
    public override void ContinuousEffect(){

    }
    
}
