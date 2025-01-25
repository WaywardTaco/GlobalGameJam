using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stock", menuName = "Scriptables/Stock", order = 1)]
public class StockScriptable : ScriptableObject
{
    [SerializeField] public string StockName = "Default StockName";
    [SerializeField] public string StockCode = "NULL";
    [SerializeField] public StockType StockType = StockType.None;
    [SerializeField] public int StartingStockValue = 0;
    [SerializeField][TextArea(15, 30)] public string StockText = "Insert Stock Text";

}
