using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public enum ResourceType {
        None,
    }

    public class Resource {
        [SerializeField] public ResourceType Type = ResourceType.None;
        [SerializeField] public int CurrentValue = 0;
    }
    public static ResourceManager Instance;

    List<Resource> ResourceTracker = new();

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void OnEnable() {
        
    }


}
