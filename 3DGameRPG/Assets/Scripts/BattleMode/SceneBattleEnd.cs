using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneBattleEnd : MonoBehaviour
{
    [SerializeField] string sceneToGo;

    [Header("DropFromEnemy")]
    [SerializeField] GameObject bot;

    private void Awake()
    {
        bot = GameObject.FindGameObjectWithTag("Enemy").gameObject;
    }

    public void ChangeSceneDirectly()
    {
        SceneManager.LoadScene(sceneToGo);
    }

    public void WinningTutorialScene()
    {
        ChangeSceneDirectly();
    }
}
