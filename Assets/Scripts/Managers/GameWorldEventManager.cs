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
            [VerticalGroup("Row/Middle"), HorizontalGroup("Row", Width = 0.2f), LabelText("Prob"), LabelWidth(40)]
                [SerializeField] public float CurrentProbability = -1.0f;
            [VerticalGroup("Row/Right"), HorizontalGroup("Row", Width = 0.2f)]
                [SerializeField] public bool IsActive;
    }

    public static GameWorldEventManager Instance { get; private set;}
    
    [SerializeField] private int NoDayEventProbability = 0;
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

            if(worldEvent.CurrentProbability == -1.0f)
                worldEvent.CurrentProbability = worldEvent.WorldEvent.StartRandomProbability; 
        }
    }

    private void Update() {
        foreach(var worldEvent in WorldEventReferences){
            if(worldEvent.IsActive){
                worldEvent.WorldEvent.ContinuousEffect();
            } else {
                if(worldEvent.WorldEvent.CheckAutoActivate())
                    StartEventPending(worldEvent.WorldEvent.EventType);
            }
        }
    }

    public void StartEventPending(WorldEventType worldEventType){
        if(worldEventType == WorldEventType.None) return;
        if(PendingToActivateEvents.Contains(worldEventType)) return;
        PendingToActivateEvents.Add(worldEventType);
    }
    public void CancelEventPending(WorldEventType worldEventType){
        if(worldEventType == WorldEventType.None) return;
        if(!PendingToActivateEvents.Contains(worldEventType)) return;
        PendingToActivateEvents.Remove(worldEventType);
    }
    public void ProcessPendingEvents(){
        foreach(var pendingEvent in PendingToActivateEvents){
            if(pendingEvent == WorldEventType.None) continue;
            ActivateEvent(pendingEvent);
        }
        PendingToActivateEvents.Clear();
    }

    public void ActivateEvent(WorldEventType worldEventType){
        if(worldEventType == WorldEventType.None) return;
        if(!EventTypeKeys.ContainsKey(worldEventType))
            return;

        WorldEventTracker worldEvent = EventTypeKeys[worldEventType];
        worldEvent.IsActive = true;
        worldEvent.WorldEvent.OnActivate();
    }

    public void DeactivateEvent(WorldEventType worldEventType){
        if(worldEventType == WorldEventType.None) return;
        if(!EventTypeKeys.ContainsKey(worldEventType))
            return;

        WorldEventTracker worldEvent = EventTypeKeys[worldEventType];
        worldEvent.IsActive = false;
        worldEvent.WorldEvent.OnDeactivate();
    }

    public WorldEventType PendNextDayEvent(){
        WorldEventType chosenEvent = WorldEventType.None;

        chosenEvent = chooseRandomEvent();

        StartEventPending(chosenEvent);
        return chosenEvent;        
    }

    private WorldEventType chooseRandomEvent(){
        // int runningTotal = (int)(NoDayEventProbability * 100);
        // foreach(var worldEvent in WorldEventReferences)
        //     runningTotal += (int)(worldEvent.CurrentProbability * 100);

        // int randomValue = UnityEngine.Random.Range(0, runningTotal);
        
        // TODO : stuff

        return WorldEventType.None;
    }
}
