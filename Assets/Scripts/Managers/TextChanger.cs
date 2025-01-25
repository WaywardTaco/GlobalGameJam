using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using TMPro;

public class TextChanger : MonoBehaviour
{

    [SerializeField] public TMP_Text[] TextList;
    [SerializeField] public string[] Strings;

    // Start is called before the first frame update
    void Start()
    {
        int count = 0;
        foreach (TMP_Text item in TextList)
        {

            item.text = Strings[count];

            count++;

        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
