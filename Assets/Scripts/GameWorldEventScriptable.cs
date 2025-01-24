using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WorldEvent", menuName = "Scriptables/WorldEvent", order = 1)]
public class GameWorldEventScriptable : ScriptableObject
{
    public enum WorldEventTypes {
        None, 
    }

    [SerializeField] public string EventName = "Default Game Event";
    [SerializeField] public WorldEventTypes EventType = WorldEventTypes.None;
    [SerializeField] public bool IsOneshotEvent = true;
    [SerializeField][TextArea(15, 30)] public string EventText = "Insert Game Event Text";

    public virtual void OnActivate(){}
    public virtual void OnDeactivate(){}
    public virtual void ContinuousEffect(){}
}
