using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBattleEnd : MonoBehaviour
{
    [SerializeField] string sceneToGo;

    [Header("Previous Scene")]
    [SerializeField] PositionLoader sceneFromLoader;
    string previousScene;

    private void Awake()
    {
        sceneFromLoader = GameObject.FindGameObjectWithTag("GameController").GetComponent<PositionLoader>();
        previousScene = sceneFromLoader.ReturnToPreviousScene();
    }

    public void WinningTutorialScene()
    {
        SceneManager.LoadScene(sceneToGo); //changeSceneDirectly
    }

    public void ReturnFromBattle()
    {
        SceneManager.LoadScene(previousScene);
    }

    public void GameOverScreen()
    {
        Destroy(sceneFromLoader.gameObject);
        SceneManager.LoadScene("GameOverScene");
    }

    public void WinningScreen()
    {
        SceneManager.LoadScene("WinScene");
    }
}
