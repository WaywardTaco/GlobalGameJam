using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;
using UnityEngine.SocialPlatforms;

public class NotificationManager : MonoBehaviour
{
    [Serializable] public class PendingNotif {
        [SerializeField] public float notifDuration;
        [SerializeField] public string notifText;
        public UnityEvent pendingEvent = new UnityEvent();
    }


    [SerializeField] private GameObject notificationCanvas;
    [SerializeField] private TMP_Text notificationText;
    [SerializeField] private float notificationScaleDuration = 0.5f;
    [SerializeField] private string debugNotifText = "";
    [Button()] private void DebugNotif(){
        Notify(1.0f, debugNotifText);
    }

    List<PendingNotif> pendingNotifs = new List<PendingNotif>();

    private Vector3 originalScale = Vector3.zero;
    private bool isOpening = false;
    private bool isClosing = false;
    private bool isOpen = false;
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
        pendingNotifs.Clear();
    }

    void Update(){
        if(!isClosing && !isOpening) return;
        elapsedTime += Time.deltaTime;
        if(elapsedTime >= notificationScaleDuration){
            if(isClosing){
                notificationCanvas.transform.localScale = Vector3.zero;
                isClosing = false;
                isOpen = false;
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

    public void PendNotif(float notifDuration, string notifText = "", UnityAction pendedFunction = null){
        PendingNotif notif = new();
        notif.notifDuration = notifDuration;
        notif.notifText = notifText;
        notif.pendingEvent.AddListener(pendedFunction);
        pendingNotifs.Add(notif);
    }

    public void PopNotifs(){
        StartCoroutine(InternalPopNotifs());
    }

    private IEnumerator InternalPopNotifs(){
        foreach(var notif in pendingNotifs){
            Notify(notif.notifDuration, notif.notifText);
            while(isOpen){
                yield return new WaitForEndOfFrame();
            }
            notif.pendingEvent.Invoke();
        }
        pendingNotifs.Clear();
    }

    private IEnumerator WaitToCloseNotif(float duration){
        yield return new WaitForSeconds(duration);
        CloseNotif();
    }

    private void OpenNotif(){
        isOpen = true;
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
