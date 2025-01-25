using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SocialPlatforms;

public class NotificationManager : MonoBehaviour
{

    [SerializeField] private GameObject notificationCanvas;
    [SerializeField] private TMP_Text notificationText;
    [SerializeField] private float notificationScaleDuration = 0.5f;
    [SerializeField] private string debugNotifText = "";
    [Button()] private void DebugNotif(){
        Notify(1.0f, debugNotifText);
    }
    private Vector3 originalScale = Vector3.zero;
    private bool isOpening = false;
    private bool isClosing = false;
    private float elapsedTime = 0.0f;

    public static NotificationManager Instance;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }
    
    void Start(){
        originalScale = notificationCanvas.transform.localScale;
        notificationCanvas.transform.localScale = Vector3.zero;
    }

    void Update(){
        if(!isClosing && !isOpening) return;
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= notificationScaleDuration){
            if(isClosing){
                notificationCanvas.transform.localScale = Vector3.zero;
                isClosing = false;
            }
            if(isOpening){
                notificationCanvas.transform.localScale = originalScale;
                isOpening = false;
            }
        } else {
            if(isClosing){
                notificationCanvas.transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, elapsedTime / notificationScaleDuration);
            }
            if(isOpening){
                notificationCanvas.transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, elapsedTime / notificationScaleDuration);
            }
        }

    }

    public void Notify(float notifDuration, string notifText = ""){
        notificationText.text = notifText;
        OpenNotif();
        StartCoroutine(WaitToCloseNotif(notifDuration));
    }

    private IEnumerator WaitToCloseNotif(float duration){
        yield return new WaitForSeconds(duration);
        CloseNotif();
    }

    private void OpenNotif(){
        isOpening = true;
        isClosing = false;
        elapsedTime = 0.0f;
    }

    private void CloseNotif(){
        isOpening = false;
        isClosing = true;
        elapsedTime = 0.0f;
    }
}
