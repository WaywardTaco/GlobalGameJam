using System.Collections;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;

public class MonitorController : MonoBehaviour 
{
    public static MonitorController Instance;

    [PropertySpace, Title("Properties", TitleAlignment = TitleAlignments.Centered)]
    [SerializeField] private LayerMask layerMask;
    [ReadOnly, SerializeField] private bool onScreen;
    [ReadOnly, SerializeField] private bool hasClickedScreen;
    [ReadOnly, SerializeField] private bool hasExitedScreen;


    [PropertySpace, Title("References", TitleAlignment = TitleAlignments.Centered)]
    [ReadOnly, SerializeField] private GameObject player;
    [ReadOnly, SerializeField] private GameObject mainCamera;
    [ReadOnly, SerializeField] private GameObject zoomCamera;
    [ReadOnly, SerializeField] private Vector3 mainCameraPos;
    [ReadOnly, SerializeField] private Vector3 zoomCameraPos;
    [ReadOnly, SerializeField] private Quaternion mainCameraRot;
    [ReadOnly, SerializeField] private Quaternion zoomCameraRot;
    [ReadOnly, SerializeField] private Vector3 tempCameraPos;
    [ReadOnly, SerializeField]private Quaternion tempCameraRot;
    [ReadOnly, SerializeField] private float elapsedTimePos;
    [ReadOnly, SerializeField] private float elapsedTimeRot;
    private float duration = 1f;
    [ReadOnly, SerializeField] private float percentagePos;
    [ReadOnly, SerializeField] private float percentageRot;

    [ReadOnly, SerializeField] private GameObject playerCursor;
    [ReadOnly, SerializeField] private GameObject tooltipMonitor;
    [ReadOnly, SerializeField] private GameObject tooltipCursor;
    public bool inTransition;

    void Awake() {
        if(Instance == null) {
            Instance = this;
        }
        else Destroy(this);

        player = GameObject.Find("Character").gameObject;
        mainCamera = GameObject.Find("Character/PlayerCamera").gameObject;
        zoomCamera = GameObject.Find("ZoomCamera").gameObject;

        playerCursor = GameObject.Find("Cursor").gameObject;
        tooltipMonitor = playerCursor.transform.Find("Tooltip(Monitor)").gameObject;
        tooltipCursor = GameObject.Find("Screen/Tooltip(Cursor)").gameObject;
    }

    void Start() {
        mainCameraPos = mainCamera.transform.position;
        zoomCameraPos = zoomCamera.transform.position;

        mainCameraRot = mainCamera.transform.rotation;
        zoomCameraRot = zoomCamera.transform.rotation;

        player.SetActive(true);
        mainCamera.SetActive(true);
        zoomCamera.SetActive(false);

        playerCursor.SetActive(true);
        tooltipMonitor.SetActive(false);
        tooltipCursor.SetActive(true);
    }

    void Update() {
        tooltipCursor.transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y + 20);

        if(!onScreen && IsMouseOverGameWindow) {
            if(!inTransition) {
                if(Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0)), Mathf.Infinity, layerMask)) {
                    tooltipMonitor.SetActive(true);
                }
                else tooltipMonitor.SetActive(false);
            }
            // else tooltipMonitor.SetActive(false);

            if(Input.GetMouseButtonDown(0) && Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2, 0)), Mathf.Infinity, layerMask)) {
                playerCursor.SetActive(false);
                Debug.Log("Screen Detected!");
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                player.GetComponent<FirstPersonController>().m_MouseLook.SetCursorLock(false);
                player.GetComponent<FirstPersonController>().m_MouseLook.XSensitivity = 0f;
                player.GetComponent<FirstPersonController>().m_MouseLook.YSensitivity = 0f;
                player.GetComponent<FirstPersonController>().enabled = false;
                hasClickedScreen = true;
            }
        }
        else {
            if(Input.GetMouseButtonDown(1)) {
                Debug.Log("Exit Screen");
                hasExitedScreen = true;
            }
        }
    
        if(hasClickedScreen) {
            elapsedTimePos += Time.deltaTime;
            percentagePos = elapsedTimePos / duration;

            elapsedTimeRot += Time.deltaTime;
            percentageRot = elapsedTimeRot / duration;

            tempCameraPos = mainCameraPos;
            tempCameraRot = mainCameraRot;
            mainCamera.transform.position = Vector3.Lerp(mainCameraPos, zoomCameraPos, percentagePos);
            mainCamera.transform.rotation = Quaternion.Lerp(mainCameraRot, zoomCameraRot, percentageRot);

            StartCoroutine(EnterScreen());
        }

        if(hasExitedScreen) {
            elapsedTimePos += Time.deltaTime;
            percentagePos = elapsedTimePos / duration;

            elapsedTimeRot += Time.deltaTime;
            percentageRot = elapsedTimeRot / duration;

            mainCamera.transform.position = Vector3.Lerp(zoomCameraPos, tempCameraPos, percentagePos);
            mainCamera.transform.rotation = Quaternion.Lerp(zoomCameraRot, tempCameraRot, percentageRot);

            StartCoroutine(ExitScreen());
        }
    }

    public void EnableMonitorTooltip() {
        tooltipMonitor.SetActive(true);
    }

    public void DisableMonitorTooltip() {
        tooltipMonitor.SetActive(false);
    }

    public void EnableTooltip(string value) {
        tooltipCursor.GetComponent<TextMeshProUGUI>().text = value;
    }

    public void DisableTooltip() {
        tooltipCursor.GetComponent<TextMeshProUGUI>().text = "";
    }

    public void ResetDay() {
        hasExitedScreen = true;
    }

    IEnumerator EnterScreen() {
        if(percentagePos >= 1 && percentageRot >= 1) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            player.GetComponent<FirstPersonController>().m_MouseLook.SetCursorLock(false);
            player.GetComponent<FirstPersonController>().m_MouseLook.XSensitivity = 0f;
            player.GetComponent<FirstPersonController>().m_MouseLook.YSensitivity = 0f;
            player.GetComponent<FirstPersonController>().enabled = false;
            hasClickedScreen = false;
            onScreen = true;
            percentagePos = 0;
            percentageRot = 0;
            elapsedTimePos = 0;
            elapsedTimeRot = 0;
        }
        yield return null;
    }

    IEnumerator ExitScreen() {
        if(percentagePos >= 1 && percentageRot >= 1) {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            player.GetComponent<FirstPersonController>().enabled = true;
            player.GetComponent<FirstPersonController>().m_MouseLook.SetCursorLock(true);
            player.GetComponent<FirstPersonController>().m_MouseLook.XSensitivity = 2f;
            player.GetComponent<FirstPersonController>().m_MouseLook.YSensitivity = 2f;
            hasExitedScreen = false;
            onScreen = false;
            percentagePos = 0;
            percentageRot = 0;
            elapsedTimePos = 0;
            elapsedTimeRot = 0;
            playerCursor.SetActive(true);
        }
        yield return null;
    }

    public void lockCursor() {
        player.GetComponent<FirstPersonController>().enabled = false;
    }

    public void UnlockCursor() {
        player.GetComponent<FirstPersonController>().enabled = true;
    }

    bool IsMouseOverGameWindow
    {
        get
        {
            Vector3 mp = Input.mousePosition;
            return !( 0>mp.x || 0>mp.y || Screen.width<mp.x || Screen.height<mp.y );
        }
    }
}