using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{

    [SerializeField] public string SceneName;

    private AssetBundle Scenes;

    void start()
    {

        Scenes = AssetBundle.LoadFromFile("Assets/Scenes");

    }

    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName, LoadSceneMode.Single);
    }

    public void exit()
    {

        Application.Quit();

    }

}
