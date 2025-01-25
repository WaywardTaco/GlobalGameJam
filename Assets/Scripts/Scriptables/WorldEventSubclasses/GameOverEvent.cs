using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "New WorldEvent", menuName = "Scriptables/WorldEvents/GameOverEvent", order = 1)]
public class GameOverEvent : ReputationDetector
{
    private GameOverScript gameOverScript;

    public override void OnActivate()
    {
        gameOverScript = GameObject.Find("Canvas")?.GetComponent<GameOverScript>();
        if(gameOverScript != null){
            gameOverScript.hasDied = false;
            SceneManager.LoadScene("Game Over");
        }
    }

}
