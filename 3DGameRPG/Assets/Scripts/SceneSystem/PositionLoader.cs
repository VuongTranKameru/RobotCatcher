using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PositionLoader : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] GameObject player;

    [Header("Position")]
    [SerializeField] Transform posWarp;
    [SerializeField] string previousScene;

    public string ReturnToPreviousScene() { return previousScene; }

    private void Start()
    {
        SceneManager.activeSceneChanged += FindPlayer;
    }

    void Update()
    {
        //TrackPlayerLocation();
    }

    void FindPlayer(Scene current, Scene next)
    {
        player = GameObject.FindGameObjectWithTag("PlayerModel");

        Debug.Log(previousScene + "; " + next.name);
        if (previousScene != next.name)
            if (previousScene.Contains("Battle"))
                ReturnFromBattleLocation(previousScene, next.name);
            else GoNewLocation(previousScene, next.name);
        //finish changing scene, take name from current scene
        previousScene = SceneManager.GetActiveScene().name;
    }

    void GoNewLocation(string previous, string current)
    {
        if (current == "SpawnRoute")
            player.transform.position = SpawnPos();
        if (current == "Chap1")
            player.transform.position = Chap1Pos();

        /*posWarp = GameObject.FindGameObjectWithTag("TeleportPoint").transform;
        player.transform.SetPositionAndRotation(posWarp.position, posWarp.rotation);*/
    }

    void ReturnFromBattleLocation(string previous, string current)
    {
        GameObject respawnLoca = GameObject.FindGameObjectWithTag("TeleportFromBtl");
        posWarp = respawnLoca.transform;
        player.transform.SetPositionAndRotation(posWarp.position, posWarp.rotation);
        Destroy(respawnLoca);
    }

    Vector3 SpawnPos() { return new Vector3(-1.5f, 0.5f, 2.5f); }

    Vector3 Chap1Pos() { return new Vector3(-33, 4.33279991f, 58); }

    void TrackPlayerLocation()
    {
        //posPlayer = player.transform;
        //Debug.Log($"pos: {posPlayer.position}; ro: {posPlayer.rotation}");
    }
}
