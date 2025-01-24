using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using System;
using System.Linq;

public class ResourceManager : MonoBehaviour
{
    public class Resource {
        [SerializeField] public ResourceType Type = ResourceType.None;
        [SerializeField] public int CurrentValue = 0;
    }
    public static ResourceManager Instance;
    public int PlayerMoney = 0;
    public int CurrentReputation = 100;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void OnEnable() {
        ResetMetrics();
    }

    private void ResetMetrics(){
        PlayerMoney = 0;
        CurrentReputation = 100;
    }


}
