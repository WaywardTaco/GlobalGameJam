using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource : MonoBehaviour
{

    public static Resource Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    [SerializeField] public string name;
    [SerializeField] public int stat;

    public void setStat(int change)
    {

        stat += change;

    }

}
