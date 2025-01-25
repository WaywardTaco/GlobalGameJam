using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Sirenix.OdinInspector;

public class HoverColorChange : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject targetObject;
    public Color targetColor;
    public float transitionDuration = 1.0f;
    public TextMeshProUGUI targetName;
    public TextMeshProUGUI targetDesc;
    public TextMeshProUGUI targetType;
    public string nameText;
    [TextArea(4, 10)] public string descText;
    public string typeText;

    private Image targetImage;
    private Color originalColor;
    private Coroutine colorTransitionCoroutine;

    void Start()
    {
        Debug.Log("HoverColorChange Start method called.");

        if (targetObject != null)
        {
            targetImage = targetObject.GetComponent<Image>();
            if (targetImage != null)
            {
                originalColor = targetImage.color;
                Debug.Log("Original color set to: " + originalColor);
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Mouse entered the button.");

        if (targetImage != null)
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

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Mouse exited the button.");
    }

    private IEnumerator TransitionColor(Color newColor)
    {
        Debug.Log("TransitionColor coroutine started.");

        float elapsedTime = 0f;
        Color startingColor = targetImage.color;

        while (elapsedTime < transitionDuration)
        {
            targetImage.color = Color.Lerp(startingColor, newColor, elapsedTime / transitionDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        targetImage.color = newColor;
        Debug.Log("Color transition completed to: " + newColor);
    }
}
