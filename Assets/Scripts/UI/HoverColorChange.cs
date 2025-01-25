using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HoverColorChange : MonoBehaviour
{
    public GameObject targetObject;
    public Color targetColor;
    public float transitionDuration = 1.0f;
    public TextMeshProUGUI targetName;
    public TextMeshProUGUI targetDesc;
    public TextMeshProUGUI targetType;
    public string nameText;
    public string descText;
    public string typeText;
    public string defaultText = "No Upgrade Selected";

    private Renderer targetRenderer;
    private Color originalColor;
    private Coroutine colorTransitionCoroutine;

    void Start()
    {
        Debug.Log("HoverColorChange Start method called.");

        if (targetObject != null)
        {
            targetRenderer = targetObject.GetComponent<Renderer>();
            if (targetRenderer != null)
            {
                originalColor = targetRenderer.material.color;
                Debug.Log("Original color set to: " + originalColor);
            }
        }

        if (targetName != null || targetDesc != null || targetType != null)
        {
            targetName.text = defaultText;
            Debug.Log("Default text set to: " + defaultText);
        }
    }

    void OnMouseEnter()
    {
        Debug.Log("Mouse entered the button.");

        if (targetRenderer != null)
        {
            if (colorTransitionCoroutine != null)
            {
                StopCoroutine(colorTransitionCoroutine);
                Debug.Log("Stopped existing color transition coroutine.");
            }
            colorTransitionCoroutine = StartCoroutine(TransitionColor(targetColor));
            Debug.Log("Started color transition to: " + targetColor);
        }

        if (targetName != null)
        {
            targetName.text = nameText;
            Debug.Log("Name text set to: " + nameText);
        }
        if (targetDesc != null)
        {
            targetDesc.text = descText;
            Debug.Log("Description text set to: " + descText);
        }
        if (targetType != null)
        {
            targetType.text = typeText;
            Debug.Log("Type text set to: " + typeText);
        }
    }

    void OnMouseExit()
    {
        Debug.Log("Mouse exited the button.");

        if (targetRenderer != null)
        {
            if (colorTransitionCoroutine != null)
            {
                StopCoroutine(colorTransitionCoroutine);
                Debug.Log("Stopped existing color transition coroutine.");
            }
            colorTransitionCoroutine = StartCoroutine(TransitionColor(originalColor));
            Debug.Log("Started color transition to original color: " + originalColor);
        }

        if (targetName != null)
        {
            targetName.text = defaultText;
            Debug.Log("Name text reset to default: " + defaultText);
        }
        if (targetDesc != null)
        {
            targetDesc.text = defaultText;
            Debug.Log("Description text reset to default: " + defaultText);
        }
        if (targetType != null)
        {
            targetType.text = defaultText;
            Debug.Log("Type text reset to default: " + defaultText);
        }
    }

    private IEnumerator TransitionColor(Color newColor)
    {
        Debug.Log("TransitionColor coroutine started.");

        float elapsedTime = 0f;
        Color startingColor = targetRenderer.material.color;

        while (elapsedTime < transitionDuration)
        {
            targetRenderer.material.color = Color.Lerp(startingColor, newColor, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        targetRenderer.material.color = newColor;
        Debug.Log("Color transition completed to: " + newColor);
    }
}
