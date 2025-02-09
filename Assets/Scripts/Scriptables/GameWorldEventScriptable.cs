using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

[CreateAssetMenu(fileName = "New WorldEvent", menuName = "Scriptables/WorldEvents/BasicEvent", order = 1)]
public class GameWorldEventScriptable : ScriptableObject
{
    [SerializeField] public Sprite EventImage;
    [SerializeField] public string EventName = "Default Game Event";
    [SerializeField] public WorldEventType EventType = WorldEventType.None;
    [SerializeField] public float StartRandomProbability = 0.0f;
    [SerializeField] public bool IsOneshotEvent = true;
    [SerializeField][TextArea(15, 30)] public string EventText = "Insert Game Event Text";

    public virtual string GetEventText(){
        return EventText;
    }

    public virtual bool CheckAutoActivate(){
        return false;
    }

    public virtual void OnActivate(){}
    public virtual void OnDeactivate(){}
    public virtual void ContinuousEffect(){}
}
