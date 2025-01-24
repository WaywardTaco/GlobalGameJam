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
    private Dictionary<GameWorldEventScriptable.WorldEventTypes, GameWorldEventScriptable> EventTypeKeys = new();

    private void Awake() {
        if(Instance == null){
            Instance = this;
        } else {
            Destroy(this);
        }
    }
    
    private void OnEnable() {
        
    }

    private void Update() {
        foreach(var worldEvent in WorldEventReferences){
            if(worldEvent.IsActive){

            }
        }
    }


}
