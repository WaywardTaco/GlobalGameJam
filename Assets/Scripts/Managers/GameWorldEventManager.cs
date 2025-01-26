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

    [Serializable] public class EventProbabilityMap {
        public float lowerBound;
        public WorldEventType worldEvent;
    }

    public static GameWorldEventManager Instance { get; private set;}
    
    [SerializeField] private int TotalDayEvents = 0;
    [SerializeField] private List<WorldEventTracker> WorldEventReferences = new();
    [SerializeField, ReadOnly] private float totalProbability = 0.0f;
    [SerializeField] private List<EventProbabilityMap> WorldEventProbabilities = new();
    [SerializeField] private List<WorldEventType> PendingToActivateEvents = new();
    private Dictionary<WorldEventType, WorldEventTracker> EventTypeKeys = new();

    private void Awake() {
        if(Instance == null){
            Instance = this;
        } else {
            Destroy(this);
        }
    }
    
    private void OnEnable() {
        totalProbability = 0.0f;
        WorldEventProbabilities.Clear();
        PendingToActivateEvents.Clear();
        EventTypeKeys.Clear();

        foreach(var worldEvent in WorldEventReferences){
            if(worldEvent.WorldEvent.EventType == WorldEventType.None) continue;
            EventTypeKeys.Add(worldEvent.WorldEvent.EventType, worldEvent);

            if(worldEvent.CurrentProbability == -1.0f)
                worldEvent.CurrentProbability = worldEvent.WorldEvent.StartRandomProbability; 
        }

        PendAutomaticEvents();
    }

    private void Update() {
        foreach(var worldEvent in WorldEventReferences){
            if(worldEvent.IsActive){
                worldEvent.WorldEvent.ContinuousEffect();
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

            WorldEventTracker tracker = EventTypeKeys[pendingEvent];
            if(tracker.WorldEvent.IsOneshotEvent)
                tracker.IsActive = false;
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

    public void PendAutomaticEvents(){
        // Debug.Log("Pending Remaining Events");

        // Pend auto condition events
        foreach(var worldEvent in WorldEventReferences){
            if(worldEvent.IsActive) continue;
            if(PendingToActivateEvents.Count >= TotalDayEvents) continue;
            if(worldEvent.WorldEvent.CheckAutoActivate())
                StartEventPending(worldEvent.WorldEvent.EventType);
        }

        // Pend remaining events
        PendRemainingEvents();

        // Debug.Log($"Pending Events: {PendingToActivateEvents.Count}");
    }

    private void PendRemainingEvents(){
        // Debug.Log($"Seeking Remaining Events: {TotalDayEvents}");
        WorldEventType chosenEvent = WorldEventType.None;

        while(PendingToActivateEvents.Count < TotalDayEvents){
            chosenEvent = chooseRandomEvent();
            StartEventPending(chosenEvent);
            // Debug.Log($"Pending Events: {PendingToActivateEvents.Count}");
        }
    }

    private WorldEventType chooseRandomEvent(){
        RefreshEventProbabilities();

        WorldEventType chosenEvent = WorldEventType.None;
        float thresholdValue = (UnityEngine.Random.Range(0, 101) / 100) * totalProbability;
        
        foreach(var eventProbabilityMaps in WorldEventProbabilities){
            if(thresholdValue < eventProbabilityMaps.lowerBound) break;
            chosenEvent = eventProbabilityMaps.worldEvent;
        }

        return chosenEvent;
    }

    private void RefreshEventProbabilities(){
        WorldEventProbabilities.Clear();
        totalProbability = 0.0f;
        foreach(var worldEvent in WorldEventReferences){
            if(worldEvent.IsActive) continue;
            if(worldEvent.CurrentProbability <= 0) continue;
            if(PendingToActivateEvents.Contains(worldEvent.WorldEvent.EventType)) continue;

            EventProbabilityMap eventMap = new EventProbabilityMap();
            float newProbabilityTotal = totalProbability + worldEvent.CurrentProbability;
            eventMap.lowerBound = totalProbability;
            eventMap.worldEvent = worldEvent.WorldEvent.EventType;

            WorldEventProbabilities.Add(eventMap);
            totalProbability = newProbabilityTotal;
        }
    }

    public List<WorldEventTracker> GetToActivateEvents(){
        List<WorldEventTracker> toActivateEvents = new();

        foreach(var worldEvent in PendingToActivateEvents){
            if(toActivateEvents.Count >= TotalDayEvents){
                break;
            }
            toActivateEvents.Add(EventTypeKeys[worldEvent]);
        }

        return toActivateEvents;
    }
}
