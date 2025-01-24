using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Sirenix.OdinInspector;

public class GameWorldEventManager : MonoBehaviour
{
    [Serializable] public class WorldEventTracker {
        [HorizontalGroup("Row")]
            [VerticalGroup("Row/Left")]
                [SerializeReference] public GameWorldEventScriptable WorldEvent;
            [VerticalGroup("Row/Right"), HorizontalGroup("Row", Width = 0.2f)]
                [SerializeField] public bool IsActive;
    }

    public static GameWorldEventManager Instance { get; private set;}
    
    [SerializeField] private List<WorldEventType> PendingToActivateEvents = new();
    [SerializeField] private List<WorldEventTracker> WorldEventReferences = new();
    private Dictionary<WorldEventType, WorldEventTracker> EventTypeKeys = new();

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
            if(worldEvent.WorldEvent.EventType == WorldEventType.None) continue;
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

    public void StartEventPending(WorldEventType worldEventType){
        if(PendingToActivateEvents.Contains(worldEventType)) return;
        PendingToActivateEvents.Add(worldEventType);
    }
    public void CancelEventPending(WorldEventType worldEventType){
        if(!PendingToActivateEvents.Contains(worldEventType)) return;
        PendingToActivateEvents.Remove(worldEventType);
    }
    public void ProcessPendingEvents(){
        foreach(var pendingEvent in PendingToActivateEvents){
            ActivateEvent(pendingEvent);
        }
        PendingToActivateEvents.Clear();
    }

    public void ActivateEvent(WorldEventType worldEventType){
        if(!EventTypeKeys.ContainsKey(worldEventType))
            return;

        WorldEventTracker worldEvent = EventTypeKeys[worldEventType];
        worldEvent.IsActive = true;
        worldEvent.WorldEvent.OnActivate();
    }

    public void DeactivateEvent(WorldEventType worldEventType){
        if(!EventTypeKeys.ContainsKey(worldEventType))
            return;

        WorldEventTracker worldEvent = EventTypeKeys[worldEventType];
        worldEvent.IsActive = false;
        worldEvent.WorldEvent.OnDeactivate();
    }
}
