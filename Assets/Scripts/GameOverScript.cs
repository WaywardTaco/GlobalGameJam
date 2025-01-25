using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] private GameObject policeEnding;
    [SerializeField] private GameObject deathEnding;
    [SerializeField] private TMP_Text moneyEarned;
    [SerializeField] private TMP_Text daysSurvived;
    public bool hasDied = true;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        moneyEarned.text = "Money Earned: " + ResourceManager.Instance.PlayerMoney;
        daysSurvived.text = "Days Survived: " + DayCycleManager.Instance.daysLeft;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (hasDied) deathEnding.SetActive(true);
        else policeEnding.SetActive(true);
    }

    public void OnNextClicked()
    {
        SceneManager.LoadScene("Bedroom");
    }
}
