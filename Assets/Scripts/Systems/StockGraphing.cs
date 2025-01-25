using Enums;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static StockManager;

public class StockGraphing : MonoBehaviour
{
    [SerializeField] GameObject point;
    [SerializeField] GameObject stockName;
    [SerializeField] GameObject Base;
    [SerializeField] GameObject Variance;
    [SerializeField] Vector2 pointSize = new Vector2(22, 22);
    [SerializeField] float blueValueParam = 10;
    [SerializeField] float leftMostPoint = -200;
    [SerializeField] float maxStockValue = 100;
    [SerializeField] float maxDaysShown = 5;

    private List<GameObject> points = new List<GameObject>();
    private List<GameObject> lines  = new List<GameObject>();
    public  List<int> values = new List<int>();

    private RectTransform stockGraphContainer;

    void Awake()
    {
        stockGraphContainer = this.GetComponent<RectTransform>();
        Invoke("InitialChange", .5f);
    }

    public void InitialChange()
    {
        ChangeStocks(1);
    }

    public void ChangeStocks(int i)
    {
        ChangeGraph(StockManager.Instance.getStock((StockType)i));
    }

    public void ChangeGraph(StockTracker stockTrack)
    {
        if (stockTrack != null)
        {
            values.Clear();
            for (int i = 0; i < stockTrack.PreviousValues.Count; i++)
                values.Add(stockTrack.PreviousValues[i]);
            values.Add(stockTrack.CurrentStockValue);
            AddInfo(stockTrack.CurrentTrendBase, stockTrack.CurrentTrendVariance);
            stockName.GetComponent<TMP_Text>().text = stockTrack.Stock.StockName;
        }

        for (int i = 0; i < points.Count; i++) Destroy(points[i]);
        for (int i = 0; i < lines.Count; i++) Destroy(lines[i]);

        ShowGraph();
    }

    private GameObject PointMaker(Vector2 Pos)
    {
        GameObject newPoint = Instantiate(point);
        newPoint.SetActive(true);
        newPoint.transform.SetParent(this.transform, false);
        RectTransform rect = newPoint.GetComponent<RectTransform>();
        rect.anchoredPosition = Pos;
        rect.sizeDelta = pointSize;
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(0, 0);

        points.Add(newPoint);
        return newPoint;
    }

    private void LineMaker(Vector2 PosA, Vector2 PosB)
    {
        GameObject line = new GameObject("Line", typeof(UnityEngine.UI.Image));
        line.transform.SetParent(this.transform, false);
        
        RectTransform rect = line.GetComponent<RectTransform>();
        Vector2 direction = (PosB - PosA).normalized;
        float distance = Vector2.Distance(PosA, PosB);
        rect.sizeDelta = new Vector2(distance, 3f);
        rect.anchorMin = new Vector2(0, 0);
        rect.anchorMax = new Vector2(0, 0);
        rect.anchoredPosition = PosA + direction * distance * .5f;
        float angle = CalculateAngle(direction);
        rect.localEulerAngles = new Vector3(0, 0, angle);

        Color lineColor = Color.blue;
        if (PosB.y - PosA.y < blueValueParam && PosB.y - PosA.y > -blueValueParam) lineColor = Color.blue;
        else if(direction.y > 0) lineColor = Color.green;
        else if(direction.y < 0) lineColor = Color.red;

        line.GetComponent<UnityEngine.UI.Image>().color = lineColor;
        lines.Add(line);
    }
    private float CalculateAngle(Vector2 direction)
    {
        Vector2 Pos2 = direction;
        Vector2 Pos1 = new Vector2(Pos2.x, 0);
        float radians = Mathf.Atan( (Pos2.y - Pos1.y) / (Pos1.x));
        return radians * 180 / Mathf.PI;
    }

    private void AddInfo(float curBase, float curVar)
    {
        string info = null;

        info = "Trend Base: ";
        info += curBase;
        Base.GetComponent<TMP_Text>().text = info;

        info = "Variance: ";
        info += curVar;

        Variance.GetComponent<TMP_Text>().text = info;
    }

    private void ShowGraph()
    {
        float graphHeight = stockGraphContainer.sizeDelta.y;
        float graphWidth = stockGraphContainer.sizeDelta.x;
        float yMax = maxStockValue;
        float xMax = maxDaysShown - 1;

        GameObject oldPoint = null;

        for (int i = 0; i < values.Count; i++)
        {
            float xPosition = pointSize.x + (i) * ((graphWidth - 2*pointSize.x) / xMax);
            float yPosition = pointSize.y + values[i] * ((graphHeight - 2*pointSize.y) / yMax);
            GameObject newPoint = PointMaker(new Vector2(xPosition, yPosition));

            if (oldPoint != null)
            {
                LineMaker(oldPoint.GetComponent<RectTransform>().anchoredPosition,
                    newPoint.GetComponent<RectTransform>().anchoredPosition);
            }

            oldPoint = newPoint;
        }
    }



}
