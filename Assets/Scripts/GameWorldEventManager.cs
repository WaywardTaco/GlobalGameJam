using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorldEventManager : MonoBehaviour
{
    [Serializable] public class WorldEventTracker {
        [SerializeReference] public GameWorldEventScriptable WorldEvent;
        [SerializeField] public bool IsActive;
    }

    public static GameWorldEventManager Instance { get; private set;}
    
    [SerializeField] private List<WorldEventTracker> WorldEventReferences = new();
    private Dictionary<GameWorldEventScriptable.WorldEventTypes, WorldEventTracker> EventTypeKeys = new();
    [SerializeField] private List<GameWorldEventScriptable.WorldEventTypes> PendingToActivateEvents = new();

    private void Awake() {
        if(Instance == null){
            Instance = this;
        } else {
            Destroy(this);
        }
    }
    
    private void OnEnable() {
        PendingToActivateEvents.Clear();
        EventTypeKeys.Clear();

        foreach(var worldEvent in WorldEventReferences){
            EventTypeKeys.Add(worldEvent.WorldEvent.EventType, worldEvent);
        }
    }

    private void Update() {
        foreach(var worldEvent in WorldEventReferences){
            if(worldEvent.IsActive){
                worldEvent.WorldEvent.ContinuousEffect();
            }
        }
    }

    public void StartEventPending(GameWorldEventScriptable.WorldEventTypes worldEventType){
        if(PendingToActivateEvents.Contains(worldEventType)) return;
        PendingToActivateEvents.Add(worldEventType);
    }
    public void CancelEventPending(GameWorldEventScriptable.WorldEventTypes worldEventType){
        if(!PendingToActivateEvents.Contains(worldEventType)) return;
        PendingToActivateEvents.Remove(worldEventType);
    }
    public void ProcessPendingEvents(){
        foreach(var pendingEvent in PendingToActivateEvents){
            ActivateEvent(pendingEvent);
        }
        PendingToActivateEvents.Clear();
    }

    public void ActivateEvent(GameWorldEventScriptable.WorldEventTypes worldEventType){
        if(!EventTypeKeys.ContainsKey(worldEventType))
            return;

        WorldEventTracker worldEvent = EventTypeKeys[worldEventType];
        worldEvent.IsActive = true;
        worldEvent.WorldEvent.OnActivate();
    }

    public void DeactivateEvent(GameWorldEventScriptable.WorldEventTypes worldEventType){
        if(!EventTypeKeys.ContainsKey(worldEventType))
            return;

        WorldEventTracker worldEvent = EventTypeKeys[worldEventType];
        worldEvent.IsActive = false;
        worldEvent.WorldEvent.OnDeactivate();
    }
}
